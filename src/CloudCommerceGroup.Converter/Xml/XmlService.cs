namespace CloudCommerceGroup.Converter.Xml
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using Core;

    public class XmlService : BaseService, IXmlService
    {
        public async Task<string> ProcessCsvToXml(string[] content)
        {
            return await Task.Run(() =>
            {
                var keys = content[0].Split(base.Delimiter);
                var rawObject = content.Skip(1).Select(q => q.Split(base.Delimiter)
                                                           .Select((x, y) => new { Key = keys[y].Trim(), Value = x })
                                                           .ToDictionary(_ => _.Key, _ => _.Value));

                XDocument xDocument = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"));
                var root = new XElement("root");
                xDocument.Add(root);
                foreach (var obj in rawObject)
                {
                    var row = new XElement("row");
                    foreach (var entry in obj)
                    {
                        XElement element = new XElement(entry.Key, entry.Value);
                        row.Add(element);
                    }
                    root.Add(row);
                }

                return xDocument.ToString();
            });
        }

        public Task ProcessXmlToCsv(string input, string output)
        {
            throw new System.NotImplementedException();
        }
    }
}