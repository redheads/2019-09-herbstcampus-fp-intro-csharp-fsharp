## Neue Anforderung!
<div style="display:flex; align-items: center;">
    <div>
        <img src="./resources/business-cat_mirrored.jpg" alt="Business Cat" 
        style="width: 100%;" />
    </div>
    <div style="flex-grow: 1; display: flex; content-align: center; align-items: center;">
        <p>
            "Ein Kontakt kann gespeichert werden, und nach erfolgreichem Speichern wird eine Benachrichtigung verschickt. Bei einem Fehler passiert nichts."
        </p>
    </div>
</div> 

----

## Persistenz
- Eintrag wird serialisiert und in Datei abgelegt
- Eine Datei für alle Einträge
- In der Praxis problematisch: Gleichzeitiger Zugriff auf die gleiche Datei
