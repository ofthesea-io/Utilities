namespace FileConverter.Test
{
    using System;
    using System.IO;
    using Json;
    using NUnit.Framework;
    using Xml;

    public class ConverterTests
    {
        #region Fields

        private const char Delimiter = ',';

        private IConverter converter;
        private JsonService jsonService;
        private XmlService xmlService;

        #endregion

        #region Methods

        [SetUp]
        public void Setup()
        {
            this.xmlService = new XmlService();
            this.jsonService = new JsonService();

            this.converter = new Converter();
        }

        [Test]
        public void Process_WhenNoInputFileIsPassed_ThrowsFileNotFoundException()
        {
            // Arrange
            string input = string.Empty;
            string output = "test.json";

            // Act

            // Assert
            Assert.ThrowsAsync<FileNotFoundException>(() => this.converter.Process(input, output));
        }


        [Test]
        public void Process_WhenNoInputFileIsPassed_CheckFileNotFoundReturnMessage()
        {
            // Arrange
            string input = string.Empty;
            string output = "test.json";

            // Act

            // Assert
            FileNotFoundException result = Assert.ThrowsAsync<FileNotFoundException>(() => this.converter.Process(input, output));
            Assert.That(result.Message, Is.EqualTo("Input file not found. Please enter a file!"));
        }

        [Test]
        public void Process_WhenInputFileIsPassedWithIncorrectExtension_ThrowsNotSupportedException()
        {
            // Arrange
            string input = "Documents/IncorrectExtension.txt";
            string output = "test.json";

            // Act

            // Assert
            Assert.ThrowsAsync<NotSupportedException>(() => this.converter.Process(input, output));
        }

        [Test]
        public void Process_WhenFileIsPassedWithIncorrectExtension_CheckInvalidExtensionReturnMessage()
        {
            // Arrange
            string input = "Documents/IncorrectExtension.txt";
            string output = "test.json";

            // Act

            // Assert
            NotSupportedException result = Assert.ThrowsAsync<NotSupportedException>(() => this.converter.Process(input, output));
            Assert.That(result.Message, Is.EqualTo("Invalid input file. Please enter a valid file!"));
        }

        [Test]
        public void Process_WhenGivenEmptyOutputPath_ThrowArgumentException()
        {
            // Arrange
            string input = string.Empty;
            string output = string.Empty;

            // Act

            // Assert
            Assert.ThrowsAsync<FileNotFoundException>(() => this.converter.Process(input, output));
        }

        [Test]
        public void Process_WhenOutputFileIsPassedWithIncorrectExtension_CheckInvalidExtensionReturnMessage()
        {
            // Arrange
            string input = "Documents/Valid.csv";
            string output = "test.txt";

            // Act

            // Assert
            NotSupportedException result = Assert.ThrowsAsync<NotSupportedException>(() => this.converter.Process(input, output));
            Assert.That(result.Message, Is.EqualTo("Invalid file. Please enter a valid file!"));
        }

        [Test]
        public void Process_WhenInvalidOutputFileExtensionIsPassed_ThrowFileNotFoundException()
        {
            // Arrange
            string input = "Documents/Valid.csv";
            string output = "output.jpg";

            // Act

            // Assert
            NotSupportedException result = Assert.ThrowsAsync<NotSupportedException>(() => this.converter.Process(input, output));
            Assert.That(result.Message, Is.EqualTo("Invalid file. Please enter a valid file!"));
        }


        [Test]
        public void Process_WhenOutputFileNotIsPassed_ThrowFileNotFoundException()
        {
            // Arrange
            string input = "Documents/Valid.csv";
            string output = string.Empty;

            // Act

            // Assert
            FileNotFoundException result = Assert.ThrowsAsync<FileNotFoundException>(() => this.converter.Process(input, output));
            Assert.That(result.Message, Is.EqualTo("Output file not found. Please enter a file!"));
        }

        [Test]
        public void Process_WhenOutputFileNotIsPassed_ShouldThrowInvalidDataException()
        {
            // Arrange
            string input = "Documents/InValid.csv";
            string output = "output.json";

            // Act

            // Assert
            InvalidDataException result = Assert.ThrowsAsync<InvalidDataException>(() => this.converter.Process(input, output));
            Assert.That(result.Message, Is.EqualTo("CSV data validation failed!"));
        }

        [Test]
        public void Process_WhenInValidDelimiterIsPassed_ShouldThrowInvalidDataException()
        {
            // Arrange
            string input = "Documents/Valid.csv";
            string output = "output.json";
            char delimiter = ';';
            // Act

            // Assert
            InvalidDataException result = Assert.ThrowsAsync<InvalidDataException>(() =>
            {
                this.converter.Delimiter = delimiter;
                return this.converter.Process(input, output);
            });
            Assert.That(result.Message, Is.EqualTo("Invalid delimiter!"));
        }

        #endregion
    }
}