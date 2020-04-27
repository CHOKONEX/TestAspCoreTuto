using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace App.Net.Core.Features.Test.Modules.TestCsharpVersions.TestCSharp6
{
    /*C# 8.0 is supported on .NET Core 3.x and .NET Standard 2.1*/
    /*********/
    /*
     * Readonly members
     * Default interface methods
     * More patterns in more places 'switch'
     * Using declarations
     * Static local functions
     * Disposable ref structs
     * Nullable reference types
     * Asynchronous streams
     * Asynchronous disposable
     * Indices and ranges
     * Null-coalescing assignment
     * Unmanaged constructed types
     * Stackalloc in nested expressions
     * Enhancement of interpolated verbatim strings both $@"..." and @$"..." are valid interpolated verbatim strings. In earlier C# versions, the $ token must appear before the @ token.
    */
    public static class TestCsharp8Module
    {
        /*Readonly members*/
        /*********/
        /*
         * readonly modifier to declare that a structure type is immutable
         * readonly modifier to declare that an instance member doesn't modify the state of a struct. 
         * In a readonly struct, a data member of a mutable reference type still can mutate its own state. 
         * For example, you can't replace a List<T> instance, but you can add new elements to it.
         */

        #region members

        /*readonly struct*/
        /*
         * In a readonly struct, every instance member is implicitly readonly
         */
        public readonly struct Coords
        {
            public Coords(double x, double y)
            {
                X = x;
                Y = y;
            }

            public double X { get; }
            public double Y { get; }

            public override string ToString() => $"({X}, {Y})";
        }

        /*readonly instance members*/
        /*
         * use the readonly modifier to mark the instance members that don't modify the state of the struct. 
         * You can't apply the readonly modifier to static members of a structure type.
         */
        public struct Point
        {
            public double X { get; set; }
            public double Y { get; set; }
            public static double Z { get; set; }
            public readonly double Distance => Math.Sqrt(X * X + Y * Y);

            private int counter;
            public int Counter
            {
                readonly get => counter;
                set => counter = value;
            }

            public readonly double Sum()
            {
                return X + Y;
            }

            //Like most structs, the ToString() method doesn't modify state. 
            //You could indicate that by adding the readonly modifier to the declaration of ToString():
            public readonly override string ToString() => $"({X}, {Y}) is {Distance} from the origin";

            public readonly void Translate(int xOffset, int yOffset)
            {
                //X += xOffset;
                //Y += yOffset;
            }
        }

        /*
         * Limitations with the design of a structure type
            When you design a structure type, you have the same capabilities as with a class type, with the following exceptions:

            You can't declare a parameterless constructor. Every structure type already provides an implicit parameterless constructor that produces the default value of the type.

            You can't initialize an instance field or property at its declaration. However, you can initialize a static or const field or a static property at its declaration.

            A constructor of a structure type must initialize all instance fields of the type.

            A structure type can't inherit from other class or structure type and it can't be the base of a class. However, a structure type can implement interfaces.

            You can't declare a finalizer within a structure type.
         */

        #endregion

        /*Default interface methods*/
        /*********/
        /*
         * Interfaces can now have the default implementation of methods.
         * Possibilities of private / public 
         * static members.
         * protected members
         * virtual members, but the class can’t override the method. An interface can only override it.
         */

        #region Default interface methods

        public partial class Student
        {
            public interface IBook
            {
                private static string commandName = "LOCK_CAR";

                void AddBook(string bookName, string autherName);
                void removeBook(string bookName);
                void rateBook(int bookID)
                {
                    //default logic here    
                    Console.WriteLine("\nExecuted the Default implementation in the interface");
                }
                private void DisplayBook()
                {
                    //default logic here    
                    Console.WriteLine(commandName);
                }
                public void TestOtherMethodsFromInterface()
                {
                    rateBook(1);
                    DisplayBook();
                }
                protected void SendCriticalCommand()
                {
                    Console.WriteLine("Critical Command Sent via Interface");
                }
                virtual void SendCommand()
                {
                    Console.WriteLine("Command Sent via Interface");
                }
            }

            public interface IAnotherBook : IBook
            {
                void IBook.SendCommand()
                {
                    Console.WriteLine("Command Sent via another Interface");
                }

                public void Send(bool bCritical)
                {
                    if (bCritical)
                        this.SendCriticalCommand();
                    else
                        Console.WriteLine("Command Sent via Morris Garage Class");
                }
            }

            public class Book : IBook, IAnotherBook
            {
                public void AddBook(string bookName, string autherName)
                {
                    Console.WriteLine("Book {0} added!", bookName);
                }

                public void removeBook(string bookName)
                {
                    Console.WriteLine("Book {0} Removed!", bookName);
                }

                //Override
                public void rateBook(int bookID)
                {
                    Console.WriteLine("\nOverride : Executed the implementation in the class");
                    //DisplayBook(); => NOT FOUND
                }
            }

            static void Main()
            {
                IBook ib = new Book();
                ib.AddBook("Wings of Fire", "Dr.A.P.J Abdul Kalam");
                ib.removeBook("Belated Bachelor Party");

                ib.rateBook(1);

                IBook mg = new Book();
                mg.SendCommand(); //Calls the virtual implementation.
                IAnotherBook mgOverridden = new Book();
                mgOverridden.SendCommand(); //Calls the overridden implementation.
            }
        }

        #endregion

        #region More patterns in more places

        /*More patterns in more places*/
        /*********/
        public partial class Student
        {
            public enum Rainbow
            {
                Red,
                Orange,
                Yellow,
                Green,
                Blue,
                Indigo,
                Violet
            }

            public class RGBColor
            {
                private int v1;
                private int v2;
                private int v3;

                public RGBColor(int v1, int v2, int v3)
                {
                    this.v1 = v1;
                    this.v2 = v2;
                    this.v3 = v3;
                }
            }

            /*Switch expressions*/
            public static RGBColor FromRainbow(Rainbow colorBand) =>
                colorBand switch
                {
                    //case Rainbow.Red:  return new RGBColor(0xFF, 0x00, 0x00);
                    Rainbow.Red => new RGBColor(0xFF, 0x00, 0x00),
                    Rainbow.Orange => new RGBColor(0xFF, 0x7F, 0x00),
                    Rainbow.Yellow => new RGBColor(0xFF, 0xFF, 0x00),
                    Rainbow.Green => new RGBColor(0x00, 0xFF, 0x00),
                    Rainbow.Blue => new RGBColor(0x00, 0x00, 0xFF),
                    Rainbow.Indigo => new RGBColor(0x4B, 0x00, 0x82),
                    Rainbow.Violet => new RGBColor(0x94, 0x00, 0xD3),
                    //default: throw new ArgumentException(message: "invalid enum value", paramName: nameof(colorBand));
                    _ => throw new ArgumentException(message: "invalid enum value", paramName: nameof(colorBand)),
                };

            /*Property patterns*/
            public class Address
            {
                public string State { get; internal set; }
            }

            public static decimal ComputeSalesTax(Address location, decimal salePrice) => location switch
            {
                { State: "WA" } => salePrice * 0.06M,
                { State: "MN" } => salePrice * 0.75M,
                { State: "MI" } => salePrice * 0.05M,
                _ => 0M
            };

            /*Tuple patterns*/
            public static string RockPaperScissors(string first, string second) => (first, second) switch
            {
                ("rock", "paper") => "rock is covered by paper. Paper wins.",
                ("rock", "scissors") => "rock breaks scissors. Rock wins.",
                ("paper", "rock") => "paper covers rock. Paper wins.",
                ("paper", "scissors") => "paper is cut by scissors. Scissors wins.",
                ("scissors", "rock") => "scissors is broken by rock. Rock wins.",
                ("scissors", "paper") => "scissors cuts paper. Scissors wins.",
                (_, _) => "tie"
            };

            /*Positional patterns*/
            public class Point
            {
                public int X { get; }
                public int Y { get; }

                public Point(int x, int y) => (X, Y) = (x, y);

                public void Deconstruct(out int x, out int y) => (x, y) = (X, Y);
            }

            public enum Quadrant
            {
                Unknown,
                Origin,
                One,
                Two,
                Three,
                Four,
                OnBorder
            }

            static Quadrant GetQuadrant(Point point) => point switch
            {
                (0, 0) => Quadrant.Origin,
                var (x, y) when x > 0 && y > 0 => Quadrant.One,
                var (x, y) when x < 0 && y > 0 => Quadrant.Two,
                var (x, y) when x < 0 && y < 0 => Quadrant.Three,
                var (x, y) when x > 0 && y < 0 => Quadrant.Four,
                var (_, _) => Quadrant.OnBorder,
                _ => Quadrant.Unknown
            };
        }

        #endregion

        /*Readonly members*/
        /*********/
        /*
         * using declaration is a variable declaration preceded by the using keyword. 
         * It tells the compiler that the variable being declared should be disposed at the end of the enclosing scope
         */

        //static int WriteLinesToFile(IEnumerable<string> lines)
        //{
        //    // We must declare the variable outside of the using block
        //    // so that it is in scope to be returned.
        //    int skippedLines = 0;
        //    using (var file = new System.IO.StreamWriter("WriteLines2.txt"))
        //    {
        //        foreach (string line in lines)
        //        {
        //            if (!line.Contains("Second"))
        //            {
        //                file.WriteLine(line);
        //            }
        //            else
        //            {
        //                skippedLines++;
        //            }
        //        }
        //    } // file is disposed here
        //    return skippedLines;
        //}

        static int WriteLinesToFile(IEnumerable<string> lines)
        {
            using var file = new System.IO.StreamWriter("WriteLines2.txt");
            // Notice how we declare skippedLines after the using statement.
            int skippedLines = 0;
            foreach (string line in lines)
            {
                if (!line.Contains("Second"))
                {
                    file.WriteLine(line);
                }
                else
                {
                    skippedLines++;
                }
            }
            // Notice how skippedLines is in scope here.
            return skippedLines;
            // file is disposed here
        }

        /*Static local functions*/
        /*********/
        public partial class Student
        {
            int M()
            {
                int y = 5;
                int x = 7;
                return Add(x, y);

                static int Add(int left, int right) => left + right;
            }
        }

        /*Disposable ref structs*/
        /*********/
        public partial class Student
        {
            ref struct Book1
            {
                public void Dispose()
                {
                }
            }

            static void Test1()
            {
                using (var book = new Book1())
                {
                    // ...
                }
            }
            static void Test2()
            {
                using var book = new Book1();
            }

            /*ref structs the cleanup choice is also limited to explicit cleanup as ref structs cannot have finalizers defined.*/
            public unsafe ref struct UnmanagedArray<T> where T : unmanaged
            {
                private T* data;
                public UnmanagedArray(int length)
                {
                    data = null;// get memory from some pool
                }

                public ref T this[int index]
                {
                    get { return ref data[index]; }
                }

                public void Dispose()
                {
                    // return memory to the pool
                }
            }

            static void Test3()
            {
                var array = new UnmanagedArray<int>(10);
                Console.WriteLine(array[0]);
                array.Dispose();
            }
        }

        /*Nullable reference types*/
        /*********/
        #region Nullable reference types

        /*
         * A reference isn't supposed to be null. When variables aren't supposed to be null, 
         * the compiler enforces rules that ensure it's safe to dereference these variables without first checking that it isn't null:
            The variable must be initialized to a non-null value.
            The variable can never be assigned the value null.
         * 
         * A reference may be null. When variables may be null, 
         * the compiler enforces different rules to ensure that you've correctly checked for a null reference:
                The variable may only be dereferenced when the compiler can guarantee that the value isn't null.
                These variables may be initialized with the default null value and may be assigned the value null in other code.

        * nullable reference type ?
        * to enable Nullable Reference Types : add <Nullable>enable</Nullable>
        * <Project Sdk="Microsoft.NET.Sdk">
              <PropertyGroup>
                <OutputType>Exe</OutputType>
                <TargetFramework>netcoreapp3.0</TargetFramework>
                <LangVersion>8.0</LangVersion>
                <Nullable>enable</Nullable>
              </PropertyGroup>
            </Project>
        * To enable per file, you can use #nullable enable
        */
        public partial class Student
        {
            static void Main(string[] args)
            {
                string s = (args.Length > 0) ? args[0] : null;
                Console.WriteLine(s.Length);
            }

#nullable disable
            public static void A()
            {
                Version.TryParse("1.0.0", out var version);
                Console.WriteLine(version.Major);
            }
#nullable enable //Nullable tells C# to assume that all declared reference types are non-null by default.
            public static void B()
            {
                Version.TryParse("1.0.0", out var version);
                Console.WriteLine(version.Major);
            }

            /*
             * By default everything is non nullable. 
             * If you want to declare a type as accepting null values, you need to add ? after the type. It is very similar to Nullable<T> (e.g. int?, bool?).
             */
            void Sample()
            {
                string str1 = null; // warning CS8625: Cannot convert null literal to non-nullable reference type.
                string? str2 = null; // ok

                Console.WriteLine(str1);
                Console.WriteLine(str2);

                ValueCannotBeNull(null); // warning CS8625: Cannot convert null literal to non-nullable reference type

                string? value1 = null;
                ValueCannotBeNull(value1); // warning CS8625: Cannot convert null literal to non-nullable reference type

                string? value2 = "test";
                ValueCannotBeNull(value2); // ok, while the type of value2 is string?, the compiler understands the value cannot be null here

                string value3 = "test";
                ValueCannotBeNull(value3); // ok
            }

            // value cannot be null
            public static void ValueCannotBeNull(string value)
            {
                _ = value.Length; // ok
            }

            // value can be null
            public static void ValueMayBeNull(string? value)
            {
                _ = value.Length; // warning CS8602: Dereference of a possibly null reference
            }

            /*Sometimes you know better than the compiler something is not null*/
            /*in this case we know that value is not null, so we can use "!" to instruct the compiler "value" is not null here*/
            static void Main2()
            {
                var value = GetValue(true);
                Console.WriteLine(value!.Length); // ! value is not null
            }

            static string? GetValue(bool returnNotNullValue)
            {
                return returnNotNullValue ? "" : null;
            }

            /*force null when something is not accepting null*/
            static void Main3()
            {
                string a = null;     // warning
                string b = null!;    // ok
                string c = default!; // ok
            }

            /*Generic types*/
            public static void ReferenceType<T>(T? value) where T : class
            {
            }

            public static void Test2<T>(T? value) where T : struct
            {
            }

            /*Preconditions attributes: AllowNull / DisallowNull*/
            private string _value = "";

            [AllowNull]
            public string Value
            {
                get => _value;
                set => _value = value ?? "";
            }

            private string? _value2 = null;

            [DisallowNull]
            public string? Value2
            {
                get => _value2;
                set => _value2 = value ?? throw new ArgumentNullException(nameof(value));
            }

            void Demo()
            {
                Value = null; // ok thanks to [DisallowNull]
                _ = Value.Length; // Currenty there is a warning but this should be fixed in the future version of the compiler

                Value2 = null; // not ok because of [DisallowNull]
                _ = Value2.Length; // not ok as Value may be null
            }

            public void DemoRef([DisallowNull]ref string? value)
            {
                value = null;
            }

            void A1()
            {
                string? value = null;
                DemoRef(ref value); // warning value is null but the method doesn't allow null as input
                _ = value.Length;   // warning value is null as the method may change the value of the ref parameter to null
            }

            /*Post-condition attributes: NotNull / MaybeNull*/
            /*
             * MaybeNull : The return value may be null if T is a reference type and no item matches the condition.
             * NotNull :   value may be null on input, but is non-null when the method returns
             */
            public class Person<T>
            {
                [return: MaybeNull] //
                public static T FirstOrDefault<T>(IEnumerable<T> items, Func<T, bool> match)
                {
                    return default(T);
                }

                public static void SetValue<T>([NotNull] ref T value)
                {
                    //
                }

                public static void AssertNotNull<T>([NotNull]T? obj) where T : class
                {
                    if (obj == null)
                        throw new Exception("Value is null");
                }

                void Sample()
                {
                    string? test = null;
                    AssertNotNull(test);
                    _ = test.ToString(); // test is not null here
                }
            }

            /*Conditional post-conditions attributes: NotNullWhen / MayBeNullWhen / NotNullIfNotNull*/
            // result may be null if TryGetValue returns false
            public bool TryGetValue(string key, [MaybeNullWhen(returnValue: false)]out Version result)
            {
                result = null;
                return false;
            }

            // result is not null if TryParse returns true
            public static bool TryParse(string? input, [NotNullWhen(returnValue: true)] out Version? result)
            {
                result = null;
                return false;
            }

            // The return value is not null if the value of the path parameter is not null
            // note that the name of the parameter is not validated, be careful about typo
            [return: NotNullIfNotNull(parameterName: "path")]
            public static string? ChangeExtension(string? path, string? extension)
            {
                return null;
            }

            void Sample3()
            {
                if (TryParse("", out var version))
                {
                    _ = version.Major; // ok, version is not null as TryParse returns true
                }

                bool parsed = TryParse("", out var version1);
                if (parsed)
                {
                    _ = version1.Major; // warning, the compiler is not able to understand that the method returns true here
                }
            }

            /*Flow attributes: DoesNotReturn / DoesNotReturnIf*/
            // This methods never returns
            [DoesNotReturn]
            public static void ThrowArgumentNullException(string argumentName)
            {
                throw new ArgumentNullException(argumentName);
            }

            // This method does not return if condition is false
            public static void MyAssert([DoesNotReturnIf(false)] bool condition)
            {
                if (!condition)
                    throw new Exception("condition is false");
            }

            /*
             * Generic constraint notnull
             * The notnull constraint allows to prevent a generic type to be nullable. 
             * For instance, TKey in Dictionary<TKey, TValue> cannot be null. 
             * If you try to add a value with key null, it throws an ArgumentNullException. 
             * To prevent using a nullable type as key, the generic parameter use the notnull constraint.
             * 
             */
            //public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary, IReadOnlyDictionary<TKey, TValue>, ISerializable, IDeserializationCallback
            //    where TKey : notnull
            //{
            //}


            /*Handling constructors for deserialization or framework such as AutoMapper*/
            public class Post
            {
                public string Title { get; set; } // warning CS8618 Non-nullable property 'Title' is uninitialized
                public string Uri { get; set; }   // warning CS8618 Non-nullable property 'Uri' is uninitialized

                //You can add a constructor and initialize the properties to remove the warnings:
                //public Post(string title, string uri)
                //{
                //    Title = title;
                //    Uri = uri;
                //}

                public string Title1 { get; set; } = default!; // ok, as the property is initialized
                public string Uri1 { get; set; } = default!;
            }

            /*TryParse/TryGetValue pattern and generics*/
            /*
             Using TryParseXXX or TryGetXXX methods is a very common pattern. 
             As seen before, you can use the [NotNullWhen(true)] attribute to indicate that the value of the out parameter is not null when the result of the method is true.
             However, in the case where the method returns false, you want to assign the out parameter with the default value. 
             Currently, the compiler emits a warning when you do that. 
             The workaround is to use the null-forgiving operator. 
             */
            public static bool TryParse<T>(string? input, [MaybeNullWhen(returnValue: false)] out T result)
            {
                if (input != null)
                {
                    result = Activator.CreateInstance<T>();
                    return true;
                }

                // The null-forgiving operator is mandatory here. This may be fixed in a future version of the compiler.
                result = default!;
                return false;
            }

            /*Adding nullable annotations to an existing code base*/
            /*
                I think that the opt-out strategy is easier to manage as it quickly allows to see the number of types that needs to be annotated.

                Change the project configuration to use C# 8 and enable nullable analysis

                Add #nullable disable at the top of each .cs file. You can use the following PowerShell script to do it automatically:

                Get-ChildItem -Recurse -Filter *.cs | ForEach-Object {
                "#nullable disable`n" + (Get-Content $_ -Raw) | Set-Content $_
                }
                For each file remove the directive, add the nullable annotations and fix warnings. Start with the files that have the least dependencies. This way the new warnings should only appear in the file where you remove the directive.

                You have finished when there are no more #nullable disable in your code!
             */
        }

        #endregion

        /*Asynchronous streams*/
        /*********/
        /*
         * It's declared with the async modifier.
         * It returns an IAsyncEnumerable<T>.
         * The method contains yield return statements to return successive elements in the asynchronous stream.
         */
        public partial class Student
        {
            public static async IAsyncEnumerable<int> GenerateSequence()
            {
                for (int i = 0; i < 20; i++)
                {
                    await Task.Delay(100);
                    yield return i;
                }
            }

            public async Task Test()
            {
                await foreach (var number in GenerateSequence())
                {
                    Console.WriteLine(number);
                }
            }
        }

        /*Asynchronous disposable*/
        /*********/
        /*
         * When your class owns unmanaged resources and releasing them requires a resource-intensive I/O operation, 
         * such as flushing the contents of an intermediate buffer into a file or sending a packet over a network to close a connection.
         * 
         * The IAsyncDisposable.DisposeAsync method of this interface returns a ValueTask that represents the asynchronous dispose operation.
         */
        public partial class Student
        {
            public class Foo : IAsyncDisposable
            {
                public async ValueTask DisposeAsync()
                {
                    Console.WriteLine("Delaying!");
                    await Task.Delay(1000);
                    Console.WriteLine("Disposed!");
                }
            }
        }

        /*Indices and ranges*/
        /*********/
        /*
         * System.Index represents an index into a sequence.
            The index from end operator ^, which specifies that an index is relative to the end of the sequence.
          System.Range represents a sub range of a sequence.
            The range operator .., which specifies the start and end of a range as its operands.
         *
         * A range specifies the start and end of a range. The start of the range is inclusive, but the end of the range is exclusive
         */
        public partial class Student
        {
            string[] words = new string[]
            {
                            // index from start    index from end
                "The",      // 0                   ^9
                "quick",    // 1                   ^8
                "brown",    // 2                   ^7
                "fox",      // 3                   ^6
                "jumped",   // 4                   ^5
                "over",     // 5                   ^4
                "the",      // 6                   ^3
                "lazy",     // 7                   ^2
                "dog"       // 8                   ^1
            };              // 9 (or words.Length) ^0
        
            public void Test6()
            {
                Console.WriteLine($"The last word is {words[^1]}"); // writes "dog"

                //"quick", "brown", and "fox". 
                //It includes words[1] through words[3]. The element words[4] isn't in the range
                string[] quickBrownFox = words[1..4];

                var lazyDog = words[^2..^0]; //"lazy" and "dog"

                var allWords = words[..]; // contains "The" through "dog".
                var firstPhrase = words[..4]; // contains "The" through "fox"
                var lastPhrase = words[6..]; // contains "the", "lazy" and "dog"

                Range phrase = 1..4;
                var text = words[phrase]; //"quick", "brown", and "fox". 
            }
        }

        /*Null-coalescing assignment*/
        /*********/
        /*
         *  null-coalescing assignment operator ??=
         *  assign the value of its right-hand operand to its left-hand operand only if the left-hand operand evaluates to null
         */
        public partial class Student
        {
            public void Test7()
            {
                List<int> numbers = null;
                int? i = null;

                numbers ??= new List<int>();
                numbers.Add(i ??= 17);
                numbers.Add(i ??= 20);

                Console.WriteLine(string.Join(" ", numbers));  // output: 17 17
                Console.WriteLine(i);  // output: 17
            }
        }

        /*Unmanaged constructed types*/
        /*********/
        /*
         *  A type is an unmanaged type if it's any of the following types:
                sbyte, byte, short, ushort, int, uint, long, ulong, char, float, double, decimal, or bool
                Any enum type
                Any pointer type
                Any user-defined struct type that contains fields of unmanaged types only and, in C# 7.3 and earlier, is not a constructed type (a type that includes at least one type argument)
         *
         */
        public partial class Student
        {
            public struct Coords<T>
            {
                public T X;
                public T Y;
            }
            /*
             * A stackalloc expression allocates a block of memory on the stack. 
             * A stack allocated memory block created during the method execution is automatically discarded when that method returns. 
             * You cannot explicitly free the memory allocated with stackalloc. 
             * A stack allocated memory block is not subject to garbage collection and doesn't have to be pinned with a fixed statement.
             * You don't have to use an unsafe context when you assign a stack allocated memory block to a Span<T> or ReadOnlySpan<T> variable.
             */
            public void Test8()
            {
                /*
                 * Like for any unmanaged type, you can create a pointer to a variable of this type or allocate a
                 * block of memory on the stack for instances of this type
                 */
                Span<Coords<int>> coordinates = stackalloc[]
                {
                    new Coords<int> { X = 0, Y = 0 },
                    new Coords<int> { X = 0, Y = 3 },
                    new Coords<int> { X = 4, Y = 0 }
                };
            }
        }

        /*Stackalloc in nested expressions*/
        /*********/
        public partial class Student
        {
            public void Test9()
            {
                Span<int> numbers = stackalloc[] { 1, 2, 3, 4, 5, 6 };
                int ind = numbers.IndexOfAny(stackalloc[] { 2, 4, 6, 8 }); //nested
                Console.WriteLine(ind);  // output: 1
            }
        }
    }
}
