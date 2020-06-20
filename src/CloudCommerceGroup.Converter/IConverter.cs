using System.Threading.Tasks;

namespace CloudCommerceGroup.Converter
{
    public interface IConverter
    {
        Task Process(string input, string output, char delimiter);
    }
}