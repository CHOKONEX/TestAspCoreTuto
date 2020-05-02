using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace App.Net.Core.Features.Test.Modules.TestCsharpVersions.TestCSharp6
{
    /*C# 6*/
    /*********/
    /*
     * Auto-properties en lecture seule
     * Initialiseurs de propriétés automatiques
     * Membres de fonction expression-bodied
     * using static
     * Null - Opérateurs conditionnel ? 
     * Interpolation de chaîne $ 
     * Filtres d’exceptions Catch when
     * Expression nameof
     * await dans des blocs catch et finally
     * Initialiser des collections associatives à l’aide d’indexeurs Dictionary [404] = "test"
     * Résolution de surcharge améliorée Task DoThings() => return Task.FromResult(0) : Le compilateur précédent ne pouvait pas faire correctement la distinction entre Task.Run(Action) et Task.Run(Func<Task>())
     * Sortie du compilateur Deterministic :
         The -deterministic option instructs the compiler to produce a byte-for-byte identical output assembly for successive compilations of the same source files.
         By default, every compilation produces unique output on each compilation. The compiler adds a timestamp, and a GUID generated from random numbers. 
         You use this option if you want to compare the byte-for-byte output to ensure consistency across builds. 
     */
    public static class TestCsharp6Module
    {
        /*Auto-properties en lecture seule*/
        /*********/
        public partial class Student
        {
            public string LastName { get; }
            public string FirstName { get; set; }

            public Student(string firstName, string lastName)
            {
                if (string.IsNullOrWhiteSpace(lastName))
                    throw new ArgumentException(message: "Cannot be blank", paramName: nameof(lastName));
                FirstName = firstName;
                LastName = lastName;
            }

            public void ChangeName(string newLastName)
            {
                // Generates CS0200: Property or indexer cannot be assigned to -- it is read only
                //TO DO ERROR LastName = newLastName;
            }
        }

        /*Initialiseurs de propriétés automatiques*/
        /*********/
        public partial class Student
        {
            public ICollection<double> Grades { get; } = new List<double>();
        }

        /*Membres de fonction expression-bodied*/
        /*********/
        public partial class Student
        {
            public override string ToString() => $"{LastName}, {FirstName}";
            public string FullName => $"{FirstName} {LastName}";
        }

        /*using static*/
        /*********/
        // using static System.Math;
        public partial class Student
        {

        }

        /*Null - Opérateurs conditionnel*/
        /*********/
        public partial class Student
        {
            public void Change(Student person)
            {
                var first = person?.FirstName;
                first = person?.FirstName ?? "Unspecified";
            }
        }

        /*Interpolation de chaîne*/
        /*********/
        public partial class Student
        {
            public string GetGradePointPercentage() => $"Name: {LastName}, {FirstName}";

            public string GetGradePointPercentageV2()
            {
                //Souvent, vous devrez mettre en forme la chaîne produite à l’aide d’une culture spécifique
                //FormattableString.ToString(IFormatProvider) pour spécifier la culture lors de la mise en forme d’une chaîne
                FormattableString str = $"Name: {LastName}, {FirstName}";
                var gradeStr = str.ToString(new System.Globalization.CultureInfo("de-DE"));
                return gradeStr;
            }
        }

        /*Filtres d’exceptions*/
        /*********/
        //catch (HttpRequestException e) when (e.Message.Contains("301"))
        public partial class Student
        {
            public static async Task<string> MakeRequest()
            {
                using (HttpClient client = new HttpClient())
                {
                    var stringTask = client.GetStringAsync("https://docs.microsoft.com/en-us/dotnet/about/");
                    try
                    {
                        var responseText = await stringTask;
                        return responseText;
                    }
                    catch (HttpRequestException e) when (e.Message.Contains("301"))
                    {
                        return "Site Moved";
                    }
                }
            }
        }

        /*Expression nameof*/
        /*********/
        //if (IsNullOrWhiteSpace(lastName))
        //throw new ArgumentException(message: "Cannot be blank", paramName: nameof(lastName));


        /*await dans des blocs catch et finally*/
        /*********/
        public partial class Student
        {
            public static async Task<string> MakeRequestAndLogFailures()
            {
                await logMethodEntrance();
                var client = new System.Net.Http.HttpClient();
                var streamTask = client.GetStringAsync("https://localHost:10000");
                try
                {
                    var responseText = await streamTask;
                    return responseText;
                }
                catch (HttpRequestException e) when (e.Message.Contains("301"))
                {
                    await logError("Recovered from redirect", e);
                    return "Site Moved";
                }
                finally
                {
                    await logMethodExit();
                    client.Dispose();
                }
            }

            private static Task logMethodExit()
            {
                throw new NotImplementedException();
            }

            private static Task logError(string v, HttpRequestException e)
            {
                throw new NotImplementedException();
            }

            private static Task logMethodEntrance()
            {
                throw new NotImplementedException();
            }
        }

        /*Initialiser des collections associatives à l’aide d’indexeurs*/
        /*********/
        public partial class Student
        {
            //private Dictionary<int, string> messages = new Dictionary<int, string>
            //{
            //    { 404, "Page not Found"},
            //    { 302, "Page moved, but left a forwarding address."},
            //    { 500, "The web server can't come out to play today."}
            //};

            private Dictionary<int, string> webErrors = new Dictionary<int, string>
            {
                [404] = "Page not Found",
                [302] = "Page moved, but left a forwarding address.",
                [500] = "The web server can't come out to play today."
            };
        }

        /*Résolution de surcharge améliorée*/
        /*********/
        public partial class Student
        {
            //Le compilateur précédent ne pouvait pas faire correctement la distinction entre Task.Run(Action) et Task.Run(Func<Task>()).

            static Task DoThings()
            {
                return Task.FromResult(0);
            }

            //Le compilateur C# 6 détermine correctement que Task.Run(Func<Task>())
            private static void Run()
            {
                Task.Run(() => DoThings());
            }
        }
    }
}
