using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Drawing;
using System;

namespace OIVPackageCreator
{
    public sealed class OIVPackageInfo
    {
        #region Attributes

        [Browsable(true)]
        [ReadOnly(true)]
        [Category("Package Attributes")]
        [DisplayName("Version")]
        [Description("The version of package format, always equal “2.0”. (Readonly)")]
        [Required(ErrorMessage = "PackageAttributes.Version is required.")]
        public float Version { get; internal set; } = 2.0f;

        [Browsable(true)]
        [ReadOnly(true)]
        [Category("Package Attributes")]
        [DisplayName("ID")]
        [Description("The unique identifier of the package. (Readonly)")]
        [Required(ErrorMessage = "PackageAttributes.ID is required.")]
        public string ID { get; internal set; } = Guid.NewGuid().ToString("B");

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Package Attributes")]
        [DisplayName("Target")]
        [Description("The target Game ID. May be one of the following values: Five, IV, EFLC, Payne")]
        [Required(ErrorMessage = "PackageAttributes.Target is required.")]
        public string Target { get; set; } = "Five";

        #endregion

        #region Metadata

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("General")]
        [DisplayName("Name")]
        [Description("The name of the mod package.")]
        [Required(ErrorMessage = "General.Name is required.")]
        public string Name { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Version")]
        [DisplayName("Major Version")]
        [Description("Major version number.")]
        [Required(ErrorMessage = "Version.MajorVersion is required.")]
        public int MajorVersion { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Version")]
        [DisplayName("Minor Version")]
        [Description("Minor version number.")]
        [Required(ErrorMessage = "Version.MinorVersion is required.")]
        public int MinorVersion { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Version")]
        [DisplayName("Tag")]
        [Description("Version tag, for example: “Alpha”, “Beta”, “Test” etc.")]
        public string Tag { get; set; }

        #endregion

        #region Author

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Author")]
        [DisplayName("Display Name")]
        [Description("Name of mod package author.")]
        [Required(ErrorMessage = "Author.DisplayName is required.")]
        public string DisplayName { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Author")]
        [DisplayName("Action Link")]
        [Description("The action link, will be open when user click on author name. For example, can be used for author's email or web page with other author's mods. (Like profile page on http://gta5-mods.com/)")]
        public string ActionLink { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Author")]
        [DisplayName("Web")]
        [Description("Link to author's home page.")]
        public string Web { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Author")]
        [DisplayName("Facebook")]
        [Description("Author's Facebook page.")]
        public string Facebook { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Author")]
        [DisplayName("Twitter")]
        [Description("Author's twitter account.")]
        public string Twitter { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Author")]
        [DisplayName("Youtube")]
        [Description("Author's YouTube account.")]
        public string Youtube { get; set; }

        #endregion

        #region Description

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Description")]
        [DisplayName("Description Text")]
        [Description("The description of mod package. No length limitation. Line breaks can be used.")]
        [Required(ErrorMessage = "Description.DescriptionText is required.")]
        public string Description { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Description")]
        [DisplayName("Footer Link")]
        [Description("Optional web link to additional information.")]
        public string FooterLink { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Description")]
        [DisplayName("Footer Link Title")]
        [Description("Optional display title for the link.")]
        public string FooterLinkTitle { get; set; }

        #endregion

        #region Large Description

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Large Description")]
        [DisplayName("Description Text")]
        [Description("Optional additional description of mod package. No length limitation. Line breaks can be used. For example, can be used for “Version history” and other stuff.")]
        public string LDDescription { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Large Description")]
        [DisplayName("Display Name")]
        [Description("Optional display name of the description. For example: “Description”, “Version history”, “Mods by me” etc.")]
        public string LDDisplayName { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Large Description")]
        [DisplayName("Footer Link")]
        [Description("Optional web link to additional information.")]
        public string LDFooterLink { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Large Description")]
        [DisplayName("Footer Link Title")]
        [Description("Optional display title for the link.")]
        public string LDFooterLinkTitle { get; set; }

        #endregion

        #region License

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("License")]
        [DisplayName("License Text")]
        [Description("Optional mod licence. No length limitation. Line breaks can be used.")]
        public string License { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("License")]
        [DisplayName("Footer Link")]
        [Description("Optional web link to additional information.")]
        public string LFooterLink { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("License")]
        [DisplayName("Footer Link Title")]
        [Description("Optional display title for the link.")]
        public string LFooterLinkTitle { get; set; }

        #endregion

        #region Colors

        [Browsable(false)]
        [ReadOnly(false)]
        [Category("Colors")]
        [DisplayName("Black Text")]
        [Description("If “True” the text in header area will be black. If “False” the text in header area will be white.")]
        public bool BlackTextEnabled { get; set; }

        [Browsable(false)]
        [ReadOnly(false)]
        [Category("Colors")]
        [DisplayName("Header Background")]
        [Description("The ARGB color of the header of the window. Each color value is hex.")]
        [Required(ErrorMessage = "Colors.HeaderBackground is required.")]
        public Color HeaderBackground { get; set; }

        [Browsable(false)]
        [ReadOnly(false)]
        [Category("Colors")]
        [DisplayName("Icon Background")]
        [Description("The ARGB color of the icon aread in the header. Each color value is hex.")]
        [Required(ErrorMessage = "Colors.IconBackground is required.")]
        public Color IconBackground { get; set; }

        #endregion

        #region Content

        [Browsable(false)]
        [ReadOnly(false)]
        [Category("Content")]
        [DisplayName("Archives")]
        [Description("Archive files to be included in the package.")]
        public List<OIVArchive> Archives { get; set; } = new List<OIVArchive>();

        [Browsable(false)]
        [ReadOnly(false)]
        [Category("Content")]
        [DisplayName("Generic Files")]
        [Description("Generic non - archive files to be included in the archive.")]
        public List<OIVGenericFile> GenericFiles { get; set; } = new List<OIVGenericFile>();

        #endregion
    }
}
