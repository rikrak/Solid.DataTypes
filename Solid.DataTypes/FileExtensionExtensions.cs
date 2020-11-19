namespace Solid.DataTypes
{
    public static class FileExtensionExtensions
    {
        public static bool IsHtml(this FileExtension extension)
        {
            return extension == FileExtensions.Html || extension == FileExtensions.Htm;
        }

        public static bool IsWord(this FileExtension extension) => extension.IsOpenXmlWord() || extension.IsLegacyWord();
        public static bool IsOpenXmlWord(this FileExtension extension) => extension == FileExtensions.Docx;
        public static bool IsLegacyWord(this FileExtension extension) => extension == FileExtensions.Doc;

        public static bool IsPowerPoint(this FileExtension extension) => extension.IsOpenXmlPowerPoint() || extension.IsLegacyPowerPoint();
        public static bool IsLegacyPowerPoint(this FileExtension extension) => extension == FileExtensions.Ppt;
        public static bool IsOpenXmlPowerPoint(this FileExtension extension) => extension == FileExtensions.Pptx;

        public static bool IsExcel(this FileExtension extension) => extension.IsLegacyExcel() || extension.IsOpenXmlExcel();
        public static bool IsLegacyExcel(this FileExtension extension) => extension == FileExtensions.Xls;
        public static bool IsOpenXmlExcel(this FileExtension extension) => extension == FileExtensions.Xlsx;

        public static bool IsPdf(this FileExtension extension) => extension == FileExtensions.Pdf;
    }
}