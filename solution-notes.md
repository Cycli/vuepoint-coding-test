# VuePoint Solutions Coding Test - Solution Notes
There's no right answer to this, but the task should separate out candidates that have a basic understanding of coding and OO principles, and those that know how to design and construct an extensible, testable, robust implementation.

# Design
## Separation of Concerns
The description gives a reasonably clear hint at the classes that need to be implemented. Separation of Concerns requires that a code-unit or class address a single, well-bounded responsibility. This helps maintainability, testability and reuse.

Suggested classes might include:
- `Program`: The starting class. This should contain very little code, just sufficient to bootstrap the game.
- `Game`: A high-level class that initialises the game, orchestrates player turns and tallies the score.
- `Player`: The class the defines player characteristics. This only needs a `Name` property and some way of setting the player's playing strategy.
- `Strategy`: This will be a class that decides what hand-gesture to play. A strategy will be assigned to the `Player`. Separating out the strategy from the player might allow the player to change strategy at some point in the game.
- `Scorer`: Responsible for scoring each round and keeping track of the win total. The scoring should not be hard-wired into the game.
- `*View`: Outputs the player hand-gestures and scores to the screen. Most modern web development frameworks separate out the Model (data) from the View (how the data looks).

## Open/Closed
Open/Closed is a coding principle that says that code should be Open for Extension but Closed for Modification. In practice, this means that the candidate should have considered the hints about how to deal with changes or new requirements. 

A good implementation should allow functionality to be modified without changing the call signatures in the classes or making wholesale changes across many classes.

## Liskov's Substitution Principle
This states that code using a base class must also be able to use derived classes without needing to know that the swap has been made. 

For example, if the candidate has implemented a basic inherited Strategy class to pick a hand-gesture at random, this should be swappable with another Strategy (say always pick Rock) without the code calling the Strategy needing any modification or additional information.

## Interface Segregation
The Interface Segregation principle states that no client should be forced to depend on methods it does not use. ISP splits interfaces that are very large into smaller and more specific ones so that clients will only have to know about the methods that are of interest to them.

While it's unlikely this will appear in a small example, a candidate might just implement a Game class that includes everything. This wouldn't be great.

## Dependency Inversion
High-level modules should not depend on low-level modules and both should depend on abstractions. In practice, nearly all classes outside basic data objects in the example should have an `Interface` to fix the class signatures, with concrete implementations. 

Dependencies between classes should be referenced by interface, not the concrete implementation. For example, using the classes above, you'd expect to see `IGame`, `IPlayer`, `IStrategy` etc. with `Game`, `Player`, `SomeStrategy` etc. as concrete implementations. 

## Testing
Ideally the candidate should deliver the code with a separate project containing Unit Tests (almost certainly using NUNIT). The tests should show some level of organisation, typically by class and method. For top-marks, the Test coverage should exercise every class method at least once.

Failing that, the candidate may just have written separate code to run tests (i.e. not using a test framework). This is better than nothing, but it might be worth asking about awareness of automated testing environments.

No tests? Didn't you read the requirement!?

## Other Stuff
Look out for sensible commenting and 'tidy'-looking code, possibly using VS's documentation tags etc.

Nice to haves would also include a broad description of the design and some user instructions, including how to run the tests.

Look out for an understanding of the access modifiers - `public`, `internal`, `protected`, `private` in the class and method scoping. For example, `private` isn't usually needed as it's the default, although it's a style thing whether to include it or not. At the class level, `private`, `internal` and *no access modifier* are equivalent as all mean that the class will normally not be accessible outside the assembly. One trick used in the example solution is that solution classes are private to the assembly, but the test get privileged access by using som trickery in the `AssemblyInfo.cs` file.
