using System;
using System.IO;
using System.Threading.Tasks;
using CloudCommerceGroup.Converter.Json;
using CloudCommerceGroup.Converter.Xml;

namespace CloudCommerceGroup.Converter
{
    public class Converter : IConverter
    {
        private const string JsonExt = ".json";
        private const string XmlExt = ".xml";
        private const string CsvExt = ".csv";
        private readonly IJsonService jsonService;
        private readonly IXmlService xmlService;

        public Converter(IXmlService xmlService, IJsonService jsonService)
        {
            this.xmlService = xmlService;
            this.jsonService = jsonService;
        }

        public async Task Process(string input, string output, char delimiter)
        {
            string[] content = this.ValidateInputFile(input);
            string extension = this.GetOutputExtension(output);

            if (!string.IsNullOrEmpty(extension))
            {
                if (extension == JsonExt)
                    await jsonService.ProcessCsvToJson(input, output);
                if (extension == XmlExt)
                    await xmlService.ProcessCsvToXml(input, output);
            }
        }

        public string[] ValidateInputFile(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("File not found. Please enter a file!");

            if (!ValidateFileExtension(path))
                throw new NotSupportedException("Invalid file. Please enter a valid file!");

            if(File.ReadAllText(path).Length == 0)
                throw new ArgumentException("No data found in file!");

            return File.ReadAllLines(path);
        }

        public string GetOutputExtension(string path)
        {
            if(string.IsNullOrEmpty(path))
                throw new ArgumentException("Incorrect output");

            var result = string.Empty;
            var extension = Path.GetExtension(path);
            if (extension != JsonExt || extension == XmlExt) result = extension;

            return result;
        }

        private bool ValidateFileExtension(string path)
        {
            var extension = Path.GetExtension(path);
            switch (extension.ToLower())
            {
                case CsvExt:
                    return true;
                case XmlExt:
                    return true;
                case JsonExt:
                    return true;
                default:
                    return false;
            }
        }
    }
}