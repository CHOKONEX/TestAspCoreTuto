using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Net.Core.Features.Test.Tests
{
    public class TestDictionary
    {
        //Ncruch -> Configuration : Copy referenced assemblies to workspace
        //Set to TRUE if referenced assemblies are to be copied into this component's workspace when executing its tests. 

        [Test]
        public void should_test_v1()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                List<UserPl> list = new List<UserPl>();
                list.Add(new UserPl { Name = "p1" });
                list.Add(new UserPl { Name = "p1" });
                list.Add(new UserPl { Name = "p2" });
                //NOT WORKING : System.ArgumentException : An item with the same key has already been added. Key: p1
                Dictionary<string, UserPl> dict = list.ToDictionary(x => x.Name);
                foreach (var item in dict)
                {
                    Console.WriteLine(item.Value.Name);
                }
            });
        }

        [Test]
        public void should_test_v2()
        {
            Assert.DoesNotThrow(() =>
            {
                List<UserPl> list = new List<UserPl>();
                list.Add(new UserPl { Name = "p1", Ville = "Rome" });
                list.Add(new UserPl { Name = "p1", Ville = "Rome" });
                list.Add(new UserPl { Name = "p2", Ville = "Madrid" });
                ILookup<string, UserPl> dict = list.ToLookup(x => x.Name);
                foreach (IGrouping<string, UserPl> item in dict)
                {
                    Console.WriteLine(string.Join(", ", item.Select(x => x.Name)));
                }
            });
        }

        [Test]
        public void should_test_v3()
        {
            Assert.DoesNotThrow(() =>
            {
                List<UserPl> list = new List<UserPl>();
                list.Add(new UserPl { Name = "p1", Ville = "Rome" });
                list.Add(new UserPl { Name = "p1", Ville = "Rome" });
                list.Add(new UserPl { Name = "p2", Ville = "Madrid" });
                Dictionary<string, List<UserPl>> dict = list.GroupBy(x=> x.Ville).ToDictionary(x => x.Key, x=> x.ToList());
                foreach (KeyValuePair<string, List<UserPl>> item in dict)
                {
                    Console.WriteLine(string.Join(", ", item.Value.Select(x=> x.Name)));
                }
            });
        }

        private class UserPl
        {
            public string Name { get; set; }
            public string Prenom { get; set; }
            public string Ville { get; set; }
        }
    }
}
