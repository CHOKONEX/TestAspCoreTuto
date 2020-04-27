Structs are value type whereas Classes are reference type.
Structs are stored on the stack whereas Classes are stored on the heap.
Value types hold their value in memory where they are declared, but reference type holds a reference to an object memory.
Value types destroyed immediately after the scope is lost whereas reference type only the variable destroy after the scope is lost. The object is later destroyed by the garbage collector.
When you copy struct into another struct, a new copy of that struct gets created modified of one struct won't affect the value of the other struct.
When you copy a class into another class, it only copies the reference variable.
Both the reference variable point to the same object on the heap. Change to one variable will affect the other reference variable.
Structs can not have destructors, but classes can have destructors.
Structs can not have explicit parameterless constructors whereas a class can structs doesn't support inheritance, but classes do. Both support inheritance from an interface.
Structs are sealed type.


Classes Only:

Can support inheritance
Are reference (pointer) types
The reference can be null
Have memory overhead per new instance
Structs Only:

Cannot support inheritance
Are value types
Are passed by value (like integers)
Cannot have a null reference (unless Nullable is used)
Do not have a memory overhead per new instance - unless 'boxed'
Both Classes and Structs:

Are compound data types typically used to contain a few variables that have some logical relationship
Can contain methods and events
Can support interfaces


+------------------------+------------------------------------------------------------------------------------------------------+---------------------------------------------------------------------------------------------------+
|                        |                                                Struct                                                |                                               Class                                               |
+------------------------+------------------------------------------------------------------------------------------------------+---------------------------------------------------------------------------------------------------+
| Type                   | Value-type                                                                                           | Reference-type                                                                                    |
| Where                  | On stack / Inline in containing type                                                                 | On Heap                                                                                           |
| Deallocation           | Stack unwinds / containing type gets deallocated                                                     | Garbage Collected                                                                                 |
| Arrays                 | Inline, elements are the actual instances of the value type                                          | Out of line, elements are just references to instances of the reference type residing on the heap |
| Aldel Cost             | Cheap allocation-deallocation                                                                        | Expensive allocation-deallocation                                                                 |
| Memory usage           | Boxed when cast to a reference type or one of the interfaces they implement,                         | No boxing-unboxing                                                                                |
|                        | Unboxed when cast back to value type                                                                 |                                                                                                   |
|                        | (Negative impact because boxes are objects that are allocated on the heap and are garbage-collected) |                                                                                                   |
| Assignments            | Copy entire data                                                                                     | Copy the reference                                                                                |
| Change to an instance  | Does not affect any of its copies                                                                    | Affect all references pointing to the instance                                                    |
| Mutability             | Should be immutable                                                                                  | Mutable                                                                                           |
| Population             | In some situations                                                                                   | Majority of types in a framework should be classes                                                |
| Lifetime               | Short-lived                                                                                          | Long-lived                                                                                        |
| Destructor             | Cannot have                                                                                          | Can have                                                                                          |
| Inheritance            | Only from an interface                                                                               | Full support                                                                                      |
| Polymorphism           | No                                                                                                   | Yes                                                                                               |
| Sealed                 | Yes                                                                                                  | When have sealed keyword                                                                          |
| Constructor            | Can not have explicit parameterless constructors                                                     | Any constructor                                                                                   |
| Null-assignments       | When marked with nullable question mark                                                              | Yes (+ When marked with nullable question mark in C# 8+)                                          |
| Abstract               | No                                                                                                   | When have abstract keyword                                                                        |
| Member Access Modifiers| public, private, internal                                                                            | public, protected, internal, protected internal, private protected                                |
+------------------------+------------------------------------------------------------------------------------------------------+---------------------------------------------------------------------------------------------------+