using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CslAppServicesConsumer.Tests.Test1
{
    public class UserService
    {
        private const string BaseUrl = "https://localhost:44356";

        public static async void Run()
        {
            await Test("admin", "admin");
            Console.WriteLine("============");
            await Test("user", "user");
        }

        private static async Task Test(string name, string password)
        {
            try
            {
                var userConnected = await Authenticate(name, password);
                string token = userConnected.Token;

                var userFound = await GetById(token, 1);
                Console.WriteLine($"userFound: {userFound}");

                var userFound2 = await GetById(token, 2);
                Console.WriteLine($"userFound: {userFound2}");

                var countUsers = await GetCountUsers(token);
                Console.WriteLine($"countUsers: {countUsers}");

                IEnumerable<User> list = await GetAll(token);
                foreach (User user in list)
                {
                    Console.WriteLine($"user: {user}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static async Task<User> Authenticate(string username, string password)
        {
            var url = $"{BaseUrl}/users/authenticate";
            AuthenticateModel authenticateModel = new AuthenticateModel(username, password);

            JsonSerializerSettings jss = new JsonSerializerSettings();
            string strValue = JsonConvert.SerializeObject(authenticateModel, jss);
            ByteArrayContent contention = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(strValue));
            contention.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpClient tRequest = new HttpClient();
            var response = await tRequest.PostAsync(url, contention);
            var json = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(json);

            JsonSerializerSettings serSettings = new JsonSerializerSettings();
            serSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var user = JsonConvert.DeserializeObject<User>(json, serSettings);
            return user;
        }

        private static async Task<IEnumerable<User>> GetAll(string token)
        {
            try
            {
                using var client = new HttpClient();
                var url = $"{BaseUrl}/users/getAll";
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(json);
                    var list = JsonConvert.DeserializeObject<IEnumerable<User>>(json);
                    return list;
                }
                Console.WriteLine($"GetAll - Response StatusCode={response.StatusCode}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new List<User>();
        }

        private static async Task<User> GetById(string token, int id)
        {
            try
            {
                using var client = new HttpClient();
                var url = $"{BaseUrl}/users/{id}";
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(json);
                    var list = JsonConvert.DeserializeObject<User>(json);
                    return list;
                }
                Console.WriteLine($"GetById - Response StatusCode={response.StatusCode}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }

        private static async Task<int> GetCountUsers(string token)
        {
            try
            {
                using var client = new HttpClient();
                var url = $"{BaseUrl}/users/countUsers";
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(json);
                    var list = Convert.ToInt32(json);
                    return list;
                }
                Console.WriteLine($"CountUsers - Response StatusCode={response.StatusCode}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return -1;
        }
    }
}
