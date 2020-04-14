using System.Collections.Generic;

namespace App.Core.Dto.Tests
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Born { get; set; }
        public Country Residience { get; set; }
        public ICollection<Book> Books { get; set; }
    }

    public class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }

    public class Book
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
    }
}
