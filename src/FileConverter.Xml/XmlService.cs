namespace FileConverter.Xml
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Composition;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using Core;

    [Export(typeof(IComponent))]
    public class XmlService : BaseService, IXmlService, IComponent
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
        ///     Converts the csv file into xml
        /// </summary>
        /// <param name="content">csv content</param>
        /// <returns>formatted xml string</returns>
        [MetaData("CsvToXml")]
        public async Task<string> ProcessCsvToXml(string[] content)
        {
            return await Task.Run(() =>
            {
                string[] keys = content[0].Split(base.Delimiter);
                IEnumerable<Dictionary<string, string>> rawObject = content.Skip(1).Select(q => q.Split(base.Delimiter)
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

        [MetaData("XmlToCsv")]
        public Task<string> ProcessXmlToCsv(string input)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}