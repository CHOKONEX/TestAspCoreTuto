using System;
using System.Collections.Generic;
using System.Text;

namespace App.Net.Core.Features.Test.Modules.TestValueType.Impl
{
    /*
     The main difference between Tuple and ValueTuple are:
        System.ValueTuple is a value type (struct), while System.Tuple is a reference type (class). This is meaningful when talking about allocations and GC pressure.
        System.ValueTuple isn't only a struct, it's a mutable one, and one has to be careful when using them as such. Think what happens when a class holds a System.ValueTuple as a field.
        System.ValueTuple exposes its items via fields instead of properties.

        - Tuple is of reference type, but ValueTuple is of value type.
        - Tuple does not provide naming conventions, but ValueTuple provide strong naming conventions.
        - In Tuples you are not allowed to create a tuple with zero component, but in ValueTuple you are allowed to create a tuple with zero elements.
        - The performance of ValueTuple is better than Tuple. Because ValueTuple provides a lightweight mechanism for returning multiple values from the existing methods. And the syntax of ValueTuple is more optimized than Tuples.
        - ValueTuple provides more flexibility for accessing the elements of the value tuples by using deconstruction and the _ keyword. But Tuple cannot provide the concept of deconstruction and the _ keyword.
        - In ValueTuple the members such as item1 and item2 are fields. But in Tuple, they are properties.
        - In ValueTuple fields are mutable. But in Tuple, fields are read-only.
    */
    public class ValueTypeModule
    {
    }
}
