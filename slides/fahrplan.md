1) Begrüßung
2) Vorstellung wir
4) Vorstellungsrunde Teilnehmer
5) Erwartungen Teilnehmer
3) Inhalte/Lernziele Workshop
6) Was ist Mob-Programming?
7) Vorstellung Aufgabenstellung/Domäne
8) Naive Implementierung des Datenmodells für einen Kontakt (string, DateTime?, int)
9) FP Basics
10) PO will Nullable loswerden für Verständlichkeit
11) Zurück zum Modell: Fokus auf Geburtsdatum

    - DateTime? ganz gut, weil schon "da oder nicht drin steckt", aber wäre es nicht schöner, ohne ständige Null-Checks und auch für Domänenexperten verständlich ausdrücken zu können?

12) Vorstellung von Option
    - Semantik für "vorhanden oder nicht"
    
13) PD: LaYumba vorstellen
10) MG: PO will neue Funktion haben: Das Datum muss als String ausgebbar sein, egal ob es da ist oder nicht
15) Funktion schreiben mit Pattern Matching: DateTime option -> string (String.Empty falls None)
16) MG: Einführung F#
17) MG: Bisheriges Datenmodell in F# modellieren
18) Neue Anforderung: Vorname darf niemals leer sein
19) PD: Value Objects
20) PD: ValueObject erstmal mit Konstruktor und Exception, wenn was schief geht
21) Datenmodell anpassen
22) MG: Neue Anforderung: Das Datum darf nur eine Datumskomponente enthalten, keine Zeitkomponente
23) Problem: Wir haben keine Funktion zur Verfügung, die mit Option<DateTime> umgehen kann
24) MG: Einführung Funktor
25) Funktion implementieren und mit map verbinden
26) Funktionen für DateTime option -> string und für map auch implementieren 
27) MG: Nächste Anforderung: Ein Kontakt kann gespeichert werden, und nach erfolgreichem Speichern wird eine Benachrichtigung verschickt
28) Speichern kann fehlschlagen: Wie drücken wir aus, dass es klappen oder fehlschlagen kann?
29) Ergebnis ist entweder ein Erfolg oder ein Fehlschlag -> Result
30) Result (Either) vorstellen
31) Monade einführen
32) PD: Railway Oriented Programming vorstellen
33) Ausprogrammieren: Kontakt kann gespeichert werden, und nach erfolgreichem Speichern wird eine Benachrichtigung verschickt
34) MG: Neue Anforderung: Rückgabe für den User notwendig: Funktion, die auf Result am Ende der Kette pattern matching macht und jeweils Strings ausgibt
35) Verweis auf Railway Oriented Programming -> "Endfunktion"
36) Endfunktion mit Pattern Matching implementieren
37) MG: Neue Anforderung: Fehler beim Erzeugen eines Kontakts (Validierung) sollen gesammelt werden, nicht beim Ersten abgebrochen wie beim Speichern
38) Applicative einführen
39) Ausprogrammieren: Fehler beim Erzeugen eines Kontakts (Validierung) sollen gesammelt werden, nicht beim Ersten abgebrochen wie beim Speichern
40) Hinweise auf Veranstaltungen zum Thema
41) MG: Exkurs: How to introduce F# into YOUR project
42) Zusammenfassung des Gelernten (durch Teilnehmer?)
43) Falls noch Zeit ist: Einfach am Projekt weiterprogrammieren und die Konzepte anwenden/vertiefen bzw. siehe BONUS
44) Abschluss-Folien
45) Feedbackrunde
46) BONUS
    1)  Smart Constructor
    2)  MG: Intermezzo: Funktionale Architektur(en)
    3)  MG: Datenmodell in F# erweitern auf Kontaktmethode
    4)  MG: Wiederholung Discriminated Unions (eingeführt schon im F# Basics Teil)
    5)  PD: Wie kann man das in C# nachbilden? Nicht ohne zusätzliche Bibliothek
