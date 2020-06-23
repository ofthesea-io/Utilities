namespace FileConverter
{
    using System.Threading.Tasks;

    public interface IConverter
    {
        #region Properties

        object MetaData { get; set; }

        #endregion

        #region Methods

        Task Process(string input, string output);

        #endregion
    }
}