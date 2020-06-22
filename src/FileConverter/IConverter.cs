namespace FileConverter
{
    using System.Threading.Tasks;

    public interface IConverter
    {
        #region Properties

        char Delimiter { get; set; }

        #endregion

        #region Methods

        Task Process(string input, string output);

        #endregion
    }
}