namespace FileConverter.Core
{
    using System;
    using System.IO;

    public abstract class BaseService
    {
        protected void ParseCsv(ref string[] content, char delimiter)
        {
            if (content.Length == 0)
                throw new NullReferenceException("CSV file is empty");

            int columns = content[0].Split(delimiter).Length;
            if (columns == 1)
                throw new InvalidDataException("Invalid delimiter!");

            for (int i = 1; i < content.Length; i++)
            {
                int j = content[i].Split(delimiter).Length;
                if (j != columns)
                    throw new InvalidDataException("CSV data validation failed!");
            }
        }
    }
}