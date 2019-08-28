# Historie und erste Grundlagen der FP 

- Funktionen als first-class citizens 
- Immutability
- Pure Funktionen 
- Daten und Transformationen 

# Mob-Programming 

- kurzes Intro zu "was ist Mob Programming" 
- erspart Teilnehmer Setup 
- alle Teilnehmer lernen das gleiche 

# Vorstellung Beispielanwendung 

Ein Adressbuch bzw. einer Kontaktliste. Da kann man mehrere Sachen nacheinander validieren (Verbinden von Options bzw. Results) und  man hat optionale Felder (Options).  

Kontaktliste, man kann Einträge via Kommandozeile hinzufügen, Auflisten (mit verschiedenen Sortierungen?), Löschen. Jeder Eintrag hat einige Pflichtfelder: Vorname, Nachname, Kontaktmethode. Und dann noch optionale Felder (Geburtstag, Twitter-Link, weitere Kontaktmethoden). Die Daten werden serialisiert und in einer Datei abgelegt (das könnten wir vorher ausprogrammieren und dann nur als Modul anbieten). Das ganze mit Tests (entweder TDD oder nachträglich). 

## Vorschlag Demo/Naming/Domain

Kontaktliste als Name finde ich irrefuehrend. Das impliziert eine Liste -> Jeder Programmierer denkt sofort an: welche Interfaces muss ich implementieren, Oh Gott, muss ich IComparable, IQuatible, etc implemtentieren... 

***Adressbuch*** ist meiner Ansicht nach besser. Ich mach alles in Englisch, darum bei mir ***Addressbook***.

@martin Was meinst du?

# Umsetzung der Beispielanwendung in C# 

(zumindest damit anfangen, mit soviel FP wie möglich, bis es zu doof wird) 

# Einführung in die Syntax von F# 

- Significant whitespace 
- Module & Reihenfolge der Dateien 
- Basisdatentypen 
- Funktionen 
- Typ-Signaturen, und dann warum man sie nicht braucht (yay Type Inference) 
- Pipeline-Operator 

# Modellieren der Fachdomäne - Daten/Typen 

- Records 
- Union Types 
- Option 
- Result(?) 

# Modellieren der Fachdomäne - Funktionen und fortgeschrittene Typen 

- Eingabedaten verarbeiten 
- Opaque Types als Value Objects 
- Validierungsfunktionen 
- Anzeige der Ergebnisse als Tabelle (typsicheres printf) 
- Sortierung der Ergebnisse 

# Implementierung der Funktionen 

- mit TDD? -> dann: Testing in F#, Vorteile von FP beim Testen 
- Verbindung mehrere Validierungsergebnisse (je nachdem, wie tief wir hier reingehen wollen, bzw. ob überhaupt - map, apply, bind, Monaden, result/option Computational Expression) - böse gesagt ist das der Kern der praktischen FP-Programmierung, das ganze Monaden-Zeugs löst ja in erster Linie das "wie stecke ich Funktionen zusammen, die nicht zusammenpassen"-Problem 

# Resourcen

- Value Objects with C#: https://enterprisecraftsmanship.com/2017/08/28/value-object-a-better-implementation/ 

