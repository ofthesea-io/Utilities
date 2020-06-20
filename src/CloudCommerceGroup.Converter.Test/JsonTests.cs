namespace CloudCommerceGroup.Converter.Test
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Json;
    using Newtonsoft.Json.Linq;
    using NUnit.Framework;

    public class JsonTests
    {
        private IJsonService jsonService;

        #region Methods

        [SetUp]
        public void Setup()
        {
            this.jsonService = new JsonService();
        }

        [Test]
        public void ProcessCsvToJson_WhenGivenInValidInputFile_ThrowException()
        {
            // Arrange

            // Act

            // Assert
        }

        [Test]
        public async Task ProcessCsvToJson_WhenGivenValidInputFile_ReturnJson()
        {
            // Arrange
            string input = "Documents/Valid.csv";
            string[] content = await File.ReadAllLinesAsync(input);

            // Act and Assert
            try
            {
                var json = await this.jsonService.ProcessCsvToJson(content);
                JToken token = JToken.Parse(json);
                Assert.NotNull(token);
            }
            catch (Exception exp)
            {
                Assert.Fail();
            }
        }

        #endregion
    }
}