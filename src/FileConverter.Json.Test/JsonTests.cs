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

        private CsvToJsonService _csvToJsonService;

        #endregion

        #region Methods

        [SetUp]
        public void Setup()
        {
            this._csvToJsonService = new CsvToJsonService();
        }

        [Test]
        public async Task Execute_WhenGivenValidInputFile_ReturnJson()
        {
            // Arrange
            string input = "Documents/Valid.csv";
            string[] content = await File.ReadAllLinesAsync(input);
            char metaData = ',';

            // Act and Assert
            try
            {
                this._csvToJsonService.MetaData = metaData;
                var json = await this._csvToJsonService.Execute(content);
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