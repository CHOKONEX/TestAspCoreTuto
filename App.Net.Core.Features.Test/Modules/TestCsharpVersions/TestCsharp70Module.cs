using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Net.Core.Features.Test.Modules.TestCsharpVersions
{
    /*C# 7.0*/
    /*********/
    /*
     * out variables
     * Tuples were first introduced as a part of .NET Framework 4.0.
     * Discards
     * Pattern matching "input is int count"
     * Ref locals and returns
     * Local functions
     * More expression-bodied members
     * Throw expressions (constructor ?? throw)
     * Generalized async return types (async ValueTask)
     * Numeric literal syntax improvements
    */
    public static class TestCsharp70Module
    {
        /*out variables*/
        /*********/
        public partial class Student
        {
            public void Test()
            {
                string input = "1";
                if (int.TryParse(input, out int result))
                    Console.WriteLine(result);
                else
                    Console.WriteLine("Could not parse input");
            }
        }

        /*Tuples*/
        /*********/
        public partial class Student
        {
            public void Test2()
            {
                (string Alpha, string Beta) namedLetters = ("a", "b");
                Console.WriteLine($"{namedLetters.Alpha}, {namedLetters.Beta}");

                var alphabetStart = (Alpha: "a", Beta: "b");
                Console.WriteLine($"{alphabetStart.Alpha}, {alphabetStart.Beta}");

                var p = new Point(3.14, 2.71);
                (double X, double Y) = p;
            }

            public class Point
            {
                public Point(double x, double y) => (X, Y) = (x, y);

                public double X { get; }
                public double Y { get; }

                public void Deconstruct(out double x, out double y) => (x, y) = (X, Y);
            }
        }

        /*Discards*/
        /*********/
        /*
         * Often when deconstructing a tuple or calling a method with out parameters, 
         * you're forced to define a variable whose value you don't care about and don't intend to use.
         */
        public partial class Student
        {
            public void Test3()
            {
                var (_, _, _, pop1, _, pop2) = QueryCityDataForYears("New York City", 1960, 2010);

                Console.WriteLine($"Population change, 1960 to 2010: {pop2 - pop1:N0}");
            }

            private static (string, double, int, int, int, int) QueryCityDataForYears(string name, int year1, int year2)
            {
                int population1 = 0, population2 = 0;
                double area = 0;

                if (name == "New York City")
                {
                    area = 468.48;
                    if (year1 == 1960)
                    {
                        population1 = 7781984;
                    }
                    if (year2 == 2010)
                    {
                        population2 = 8175133;
                    }
                    return (name, area, year1, population1, year2, population2);
                }

                return ("", 0, 0, 0, 0, 0);
            }
        }

        /*Pattern matching The is pattern expression*/
        /*********/
        public partial class Student
        {
            public void Test4()
            {
                int input = 1;
                int sum = 0;
                if (input is int count)
                    sum += count;
            }

            public static int SumPositiveNumbers(IEnumerable<object> sequence)
            {
                int sum = 0;
                foreach (var i in sequence)
                {
                    switch (i)
                    {
                        case 0:
                            break;
                        case IEnumerable<int> childSequence:
                            {
                                foreach (var item in childSequence)
                                    sum += (item > 0) ? item : 0;
                                break;
                            }
                        case int n when n > 0: //case int n when n > 0: is a type pattern with an additional when condition.
                            sum += n;
                            break;
                        case null:
                            throw new NullReferenceException("Null found in sequence");
                        default:
                            throw new InvalidOperationException("Unrecognized type");
                    }
                }
                return sum;
            }
        }

        /*Ref locals and returns*/
        /*********/
        /*
         This feature enables algorithms that use and return references to variables defined elsewhere.
         ref locals and returns can't be used with async methods
         */
        public partial class Student
        {
            public void Test5()
            {
                int[,] matrix = null;
                ref var item = ref Find(matrix, (val) => val == 42);
                Console.WriteLine(item);
                item = 24;
                Console.WriteLine(matrix[4, 2]);
            }

            public static ref int Find(int[,] matrix, Func<int, bool> predicate)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                    for (int j = 0; j < matrix.GetLength(1); j++)
                        if (predicate(matrix[i, j]))
                            return ref matrix[i, j];
                throw new InvalidOperationException("Not found");
            }
        }

        /*Local functions*/
        /*********/
        /*
         * local functions can capture variables from enclosing block
         * Local functions can not be static
         * it can be asynchronous, it can be generic, it can be dynamic.
         * allowed to use async and unsafe modifiers.
         * not allowed to apply attributes to the local function, or to its parameters, or to its parameter type.
         * Multiple local functions are allowed.
         * no public/private : default is private
         * not allowed to use any member access modifiers in the local function definition, including private keyword because they are by default private and you are not allowed to make them public
         * transformed by compiler to regular private static method
         * => Improving C# Lambda Code Readability with Local Functions
        */
        public partial class Student
        {
            public static IEnumerable<char> AlphabetSubset3(char start, char end)
            {
                if (start < 'a' || start > 'z')
                    throw new ArgumentOutOfRangeException(paramName: nameof(start), message: "start must be a letter");
                if (end < 'a' || end > 'z')
                    throw new ArgumentOutOfRangeException(paramName: nameof(end), message: "end must be a letter");

                if (end <= start)
                    throw new ArgumentException($"{nameof(end)} must be greater than {nameof(start)}");

                return alphabetSubsetImplementation();

                IEnumerable<char> alphabetSubsetImplementation()
                {
                    for (var c = start; c < end; c++)
                        yield return c;
                }
            }

            public Task<string> PerformLongRunningWork(string address, int index, string name)
            {
                if (string.IsNullOrWhiteSpace(address))
                    throw new ArgumentException(message: "An address is required", paramName: nameof(address));
                if (index < 0)
                    throw new ArgumentOutOfRangeException(paramName: nameof(index), message: "The index must be non-negative");
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentException(message: "You must supply a name", paramName: nameof(name));

                return longRunningWorkImplementation();

                async Task<string> longRunningWorkImplementation()
                {
                    var interimResult = await FirstWork(address);
                    var secondResult = await SecondStep(index, name);
                    return $"The results are {interimResult} and {secondResult}. Enjoy.";
                }
            }

            private async Task<object> SecondStep(int index, string name)
            {
                throw new NotImplementedException();
            }

            private async Task<object> FirstWork(string address)
            {
                throw new NotImplementedException();
            }

            //Local functions VS lambda expressions
            /*
             * benefits of local functions:
                - A lambda causes allocation.
                - There's no elegant way of writing a recursive lambda.
                - They can't use yield return and probably some other things.
            */
            public static int LocalFunctionFactorial(int n)
            {
                return nthFactorial(n);

                int nthFactorial(int number) => (number < 2) ?
                    1 : number * nthFactorial(number - 1);
            }

            public static int LambdaFactorial(int n)
            {
                Func<int, int> nthFactorial = default(Func<int, int>);
                nthFactorial = (number) => (number < 2) ?
                    1 : number * nthFactorial(number - 1);

                return nthFactorial(n);
            }
        }

        /*More expression-bodied members*/
        /*********/
        public partial class Student
        {
            // Expression-bodied get / set accessors.
            private string label;
            public string Label
            {
                get => label;
                set => label = value ?? "Default label";
            }

            // Expression-bodied constructor
            public Student(string label) => this.Label = label;

            // Expression-bodied finalizer
            ~Student() => Console.Error.WriteLine("Finalized!");
        }

        /*More expression-bodied members*/
        /*********/
        public partial class Student
        {
            private static void DisplayFirstNumber(string[] args)
            {
                string arg = args.Length >= 1 ? args[0] : throw new ArgumentException("You must supply an argument");
                if (Int64.TryParse(arg, out var number))
                    Console.WriteLine($"You entered {number:F0}");
                else
                    Console.WriteLine($"{arg} is not a number.");
            }
        }

        /*Generalized async return types*/
        /*********/
        /*
         * Returning a Task object from async methods can introduce performance bottlenecks in certain paths. 
         * Task is a reference type, so using it means allocating an object. 
         * In cases where a method declared with the async modifier returns a cached result, or completes synchronously, 
         * the extra allocations can become a significant time cost in performance critical sections of code. 
         * It can become costly if those allocations occur in tight loops.
        */
        public partial class Student
        {
            public async ValueTask<int> Func()
            {
                await Task.Delay(100);
                return 5;
            }
        }

        /*Generalized async return types*/
        /*********/
        /*
         * Returning a Task object from async methods can introduce performance bottlenecks in certain paths. 
         * Task is a reference type, so using it means allocating an object. 
         * In cases where a method declared with the async modifier returns a cached result, or completes synchronously, 
         * the extra allocations can become a significant time cost in performance critical sections of code. 
         * It can become costly if those allocations occur in tight loops.
        */
        public partial class Student
        {
            public const int Sixteen = 0b0001_0000;
            public const int ThirtyTwo = 0b0010_0000;
            public const int SixtyFour = 0b0100_0000;
            public const int OneHundredTwentyEight = 0b1000_0000;

            public const double AvogadroConstant = 6.022_140_857_747_474e23;
            public const decimal GoldenRatio = 1.618_033_988_749_894_848_204_586_834_365_638_117_720_309_179M;
        }
    }
}
