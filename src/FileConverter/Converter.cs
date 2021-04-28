namespace FileConverter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Core;

    public class Converter : BaseService, IConverter
    {
        private string _outputExtension;

        private string _inputExtension;

        private readonly IConfiguration _confguration;

        #region Constructors

        public Converter(IConfiguration configuration)
        {
            this._confguration = configuration;
            this.RegisterServices(this._confguration.PluginLocation);
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
            this.ValidateInputFile(input);
            this.ValidateOutputFile(output);

            this._outputExtension = this.GetFileExtension(output);
            this._inputExtension = this.GetFileExtension(input);

            string conversionType = $"{this._inputExtension}to{this._outputExtension}";

            if (!string.IsNullOrEmpty(conversionType))
            {
                IEnumerable<IProcessor> processors = this.Composition.GetExports<IProcessor>();

                var data = await File.ReadAllLinesAsync(input);

                var processor = processors.FirstOrDefault(q => q.ConversionType.ToLower() == conversionType.ToLower());

                if (processor != null)
                {
                    if (this.MetaData != null) 
                        processor.MetaData = this.MetaData;

                    var result = await processor.Execute(data);
                    await this.Save(output, result);
                }
                else
                {
                    throw new NotSupportedException("Conversion process not found!");
                }
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
        private void ValidateInputFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Input file not found. Please enter a file!");
            }

            if (File.ReadAllText(path).Length == 0)
            {
                throw new ArgumentException("No content found in file!");
            }
        }

        /// <summary>
        ///     Validation of the output file
        /// </summary>
        /// <param name="path">The path to the output file</param>
        private void ValidateOutputFile(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new FileNotFoundException("Output file not found. Please enter a file!");
            }

            if (path.Contains(Path.DirectorySeparatorChar) || path.Contains(Path.AltDirectorySeparatorChar))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
        }

        /// <summary>
        ///     Determines the extension of the file
        /// </summary>
        /// <param name="path">The path to the document</param>
        /// <returns>The extension</returns>
        private string GetFileExtension(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new FileNotFoundException("File not found!");

            string extension = Path.GetExtension(path);
            if (string.IsNullOrEmpty(extension)) 
                throw new ArgumentException("No extension found!");

            return extension.Substring(1, extension.Length - 1);
        }

        #endregion
    }
}