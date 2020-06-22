namespace FileConverter.Core
{
    using System.Threading.Tasks;

    public interface IProcessor
    { 
        string ConversionType { get; }

        #region Methods

        Task<string> Execute(string[] content);

        #endregion
    }
}