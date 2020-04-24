# MbSoftLab.TemplateEngine.Core

Die TemplateEngine ersetzt Werte aus Propertys von C# Klassen in Templatezeichenketten.
Die C# Klasse mit den Propertys zur Datenhaltung wird hier als "TemplateDataModel-Klasse" bezeichnet. 
Die Zeichenkette mit den Platzhaltern (${PropertyName}) wird Stringtemplate oder Templatezeichenkette genannt.
Auch der Zugriff auf Parameterlose Public-Methoden der Klassen ist über `${MethodName()}` möglich. 


In der Templatezeichenkette kannst du über `${DeinPropertyName}` den Wert aus Deiner C#-Property an dein String-Template binden. 
Das Start und und Endzeichen `${" "}` kannst du über die Eigenschaften OpeningDelimiter/CloseingDelimiter selbst festlegen. 
Als Standard ist `${` für das Startzeichen und `}` für das Endzeichen hinterlegt.

Es werden gängige Systemdatentypen unterstützt. Eine Übersicht zur Datentypkompatibilität findest Du hier.

*Beispiel für ein Problem, welches mit dieser TemplateEngine gelöst werden kann:*

```XML
Du möchtest aus dieser Eingabe: <MyTag>${ProjektName}</MyTag>

Eine solche Ausgabe erzeugen: <MyTag>Projektname</MyTag>
````

```csharp
 TemplateDataModel templateDataModel = new TemplateDataModel
 {
     ProjektName = "Projektname"
 };

string template = "<MyTag>${ProjektName}</MyTag>";

TemplateEngine templateEngine = new TemplateEngine(templateDataModel,template);
string outputString = templateEngine.CreateStringFromTemplate();

Console.Write(outputString); // Ausgabe: <MyTag>Projektname</MyTag> 
```

---


## Methoden 
|Methodenname                                                            |Beschreibung                                                     |
|------------------------------------------------------------------------|-----------------------------------------------------------------|
|`string CreateStringFromTemplate( [string template] )`                |*Erzeugt eine Zeichenkette aus dem Datenmodell* und dem Template   |
|`void LoadTemplateFromFile(string filename)`                          |*Lädt eine Templatezeichenkette aus einer* Datei.                  |
|`TemplateEngine()`                                                   |Konstruktor         |
|`TemplateEngine(object templateDataModel, string stringTemplate)`     |Konstruktor         |
|`TemplateEngine(object templateDataModel)`                            |Konstruktor         |
|`TemplateEngine< T >()`                                               |Konstruktor         |
|`TemplateEngine< T >(T templateDataModel, string stringTemplate)`     |Konstruktor         |
|`TemplateEngine< T >(T templateDataModel)`                            |Konstruktor         |

---

## Propertys 

|Propertyname                            |Datentyp          |Beschreibung                                                           |
|----------------------------------------|------------------|-------------------------------------------------------------------------|
|`OpeningDelimiter`                    |String            |Legt das Stratzeichen für die Propertyersetzung fest                     |
|`CloseingDelimiter`                   |String            |Legt das Endzeichen für die Propertyersetzung fest                       |
|`TemplateDataModel`                      |Generic / object  |Modell mit Properys für Datenhaltung                                     |
|`TemplateString`                      |string            |Templatezeichenkette                                                     |
|`NullStringValue`                     |string            |Zeichenkette für NULL-Werte                                              |

---

## Beispiele:

### **Das Template laden und mit Daten aus dem Modell füllen**
```csharp
// Modell Klasse für die Datenhaltung erstellen
 TemplateDataModel templateDataModel = new TemplateDataModel
 {
     ProjektName = "Projektname"
 };

string template = "<MyTag>${ProjektName}</MyTag>";

TemplateEngine templateEngine = new TemplateEngine(templateDataModel,template);
string outputString = templateEngine.CreateStringFromTemplate();

Console.Write(outputString); // Ausgabe: <MyTag>Projektname</MyTag> 
```
### **Ein Template aus einer Datei laden**
```csharp
    TemplateDataModel templateDataModel = new TemplateDataModel
    {
      ProjektName = "Projektname",
      KundeNummer = "1234",
      ProjektLink = "https://google.de",
      ProjektKuerzel = "PKZL"
    };

     TemplateEngine templateEngine = new TemplateEngine(templateDataModel);
     templateEngine.LoadTemplateFromFile("Html.template.html");
     string outputString = templateEngine.CreateStringFromTemplate();

     Console.WriteLine(outputString);
```


### **Template und Model über PropertyInjection** 
```csharp
 TemplateDataModel templateDataModel = new TemplateDataModel
 {
     ProjektName = "Projektname",
     KundeNummer = "1234",
     ProjektLink = "https://google.de",
     ProjektKuerzel = "PKZL"
 };

 string template = "<p>${ProjektName}</p>";
 TemplateEngine templateEngine = new TemplateEngine();
 templateEngine.TemplateDataModel = templateDataModel;
 templateEngine.TemplateString = template;
 Console.WriteLine(templateEngine.CreateStringFromTemplate());

```

### **Template und Model über DependencyInjection** 
```csharp

  TemplateDataModel templateDataModel = new TemplateDataModel
  {
      ProjektName = "Projektname",
      KundeNummer = "1234",
      ProjektLink = "https://google.de",
      ProjektKuerzel = "PKZL"
  };

  string template = "<p>${ProjektName}</p>";
  TemplateEngine templateEngine = new TemplateEngine(templateDataModel,template);
  Console.WriteLine(templateEngine.CreateStringFromTemplate());


```
### **TemplateEngine mit PropertyInjection und Generic Type.**
```csharp
 TemplateDataModel templateDataModel = new TemplateDataModel
 {
     ProjektName = "Projektname",
     KundeNummer = null,
     ProjektLink = "https://google.de",
     ProjektKuerzel = "KZ"
 };

 string template = "<p>{{ProjektName}}</p><p>{{KundeNummer}}</p>";
 TemplateEngine<TemplateDataModel> templateEngine = new TemplateEngine<TemplateDataModel>()
 {
     TemplateDataModel = templateDataModel,
     TemplateString = template,
     OpeningDelimiter = "{{",
     CloseingDelimiter = "}}",
     NullStringValue = "???"
 };
 Console.WriteLine(templateEngine.CreateStringFromTemplate());

```


---

 ## Datentypkompatibilität
 
- ✔ String
- ✔ Byte
- ✔ Short
- ✔ UShort
- ✔ Long
- ✔ ULong
- ✔ SByte
- ✔ Char
- ✔ UInt16
- ✔ Int32
- ✔ UInt64
- ✔ Int16
- ✔ Int32
- ✔ Int64
- ✔ Decimal
- ✔ Double
- ✔ DateTime
- ✔ Boolean
- ❌ Object
- ❌ CustomClasses
- ❌ IList, List, Dictionary, IEnumerable, etc..

 

---

 