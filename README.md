# Präsentation: Entwicklung eines einfachen Chatbots mit WPF in C#
 
---
 
## 1. Einleitung
 
**Inhalt der Präsentation?**
- Wir werden uns anschauen, wie ein einfacher Chatbot in C# mit WPF entwickelt wurde.
- Wir gehen Schritt für Schritt durch den Entwicklungsprozess: von der Gestaltung über die Logik bis hin zu den Tests.
 
**Was macht dieser Chatbot?**
- Der Chatbot ist ein textbasierter Bot, der auf Schlüsselwörter in den Nachrichten des Benutzers reagiert.
- Die Benutzeroberfläche wurde mit WPF erstellt, mit einem Benutzerfreundlichen Design

**Aufgaben Aufteilung**

Beren:  Code, Präsentation

Even:   Code, überarbeiteter Code, QOL

Felipe: Code; Präsentation

Yannick:Code, Code Dokumentation
 
---
 
## 2. Aufbau des Chatbots
 
### 2.1. Benutzeroberfläche (Frontend)
- **MainWindow.xaml**: Hier wird das Layout der Benutzeroberfläche definiert.
  Wichtige UI-Elemente:
  - **TextBox (chatHistory)**: Zeigt den Chatverlauf an, ist nur lesbar und scrollt automatisch bei längeren Nachrichten.
  - **TextBox (userInput)**: Ermöglicht die Eingabe von Nachrichten durch den Benutzer.
  - **Button (SendButton)**: Sendet die Benutzernachricht und zeigt die Antwort des Bots an.
 
```xml
<Window x:Class="WPFCHATBOT.MainWindow"
        Title="ChatBot" Height="500" Width="800" Background="#F0F0F0" WindowStartupLocation="CenterScreen">
 
    <Grid Margin="10">
    <Grid.RowDefinitions>
        <RowDefinition Height="*"/>
        <RowDefinition Height="Auto"/>
    </Grid.RowDefinitions>
 
        <!-- Chat Verlauf -->
        <TextBox x:Name="chatHistory"
                 HorizontalAlignment="Stretch"
                 VerticalAlignment="Stretch"
                 IsReadOnly="True"
                 TextWrapping="Wrap"
                 ScrollViewer.VerticalScrollBarVisibility="Auto"
                 Grid.Row="0"/>
 
        <!-- Eingabe und Buttons -->
        <TextBox x:Name="userInput" Grid.Row="1" Width="600"/>
        <Button x:Name="SendButton" Content="Senden" Grid.Row="1" HorizontalAlignment="Right" Width="100"/>
    </Grid>
</Window>
```
- **MainWindow.xaml.cs**: Die Logik für die Benutzeroberfläche.
  - Verarbeitet die Benutzereingaben und leitet sie an den Bot weiter.
 
### 2.2. Logik des Bots (Backend)
- **BotEngine.cs**
  - Der Bot durchsucht die Nachricht des Benutzers nach Schlüsselwörtern.
  - Gibt eine passende Antwort, wenn ein Schlüsselwort gefunden wird.
 
```csharp
public string ProcessMessage(string keyword)
{
    var message = storage.messages.FirstOrDefault(m => m.Keyword == keyword);
    return message != null ? message.Answer : "Entschuldigung, ich habe das nicht verstanden.";
}
```
 
- **Message.cs**: enthält das Modell der Nachricht, die ein Schlüsselwort und die zugehörige Antwort speichert.
 
```csharp
public Message(string keyword, string answer)
{
    Keyword = keyword;
    Answer = answer;
}
```
 
- **Storage.cs**: verwaltet das Laden der Schlüsselwörter und Antworten aus einer Textdatei und speichert sie in Message-Objekten.
 
```csharp
public void Load()
{
    string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "keywords.txt");
    var lines = File.ReadAllLines(filePath);
    foreach (var line in lines)
    {
        var parts = line.Split(':');
        if (parts.Length == 2)
        {
            messages.Add(new Message(parts[0], parts[1]));
        }
    }
}
```
 
