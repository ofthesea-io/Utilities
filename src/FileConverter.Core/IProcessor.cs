namespace FileConverter.Core
{
    using System.Threading.Tasks;

    public interface IProcessor
    {
        public object MetaData { get; set; }

        string ConversionType { get; }

        #region Methods

        Task<string> Execute(string[] content);

        #endregion
    }
}