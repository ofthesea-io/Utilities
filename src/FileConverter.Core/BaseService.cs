namespace FileConverter.Core
{
    public abstract class BaseService
    {
        #region Properties

        public virtual char Delimiter { get; set; } = ',';

        #endregion
    }
}