using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace App.Net.Core.Features.Test.Modules.TestCsharpVersions.TestCSharp6
{
    /*C# 4*/
    /*********/
    /*
     * Named and Optional Parameters
     * Dynamic programming
     * Covariance and contravariance for generics and delegates
    */
    public static class TestCsharp4Module
    {
        /*Named Parameters*/
        /*********/
        public partial class Student
        {
            static void Main()
            {
                ShowMessage(name: "Jane", age: 17);
            }

            public static void ShowMessage(int age, string name)
            {
                Console.WriteLine("{0} is {1} years old", name, age);
            }
        }

        /*Optional Parameters*/
        /*********/
        public partial class Student
        {
            static void Main3()
            {
                Power(4, 4);
                Power(5);
            }

            public static void Power(int x, int y = 2)
            {
                int z = 1;

                for (int i = 0; i < y; i++)
                {
                    z *= x;
                }

                Console.WriteLine(z);
            }
        }

        /*Dynamic programming*/
        /*********/
        public class Duck
        {
            public void quack()
            {
                Console.WriteLine("Quaaaack");
            }
        }

        public class Person
        {
            public void quack()
            {
                Console.WriteLine("Person imitates a duck");
            }
        }

        public partial class Student
        {
            static void Main1()
            {
                Duck donald = new Duck();
                Person josh = new Person();

                InTheForest(donald);
                InTheForest(josh);
            }

            public static void InTheForest(dynamic duck)
            {
                duck.quack();
            }
        }

        /*C# covariance & contravariance*/
        /*********/
        /*
            * covariant if it preserves the ordering, ≤, of types, which orders types from more specific to more generic;
            * contravariant if it reverses this ordering, which orders types from more generic to more specific;
            * invariant if neither of these apply.
            
            * la covariance permet de caster un type générique A<T> dans un type générique A<K> si T hérite directement ou indirectement de K et si le type T est un paramètre de sortie.
              La covariance (ainsi que la contravariance) fonctionne uniquement avec des interfaces et des délégués. Les arguments génériques doivent être des types références.

            * La contravariance est la possiblité de caster un type générique A<T> dans un type générique A<K> si T est parent direct ou indirect de K et si T est un paramètre d’entrée.
        */

        public partial class Student
        {
            static void Main4()
            {
                IEnumerable<string> strings = new List<string>() { "1", "3", "2", "5" }; //Covariance
                PrintAll(strings);
            }

            //Contravariance
            // IEnumerable<string> is implicitly converted to IEnumerable<object>
            static void PrintAll(IEnumerable<object> objects)
            {
                foreach (object o in objects)
                {
                    System.Console.WriteLine(o);
                }
            }


            /*
             COVARIANCE
             « out » dans le but de permettre la covariance avec les génériques.
             Convertir un « IMembres<Responsable> » en « IMembres<Moderateur> » étant donné que la classe « Responsable » dérive de « Moderateur »
            */
            interface IMembres<out T> { }
            class Membres<T> : IMembres<T> { }
            class Moderateur { }
            class Responsable : Moderateur { }

            static void Main(string[] args)
            {
                IMembres<Responsable> responsables = new Membres<Responsable>();
                IMembres<Moderateur> moderateurs = responsables;
                IMembres<object> membres = responsables;
            }


            /*
             CONTRAVARIANCE
             « in » dans le but de permettre la contravariance avec les génériques.
            */
            public class Person
            {
                public string FirstName { get; set; }
                public string LastName { get; set; }
            }

            public class Customer : Person
            {
                public int CustomerId { get; set; }
                public void Process() {}
            }

            public class SalesRep : Person
            {
                public int SalesRepId { get; set; }
                public void SellStuff() {}
            }

            public class FooClass<T>
            {
                public static void Process(MyInterface<T> obj)
                {
                }
            }
            public interface MyInterface<in T>
            {
                void SetItem(T obj);
                void Copy(T obj);
            }
            public class MyClass<T> : MyInterface<T>
            {
                public void SetItem(T obj)
                {
                    _item = obj;
                }
                private T _item;
                public void Copy(T obj)
                {
                }
            }

            static void Main6(string[] args)
            {
                MyInterface<Customer> customer = new MyClass<Customer>();
                MyInterface<Person> person = new MyClass<Person>();
                person.SetItem(new SalesRep());
                FooClass<Customer>.Process(customer);
                FooClass<Customer>.Process(person);
            }


            /*Exemple pratique : La création d’une interface covariant et contravariant*/

            public abstract class Person3 { }
            public class Employe : Person3 { }
            public class Customer2 : Person3 { }
            public class Manager : Employe { }

            public void TestCovariance()
            {
                Person3[] MaSociete = new Manager[10];  // .NET 3.5 / 4 : OK 
                Employe[] MaSociete2 = new Manager[10]; // .NET 3.5 / 4 : OK 

                IEnumerable<Person3> ipl = new List<Manager>();  // .NET 3.5 : Erreur de compilation  / .NET 4 : OK
                Func<string> funcString = () => { return "Bonjour le monde"; };
                Func<object> funcObject = funcString;         // .NET 3.5 : Erreur de compilation  / .NET 4 : OK
            }

            public void TestContravariance()
            {
                Action<object> actObject = (object o) => { };
                Action<string> actString = actObject;          // .NET 3.5 : Erreur de compilation  / .NET 4 : OK
            }

            //in if T is used for methods
            //out for returns
            public interface IDataTransformer<in T, out K>
            {
                K Transform(T data);
            }

            public class DataTransformer<T, K> : IDataTransformer<T, K>
            {
                public K Transform(T data)
                {
                    return default(K);
                }
            }

            public void TestCoAndContra()
            {
                IDataTransformer<Manager, string> dataTransformer = new DataTransformer<Manager, string>(); // .NET 3.5 / 4 : OK
                IDataTransformer<Manager, string> dataTransformer2 = new DataTransformer<Person3, string>(); // .NET 3.5 / 4 : Erreur de compilation 
                IDataTransformer<Manager, object> dataTransformer3 = new DataTransformer<Employe, string>(); // .NET 3.5 / 4 : Erreur de compilation 
            }
        }
    }
}
