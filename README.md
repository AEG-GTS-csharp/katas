# Code-Kata-Sammlung

## Was ist ein Code Kata

> Kata bezeichnet in der Programmierung eine kleine, abgeschlossene Übung. Der Name rührt aus den japanischen Kampfkünsten her (vergleiche Kata) und betont die Bedeutung von Praxis und häufiger Wiederholung für das Lernen. Der Begriff wurde 2007 von Dave Thomas geprägt. In seinem Blog CodeKata[1] stellt Thomas 21 Übungen vor. Das Kata Manifesto[2] nennt zusätzlich sicheres Experimentieren, Vielfalt und gemeinsames Lernen als wichtige Aspekte. Eine Kata hat demnach nicht nur eine Lösung des gestellten Problems, sondern soll auf verschiedene Arten und mit unterschiedlichen Techniken implementiert werden. Die individuelle Lösung des gestellten Problems an sich ist ein klares Nicht-Ziel, weshalb triviale Probleme wie das Fizz Buzz Kata[3] eine gute Einführung sind.

## Regeln für 2 Personen

- Person A ein schreibt einen Test, der fehlschlägt.
- Person B versucht in kürzester Zeit das Progammierproblem soweit zu lösen, dass der Test nicht mehr fehlschlägt.
- Anschließend tauschen A und B die Rollen

## Setup

### Projektordner anlegen und ins Verzeichnis wechseln

``` ps1
ni -Type Directory {Name-of-Project}
cd {Name-of-Project}
```
### Projetmappe anlegen

``` ps1
dotnet new sln
```

### Konsolenanwendung erstellen

``` ps1
dotnet new console app
```

### Testanwendung erstellen

```
dotnet new xunit tests
```

### Konsolenanwendung und Testanwendung zur Projektmappe hinzufügen

``` ps1
dotnet sln .\{Proktmappenname}.slnx add .\app\app.csproj
dotnet sln .\{Proktmappenname}.slnx add .\tests\tests.csproj
```

### Referenz von der Konsolenanwendung zur Testanwendung

``` ps1
dotnet add .\tests\tests.csproj reference .\app\app.csproj
```

### Projektmappe bauen und Konsolen- und Testanwendung ausführen

``` ps1
dotnet build
dotnet run --project ./app/app.csproj
dotnet test
```
