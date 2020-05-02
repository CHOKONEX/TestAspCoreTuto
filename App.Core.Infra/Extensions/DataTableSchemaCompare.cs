using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

/*
 https://stackoverflow.com/questions/7313282/check-to-see-if-2-datatable-have-same-schema
*/
namespace App.Core.Infra.Extensions
{
    public static class DataTableSchemaCompare
    {
        public static bool SchemaEquals(this DataTable dt, DataTable value)
        {
            if (dt == null) throw new ArgumentNullException(nameof(dt));
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (dt.Columns.Count != value.Columns.Count)
                return false;

            var dtColumns = dt.Columns.Cast<DataColumn>();
            var valueColumns = value.Columns.Cast<DataColumn>();

            var exceptCount = dtColumns.Except(valueColumns, DataColumnEqualityComparer.Instance).Count();
            return (exceptCount == 0);
        }

        public static bool SchemaMatches(this DataTable table, DataTable referenceTable)
        {
            if (table.Columns.Count != referenceTable.Columns.Count || table.PrimaryKey.Count() != referenceTable.PrimaryKey.Count())
                return false;
            foreach (DataColumn referenceColumn in referenceTable.Columns)
            {
                try
                {
                    DataColumn column = table.Columns[referenceColumn.ColumnName];
                    if (column == null || !referenceColumn.AllowDBNull.Equals(column.AllowDBNull) || !referenceColumn.ColumnName.Equals(column.ColumnName)
                        || !referenceColumn.DataType.Equals(column.DataType) || !referenceColumn.Expression.Equals(column.Expression) || !referenceColumn.ReadOnly.Equals(column.ReadOnly))
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
            foreach (DataColumn referenceKey in referenceTable.PrimaryKey)
            {
                try
                {
                    DataColumn key = table.PrimaryKey.Single(x => x.ColumnName == referenceKey.ColumnName);
                    if (key == null)
                        return false;
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        class DataColumnEqualityComparer : IEqualityComparer<DataColumn>
        {
            #region IEqualityComparer Members

            private DataColumnEqualityComparer() { }
            public static DataColumnEqualityComparer Instance = new DataColumnEqualityComparer();


            public bool Equals(DataColumn x, DataColumn y)
            {
                if (x.ColumnName != y.ColumnName)
                    return false;
                if (x.DataType != y.DataType)
                    return false;

                return true;
            }

            public int GetHashCode(DataColumn obj)
            {
                int hash = 17;
                hash = 31 * hash + obj.ColumnName.GetHashCode();
                hash = 31 * hash + obj.DataType.GetHashCode();

                return hash;
            }

            #endregion
        }
    }
}
