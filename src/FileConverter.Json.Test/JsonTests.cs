namespace FileConverter.Json.Test
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;
    using NUnit.Framework;

    public class JsonTests
    {
        #region Fields

        private JsonService _jsonService;

        #endregion

        #region Methods

        [SetUp]
        public void Setup()
        {
            this._jsonService = new JsonService();
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
                var json = await this._jsonService.ProcessCsvToJson(content);
                JToken token = JToken.Parse(json);
                Assert.NotNull(token);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        #endregion
    }
}