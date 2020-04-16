﻿using Dapper.Contrib.Extensions;

namespace App.Core.Dto.Tests
{
    public enum InvoiceKind
    {
        StoreInvoice = 1,
        WebInvoice = 2
    }

    [Table("Invoice")] //Specifie the destination table name mapped to the entity.
    public class InvoiceContrib
    {
        [Key] //Specifie the property is a key that is automatically generated by the database (Identity Column).
        public int InvoiceID { get; set; }

        public string Code { get; set; }
        public InvoiceKind Kind { get; set; }

        [Write(false)] //Specifie if the property is writable or not.
        [Computed] //Specifie the property should be excluded from update.
        public string FakeProperty { get; set; }
    }

    [Table("InvoiceDetail")]
    public class InvoiceDetailContrib
    {
        [ExplicitKey] //Specifie the property is a key that is NOT automatically generated by the database.
        public int InvoiceID { get; set; }

        public string Detail { get; set; }
    }

    public class Invoice
    {
        public int InvoiceID { get; set; }
        public string Code { get; set; }
        public InvoiceKind Kind { get; set; }
        //public List<InvoiceItem> Items { get; set; }
        //public InvoiceDetail Detail { get; set; }
    }

    public class StoreInvoice : Invoice
    {
    }

    public class WebInvoice : Invoice
    {
    }
}
