using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace App.Net.Core.Features.Test.Modules.TestCsharpVersions.TestCSharp6
{
    /*C# 5*/
    /*********/
    /*
     * Asynchronous programming
     * Lambda expressions
     * Caller Information
    */
    public static class TestCsharp5Module
    {
        /*Asynchronous programming*/
        /*********/
        public partial class Student
        {
            private readonly HttpClient _httpClient = new HttpClient();

            public async Task<int> GetDotNetCountAsync()
            {
                // Suspends GetDotNetCountAsync() to allow the caller (the web server)
                // to accept another request, rather than blocking on this one.
                var html = await _httpClient.GetStringAsync("https://dotnetfoundation.org");

                return Regex.Matches(html, @"\.NET").Count;
            }
        }

        /*Lambda expressions*/
        /*********/
        public partial class Student
        {
            static void Main(string[] args)
            {
                string a = ", middle name,";

                Func<string, string> del1 = (string temp) =>
                {
                    temp += a;
                    temp += "the value is added to name";
                    return temp;
                };
                Console.WriteLine(del1("New String"));
            }
        }

        /*Caller Information*/
        /*********/
        public partial class Student
        {
            public static void Main()
            {
                ShowCallerInfo("msg");
                Console.ReadKey();
            }

            public static void ShowCallerInfo(string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            {
                Trace.WriteLine("message: " + message);
                Trace.WriteLine("member name: " + memberName); //Caller Name: Main
                Trace.WriteLine("source file path: " + sourceFilePath); //Caller FilePath: h:\ConsoleApplication1\Student.cs
                Trace.WriteLine("source line number: " + sourceLineNumber); //Caller Line number: 62
            }

            public class EmployeeVM : INotifyPropertyChanged
            {
                public event PropertyChangedEventHandler PropertyChanged;

                public void OnPropertyChanged([CallerMemberName] string propertyName = null)
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                }

                private string _name;

                public string Name
                {
                    get => _name;
                    set => _name = value;// The compiler converts the above line to:// RaisePropertyChanged ("Name");
                }

                private string _phone;

                public string Phone
                {
                    get => _phone;
                    set
                    {
                        _phone = value;
                        OnPropertyChanged();
                        // The compiler converts the above line to:
                        // RaisePropertyChanged ("Phone");
                    }
                }
            }
        }
    }
}
