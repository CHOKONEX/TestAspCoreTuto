using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Net.Core.Features.Test.Modules.TestCsharpVersions
{
    /*C# 7.3*/
    /*********/
    /*
     * Enabling more efficient safe code
     * ref local variables may be reassigned
     * stackalloc arrays support initializers
     * Enhanced generic constraints unmanaged 
     * Make existing features better
    */
    public static class TestCsharp73Module
    {
        /*Enabling more efficient safe code*/
        /*********/
        /*
         * The fixed statement prevents the garbage collector from relocating a movable variable. 
         * The fixed statement is only permitted in an unsafe context.
         * The fixed statement sets a pointer to a managed variable and "pins" that variable during the execution of the statement. 
         * 
         * vous pouvez utiliser l’instruction fixed pour créer une mémoire tampon avec un tableau de taille fixe dans une structure de données. 
         * Les mémoires tampons de taille fixe sont utiles quand vous écrivez des méthodes qui sont interopérables avec les sources de données d’autres langages ou plateformes.
         * 
         * unsafe keyword : necessary to deal in pointers.
         * fixed has two uses:
            - it allows you to pin an array and obtain a pointer to the data
            - when used in an unsafe struct field, it declares a "fixed buffer" - a reserved block of space in a type that is accessed via pointers rather than regular fields
         */

        class Point
        {
            public int x;
            public int y;
        }

        unsafe private static void ModifyFixedStorage()
        {
            // Variable pt is a managed variable, subject to garbage collection.
            Point pt = new Point();

            // Using fixed allows the address of pt members to be taken,
            // and "pins" pt so that it is not relocated.

            fixed (int* p = &pt.x)
            {
                *p = 1;
            }

            Point point = new Point();
            double[] arr = { 0, 1.5, 2.3, 3.4, 4.0, 5.9 };
            string str = "Hello World";

            // The following two assignments are equivalent. Each assigns the address
            // of the first element in array arr to pointer p.

            // You can initialize a pointer by using an array.
            fixed (double* p = arr) { /*...*/ }

            // You can initialize a pointer by using the address of a variable.
            fixed (double* p = &arr[0]) { /*...*/ }

            // The following assignment initializes p by using a string.
            fixed (char* p = str) { /*...*/ }

            // The following assignment is not valid, because str[0] is a char,
            // which is a value, not a variable.
            //fixed (char* p = &str[0]) { /*...*/ }

            fixed (int* p1 = &point.x)
            {
                fixed (double* p2 = &arr[5])
                {
                    // Do something with p1 and p2.
                }
            }

            unsafe static string Transform()
            {
                // Get random string.
                string value = System.IO.Path.GetRandomFileName();

                // Use fixed statement on a char pointer.
                // ... The pointer now points to memory that won't be moved.
                fixed (char* pointer = value)
                {
                    // Add one to each of the characters.
                    for (int i = 0; pointer[i] != '\0'; ++i)
                    {
                        pointer[i]++;
                    }
                    // Return the mutated string.
                    return new string(pointer);
                }
            }
        }

        /*
         * In earlier versions of C#, you needed to pin a variable to access one of the integers that are part of myFixedField. 
         * Now, the following code compiles without pinning the variable p inside a separate fixed statement
         */
        public partial class Student
        {
            unsafe struct S
            {
                public fixed int myFixedField[10];
            }

            class C
            {
                static S s = new S();

                unsafe public void M()
                {
                    int p = s.myFixedField[5]; // indexing fixed-size array fields would be ok
                }

                //unsafe static void Main()
                //{
                //    int* ptr = s.myFixedField; // taking a pointer explicitly still requires pinning.
                //    int p = ptr[5];
                //}

                unsafe public void M1()
                {
                    fixed (int* ptr = s.myFixedField)
                    {
                        int p = ptr[5];
                    }
                }
            }
        }

        /*ref local variables may be reassigned*/
        /*********/
        public partial class Student
        {
            public void Method2()
            {
                VeryLargeStruct veryLargeStruct = null;
                ref VeryLargeStruct refLocal = ref veryLargeStruct; // initialization

                VeryLargeStruct anotherVeryLargeStruct = null;
                refLocal = ref anotherVeryLargeStruct; // reassigned, refLocal refers to different storage.
            }

            private class VeryLargeStruct
            {
            }
        }

        /*stackalloc arrays support initializers*/
        /*********/
        /*
         * A stackalloc expression allocates a block of memory on the stack. 
         * A stack allocated memory block created during the method execution is automatically discarded when that method returns. 
         * You cannot explicitly free the memory allocated with stackalloc. 
         * A stack allocated memory block is not subject to garbage collection and doesn't have to be pinned with a fixed statement.
         */
        public partial class Student
        {
            public void Test()
            {
                unsafe
                {
                    int* pArr = stackalloc int[3] { 1, 2, 3 };
                    int* pArr2 = stackalloc int[] { 1, 2, 3 };
                }
                //You don't have to use an unsafe context when you assign a stack allocated memory block to a Span<T> or ReadOnlySpan<T> variable.
                Span<int> arr = stackalloc[] { 1, 2, 3 };

                unsafe
                {
                    int length = 3;
                    int* numbers = stackalloc int[length];
                    for (var i = 0; i < length; i++)
                    {
                        numbers[i] = i;
                    }
                }
            }
        }


        /*Enhanced generic constraints unmanaged */
        /*********/
        /*
         * A type is an unmanaged type if it's any of the following types:
            sbyte, byte, short, ushort, int, uint, long, ulong, char, float, double, decimal, or bool
            Any enum type
            Any pointer type
            Any user-defined struct type that contains fields of unmanaged types only and, 
            in C# 7.3 and earlier, is not a constructed type (a type that includes at least one type argument)
         */
        public partial class Student
        {
            public struct Coords<T>
            {
                public T X;
                public T Y;
            }

            public static void Main()
            {
                DisplaySize<Coords<int>>();
                DisplaySize<Coords<double>>();
            }

            //generic method
            private unsafe static void DisplaySize<T>() where T : unmanaged
            {
                Console.WriteLine($"{typeof(T)} is unmanaged and its size is {sizeof(T)} bytes");
            }

            //generic constraints
            public struct Coords2<T> where T : unmanaged
            {
                public T X;
                public T Y;
            }
        }

        /*Make existing features better*/
        /*Tuples support == and !=*/
        /*********/
        public partial class Student
        {
            public void Test3()
            {
                var left = (a: 5, b: 10);
                var right = (a: 5, b: 10);
                Console.WriteLine(left == right); // displays 'true'
            }
        }

        /*Make existing features better*/
        /*Attach attributes to the backing fields for auto-implemented properties*/
        /*********/
        public partial class Student
        {
            [field: SomeThingAboutField]
            public int SomeProperty { get; set; }

            // default: applies to method
            [ValidatedContract]
            int Method1() { return 0; }

            // applies to method
            [method: ValidatedContract]
            int Method5() { return 0; }

            // applies to return value
            [return: ValidatedContract]
            int Method3() { return 0; }

            private class SomeThingAboutFieldAttribute : Attribute
            {
            }

            private class ValidatedContractAttribute : Attribute
            {
            }
        }

        /*Make existing features better*/
        /*in method overload resolution tiebreaker*/
        /*********/
        public partial class Student
        {
            //// Compiler error CS0663: "Cannot define overloaded
            //// methods that differ only on in, ref and out".
            //public void SampleMethod(in int i) { }
            //public void SampleMethod(ref int i) { }

            public void SampleMethod(in int i) { }
            public void SampleMethod(int i) { }

            static void Method(int argument)
            {
                // implementation removed
            }

            static void Method(in int argument)
            {
                // implementation removed
            }

            static void M(S arg)
            {
            }

            static void M(in S arg)
            {
            }
        }

        /*Make existing features better*/
        /*Extend expression variables in initializers*/
        /*********/
        /*
         * The syntax added in C# 7.0 to allow out variable declarations has been extended 
         * to include field initializers, property initializers, constructor initializers, and query clauses
         */
        public partial class Student
        {
            public class B
            {
                public B(int i, out int j)
                {
                    j = i;
                }
            }

            public class D : B
            {
                public D(int i) : base(i, out var j)
                {
                    Console.WriteLine($"The value of 'j' is {j}");
                }
            }
        }
    }
}
