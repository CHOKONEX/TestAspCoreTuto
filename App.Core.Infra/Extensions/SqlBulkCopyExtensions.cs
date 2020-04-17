using System;
using System.Reflection;
using System.Data.SqlClient;
using static System.Reflection.BindingFlags;

namespace App.Core.Infra.Extensions
{
    public static class SqlBulkCopyExtensions
    {
        private const string _rowsCopiedFieldName = "_rowsCopied";

        private static readonly Lazy<FieldInfo> _rowsCopiedLazy = new Lazy<FieldInfo>(()
            => typeof(SqlBulkCopy).GetField(_rowsCopiedFieldName, NonPublic | GetField | Instance));

        public static int GetRowsCopied(this SqlBulkCopy sqlBulkCopy)
            => (int)_rowsCopiedLazy.Value.GetValue(sqlBulkCopy);
    }
}
