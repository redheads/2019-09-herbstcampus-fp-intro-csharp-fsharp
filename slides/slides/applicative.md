### Problem: Funktion mit mehreren eingepackten Parametern

```fsharp
let add a b = a + b

let onlyPositive i =
  if i > 0 then
    Some i
  else
    None

let addTwoNumbers a b =
  let positiveA = onlyPositive a
  let positiveB = onlyPositive b
  // passt nicht, 2x int erwartet, aber 2x int option übergeben
  let sum = add positiveA positiveB

  // für zwei in F# bereits vordefiniert:
  let sum = Option.map2 add positiveA positiveB

  // aber was, wenn man mehr Parameter hat?

```

---

### Applicative

![img](./resources/Applicative_1_small.png)

---

### Applicative

- Container mit "apply" Funktion (die bestimmten Regeln folgt): Applicative
- Bezeichnung in der FP-Welt: **Applicative Functor**

```fsharp
  apply: AF (a -> b) -> AF a -> AF b
```

- Andere Bezeichnungen für "apply": ap, <*>

---

### Funktion mit mehreren Parametern

```fsharp
// F#
let sum a b c = a + b + c

let onlyPositive i =
    if i > 0 then Some i
    else None

let addNumbers a b c =
    let positiveA = onlyPositive a
    let positiveB = onlyPositive b
    let positiveC = onlyPositive c

    // sum ist vom Typ: (int -> int -> int -> int)
    // jede Zeile füllt ein Argument mehr aus
    // (Partial Application dank Currying)
    let (sum' : (int -> int -> int) option) = Option.map sum positiveA
    let (sum'' : (int -> int) option) = Option.apply sum' positiveB
    let (sum''' : (int) option) = Option.apply sum'' positiveC
```

----

### Funktion mit mehreren Parametern

```csharp
// C#
Func<int, int, int, int> sum = (a, b, c) => a + b + c;

Func<int, Validation<int>> onlyPositive = i
    => i > 0
        ? Valid(i)
        : Error($"Number {i} is not positive.");

Validation<int> AddNumbers(int a, int b, int c) {
    return Valid(sum)              // returns int -> int -> int -> int
        .Apply(onlyPositive(a))    // returns int -> int -> int
        .Apply(onlyPositive(b))    // returns int -> int
        .Apply(onlyPositive(c));   // returns int

AddNumbers(1, 2, 3);    // --> Valid(6)
AddNumbers(-1, -2, -3); // --> [
                        // Error("Number -1 is not positive"),
                        // Error("Number -2 is not positive"),
                        // Error("Number -3 is not positive")
                        // ]
```
