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
 TemplateDataModel templateDataModel = new TemplateDataModel# MbSoftLab.TemplateEngine.Core
```csharp
namespace MbSoftLab.TemplateEngine.Core
{
    public class TemplateEngine : TemplateEngine<object>{}

    public class TemplateEngine<T>{...}
}
```
 
 
The TemplateEngine replaces values from properties of C# classes in template strings.
The C# class with the data holding properties is called the TemplateDataModel class. 
The string with the placeholders is called a string template or template string.


You can bind the value from your C-property to your string template by using `${YourPropertyName}`. 
You can set a custom delimiters. Use the `OpeningDelimiter` and `CloseingDelimiter` properties to handle this.

Also you can access parameterless public methods of TemplateDataModell classes by using `${MethodName()}` in your template. 


The default is `${` for the start delimiter and `}` for the end delimiter.
 
![picture of what can done by the TemplateEngine](https://musikbeatssoftwarelab.bitbucket.io/MbSoftlab.TemplateEngine.Core/ProblemDescription.PNG "What the Engine can do")

```csharp
 Person person = new Person
 {
     FirstName = "Jo",
     LastName="Doe"
 };

string template = "<MyTag>${FirstName}, ${LastName}</MyTag>";

TemplateEngine templateEngine = new TemplateEngine(person,template);
string outputString = templateEngine.CreateStringFromTemplate();

Console.Write(outputString); // Output: <MyTag>Jo, Doe</MyTag> 
```

---


## Methods 
|Methodname                                                            |Description                                                     |
|------------------------------------------------------------------------|-----------------------------------------------------------------|
|`string CreateStringFromTemplate( [string template] )`                |*Creates a String from Datamodell and Template*   |
|`void LoadTemplateFromFile(string filename)`                          |*Loads a Stringtemplate from file*.                  |
|`TemplateEngine()`                                                   |Constructor         |
|`TemplateEngine(object templateDataModel, string stringTemplate)`     |Constructor         |
|`TemplateEngine(object templateDataModel)`                            |Constructor         |
|`TemplateEngine< T >()`                                               |Constructor         |
|`TemplateEngine< T >(T templateDataModel, string stringTemplate)`     |Constructor         |
|`TemplateEngine< T >(T templateDataModel)`                            |Constructor         |

---

## Propertys 

|Propertyname                            |Datatype          |Description                                                           |
|----------------------------------------|------------------|-------------------------------------------------------------------------|
|`OpeningDelimiter`                    |String            |Set the beginning delimiter for propertyreplacement                     |
|`CloseingDelimiter`                   |String            |Set the ending delimiter for propertyreplacement                       |
|`TemplateDataModel`                      |Generic / object  |Modell with Properys for Dataholding                                     |
|`TemplateString`                      |string            |Templatestring                                                     |
|`NullStringValue`                     |string            |String for NULL-Values                                              |

---

## Exampels

### **Load the template and fill with Data from modell**
```csharp
// Create a modell Class for Data
 TemplateDataModel templateDataModel = new TemplateDataModel
 {
     ProjektName = "Projektname"
 };

string template = "<MyTag>${ProjektName}</MyTag>";

TemplateEngine templateEngine = new TemplateEngine(templateDataModel,template);
string outputString = templateEngine.CreateStringFromTemplate();

Console.Write(outputString); // Ausgabe: <MyTag>Projektname</MyTag> 
```
### **Load template from file**
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


### **Template and model over PropertyInjection** 
```csharp
 TemplateDataModel templateDataModel = new TemplateDataModel
 {
     ProjectName = "Projectname",
     KundeNummer = "1234",
     ProjektLink = "https://google.de",
     ProjektKuerzel = "PKZL"
 };

 string template = "<p>${ProjectName}</p>";
 TemplateEngine templateEngine = new TemplateEngine();
 templateEngine.TemplateDataModel = templateDataModel;
 templateEngine.TemplateString = template;
 Console.WriteLine(templateEngine.CreateStringFromTemplate());

```

### **Template and model over DependencyInjection** 
```csharp

  TemplateDataModel templateDataModel = new TemplateDataModel
  {
      ProjectName = "Projectname",
      KundeNummer = "1234",
      ProjektLink = "https://google.de",
      ProjektKuerzel = "PKZL"
  };

  string template = "<p>${ProjectName}</p>";
  TemplateEngine templateEngine = new TemplateEngine(templateDataModel,template);
  Console.WriteLine(templateEngine.CreateStringFromTemplate());


```
### **TemplateEngine with PropertyInjection and generic type**
```csharp
 TemplateDataModel templateDataModel = new TemplateDataModel
 {
     ProjectName = "Projectname",
     KundeNummer = null,
     ProjektLink = "https://google.de",
     ProjektKuerzel = "KZ"
 };

 string template = "<p>{{ProjectName}}</p><p>{{KundeNummer}}</p>";
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

 ## Datatype compatibility
 
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
 
## Repo 

https://bitbucket.org/MusikBeatsSoftwareLab/mbsoftlab.templateengine/src/master/

---

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

 