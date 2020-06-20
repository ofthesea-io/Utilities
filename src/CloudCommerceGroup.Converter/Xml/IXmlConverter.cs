namespace CloudCommerceGroup.Converter.Xml
{
    using System.Threading.Tasks;

    public interface IXmlConverter
    {
        Task ConvertCsvToXml();

        Task ConvertXmlToCsv();
    }
}