using System.IO;
using System.IO.Compression;
using System.Xml;
using System;
using System.Collections.Generic;

namespace OIVPackageCreator
{
    public sealed class OIVPackageManager
    {
        private string filename;
        private OIVPackageFormat version;

        public OIVPackageManager(string filename, OIVPackageFormat version)
        {
            this.filename = filename;
            this.version = version;
        }

        public OIVPackageManager(string fileName) : this(fileName, OIVPackageFormat.OIVFormat_2)
        { }

        public void CreatePackage(OIVPackageInfo data)
        {
            var mdPath = Path.GetTempPath() + "assembly.xml";
            WriteXMLMetadata(mdPath, data);
            WriteFileContent(filename, mdPath, data);
        }

        public OIVPackageInfo ReadPackage()
        {
            var path = Path.GetTempPath() + "\\" + Guid.NewGuid().ToString() + "\\";

            if (Directory.Exists(path)) Directory.Delete(path);

            ZipFile.ExtractToDirectory(filename, path);

            OIVPackageInfo info;
            ReadXMLMetadata(path, out info);
            return info;
        }

        /// <summary>
        /// Create a .oiv package with XML metadata in the archive based on the supplied OIV package info.
        /// </summary>
        /// <param name="relativePath"></param>
        /// <param name="data"></param>
        private void WriteXMLMetadata(string filePath, OIVPackageInfo data)
        {
            if (version != OIVPackageFormat.OIVFormat_2)
                throw new InvalidDataException("Wrong package format.");

            var xmlDoc = new XmlDocument();

            var xmlDec = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);

            var root = xmlDoc.DocumentElement;

            xmlDoc.InsertBefore(xmlDec, root);

            var packageRoot = xmlDoc.CreateElement(null, "package", null);
            xmlDoc.AppendChild(packageRoot);

            var attribute = xmlDoc.CreateAttribute("version");
            attribute.Value = data.Version.ToString(".0#");

            packageRoot.Attributes.Append(attribute);

            attribute = xmlDoc.CreateAttribute("id");
            attribute.Value = data.ID.ToUpper();

            packageRoot.Attributes.Append(attribute);

            attribute = xmlDoc.CreateAttribute("target");
            attribute.Value = data.Target.ToString();

            packageRoot.Attributes.Append(attribute);

            var subNode = xmlDoc.CreateElement("metadata");
            packageRoot.AppendChild(subNode);

            var element = xmlDoc.CreateElement("name");
            element.InnerText = data.Name;

            subNode.AppendChild(element);

            #region version

            element = xmlDoc.CreateElement("version");

            var element1 = xmlDoc.CreateElement("major");
            element1.InnerText = data.MajorVersion.ToString();

            element.AppendChild(element1);

            element1 = xmlDoc.CreateElement("minor");
            element1.InnerText = data.MinorVersion.ToString();

            element.AppendChild(element1);

            if (data.Tag?.Length > 0)
            {
                element1 = xmlDoc.CreateElement("tag");
                element1.InnerText = data.Tag;

                element.AppendChild(element1);
            }

            subNode.AppendChild(element);

            #endregion

            #region author

            element = xmlDoc.CreateElement("author");
            subNode.AppendChild(element);

            element1 = xmlDoc.CreateElement("displayName");
            element1.InnerText = data.DisplayName;

            element.AppendChild(element1);

            if (data.ActionLink?.Length > 0)
            {
                element1 = xmlDoc.CreateElement("actionLink");
                element1.InnerText = data.ActionLink;

                element.AppendChild(element1);
            }

            if (data.Web?.Length > 0)
            {
                element1 = xmlDoc.CreateElement("web");
                element1.InnerText = data.Web;

                element.AppendChild(element1);
            }

            if (data.Facebook?.Length > 0)
            {
                element1 = xmlDoc.CreateElement("facebook");
                element1.InnerText = data.Facebook;

                element.AppendChild(element1);
            }

            if (data.Twitter?.Length > 0)
            {
                element1 = xmlDoc.CreateElement("twitter");
                element1.InnerText = data.Twitter;

                element.AppendChild(element1);
            }

            if (data.Youtube?.Length > 0)
            {
                element1 = xmlDoc.CreateElement("youtube");
                element1.InnerText = data.Youtube;

                element.AppendChild(element1);
            }

