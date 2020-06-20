namespace CloudCommerceGroup.Converter.Test
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
        public void ValidateInputFile_WhenNoFileIsPassed_ThrowsFileNotFoundException()
        {
            // Arrange
            string filePath = string.Empty;

            // Act

            // Assert
            Assert.Throws<FileNotFoundException>(() => this.converter.ValidateInputFile(filePath));
        }


        [Test]
        public void ValidateInputFile_WhenNoFileIsPassed_CheckFileNotFoundReturnMessage()
        {
            // Arrange
            string filePath = string.Empty;

            // Act

            // Assert
            FileNotFoundException result = Assert.Throws<FileNotFoundException>(() => this.converter.ValidateInputFile(filePath));
            Assert.That(result.Message, Is.EqualTo("File not found. Please enter a file!"));
        }

        [Test]
        public void ValidateInputFile_WhenFileIsPassedWithIncorrectExtension_ThrowsNotSupportedException()
        {
            // Arrange
            string filePath = "Documents/IncorrectExtension.txt";

            // Act

            // Assert
            Assert.Throws<NotSupportedException>(() => this.converter.ValidateInputFile(filePath));
        }

        [Test]
        public void ValidateInputFile_WhenFileIsPassedWithIncorrectExtension_CheckInvalidExtentionReturnMessage()
        {
            // Arrange
            string filePath = "Documents/IncorrectExtension.txt";

            // Act

            // Assert
            NotSupportedException result = Assert.Throws<NotSupportedException>(() => this.converter.ValidateInputFile(filePath));
            Assert.That(result.Message, Is.EqualTo("Invalid file. Please enter a valid file!"));
        }

        [Test]
        public void ValidateInputFile_WhenFileIsValid_Content()
        {
            // Arrange
            string filePath = "Documents/Valid.csv";

            // Act
            var content = this.converter.ValidateInputFile(filePath);

            // Assert
            Assert.That(content, Is.All.Not.Null);
            Assert.That(content.Length, Is.GreaterThan(1));
        }

        [Test]
        public void GetOutputExtension_WhenGivenEmptyPath_ThrowArgumentException()
        {
            // Arrange
            string filePath = string.Empty;

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => this.converter.GetOutputExtension(filePath));
        }

        #endregion
    }
}