namespace FileConverter.Xml.Test
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using NUnit.Framework;

    public class XmlTests
    {
        #region Fields

        private IXmlService xmlService;

        #endregion

        #region Methods

        [SetUp]
        public void Setup()
        {
            this.xmlService = new XmlService();
        }

        [Test]
        public async Task ProcessCsvToXml_WhenGivenValidInputFile_ReturnJson()
        {
            // Arrange
            string input = "Documents/Valid.csv";
            string[] content = await File.ReadAllLinesAsync(input);

            // Act and Assert
            try
            {
                string xml = await this.xmlService.ProcessCsvToXml(content);
                XDocument xDocument = XDocument.Parse(xml);
                Assert.NotNull(xDocument);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        #endregion
    }
}