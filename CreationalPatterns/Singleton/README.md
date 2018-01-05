# Singleton

Ensure a class has only one instance, and provide a global point of access to it.

## Problem

* How can it be ensured that a class has only one instance?
* How can the sole instance of a class be accessed easily?
* How can a class control its instantion?
* How can the number of instances of a class be restricted?

## Solution

* Hide the constructor of the class
* Define a public static operation `getInstance()` that returns the sole instance of the class.

## Common Structure

![Structure](img/structure.png)

* Singleton
  * declares static method `getInstance` that should return the same instance of `Singleton` class.
  * may be responsible for creating its own unique instance.

## Collaboration

* Clients access a Singleton instance solely through static `getInstance()` method.

## Benefits

* Ensures that class has only a single instance.
* Provides global access point to that instance.
* Allows deferred initialization

## Drawbacks

* Violate single responsibility principle
    * They control their own creation and lifecycle.
* instance is tightly coupled to Singleton class => Difficult to test
* Requires special treatment in a multithreaded environment.

## Example

There are two ways to implement singleton.

![ChocolateBoiler as Singleton](/Diagrams/Singleton.png)

### Static Initialization

:thumbsup: **You may want to create your Singleton eagerly**, if
* your application always creates and uses an instance of the `Singleton`
* the overhead of creation and runtime aspects of the Singleton are not onerous

:thumbsdown: **Downside**
* You have less control over the mechanics of the instantiation

[ChocolateBoiler_StaticInit.cs](/CreationalPatterns/Singleton/ChocolateBoiler_StaticInit.cs).
```cs
    public sealed class ChocolateBoiler_StaticInit
    {
        // singleton instance
        private static readonly ChocolateBoiler_StaticInit instance = new ChocolateBoiler_StaticInit();

        public static ChocolateBoiler_StaticInit Instance
        {
            get
            {
                return instance;
            }
        }
    }
```

The class is marked as **sealed** to prevent derivation, which could add instances. In addition, the variable is marked **readonly**, which means that it can be assigned only during static Initialization or in a class constructor.

Because the **Singleton** instance is referenced by a private static member variable, the instantiation does not occur until the class is first referenced by a call to the **Instance** property. This solution therefore implements a form of the lazy instantiation property, as in the Design Patterns form of Singleton.

### Multithreaded Singleton

:thumbsup: **Use double-checked locking**, if
* you want to keep separate threads from creating new instances of the singleton at the same time (thread-safe)
* you want to create an instance only when the instance is needed.

[ChocolateBoiler_ThreadSafe.cs](/CreationalPatterns/Singleton/ChocolateBoiler_ThreadSafe.cs).
```cs
    public sealed class ChocolateBuilder_ThreadSafe
    {       
        // singleton instance
        // the volatile keyword ensures that multiple threads handle the singleton instance variable correctly
        private static volatile ChocolateBuilder_ThreadSafe instance;
        private static object syncLock = new object();

        public static ChocolateBuilder_ThreadSafe Instance
        {
            get
            {
                // Check for an instance and if there isn't one, enter a locked block
                if (instance == null)
                {
                    lock (syncLock)
                    {
                        // Once in the block, check again and if still null
                        if(instance == null)
                        {
                            instance = new ChocolateBuilder_ThreadSafe();
                        }                 
                    }  
                }
                return instance;
            }
        }
     }
```
The singleton instance is declared to be **volatile** to ensure that assignment to the instance variable completes before the instance variable can be accessed.
Lastly, this approach uses a **syncRoot** instance to lock on, rather than locking on the type itself, to avoid deadlocks.

This double-checked locking approach solves the thread concurrency problems while avoiding an exclusive lock in every call to the instance propery method. It also allows you to delay instantiation until the object is first accessed.

## Relations with Other Patterns

* **AbstractFactory**, **Builder**, **Prototype**  - These patterns can be implemented using the Singleton pattern
* **Flyweight** - There should be only one Singleton instance, whereas Flyweight class can have multiple instances with a different intrinsic state. Whatsmore, Singleton object can be mutable but Flyweight objects are immutable.

Reference: 
* MSDN - [Implementing Singleton in C#](https://msdn.microsoft.com/en-us/library/ff650316.aspx)
