namespace App.Core.Infra.FileResourceReader
{
    public interface ISqlFileQueryReader
    {
        string GetQuery(string sqlFileName);
    }
}