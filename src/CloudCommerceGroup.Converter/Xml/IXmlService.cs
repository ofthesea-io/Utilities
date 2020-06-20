namespace CloudCommerceGroup.Converter.Xml
{
    using System.Threading.Tasks;

    public interface IXmlService
    {
        Task ProcessCsvToXml(string input, string output);

        Task ProcessXmlToCsv(string input, string output);
    }
}