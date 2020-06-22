namespace FileConverter.Core
{
    using System.Threading.Tasks;

    public interface IProcessor
    {
        #region Methods

        Task<string> Process(string[] content);

        #endregion
    }
}