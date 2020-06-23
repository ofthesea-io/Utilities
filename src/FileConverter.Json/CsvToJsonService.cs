namespace FileConverter.Json
{
    using System.Collections.Generic;
    using System.Composition;
    using System.Linq;
    using System.Threading.Tasks;
    using Core;
    using Newtonsoft.Json;

    [Export(typeof(IProcessor))]
    public class CsvToJsonService : BaseService, IProcessor
    {
        #region Methods

        public object MetaData { get; set; }

        public string ConversionType { get; } = "CsvToJson";

        /// <summary>
        ///     Converts the csv file into json
        /// </summary>
        /// <param name="content">csv content</param>
        /// <returns>formatted json string</returns>
        public async Task<string> Execute(string[] content)
        {
            ParseCsv(ref content, (char)this.MetaData);
            return await Task.Run(() =>
            {
                string[] keys = content[0].Split((char)this.MetaData);
                IEnumerable<Dictionary<string, string>> rawObject = content.Skip(1)
                    .Select(q => q.Split((char)this.MetaData)
                   .Select((x, y) => new {Key = keys[y].Trim(), Value = x})
                   .ToDictionary(_ => _.Key, _ => _.Value));

                string json = JsonConvert.SerializeObject(rawObject, Formatting.Indented);

                return json;
            });
        }

        #endregion
    }
}