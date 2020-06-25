namespace FileConverter.Xml
{
    using System.Collections.Generic;
    using System.Composition;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using Core;

    [Export(typeof(IProcessor))]
    [ExportMetadata("ConversionType", "CsvToXml")]
    public class CsvToXmlService : BaseService, IProcessor
    {
        #region Properties

        public override object MetaData { get; set; }

        public string ConversionType { get; } = "CsvToXml";

        #endregion

        #region Methods

        /// <summary>
        ///     Converts the csv file into xml
        /// </summary>
        /// <param name="content">csv content</param>
        /// <returns>formatted xml string</returns>
        public async Task<string> Execute(string[] content)
        {
            this.ParseCsv(ref content, (char)this.MetaData);
            return await Task.Run(() =>
            {
                string[] keys = content[0].Split((char)this.MetaData);
                IEnumerable<Dictionary<string, string>> rawObject = content.Skip(1)
                    .Select(q => q.Split((char)this.MetaData)
                    .Select((x, y) => new {Key = keys[y].Trim(), Value = x})
                    .ToDictionary(_ => _.Key, _ => _.Value));

                XDocument xDocument = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"));
                XElement root = new XElement("root");
                xDocument.Add(root);
                foreach (Dictionary<string, string> obj in rawObject)
                {
                    XElement row = new XElement("row");
                    foreach (KeyValuePair<string, string> entry in obj)
                    {
                        XElement element = new XElement(entry.Key, entry.Value);
                        row.Add(element);
                    }

                    root.Add(row);
                }

                return xDocument.ToString();
            });
        }

        #endregion
    }
}