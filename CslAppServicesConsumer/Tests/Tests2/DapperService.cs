using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using App.Core.Dto.Tests;
using Newtonsoft.Json;

namespace CslAppServicesConsumer.Tests.Test1
{
    public static class DapperService
    {
        private const string BaseUrl = "https://localhost:44356";

        public static async void Run()
        {
            Console.WriteLine("===DAPPER======");
            await Test();
            Console.WriteLine("============");
        }

        private static async Task Test()
        {
            try
            {
                IEnumerable<Person> list = await GetAll();
                foreach (Person user in list)
                {
                    Console.WriteLine($"Person: {user}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static async Task<IEnumerable<Person>> GetAll()
        {
            try
            {
                using var client = new HttpClient();
                var url = $"{BaseUrl}/test/dapper/getAll";
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(json);
                    var list = JsonConvert.DeserializeObject<IEnumerable<Person>>(json);
                    return list;
                }
                Console.WriteLine($"getAll - Response StatusCode={response.StatusCode}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new List<Person>();
        }

        
    }
}
