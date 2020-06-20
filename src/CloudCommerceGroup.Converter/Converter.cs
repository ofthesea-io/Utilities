namespace CloudCommerceGroup.Converter
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Core;
    using Json;
    using Microsoft.VisualBasic.FileIO;
    using Xml;

    public class Converter : BaseService, IConverter
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
            string data = string.Empty;

            if (!string.IsNullOrEmpty(extension))
            {
                if (extension == Converter.JsonExt)
                 data = await this.jsonService.ProcessCsvToJson(content);
                if (extension == Converter.XmlExt)
                 data = await this.xmlService.ProcessCsvToXml(content);

                await this.Save(output, data);
            }
        }

        private async Task Save(string output, string content)
        {
            if(string.IsNullOrEmpty(content))
                throw new ArgumentException("No content to save!");

            if(File.Exists(output))
                File.Delete(output);

            await File.WriteAllTextAsync(output, content, Encoding.UTF8);
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

        private bool ParseCsv(string input)
        {
            return true;
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