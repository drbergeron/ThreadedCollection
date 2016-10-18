## ThreadedCollection

Threaded collection is a collection to push thread safe work into for processing as fast as possible. User should then pull the results back out (results should be stored on class). 
Uses the .net managed background thread pool
to perform the work.

### Usage

It's easy to add items that need work to the collection, and then process when ready.

```C#
    ThreadedCollection<ObjectToPerformWork> tcol = new ThreadedCollection<ObjectToPerformWork>();
    for(int i = 0; i< 20000; ++i)
    {
        tcol.Add(new ObjectToPerformWork());
    }

    tcol.LogDuration = true; //log how long it takes to process all items in the collection

    tcol.Process();

    //or if you need to process without threading to do work comparisons:   tcol.ProcessNoThreading();
    // Console.WriteLine($"Non Async Took {tcol.Duration.TotalMilliseconds}ms");

    Console.WriteLine($"Async Took {tcol.Duration.TotalMilliseconds}ms");
```

Your Class to perform work must inherit from `IThreadedCollectionObject` and implement the `Process()` method:

```C#
    public async Task Process()
    {
        await Task.Run(() => {
            //Perform all work in here
        });
    }
```

- Any resources used in the process method should be threadsafe.
- When I use this I create classes that have all the information they need to do work contained within them, making them thread safe by default.

Test project:
```
test
Async Took 87.6544ms
Non Async Took 288.9136ms
Press any key to continue . . .
```

TODO:
 - Implement thread safe results sets.
