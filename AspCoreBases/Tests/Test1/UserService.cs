using App.Core.Infra.SqlResourcesReader;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using TestAspCoreTuto.Bootstrapping.Helpers;

namespace TestAspCoreTuto.Tests.Test1
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }

    public class UserService : IUserService
    {
        private readonly List<User> _users = new List<User>
        {
            new User
            {
                Id = 1, FirstName = "Admin", 
                LastName = "User", Username = "admin", 
                Password = "admin", Role = "Admin",
                Email = "admin@gmail.com", Poste = "Director", Department = "PARIS"
            },
            new User
            {
                Id = 2, FirstName = "Normal", 
                LastName = "User", Username = "user", 
                Password = "user", Role = "User",
                Email = "user@gmail.com", Poste = "Employee", Department = "PARIS"
            },
            new User
            {
                Id = 3, FirstName = "dev",
                LastName = "dev", Username = "dev",
                Password = "dev", Role = "Dev",
                Poste = "Employee", Department = "US"
            }
        };

        private readonly AppSettings _appSettings;
        private readonly ISqlFileQueryReader _sqlFileQueryReader;

        public UserService(IOptions<AppSettings> appSettings, ISqlFileQueryReader sqlFileQueryReader)
        {
            _appSettings = appSettings.Value;
            _sqlFileQueryReader = sqlFileQueryReader ?? throw new ArgumentNullException(nameof(sqlFileQueryReader));
        }

        public User Authenticate(string username, string password)
        {
            string query = _sqlFileQueryReader.GetQuery("SqlTestFile.sql");
            Console.WriteLine(query);

            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);
            if (user == null)
                return null;

            user.Token = TokenGenerator.GenerateToken(user.Id, user.Role, user.Email, _appSettings.Secret);
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User GetById(int id)
        {
            var user = _users.FirstOrDefault(x => x.Id == id);
            return user;
        }
    }
}