            #endregion

            #region description

            element = xmlDoc.CreateElement("description");
            subNode.AppendChild(element);

            if (data.FooterLink?.Length > 0)
            {
                attribute = xmlDoc.CreateAttribute("footerLink");
                attribute.Value = data.FooterLink;

                element.Attributes.Append(attribute);
            }

            if (data.FooterLinkTitle?.Length > 0)
            {
                attribute = xmlDoc.CreateAttribute("footerLinkTitle");
                attribute.Value = data.FooterLinkTitle;

                element.Attributes.Append(attribute);
            }

            var cData = xmlDoc.CreateCDataSection(data.Description);

            element.AppendChild(cData);

            #endregion

            #region largeDescription

            if (data.LDDescription?.Length > 0)
            {
                element = xmlDoc.CreateElement("largeDescription");
                subNode.AppendChild(element);

                attribute = xmlDoc.CreateAttribute("displayName");
                attribute.Value = data.LDDisplayName;

                element.Attributes.Append(attribute);

                attribute = xmlDoc.CreateAttribute("footerLink");
                attribute.Value = data.LDFooterLink;

                element.Attributes.Append(attribute);

                attribute = xmlDoc.CreateAttribute("footerLinkTitle");
                attribute.Value = data.LDFooterLinkTitle;

                element.Attributes.Append(attribute);

                cData = xmlDoc.CreateCDataSection(data.LDDescription);

                element.AppendChild(cData);
            }

            #endregion

            #region license

            if (data.License?.Length > 0)
            {

                element = xmlDoc.CreateElement("license");
                subNode.AppendChild(element);

                attribute = xmlDoc.CreateAttribute("footerLink");
                attribute.Value = data.LFooterLink;

                cData = xmlDoc.CreateCDataSection("value");

                element.AppendChild(cData);
            }

            #region colors

            subNode = xmlDoc.CreateElement("colors");
            packageRoot.AppendChild(subNode);

            element = xmlDoc.CreateElement("headerBackground");
            subNode.AppendChild(element);

            attribute = xmlDoc.CreateAttribute("useBlackTextColor");
            attribute.Value = data.BlackTextEnabled.ToString();

            element.Attributes.Append(attribute);

            element.InnerText = string.Format("${0}{1}{2}{3}",
                data.HeaderBackground.A.ToString("X2"),
                data.HeaderBackground.R.ToString("X2"),
                data.HeaderBackground.G.ToString("X2"),
                data.HeaderBackground.B.ToString("X2"));

            element = xmlDoc.CreateElement("iconBackground");

            element.InnerText = string.Format("${0}{1}{2}{3}",
                data.IconBackground.A.ToString("X2"),
                data.IconBackground.R.ToString("X2"),
                data.IconBackground.G.ToString("X2"),
                data.IconBackground.B.ToString("X2"));

            subNode.AppendChild(element);

            #endregion

            subNode = xmlDoc.CreateElement("content");
            packageRoot.AppendChild(subNode);

            if (!ReferenceEquals(data.GenericFiles, null))
            {
                foreach (var file in data.GenericFiles)
                {
                    element = xmlDoc.CreateElement("add");
                    subNode.AppendChild(element);

                    attribute = xmlDoc.CreateAttribute("source");

                    attribute.Value = "file\\" + file.Destination.Replace('/', '\\');

                    element.Attributes.Append(attribute);

                    element.InnerText = file.Destination;
                }
            }

