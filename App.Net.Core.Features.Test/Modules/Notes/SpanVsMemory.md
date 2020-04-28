La « Managed Memory »
C’est la mémoire utilisée lorsque l’on crée une nouvelle instance via le mot clé « new ». 
Elle va contenir toutes les variables par références et est gérée par le Garbage Collector de façon automatique.
Il est difficile d’évaluer sa durée de vie, sa libération de mémoire étant faite de façon automatique.
En effet, le Garbage Collector a en interne un système de nettoyage des objets en mémoire, à intervalles réguliers il va parcourir son graphe de références, 
et si une référence n’est plus utilisée, celle-ci sera détruite et libérée de la mémoire.

La « Stack Memory » Span<T>
On peut y accéder grâce à la commande « stackalloc » et permet une allocation et désallocation rapide en mémoire d’une variable de type « value » dans la stack.
Sa durée de vie se limite au scope de son exécution (par exemple à la fin de l’exécution d’une fonction).
Exemple d’utilisation:

        unsafe public void StackExemple()
        {
            char* helloString = stackallock char [5];
        }
À la fin de cette fonction, la variable helloString qui est allouée dans la stack va être automatiquement libérée de la mémoire.

La « Unmanaged memory »
c’est une mémoire qui n’est pas contrôlée par le Garbage Collector et est donc à utiliser avec grande précaution, 
car la libération de cette mémoire est à la responsabilité du développeur.
Elle est à privilégier lorsque l’on veut alléger le travail du Garbage Collector, voire obligatoire si l’on veut faire de l’interopérabilité. 
Utile quand on doit travailler avec des gros blocs de données !
On peut interagir avec grâce à la classe Marshal.


        string ReverseString (string parameterString);
on ne gère que les variables managées automatiquement par le GC (la stack et la heap). Si quelqu’un veut utiliser une variable en mémoire non managée par le GC
        unsafe String ReverseString(char* parameterStringUnmanaged, int lenght);

Supposons maintenant que l’on veuille gérer des portions de texte.
On ajoute 2 signatures, l’une pour les mémoires managées et l’autre acceptant un pointeur :
        string ReverseString(string parameterString, int startIndex, int lenght);
        unsafe string ReverseString(char* parameterStringUnmanaged, int lenght, int startIndex, int endIndex);

=> Le nouveau type Span<T> gère automatiquement l’accès aux 3 mémoires du monde .NET sans se soucier du type !

        unsafe
        {
            IntPtr unmanagedPointer = Marshal.AllocHGlobal(128);
            Span<byte> unmanagedData = new Span<byte>( unmanagedPointer.ToPointer(), 128);
            Marshal.FreeHGlobal(unmanagedPointer);
        }

        char[] array = new char[] { 'S', 'O', 'A', 'T'};
        Span<char> fromArray = array;

lorsque nous devons travailler sur des strings ou autres types immuables, nous pouvons utiliser ReadOnlySpan<T> :
        string stringSpan = "hello joel";
        ReadOnlySpan<char> fromString = stringSpan.AsReadOnlySpan;

     
Ces 2 signatures peuvent maintenant être fusionnées en une seule et unique méthode qui implémentera la logique :
        string ReverseString(string parameterString, int startIndex, int lenght);
        unsafe string ReverseString(char* parameterStringUnmanaged, int lenght, int startIndex, int endIndex);
        
        => public string ReverseStringTest(ReadOnlySpan<char> parameterString);

AVANTAGES :
    - Span<T> est sa capacité à splitter des données en plusieurs morceaux sans que cela coûte de la mémoire système.

            string grosVolumeDeDonnees = "Beaucoup de données ici";
            string boutDeDonnee_AvecAllocation = grosVolumeDeDonnees.Substring(1, 5);

        Ici, nous déclarons une string avec beaucoup de données, ce qui va engendrer déjà un coût en mémoire dans la stack.
        Ensuite, nous voulons récupérer un bout de cette string en faisant un substring… En faisant cela, une autre allocation en mémoire va être effectuée, 
        ce qui va augmenter drastiquement le coût de nos opérations.
        Avec ReadOnlySpan, aucune allocation en mémoire n’est effectuée, seuls les pointeurs vont être déplacés pour correspondre au morceau de texte que nous voulons !

            ReadOnlySpan<char> boutDeDonnee_SansAllocation = grosVolumeDeDonnees.AsReadOnlySpan().Slice(1, 5);

    On peut imaginer de multiples usages :
    – Parcourir des grosses chaînes de caractères sans allocation mémoire
    – Parser du json/xml
    – Lire et écrire des données binaires

LIMITATIONS : 
    - Stack Only
        Les Span ne peuvent être stockés eux-mêmes que dans la mémoire stack. 
        Ce choix a été motivé par plusieurs facteurs, mais les principaux sont dus à la volonté de simplifier le travail du Garbage Collector.
    tant donné que Span ne peut résider que dans la stack :
        Span ne peut pas être une propriété d’une classe (cette dernière étant stockée dans le heap)
        Span ne peut être boxé


