namespace FileConverter.Json
{
    using System.Linq;
    using System.Threading.Tasks;
    using Core;
    using Newtonsoft.Json;

    public class JsonService : BaseService, IJsonService
    {
        /// <summary>
        ///     Converts the csv file into json
        /// </summary>
        /// <param name="content">csv content</param>
        /// <returns>formatted json string</returns>
        public async Task<string> ProcessCsvToJson(string[] content)
        {
            return await Task.Run(() =>
            {
                var keys = content[0].Split(base.Delimiter);
                var rawObject = content.Skip(1).Select(q => q.Split(base.Delimiter)
                    .Select((x, y) => new { Key = keys[y].Trim(), Value = x })
                    .ToDictionary(_ => _.Key, _ => _.Value));

                var json = JsonConvert.SerializeObject(rawObject, Formatting.Indented);

                return json;
            });
        }

        /// <summary>
        ///  Not yet implemented
        /// </summary>
        /// <param name="content">json string</param>
        /// <returns>csv string</returns>
        public Task<string> ProcessJsonToCsv(string content)
        {
            throw new System.NotImplementedException("Currently not implemented");
        }
    }
}