using App.Core.Dto.Tests;
using App.Core.Infra.Database;
using App.Core.Infra.SqlResourcesReader;
using Asp.Core.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Core.Infra.Repositories.Databases
{
    [Singleton]
    public class DapperTestRepository : IDapperTestRepository
    {
        private readonly IDatabaseReader _databaseReader;
        private readonly ISqlFileQueryReader _sqlFileQueryReader;

        public DapperTestRepository(IDatabaseReader databaseReader, ISqlFileQueryReader sqlFileQueryReader)
        {
            _databaseReader = databaseReader;
            _sqlFileQueryReader = sqlFileQueryReader;
        }

        public async Task<IEnumerable<Person>> GetPersons()
        {
            string sql = _sqlFileQueryReader.GetQuery("TestDapperOneToMany.sql");
            object param = null;

            var persons = new Dictionary<int, Person>();
            return await _databaseReader.ReadOneToManyAsync<Person, Country, Book, Person>(
                sql,
                (person, country, book) => MapToPerson(person, country, book, persons),
                "CountryId,BookId", param);
        }

        public async Task<IEnumerable<Person>> GetPersonsV2()
        {
            string sql = _sqlFileQueryReader.GetQuery("TestDapperOneToMany.sql");
            object param = null;

            return await _databaseReader.ReadOneToManyAsync<Person, Country, Book, Person>(sql, MapToPersonV2, "CountryId,BookId", param);
        }

        private static Person MapToPerson(Person person, Country country, Book book, Dictionary<int, Person> persons)
        {
            //person
            Person personEntity;

            //trip
            if (!persons.TryGetValue(person.Id, out personEntity))
            {
                persons.Add(person.Id, personEntity = person);
            }

            //country
            if (personEntity.Residience == null)
            {
                if (country == null)
                {
                    country = new Country { CountryName = "" };
                }
                personEntity.Residience = country;
            }

            //books
            if (personEntity.Books == null)
            {
                personEntity.Books = new List<Book>();
            }

            if (book != null)
            {
                if (!personEntity.Books.Any(x => x.BookId == book.BookId))
                {
                    personEntity.Books.Add(book);
                }
            }

            return personEntity;
        }

        private static Person MapToPersonV2(Person person, Country country, Book book)
        {
            //country
            if (person.Residience == null)
            {
                if (country == null)
                {
                    country = new Country { CountryName = "" };
                }
                person.Residience = country;
            }

            //books
            if (person.Books == null)
            {
                person.Books = new List<Book>();
            }

            if (book != null && !person.Books.Any(x => x.BookId == book.BookId))
            {
                person.Books.Add(book);
            }

            return person;
        }
    }
}