Span<T> is a ref struct that is allocated on the stack rather than on the managed heap. 
Ref struct types have a number of restrictions to ensure that they cannot be promoted to the managed heap, 
including that 
    they can't be boxed, 
    they can't be assigned to variables of type Object, dynamic or to any interface type, 
    they can't be fields in a reference type, 
    and they can't be used across await and yield boundaries. 
In addition, calls to two methods, Equals(Object) and GetHashCode, throw a NotSupportedException.
    Span can only live on the execution stack.
    Span cannot be boxed or put on the heap.
    Span cannot be used as a generic type argument.
    Span cannot be an instance field of a type that itself is not stack-only.
    Span cannot be used within asynchronous methods.

Like Span<T>, Memory<T> represents a contiguous region of memory. 
Unlike Span<T>, however, Memory<T> is not a ref struct. 
This means that Memory<T> can be placed on the managed heap, whereas Span<T> cannot. 
As a result, the Memory<T> structure does not have the same restrictions as a Span<T> instance. 
In particular:
    It can be used as a field in a class.
    It can be used across await and yield boundaries.


    - Where possible, synchronous methods should accept Span<T> arguments instead of Memory<T>, 
    whereas asynchronous methods should accept Memory<T> arguments.
    - Methods without return types (i.e., void methods) that take Memory<T> as an argument shouldn’t use it after the method returns 
    (within background threads, for instance).
    - Async methods that take Memory<T> as an argument and return a task shouldn’t use it after the method returns 
    (i.e., after the method caller that’s waiting on that task continues its execution).
    - If you define a type that either takes in a Memory<T> in its constructor or has a settable Memory<T> property on it, 
    that type consumes the Memory<T> instance provided to it. Each Memory<T> instance can have just one consumer at a time.

Contrairement à Span<T>, Memory<T> peut être stockée sur le tas managé.
Span<T> et Memory<T> sont des mémoires tampons de données structurées qui peuvent être utilisées dans les pipelines.


Tip 1: Use Memory.Span.Slice(…) Instead of Memory.Slice(…).Span
Tip 2: Use AsSpan(…) Instead of AsSpan().Slice(…)

A Span<T> represents a contiguous region of arbitrary memory. A Span<T> instance is often used to hold the elements of an array or a portion of an array. 
Unlike an array, however, a Span<T> instance can point to managed memory, native memory, or memory managed on the stack. 

        public static void WorkWithSpans()
        {
            // Create a span over an array.
            var array = new byte[100];
            var arraySpan = new Span<byte>(array);

            InitializeSpan(arraySpan);
            Console.WriteLine($"The sum is {ComputeSum(arraySpan):N0}");

            // Create an array from native memory.
            var native = Marshal.AllocHGlobal(100);
            Span<byte> nativeSpan;
            unsafe
            {
                nativeSpan = new Span<byte>(native.ToPointer(), 100);
            }

            InitializeSpan(nativeSpan);
            Console.WriteLine($"The sum is {ComputeSum(nativeSpan):N0}");

            Marshal.FreeHGlobal(native);

            // Create a span on the stack.
            Span<byte> stackSpan = stackalloc byte[100];

            InitializeSpan(stackSpan);
            Console.WriteLine($"The sum is {ComputeSum(stackSpan):N0}");
        }

WHAT IS SPAN<T>?
Span<T> provides type-safe access to a contiguous area of memory. 
This memory can be located on the heap, the stack or even be formed of unmanaged memory. 
Span<T> has a related type ReadOnlySpan<T> which provides a read-only view of in-memory data. 
The ReadOnlySpan can be used to view the memory occupied by an immutable type like a String for example.

    -IMPROVING SPEED AND REDUCING ALLOCATIONS IN EXISTING CODE

        public string GetLastNameUsingSubstring(string fullName)
        {
            var lastSpaceIndex = fullName.LastIndexOf(" ", StringComparison.Ordinal);
            return lastSpaceIndex == -1
                ? string.Empty
                : fullName.Substring(lastSpaceIndex + 1);
        }
        =>
        public ReadOnlySpan<char> GetLastNameWithSpan(ReadOnlySpan<char> fullName)
        {
            var lastSpaceIndex = fullName.LastIndexOf(' ');
            return lastSpaceIndex == -1 
                ? ReadOnlySpan<char>.Empty 
                : fullName.Slice(lastSpaceIndex + 1);
        }


Sample 1: Return the Sum of the Elements of a Byte Array

        public static int ArraySum(byte[] data)
        {
            int sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                sum += data[i];
            }
            return sum;
        }
        public static int SpanSum(Span<byte> data)
        {
            int sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                sum += data[i];
            }
            return sum;
        }
        static void Main(string[] args)
        {
            byte[] data = { 1, 2, 3, 4, 5, 6, 7 };
            ArraySum(data); // returns 28
            SpanSum(data); // returns 28
        }
    => There’s practically no difference in performance between iterating spans and arrays.

Sample 2: Return the Sum of a String Containing Comma Separated Integers


