namespace FileConverter.Xml
{
    using System.Threading.Tasks;

    public interface IXmlService
    {
        Task<string> ProcessCsvToXml(string[] content);

        Task<string> ProcessXmlToCsv(string input);
    }
}