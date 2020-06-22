namespace FileConverter
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Core;

    public class Converter : BaseService, IConverter
    {
        #region Fields

        private const string JsonExt = ".json";
        private const string XmlExt = ".xml";
        private const string CsvExt = ".csv";

        #endregion

        #region Constructors

        public Converter()
        {
            this.RegisterServices();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Currently processes the csv into json or xml.
        /// </summary>
        /// <param name="input">The input file</param>
        /// <param name="output">The output file</param>
        /// <returns>Task or exception</returns>
        public async Task Process(string input, string output)
        {
            string extension = this.ValidateOutputFile(output);
            string[] content = this.ValidateInputFile(input);
            string data = string.Empty;

            if (!string.IsNullOrEmpty(extension))
            {
                switch (extension)
                {
                    //case Converter.JsonExt:
                    //    data = await this._jsonService.ProcessCsvToJson(content);
                    //    break;
                    //case Converter.XmlExt:
                    //    data = await this._xmlService.ProcessCsvToXml(content);
                    //    break;
                }

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
            if (string.IsNullOrEmpty(content))
                throw new ArgumentException("No content to save!");

            if (File.Exists(output))
                File.Delete(output);

            await File.WriteAllTextAsync(output, content, Encoding.UTF8);
        }

        /// <summary>
        ///     Validation of the input file
        /// </summary>
        /// <param name="path">The path to the input file</param>
        /// <returns>
        ///     If the file is valid, it reads the content and returns is as a string array
        /// </returns>
        private string[] ValidateInputFile(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("Input file not found. Please enter a file!");

            if (!this.ValidateFileExtension(path))
                throw new NotSupportedException("Invalid input file. Please enter a valid file!");

            if (File.ReadAllText(path).Length == 0)
                throw new ArgumentException("No data found in file!");

            string[] data = File.ReadAllLines(path);

            this.ParseCsv(ref data);

            return data;
        }

        /// <summary>
        ///     Validation of the output file
        /// </summary>
        /// <param name="path">The path to the output file</param>
        /// <returns>
        ///     If the file is valid, it return the extension so that the process function
        ///     knows what to process
        /// </returns>
        private string ValidateOutputFile(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new FileNotFoundException("Output file not found. Please enter a file!");

            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Incorrect output");

            if (path.Contains(Path.DirectorySeparatorChar) || path.Contains(Path.AltDirectorySeparatorChar)) Directory.CreateDirectory(Path.GetDirectoryName(path));

            string extension = Path.GetExtension(path);
            if (extension != Converter.JsonExt && extension != Converter.XmlExt)
                throw new NotSupportedException("Invalid file. Please enter a valid file!");

            return extension;
        }

        /// <summary>
        ///     A very simple csv parse function to validate CSV content
        /// </summary>
        /// <param name="content">CSV content</param>
        /// <returns>
        ///     If the content is valid it return true else it returns false
        /// </returns>
        private void ParseCsv(ref string[] content)
        {
            if (content.Length == 0)
                throw new NullReferenceException("CSV file is empty");

            int columns = content[0].Split(this.Delimiter).Length;
            if (columns == 1)
                throw new InvalidDataException("Invalid delimiter!");

            for (int i = 1; i < content.Length; i++)
            {
                int j = content[i].Split(this.Delimiter).Length;
                if (j != columns)
                    throw new InvalidDataException("CSV data validation failed!");
            }
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