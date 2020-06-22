namespace FileConverter.Json
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Composition;
    using System.Linq;
    using System.Threading.Tasks;
    using Core;
    using Newtonsoft.Json;

    [Export(typeof(IComponent))]
    public class JsonService : BaseService, IJsonService, IComponent
    {
        #region Properties

        public ISite Site { get; set; }

        #endregion

        #region Methods

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public event EventHandler Disposed;

        /// <summary>
        ///     Converts the csv file into json
        /// </summary>
        /// <param name="content">csv content</param>
        /// <returns>formatted json string</returns>
        [MetaData("CsvToJson")]
        public async Task<string> ProcessCsvToJson(string[] content)
        {
            return await Task.Run(() =>
            {
                string[] keys = content[0].Split(base.Delimiter);
                IEnumerable<Dictionary<string, string>> rawObject = content.Skip(1).Select(q => q.Split(base.Delimiter)
                                                                                               .Select((x, y) => new {Key = keys[y].Trim(), Value = x})
                                                                                               .ToDictionary(_ => _.Key, _ => _.Value));

                string json = JsonConvert.SerializeObject(rawObject, Formatting.Indented);

                return json;
            });
        }

        /// <summary>
        ///     Not yet implemented
        /// </summary>
        /// <param name="content">json string</param>
        /// <returns>csv string</returns>
        [MetaData("JsonToCsv")]
        public Task<string> ProcessJsonToCsv(string content)
        {
            throw new NotImplementedException("Currently not implemented");
        }

        #endregion
    }
}