---
 
### 2.3 UML-Diagramm
 
![image](https://github.com/user-attachments/assets/25e17c22-d707-4bbb-83a3-2a721926361c)


## 3. Test Unit
 # Unit Tests für das WPF ChatBot Projekt
 
Dieser Abschnitt beschreibt die Unit Tests, die für die `BotEngine` und `Storage` Klassen des WPF ChatBot Projekts geschrieben wurden. Die Tests stellen sicher, dass der Chatbot korrekt auf bekannte und unbekannte Schlüsselwörter reagiert und dass die `Storage` Klasse in der Lage ist, die Daten korrekt aus einer Schlüsselwort-Datei zu laden und abzurufen.
 
## BotEngineTests
 
Die Klasse `BotEngineTests` überprüft das Verhalten der Methode `GetAnswer` in der Klasse `BotEngine`. Diese Tests stellen sicher, dass der Chatbot angemessene Antworten basierend auf dem eingegebenen Schlüsselwort zurückgibt.
 
- **GetAnswer_Returns_CorrectResponse_ForKnownKeyword**:  
  Dieser Test überprüft, ob der Bot die korrekte Antwort für ein bekanntes Schlüsselwort ("Hallo") zurückgibt.
  - **Arrange**: Initialisiert Instanzen von `Storage` und `BotEngine`.
  - **Act**: Ruft `GetAnswer("Hallo")` auf, um eine Benutzereingabe zu simulieren.
  - **Assert**: Überprüft, ob die Antwort der erwarteten Ausgabe entspricht: `"Hallo! Wie kann ich Ihnen helfen?"`.
 
- **GetAnswer_Returns_NotFoundMessage_ForUnknownKeyword**:  
  Dieser Test überprüft, ob der Bot eine Standard-Nachricht zurückgibt, wenn ein unbekanntes Schlüsselwort eingegeben wird.
  - **Arrange**: Initialisiert Instanzen von `Storage` und `BotEngine`.
  - **Act**: Ruft `GetAnswer("UnbekanntesKeyword")` auf, um eine nicht erkannte Eingabe zu simulieren.
  - **Assert**: Überprüft, ob der Bot mit `"Schlüsselwort nicht gefunden."` antwortet, was darauf hinweist, dass das Schlüsselwort nicht im System vorhanden ist.
 
## StorageTests
 
Die Klasse `StorageTests` stellt sicher, dass die `Storage` Klasse die Schlüsselwort-Antwort-Paare korrekt aus einer Datei lädt und die richtigen Antworten basierend auf diesen Schlüsselwörtern abruft.
 
- **Load_InitializesMessages_FromFile**:  
  Dieser Test stellt sicher, dass die `Storage` Klasse die Schlüsselwort-Antwort-Paare erfolgreich aus der Datei lädt.
  - **Arrange**: Initialisiert eine Instanz von `Storage`.
  - **Act**: Ruft `GetAllKeywords()` auf, um die Liste der geladenen Schlüsselwörter abzurufen.
  - **Assert**: Stellt sicher, dass die Liste der Schlüsselwörter nicht leer ist und das Schlüsselwort `"Hallo"` enthält.
 
- **FindAnswer_Returns_CorrectAnswer_ForKeyword**:  
  Dieser Test überprüft, ob die korrekte Antwort für ein bekanntes Schlüsselwort von der `Storage` Klasse zurückgegeben wird.
  - **Arrange**: Initialisiert eine Instanz von `Storage`.
  - **Act**: Ruft `FindAnswer("Hallo")` auf, um die zugehörige Antwort abzurufen.
  - **Assert**: Überprüft, ob die Antwort der erwarteten Antwort entspricht: `"Hallo! Wie kann ich Ihnen helfen?"`.

 
