using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Net.Core.Features.Test.Modules.TestCsharpVersions
{
    /*C# 7.1*/
    /*********/
    /*
     * Async main
     * Default literal expressions
     * Inferred tuple element names
     * Pattern matching on generic type parameters
     * Reference assembly generation -refout and -refonly
    */
    public static class TestCsharp71Module
    {
        /*Async main*/
        /*********/
        public partial class Student
        {
            static async Task Main()
            {
                await SomeAsyncMethod();
            }

            static async Task<int> SomeAsyncMethod()
            {
                return await Task.FromResult<int>(0);
            }
        }

        /*Default literal expressions*/
        /*********/
        public partial class Student
        {
            public void Test()
            {
                //Func<string, bool> whereClause = default(Func<string, bool>);
                Func<string, bool> whereClause = default;
            }
        }

        /*Inferred tuple element names*/
        /*********/
        /*when you initialize a tuple, the variables used for the right side of the assignment are the same as the names you'd like for the tuple elements*/
        public partial class Student
        {
            public void Test1()
            {
                int count = 5;
                string label = "Colors used in the map";
                var pair = (count: count, label: label);
            }
        }

        /*Pattern matching on generic type parameters*/
        /*********/
        /*
         * checking types that may be either struct or class types
         * pattern expression for 'is' and the 'switch' type pattern
         */
        public partial class Student
        {
            public class A {}
            public struct B { }

            public void Method<T>(T param)
            {
                switch (param)
                {
                    case A a:
                        Console.WriteLine("A");
                        break;
                    case B b:
                        Console.WriteLine("B");
                        break;
                }
            }
        }

        /*Reference assembly generation*/
        /*********/
        /*
         * There are two new compiler options that generate reference-only assemblies: -refout and -refonly
         * The -refout option specifies a file path where the reference assembly should be output. 
         * The -refonly option indicates that a reference assembly should be output instead of an implementation assembly, as the primary output. 
         */
    }
}
