namespace App.Core.Infra.SqlResourcesReader
{
    public interface ISqlFileQueryReader
    {
        string GetQuery(string sqlFileName);
    }
}