namespace FileConverter.Test
{
    using System;
    using System.IO;
    using Json;
    using NUnit.Framework;
    using Xml;

    public class JConverterTests
    {
        #region Fields

        private IConverter converter;
        private JsonService jsonService;
        private XmlService xmlService;

        private const char Delimiter = ',';

        #endregion

        #region Methods

        [SetUp]
        public void Setup()
        {
            this.xmlService = new XmlService();
            this.jsonService = new JsonService();

            this.converter = new Converter(this.xmlService, this.jsonService);
        }

        [Test]
        public void Process_WhenNoFileIsPassed_ThrowsFileNotFoundException()
        {
            // Arrange
            string input = string.Empty;
            string output = "test.json";

            // Act

            // Assert
            Assert.ThrowsAsync<FileNotFoundException>(() => this.converter.Process(input, output, JConverterTests.Delimiter));
        }


        [Test]
        public void Process_WhenNoFileIsPassed_CheckFileNotFoundReturnMessage()
        {
            // Arrange
            string input = string.Empty;
            string output = "test.json";

            // Act

            // Assert
            FileNotFoundException result = Assert.ThrowsAsync<FileNotFoundException>(() => this.converter.Process(input, output, JConverterTests.Delimiter));
            Assert.That(result.Message, Is.EqualTo("File not found. Please enter a file!"));
        }

        [Test]
        public void Process_WhenFileIsPassedWithIncorrectExtension_ThrowsNotSupportedException()
        {
            // Arrange
            string input = "Documents/IncorrectExtension.txt";
            string output = "test.json";

            // Act

            // Assert
            Assert.ThrowsAsync<NotSupportedException>(() => this.converter.Process(input, output, JConverterTests.Delimiter));
        }

        [Test]
        public void Process_WhenFileIsPassedWithIncorrectExtension_CheckInvalidExtensionReturnMessage()
        {
            // Arrange
            string input = "Documents/IncorrectExtension.txt";
            string output = "test.json";

            // Act

            // Assert
            NotSupportedException result = Assert.ThrowsAsync<NotSupportedException>(() => this.converter.Process(input, output, JConverterTests.Delimiter));
            Assert.That(result.Message, Is.EqualTo("Invalid file. Please enter a valid file!"));
        }

        [Test]
        public void Process_WhenGivenEmptyOutputPath_ThrowArgumentException()
        {
            // Arrange
            string input = string.Empty;
            string output = string.Empty;

            // Act

            // Assert
            Assert.ThrowsAsync<FileNotFoundException>(() => this.converter.Process(input, output, JConverterTests.Delimiter));
        }

        #endregion
    }
}