namespace CloudCommerceGroup.Converter
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Json;
    using Xml;

    public class Converter : IConverter
    {
        #region Fields

        private const string JsonExt = ".json";
        private const string XmlExt = ".xml";
        private const string CsvExt = ".csv";
        private readonly IJsonService jsonService;
        private readonly IXmlService xmlService;

        #endregion

        #region Constructors

        public Converter(IXmlService xmlService, IJsonService jsonService)
        {
            this.xmlService = xmlService;
            this.jsonService = jsonService;
        }

        #endregion

        #region Methods

        public async Task Process(string input, string output, char delimiter)
        {
            string[] content = this.ValidateInputFile(input);
            string extension = this.GetOutputExtension(output);

            if (!string.IsNullOrEmpty(extension))
            {
                if (extension == Converter.JsonExt)
                    await this.jsonService.ProcessCsvToJson(content, output, delimiter);
                if (extension == Converter.XmlExt)
                    await this.xmlService.ProcessCsvToXml(input, output);
            }
        }

        private string[] ValidateInputFile(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("File not found. Please enter a file!");

            if (!this.ValidateFileExtension(path))
                throw new NotSupportedException("Invalid file. Please enter a valid file!");

            if (File.ReadAllText(path).Length == 0)
                throw new ArgumentException("No data found in file!");

            return File.ReadAllLines(path);
        }

        private string GetOutputExtension(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Incorrect output");

            string result = string.Empty;
            string? extension = Path.GetExtension(path);
            if (extension == Converter.JsonExt || extension == Converter.XmlExt)
                result = extension;

            return result;
        }

        private bool ValidateFileExtension(string path)
        {
            string? extension = Path.GetExtension(path);
            switch (extension.ToLower())
            {
                case Converter.CsvExt:
                    return true;
                case Converter.XmlExt:
                    return true;
                case Converter.JsonExt:
                    return true;
                default:
                    return false;
            }
        }

        #endregion
    }
}