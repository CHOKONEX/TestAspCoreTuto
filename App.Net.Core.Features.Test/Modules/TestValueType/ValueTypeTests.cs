using NUnit.Framework;
using System;

namespace App.Net.Core.Features.Test
{
    [TestFixture]
    public class ValueTypeTests
    {
        [Test]
        public void should_test_v1()
        {
            // ValueTuple with 0 element 
            ValueTuple<int> ValTpl0 = new ValueTuple<int>();
            Assert.AreEqual(0, ValTpl0.Item1);

            // ValueTuple with one element 
            ValueTuple<int> ValTpl1 = new ValueTuple<int>(345678);
            Assert.AreEqual(345678, ValTpl1.Item1);

            // ValueTuple with three elements 
            ValueTuple<string, string, int> ValTpl2 = new ValueTuple<string, string, int>("C#", "Java", 586);
            // ValueTuple with eight elements 
            ValueTuple<int, int, int, int, int, int, int, ValueTuple<int>> ValTpl3 = new ValueTuple<int,
                                      int, int, int, int, int, int, ValueTuple<int>>(45, 67, 65, 34, 34,
                                                                        34, 23, new ValueTuple<int>(90));
        }
        [Test]
        public void should_test_v2()
        {
            //Using parenthesis()
            (int age, string Aname, string Lang) author = (23, "Sonia", "C#");
            Assert.AreEqual(23, author.age);

            (int age, string Aname, string Lang) authorV2 = (age: 23, Aname: "Sonia", Lang: "C#");
            Assert.AreEqual(23, authorV2.age);
            Assert.AreEqual("Sonia", authorV2.Aname);
            authorV2.age = 24;
            Assert.AreEqual(24, authorV2.age);

            ValueTuple<int, string, string> authorV3 = (20, "Siya", "Ruby");
            Assert.AreEqual(20, authorV3.Item1);

            ValueTuple<int, string, string> authorV4 = (age: 23, Aname: "Sonia", Lang: "C#");
            Assert.AreEqual(23, authorV4.Item1);

            (int age, string Aname, string Lang) authorV5 = new ValueTuple<int, string, string>(23, "Siya", "Ruby");
            Assert.AreEqual(23, authorV5.age);

            (int age, string name, string country) authorV6 = TouristDetails();
            Assert.AreEqual(23, authorV6.age);
            Assert.AreEqual("USA", authorV6.country);

            (int, string, string) authorV7 = GetPerson();
            Assert.AreEqual(1, authorV7.Item1);
        }

        [Test]
        public void should_test_v3()
        {
            // use discard _ for the unused member LName
            (int id, string FName, _) = GetPerson();
            Assert.AreEqual(1, id);
            Assert.AreEqual("Bill", FName);
            //Assert.AreEqual("Gates", _);  //TODO NOT WORKING

            //(int Id, string FName, _) authorV7 = GetPerson(); //TODO NOT WORKING
            //Assert.AreEqual(1, authorV7.Id);
            //Assert.AreEqual("Gates", authorV7.Item3);
        }

        private static (int, string, string) TouristDetails()
        {
            return (23, "Sophite", "USA");
        }

        private static (int age, string firstName, string lastName) TouristDetails2()
        {
            return (23, "Sophite", "USA");
        }

        private static (int, string, string) GetPerson()
        {
            return (Id: 1, FirstName: "Bill", LastName: "Gates");
        }
    }
}