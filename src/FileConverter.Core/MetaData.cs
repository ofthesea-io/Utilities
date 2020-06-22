namespace FileConverter.Core
{
    using System;
    using System.ComponentModel;
    using System.Composition;

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Method)]
    public class MetaData : ExportAttribute
    {
        #region Constructors

        public MetaData(string conversionType)
            : base(typeof(IComponent))
        {
            this.ConversionType = conversionType;
        }

        #endregion

        #region Properties

        public string ConversionType { get; }

        #endregion
    }
}