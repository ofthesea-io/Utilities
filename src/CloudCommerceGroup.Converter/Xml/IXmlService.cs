namespace CloudCommerceGroup.Converter.Xml
{
    using System.Threading.Tasks;

    public interface IXmlService
    {
        Task<string> ProcessCsvToXml(string[] content);

        Task ProcessXmlToCsv(string input, string output);
    }
}