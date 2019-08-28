## Vorhandensein eines Werts
#### oder: null muss weg.
----

```csharp
// Enthält die Signatur die ganze Wahrheit?
public string Stringify<T>(T data)
{
    return null;
}
```

----

```csharp
// Sind Magic Values eine gute Idee?
public int Intify(string s)
{
    int result = -1;
    int.TryParse(s, out result);
    return result;
}
```

----

```csharp
public class Data
{
    public string Name;
}

public class Do
{
    public Data CreateData() => null;

    public string CreateAndUseData()
    {
        var data = CreateData();
        // kein null-Check -> ist dem Compiler egal
        return data.Name;
    }
}
```

----

## Option
```
// Pseudocode
type Option<T> = Some<T> | None
```
- entweder ein Wert ist da - dann ist er in "Some" eingepackt
- oder es ist kein Wert da, dann gibt es ein leeres "None"
- alternative Bezeichnungen: Optional, Maybe

----

## Mit Option
```csharp
public Option<int> IntifyOption(string s)
{
    int result = -1;
    bool success = int.TryParse(s, out result);
    return success ? Some(result) : None;
}
```

----

### Wie komme ich an einen eingepackten Wert ran?
> Pattern matching allows you to match a value against some patterns to select a branch of the code.

```csharp
public string Stringify<T>(Option<T> data)
{
    return data.Match(
        None: () => "",
        Some: (existingData) => existingData.ToString()
    );
}
```

----

### Vorteile
- Explizite Semantik: Wert ist da - oder eben nicht
- Auch für Nicht-Programmierer verständlich(er): "optional" vs. "nullable"
- Die Signatur von Match erzwingt eine Behandlung beider Fälle - nie wieder vergessene Null-Checks!
- Achtung: In C# bleibt das Problem, dass "Option" auch ein Objekt ist - und daher selbst null sein kann

----

## LINQ - für Listen (IEnumerable in C#)

Allg.: Funktionen, die auf eine Liste angewendet werden

Bsp:

- Option ist eigentlich nur eine Liste mit 2 Werten (Some und None)
- Result -> Liste mit 2 Werten (Left und Right)
- etc.

---

In FP unterscheidet man die Wrapper-Klassen (zB IEnumerable) anhand der Funktionen, die sie bereitstellen
