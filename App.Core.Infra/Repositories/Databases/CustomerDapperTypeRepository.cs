using App.Core.Dto.Tests;
using App.Core.Infra.Database;
using App.Core.Infra.SqlResourcesReader;
using Asp.Core.Attributes;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace App.Core.Infra.Repositories.Databases
{
    [Singleton]
    public class CustomerDapperTypeRepository : ICustomerDapperTypeRepository
    {
        private readonly IDatabaseReader _databaseReader;
        private readonly ISqlFileQueryReader _sqlFileQueryReader;
        private readonly IDatabaseExecutor _databaseExecutor;

        public CustomerDapperTypeRepository(ISqlFileQueryReader sqlFileQueryReader, IDatabaseReader databaseReader, IDatabaseExecutor databaseExecutor)
        {
            _sqlFileQueryReader = sqlFileQueryReader ?? throw new ArgumentNullException(nameof(sqlFileQueryReader));
            _databaseReader = databaseReader ?? throw new ArgumentNullException(nameof(databaseReader)); ;
            _databaseExecutor = databaseExecutor ?? throw new ArgumentNullException(nameof(databaseExecutor));
        }

        public async Task<IEnumerable<CustomerModel>> GetAllCustomers()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Code");
            dt.Columns.Add("Name");
            for (int i = 0; i < 5; i++)
            {
                dt.Rows.Add("Code_" + i, "Name_" + i);
            }

            await _databaseExecutor.ExecuteAsync("Customer_Seed",
                new { Customers = dt.AsTableValuedParameter("TVP_Customer") },
                commandType: CommandType.StoredProcedure);

            //Verify
            string sql = "select * from Customer";
            return await _databaseReader.ReadManyAsync<CustomerModel>(sql);
        }

        public async Task<IEnumerable<Invoice>> GetDirectorsIdentities()
        {
            string sql = "SELECT * FROM Invoice;";
            return await _databaseReader.ExecuteReaderAsync(sql, MapFromDataReaderDynamic);
        }

        private IEnumerable<Invoice> MapFromDataReaderDynamic(IDataReader reader)
        {
            var invoices = new List<Invoice>();

            var storeInvoiceParser = reader.GetRowParser<StoreInvoice>();
            var webInvoiceParser = reader.GetRowParser<WebInvoice>();

            while (reader.Read())
            {
                Invoice invoice;

                int indexOfKind = reader.GetOrdinal("Kind");
                switch ((InvoiceKind)reader.GetInt32(indexOfKind))
                {
                    case InvoiceKind.StoreInvoice:
                        invoice = storeInvoiceParser(reader);
                        break;
                    case InvoiceKind.WebInvoice:
                        invoice = webInvoiceParser(reader);
                        break;
                    default:
                        throw new Exception("Not exist");
                }

                invoices.Add(invoice);
            }

            return invoices;
        }
    }
}