            if (!ReferenceEquals(data.Archives, null))
            {
                foreach (var file in data.Archives)
                {
                    element = xmlDoc.CreateElement("archive");
                    subNode.AppendChild(element);

                    attribute = xmlDoc.CreateAttribute("path");
                    attribute.Value = file.Path.Replace('/', '\\');

                    element.Attributes.Append(attribute);

                    attribute = xmlDoc.CreateAttribute("createIfNotExist");
                    attribute.Value = file.CreateIfNotExist.ToString();

                    element.Attributes.Append(attribute);

                    attribute = xmlDoc.CreateAttribute("type");
                    attribute.Value = file.Version.ToString();

                    element.Attributes.Append(attribute);

                    foreach (var source in file.SourceFiles)
                    {
                        element1 = xmlDoc.CreateElement("add");
                        element.AppendChild(element1);

                        attribute = xmlDoc.CreateAttribute("source");

                        attribute.Value = "rpf\\" + file.Path.Replace('/', '\\') + "\\" + source.Name;

                        element1.Attributes.Append(attribute);

                        element1.InnerText = source.Name;
                    }

                    foreach (var n in file.NestedArchives)
                    {
                        foreach (var innerN in GetNodes(n))
                        {
                            element1 = xmlDoc.CreateElement("archive");
                            element.AppendChild(element1);

                            attribute = xmlDoc.CreateAttribute("path");
                            attribute.Value = innerN.Path.Replace('/', '\\');

                            element1.Attributes.Append(attribute);

                            attribute = xmlDoc.CreateAttribute("createIfNotExist");
                            attribute.Value = innerN.CreateIfNotExist.ToString();

                            element1.Attributes.Append(attribute);

                            attribute = xmlDoc.CreateAttribute("type");
                            attribute.Value = innerN.Version.ToString();

                            element1.Attributes.Append(attribute);

                            foreach (var f in innerN.SourceFiles)
                            {
                                var element2 = xmlDoc.CreateElement("add");
                                element1.AppendChild(element2);

                                attribute = xmlDoc.CreateAttribute("source");

                                attribute.Value = "rpf\\" + file.Path.Replace('/', '\\') + "\\" + f.Name;

                                element2.Attributes.Append(attribute);

                                element2.InnerText = f.Name;
                            }
                        }
                    }
                }
            }

            #endregion

            using (XmlTextWriter writer = new XmlTextWriter(filePath, null))
            {
                writer.Formatting = Formatting.Indented;
                xmlDoc.Save(writer);
            }
        }

        public static IEnumerable<OIVArchive> GetNodes(OIVArchive node)
        {
            if (node == null)
            {
                yield break;
            }
            yield return node;
            foreach (var n in node.NestedArchives)
            {
                foreach (var innerN in GetNodes(n))
                {
                    yield return innerN;
                }
            }
        }

        public static IEnumerable<OIVArchiveFile> GetNestedFiles(OIVArchive node)
        {
            if (node == null)
            {
                yield break;
            }

            foreach (var file in node.SourceFiles)
                yield return file;

            foreach (var n in node.NestedArchives)
            {
                foreach (var innerN in GetNodes(n))
                {
                    foreach (var file in innerN.SourceFiles)
                        yield return file;
                }
            }
        }

        private void WriteFileContent(string filePath, string mdPath, OIVPackageInfo data)
        {
            if (File.Exists(filePath)) File.Delete(filePath);

            using (var zipfile = ZipFile.Open(filePath, ZipArchiveMode.Create))
            {
                zipfile.CreateEntryFromFile(mdPath, "assembly.xml");
                zipfile.CreateEntry("content/");

                if (data.GenericFiles?.Count > 0)
                {
                    foreach (var file in data.GenericFiles)
                    {
                        zipfile.CreateEntryFromFile(file.Source, "content/file/" + file.Destination);
                    }
                }

                if (data.Archives?.Count > 0)
                {
                    foreach (var archive in data.Archives)
                    {
                        //  foreach (var sourceFile in archive.SourceFiles)
                        //   {
                        //      zipfile.CreateEntryFromFile(sourceFile.Source, "content/rpf/" + archive.Path + "/" + sourceFile.Name);
                        //   }
                        foreach (var file in GetNestedFiles(archive))
                        {
                            zipfile.CreateEntryFromFile(file.Source, "content/rpf/" + archive.Path + "/" + file.Name);
                        }
                    }
                }

                zipfile.Dispose();
            }
        }

