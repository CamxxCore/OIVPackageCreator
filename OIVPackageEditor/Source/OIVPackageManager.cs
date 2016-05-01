using System.IO;
using System.IO.Compression;
using System.Xml;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace OIVPackageEditor
{
    public sealed class OIVPackageManager : IDisposable
    {
        private string filePath;

        private string workingDir = Path.GetTempPath() + "\\" + Guid.NewGuid().ToString() + "\\";

        private OIVPackageFormat version;

        public OIVPackageManager(string filePath, OIVPackageFormat version)
        {
            this.filePath = filePath;
            this.version = version;
            SetupWorkingDir();
        }

        public OIVPackageManager(string filePath) : this(filePath, OIVPackageFormat.OIVFormat_2)
        { }

        private void SetupWorkingDir()
        {
            RemoveWorkingDir();

            try
            {
                Directory.CreateDirectory(workingDir);
            }

            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("Failed to create application working directory.\n" + e.Message);
            }

            catch (Exception e)
            {
                Console.WriteLine("Failed to create application working directory.\n" + e.Message);
            }
        }

        private void RemoveWorkingDir()
        {
            if (Directory.Exists(workingDir))
            {
                try
                {
                    Directory.Delete(workingDir, true);
                }

                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine("Failed to extract the package to application working directory. Access to the path was denied.");
                }

                catch (Exception e)
                {
                    Console.WriteLine("Failed to remove application working directory.\n" + e.Message);
                }
            }
        }

        public async Task ExportPackage(OIVPackageInfo data)
        {
            WriteXMLMetadata(data);
            await Task.Run(() => WriteFileContent(filePath, data));
        }

        public async Task<OIVPackageInfo> ReadPackage()
        {
            try
            {
                await Task.Run(() => ZipFile.ExtractToDirectory(filePath, workingDir));

                OIVPackageInfo info;
                ReadXMLMetadata(workingDir, out info);

                string iconPath = workingDir + "icon.png";

                if (File.Exists(iconPath))
                {
                    info.IconPath = iconPath;
                }

                return info;
            }

            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("Failed to extract the package to application working directory. Access to the path was denied.");
                return null;
            }

            catch (Exception e)
            {
                Console.WriteLine("Failed to remove application working directory.\n" + e.Message);
                return null;
            }
        }

        /// <summary>
        /// Create a .oiv package with XML metadata in the archive based on the supplied OIV package info.
        /// </summary>
        /// <param name="relativePath"></param>
        /// <param name="data"></param>
        private void WriteXMLMetadata(OIVPackageInfo data)
        {
            if (version != OIVPackageFormat.OIVFormat_2)
                throw new InvalidDataException("Wrong package format.");

            string filePath = workingDir + "assembly.xml";

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

            #endregion

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

                    attribute.Value = "file\\" + file.Destination.TrimEnd('\\');

                    element.Attributes.Append(attribute);

                    element.InnerText = file.Destination.TrimEnd('\\');
                }
            }

            if (!ReferenceEquals(data.Archives, null))
            {
                foreach (var arc in data.Archives)
                {
                    element = xmlDoc.CreateElement("archive");
                    subNode.AppendChild(element);

                    attribute = xmlDoc.CreateAttribute("path");
                    attribute.Value = arc.Path;

                    element.Attributes.Append(attribute);

                    attribute = xmlDoc.CreateAttribute("createIfNotExist");
                    attribute.Value = arc.CreateIfNotExist.ToString();

                    element.Attributes.Append(attribute);

                    attribute = xmlDoc.CreateAttribute("type");
                    attribute.Value = arc.Version.ToString();

                    element.Attributes.Append(attribute);

                    foreach (var source in arc.SourceFiles)
                    {
                        element1 = xmlDoc.CreateElement("add");
                        element.AppendChild(element1);

                        attribute = xmlDoc.CreateAttribute("source");

                        attribute.Value = "rpf\\" + arc.Name + "\\" + source.Name.Trim('\\');

                        element1.Attributes.Append(attribute);

                        element1.InnerText = source.Name;
                    }

                    foreach (var n in arc.NestedArchives)
                    {
                        foreach (var innerN in n.GetNodes())
                        {
                            element1 = xmlDoc.CreateElement("archive");
                            element.AppendChild(element1);

                            attribute = xmlDoc.CreateAttribute("path");
                            attribute.Value = innerN.Path;

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

                                attribute.Value = "rpf\\" + arc.Name + "\\" + f.Name.Trim('\\');

                                element2.Attributes.Append(attribute);

                                element2.InnerText = f.Name;
                            }
                        }
                    }
                }
            }

            using (XmlTextWriter writer = new XmlTextWriter(filePath, null))
            {
                writer.Formatting = Formatting.Indented;
                xmlDoc.Save(writer);
            }
        }

        private void WriteFileContent(string fileName, OIVPackageInfo data)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }

                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine("Failed to remove exiting package file. Access to the path was denied.");
                    return;
                }

                catch (Exception e)
                {
                    Console.WriteLine("Failed to remove exiting package file.\n" + e.Message);
                    return;
                }
            }

            using (var zipfile = ZipFile.Open(filePath, ZipArchiveMode.Create))
            {
                zipfile.CreateEntryFromFile(workingDir + "assembly.xml", "assembly.xml");
                zipfile.CreateEntry("content/");

                if (data.GenericFiles?.Count > 0)
                {
                    foreach (var file in data.GenericFiles)
                    {
                        zipfile.CreateEntryFromFile(file.Source, "content/file/" + 
                            file.Destination.TrimEnd('\\').Replace('\\', '/'));
                    }
                }

                if (data.Archives?.Count > 0)
                {
                    foreach (var archive in data.Archives)
                    {
                        foreach (var file in archive.GetNestedFiles())
                        {
                            zipfile.CreateEntryFromFile(file.Source, "content/rpf/" + 
                                (archive.Name + "/" + file.Name.Trim('\\')).Replace('\\', '/'));
                        }
                    }
                }

                var userPath = data.IconPath;

                if (data.IconPath?.Length > 0)
                {
                    var image = Image.FromFile(userPath);

                    var filename = data.IconPath.Substring(data.IconPath.LastIndexOf('\\') + 1);

                    if (filename != "icon.png")
                    {
                        string path = workingDir + "icon.png";
                        File.Copy(userPath, path, true);
                        userPath = path;
                    }

                    if (image.HorizontalResolution != 128 || image.VerticalResolution != 128)
                    {
                        userPath = FixImageRes(userPath);
                    }
                }

                else
                {
                    userPath = workingDir + "icon.png";

                    using (Image image = Properties.Resources.Package)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            image.Save(userPath, System.Drawing.Imaging.ImageFormat.Png);
                        }
                    }
                }

                zipfile.CreateEntryFromFile(userPath, "icon.png");

                zipfile.Dispose();
            }
        }

        private string FixImageRes(string imagePath)
        {
            var temp = workingDir + "icon.png";

            using (Image image = new Bitmap(128, 128))
            {
                using (Bitmap source = new Bitmap(imagePath))
                {
                    using (var graphics = Graphics.FromImage(image))
                    {
                        graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                        graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
                        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        graphics.DrawImage(source, 0, 0, image.Width, image.Height);
                    }
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    image.Save(temp, System.Drawing.Imaging.ImageFormat.Png);
                }
            }

            return temp;
        }


        private static void GetArchiveChildren(string filePath, XmlNode node, ref OIVArchive root)
        {
            foreach (XmlNode subNode in node.ChildNodes)
            {
                if (subNode.Name == "add")
                {
                    root?.Add(filePath + "content\\" + subNode.Attributes.GetNamedItem("source").Value,
                        subNode.InnerText);
                }

                else if (subNode.Name == "archive")
                {
                    var archive = new OIVArchive();

                    archive.Path = subNode.Attributes.GetNamedItem("path").Value;

                    archive.CreateIfNotExist = Convert.ToBoolean(subNode.Attributes.GetNamedItem("createIfNotExist").Value);

                    archive.Version = (RageArchiveType)Enum.Parse(typeof(RageArchiveType), subNode.Attributes.GetNamedItem("type").Value);

                    root.NestedArchives.Add(archive);

                    GetArchiveChildren(filePath, subNode, ref archive);
                }
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

            info.HeaderBackground = ColorTranslator.FromHtml(element1.InnerText.Replace('$', '#'));

            element1 = element.SelectSingleNode("iconBackground");

            info.IconBackground = ColorTranslator.FromHtml(element1.InnerText.Replace('$', '#'));

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

                    GetArchiveChildren(filePath, node, ref archive);

                    info.Archives.Add(archive);
                }
            }

            data = info;
        }

        public void Dispose()
        {
            RemoveWorkingDir();
        }

        public enum OIVPackageFormat
        {
            OIVFormat_1 = 1,
            OIVFormat_2 = 2
        }
    }
}
