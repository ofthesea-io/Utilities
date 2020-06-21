namespace FileConverter
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Core;
    using Json;
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
            string extension = this.ValidateOutputFile(output);
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

        /// <summary>
        ///     Save the content to a file
        /// </summary>
        /// <param name="output">The output file name</param>
        /// <param name="content">The content to save</param>
        /// <returns>Task</returns>
        private async Task Save(string output, string content)
        {
            if(string.IsNullOrEmpty(content))
                throw new ArgumentException("No content to save!");

            if(File.Exists(output))
                File.Delete(output);

            await File.WriteAllTextAsync(output, content, Encoding.UTF8);
        }

        /// <summary>
        ///    Validation of the input file
        /// </summary>
        /// <param name="path">The path to the input file</param>
        /// <returns>
        ///     If the file is valid, it reads the content and returns is as a string array
        /// </returns>
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

        /// <summary>
        ///  Validation of the output file
        /// </summary>
        /// <param name="path">The path to the output file</param>
        /// <returns>
        ///     If the file is valid, it return the extension so that the process function
        ///     knows what to process
        /// </returns>
        private string ValidateOutputFile(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new FileNotFoundException("File not found. Please enter a file!");

            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Incorrect output");

            string result;
            string extension = Path.GetExtension(path);
            if (extension == Converter.JsonExt || extension == Converter.XmlExt)
                result = extension;
            else
                throw new NotSupportedException("Invalid file. Please enter a valid file!");

            return result;
        }

        /// <summary>
        /// A very simple csv parse function to validate CSV content
        /// </summary>
        /// <param name="input">CSV content</param>
        /// <returns>
        ///     If the content is valid it return true else it returns false
        /// </returns>
        private bool ParseCsv(string input)
        {
            return true;
        }

        private bool ValidateFileExtension(string path)
        {
            string extension = Path.GetExtension(path);
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