        private void ReadXMLMetadata(string filePath, out OIVPackageInfo data)
        {
            OIVPackageInfo info = new OIVPackageInfo();

            var xmlDoc = new XmlDocument();

            xmlDoc.Load(filePath + "assembly.xml");

            var packageRoot = xmlDoc.SelectSingleNode("/package");

            info.Version = Convert.ToSingle(packageRoot.Attributes.GetNamedItem("version").Value);

            info.ID = packageRoot.Attributes.GetNamedItem("id").Value;

            info.Target = packageRoot.Attributes.GetNamedItem("target").Value;

            var element = packageRoot.SelectSingleNode("metadata");

            info.Name = element.SelectSingleNode("name").InnerText;

            var element1 = element.SelectSingleNode("version");

            info.MajorVersion = Convert.ToInt32(element1.SelectSingleNode("major").InnerText);

            info.MinorVersion = Convert.ToInt32(element1.SelectSingleNode("minor").InnerText);

            info.Tag = element1.SelectSingleNode("tag")?.InnerText;

            element1 = element.SelectSingleNode("author");

            info.DisplayName = element1.SelectSingleNode("displayName").InnerText;

            info.ActionLink = element1.SelectSingleNode("actionLink")?.InnerText;

            info.Web = element1.SelectSingleNode("web")?.InnerText;

            info.Facebook = element1.SelectSingleNode("facebook")?.InnerText;

            info.Twitter = element1.SelectSingleNode("twitter")?.InnerText;

            info.Youtube = element1.SelectSingleNode("youtube")?.InnerText;

            element1 = element.SelectSingleNode("description");

            info.FooterLink = element1.Attributes.GetNamedItem("footerLink")?.Value;

            info.FooterLinkTitle = element1.Attributes.GetNamedItem("footerLinkTitle")?.Value;

            var cData = (XmlCDataSection)(element1.ChildNodes[0]);

            info.Description = cData.Data;

            element1 = element.SelectSingleNode("largeDescription");

            if (element1 != null)
            {
                info.LDDisplayName = element1.Attributes.GetNamedItem("displayName")?.Value;

                info.LDFooterLink = element1.Attributes.GetNamedItem("footerLink")?.Value;

                info.LDFooterLinkTitle = element1.Attributes.GetNamedItem("footerLinkTitle")?.Value;

                cData = (XmlCDataSection)(element1.ChildNodes[0]);

                info.LDDescription = cData.Data;
            }

            element1 = element.SelectSingleNode("license");

            if (element1 != null)
            {
                info.LFooterLink = element1.Attributes.GetNamedItem("footerLink")?.Value;

                info.LFooterLinkTitle = element1.Attributes.GetNamedItem("footerLinkTitle")?.Value;

                cData = (XmlCDataSection)(element1.ChildNodes[0]);

                info.License = cData.Data;
            }

            element = packageRoot.SelectSingleNode("colors");

            element1 = element.SelectSingleNode("headerBackground");

            info.BlackTextEnabled = Convert.ToBoolean(element1.Attributes.GetNamedItem("useBlackTextColor").Value);

            info.HeaderBackground = System.Drawing.ColorTranslator.FromHtml(element1.InnerText.Replace('$', '#'));

            element1 = element.SelectSingleNode("iconBackground");

            info.IconBackground = System.Drawing.ColorTranslator.FromHtml(element1.InnerText.Replace('$', '#'));

            element1 = packageRoot.SelectSingleNode("content");

            foreach (XmlNode node in element1.ChildNodes)
            {
                if (node.Name == "add")
                {
                    var source = node.Attributes.GetNamedItem("source");

                    info.GenericFiles.Add(new OIVGenericFile(filePath + "content\\" + source.Value, node.InnerText));
                }

                else if (node.Name == "archive")
                {
                    var archive = new OIVArchive();

                    archive.Path = node.Attributes.GetNamedItem("path").Value;

                    archive.CreateIfNotExist = Convert.ToBoolean(node.Attributes.GetNamedItem("createIfNotExist").Value);

                    archive.Version = (RageArchiveType)Enum.Parse(typeof(RageArchiveType), node.Attributes.GetNamedItem("type").Value);

                    foreach (XmlNode subNode in node.ChildNodes)
                    {
                        if (subNode.Name == "add")
                        {
                            archive.SourceFiles.Add(new OIVArchiveFile(archive,
                                filePath + "content\\" + subNode.Attributes.GetNamedItem("source").Value,
                                subNode.InnerText));
                        }
                    }
                }
            }

            data = info;
        }

        public enum OIVPackageFormat
        {
            OIVFormat_1 = 1,
            OIVFormat_2 = 2
        }
    }
}
