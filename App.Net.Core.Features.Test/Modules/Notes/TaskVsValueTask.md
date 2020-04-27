Task<T> est une classe et entraîne la surcharge inutile de son allocation lorsque le résultat est immédiatement disponible.

ValueTask<T> est une structure qui a été introduite pour empêcher l'allocation d'un objet Task au cas où 
le résultat de l'opération asynchrone est déjà disponible au moment de l'attente.

ValueTask is a discriminated union of a T and a Task, 
making it allocation-free for ReadAsync to synchronously return a T value it has available (in contrast to using Task.FromResult, 
which needs to allocate a Task instance). 
ValueTask is awaitable, so most consumption of instances will be indistinguishable from with a Task.

Being a structure, ValueTask enables writing async methods that do not allocate memory when running synchronously. 

        private async Task<int> MultipyAsync(int x, int y)
        {
            if (x == 0 || y == 0)
            {
                return 0;
            }

            return await Task.Run(() => x * y);
        }

        private async ValueTask<int> MultiplyAsync(int x, int y)
        {
            if (x == 0 || y == 0)
            {
                return 0;
            }

            return await Task.Run(() => x * y);
        }

Augmentation de la performance

            Nécessite une allocation de tas
            Prend 120ns avec JIT
            async Task<int> TestTask(int d)
            {
                await Task.Delay(d);
                return 10;
            }

            Pas d'allocation de tas si le résultat est connu de manière synchrone (ce qui n'est pas le cas à cause de Task.Delay , 
            mais se trouve souvent dans de nombreux scénarios async / d' await réels)
            Prend 65ns avec JIT
            async ValueTask<int> TestValueTask(int d)
            {
                await Task.Delay(d);
                return 10;
            }

Flexibilité accrue de la mise en œuvre

        Les implémentations d'une interface asynchrone souhaitant être synchrone sinon obligées d'utiliser 'Task.Run ou Task.FromResult'

        Avec ValueTask<T> , les implémentations sont plus libres de choisir entre être synchrones ou asynchrones sans affecter les appelants.

        Implémentation synchrone:
        class SynchronousFoo<T> : IFoo<T>
        {
            public ValueTask<T> BarAsync()
            {
                var value = default(T);
                return new ValueTask<T>(value);
            }
        }

        Implémentation asynchrone
        class AsynchronousFoo<T> : IFoo<T>
        {
            public async ValueTask<T> BarAsync()
            {
                var value = default(T);
                await Task.Delay(1);
                return value;
            }
        }

FromResult<T> , which needs to allocate a Task<T> instance). 
ValueTask<T> is awaitable, so most consumption of instances will be indistinguishable from with a Task<T> . 
ValueTask, being a struct, enables writing async methods that do not allocate memory when they run synchronously without compromising API consistency.


Awaiting a ValueTask / ValueTask<TResult> multiple times. 
The underlying object may have been recycled already and be in use by another operation. 
In contrast, a Task / Task<TResult> will never transition from a complete to incomplete state, so you can await it as many times as you need to, 
and will always get the same answer every time.

Awaiting a ValueTask / ValueTask<TResult> concurrently. 
The underlying object expects to work with only a single callback from a single consumer at a time, 
and attempting to await it concurrently could easily introduce race conditions and subtle program errors. 
It’s also just a more specific case of the above bad operation: “awaiting a ValueTask / ValueTask<TResult> multiple times.”
In contrast, Task / Task<TResult> do support any number of concurrent awaits.

Using .GetAwaiter().GetResult() when the operation hasn’t yet completed. 
The IValueTaskSource / IValueTaskSource<TResult> implementation need not support blocking until the operation completes, 
and likely doesn’t, so such an operation is inherently a race condition and is unlikely to behave the way the caller intends. 
In contrast, Task / Task<TResult> do enable this, blocking the caller until the task completes.

=> The short rule is this: with a ValueTask or a ValueTask<TResult>, 
you should either await it directly (optionally with .ConfigureAwait(false)) 
or call AsTask() on it directly, and then never use it again,

            // Given this ValueTask<int>-returning method…
            public ValueTask<int> SomeValueTaskReturningMethodAsync();
            …
            // GOOD
            int result = await SomeValueTaskReturningMethodAsync();

            // GOOD
            int result = await SomeValueTaskReturningMethodAsync().ConfigureAwait(false);

            // GOOD
            Task<int> t = SomeValueTaskReturningMethodAsync().AsTask();

            // WARNING
            ValueTask<int> vt = SomeValueTaskReturningMethodAsync();
            ... // storing the instance into a local makes it much more likely it'll be misused,
                // but it could still be ok

            // BAD: awaits multiple times
            ValueTask<int> vt = SomeValueTaskReturningMethodAsync();
            int result = await vt;
            int result2 = await vt;

            // BAD: awaits concurrently (and, by definition then, multiple times)
            ValueTask<int> vt = SomeValueTaskReturningMethodAsync();
            Task.Run(async () => await vt);
            Task.Run(async () => await vt);

            // BAD: uses GetAwaiter().GetResult() when it's not known to be done
            ValueTask<int> vt = SomeValueTaskReturningMethodAsync();
            int result = vt.GetAwaiter().GetResult();