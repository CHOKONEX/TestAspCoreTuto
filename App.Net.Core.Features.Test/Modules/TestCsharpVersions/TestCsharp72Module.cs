using System;

namespace App.Net.Core.Features.Test.Modules.TestCsharpVersions
{
    /*C# 7.2*/
    /*********/
    /*
     * Safe efficient code enhancements
     * Non-trailing named arguments => (3, b: 4, c: 5)
     * Leading underscores in numeric literals
     * private protected access modifier
     * Conditional ref expressions => ref var r = ref (arr != null ? ref arr[0] : ref otherArr[0])
    */
    public static class TestCsharp72Module
    {
        /*Safe efficient code enhancements*/
        /*********/
        /*
         * in
         * ref readonly
         * readonly struct
         * 
            The in modifier on parameters, to specify that an argument is passed by reference but not modified by the called method. Adding the in modifier to an argument is a source compatible change.
            The ref readonly modifier on method returns, to indicate that a method returns its value by reference but doesn't allow writes to that object. Adding the ref readonly modifier is a source compatible change, if the return is assigned to a value. Adding the readonly modifier to an existing ref return statement is an incompatible change. It requires callers to update the declaration of ref local variables to include the readonly modifier.
            The readonly struct declaration, to indicate that a struct is immutable and should be passed as an in parameter to its member methods. Adding the readonly modifier to an existing struct declaration is a binary compatible change.
            The ref struct declaration, to indicate that a struct type accesses managed memory directly and must always be stack allocated. Adding the ref modifier to an existing struct declaration is an incompatible change. A ref struct cannot be a member of a class or used in other locations where it may be allocated on the heap.
         */

        /*
         Declaring a struct using the readonly modifier informs the compiler that your intent is to create an immutable type. 
         The compiler enforces that design decision with the following rules:
            All field members must be readonly
            All properties must be read-only, including auto-implemented properties.
        */
        readonly public struct ReadonlyPoint3D
        {
            public ReadonlyPoint3D(double x, double y, double z)
            {
                X = x;
                Y = y;
                Z = z;
            }

            public double X { get; }
            public double Y { get; }
            public double Z { get; }
        }

        /*Declare readonly members when a struct can't be immutable*/
        /***/
        public struct Point3D
        {
            public Point3D(double x)
            {
                _x = x;
            }

            private double _x;
            public double X
            {
                readonly get => _x;
                set => _x = value;
            }

            public readonly double Distance => Math.Sqrt(X * X);

            public readonly override string ToString() => $"{X}";
        }

        /*
         * You can return values by reference when the value being returned isn't local to the returning method. 
         * Returning by reference means that only the reference is copied, not the structure. 
         */
        /*Use ref readonly return statements for large structures when possible*/
        /****/
        public struct Point3D1
        {
            private static Point3D origin = new Point3D(0);

            // Dangerous! returning a mutable reference to internal storage
            public ref Point3D Origin => ref origin;

            // other members removed for space
        }

        /*You don't want callers modifying the origin, so you should return the value by ref readonly*/
        public struct Point3D2
        {
            private static Point3D origin = new Point3D(0);

            public static ref readonly Point3D Origin => ref origin;

            // other members removed for space
        }

        /*
         * The in keyword specifies passing the argument by reference, but the called method doesn't modify the value.
         * 
            out: This method sets the value of the argument used as this parameter.
            ref: This method may set the value of the argument used as this parameter.
            in: This method doesn't modify the value of the argument used as this parameter.
        */
        /*
         * default : Avoid mutable structs as an in argument
         */
        private static double CalculateDistance(in Point3D point1, in Point3D point2 = default)
        {
            double xDifference = point1.X - point2.X;

            return Math.Sqrt(xDifference * xDifference);
        }

        //An overload exists that differs by the presence or absence of in. In that case, the by value overload is a better match.
        public static void Test5()
        {
            Point3D pt1 = new Point3D();
            Point3D pt2 = new Point3D();
            var distance = CalculateDistance(in pt1, in pt2);
            distance = CalculateDistance(in pt1, new Point3D());
            distance = CalculateDistance(pt1, in Point3D2.Origin);
        }

        /*Non-trailing named arguments*/
        /*********/
        /*
         it is now allowed to have named arguments also after positional ones.
         */
        public partial class Student
        {
            public void Test()
            {
                var v = Volume(a: 3, c: 5, b: 4);
                v = Volume(3, b: 4, c: 5);
                v = Volume(3, b: 4, 5);
                //v = Volume(3, c: 5, 4); //INCORRECT
            }

            public int Volume(int a, int b, int c)
            {
                return a * b * c;
            }
        }

        /*Leading underscores in numeric literals*/
        /*********/
        public partial class Student
        {
            int binaryValue = 0b_0101_0101;
        }

        /*private protected access modifier*/
        /*********/
        /*
         * private protected  : indicates that a member may be accessed by containing class or derived classes that are declared in the same assembly
         * protected internal : allows access by derived classes or classes that are in the same assembly
         */
        public partial class Student
        {
            public class BaseClass
            {
                private protected int myValue = 0;
                internal protected int ProtectedInternalValue = 0;
            }

            public class DerivedClass1 : BaseClass
            {
                void Access()
                {
                    var baseObject = new BaseClass();

                    // Error CS1540, because myValue can only be accessed by
                    // classes derived from BaseClass.
                    // baseObject.myValue = 5;

                    // OK, accessed through the current derived class instance
                    myValue = 5;
                }
            }


            public class ProtectedInternalClass
            {
                void Access()
                {
                    var baseObject = new BaseClass();
                    //allows access by derived classes or classes that are in the same assembly
                    baseObject.ProtectedInternalValue = 5;
                }
            }
        }

        /*Conditional ref expressions*/
        /*********/
        /*
         * the conditional expression may produce a ref result instead of a value result. 
         */
        public partial class Student
        {
            public void Test3(object[] arr, object[] otherArr)
            {
                ref var r = ref (arr != null ? ref arr[0] : ref otherArr[0]);
            }
        }
    }
}
