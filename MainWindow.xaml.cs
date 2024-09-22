using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


using System.Windows;
using System.IO;

namespace WPFCHATBOT
{
    /// <summary>
    /// Das Hauptfenster der Anwendung. Verwaltet die Nutzeraktionen.
    /// </summary>
    public partial class MainWindow : Window
    {
        private string logFilePath = "chatlog.txt";
        private BotEngine botEngine;
        private Storage storage;

        /// <summary>
        /// Konstruktor des Hauptfensters. Initialisiert den BotEngine.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            botEngine = new BotEngine();
            storage = new Storage();
            File.WriteAllText(logFilePath, "Chat Log\n===========\n\n");
        }

        /// <summary>
        /// Behandelt das Schliessen des Fensters und speichert den Chatverlauf.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            // Zeigt beim Schliessen des Fensters eine Bestätigungsmeldung an
            var result = MessageBox.Show("Möchtest du den Chatlog speichern bevor du die Anwendung schliesst ?",
                                         "Schliessbestätigung",
                                         MessageBoxButton.YesNoCancel,
                                         MessageBoxImage.Warning);

            if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;  // Bricht das Schliessen des Fensters ab
                return;
            }

            if (result == MessageBoxResult.No)
            {
                base.OnClosing(e); // Hier wird das Fenster geschlossen
            }

            if (result == MessageBoxResult.Yes)
            {
                // Trigger the Save Chat Log functionality
                SaveLogBeforeExit();
            }

            base.OnClosing(e);  // Hier wird das Fenster geschlossen
        }

        /// <summary>
        /// Implementiert die Funktionalität zum Speichern des Chatverlaufs vor dem Beenden.
        /// </summary>
        private void SaveLogBeforeExit()
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Textdatei (*.txt)|*.txt",
                Title = "Chatverlauf speichern"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                File.Copy(logFilePath, saveFileDialog.FileName);  // Speichert die Log-Datei vor dem Beenden
            }
        }

        /// <summary>
        /// Hilfsmethode zum Protokollieren der Benutzer- und Bot-Nachrichten.
        /// </summary>
        /// <param name="userMessage"></param>
        /// <param name="botResponse"></param>
        private void LogConversation(string userMessage, string botResponse)
        {
            // Aktuellen Zeitstempel abrufen
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Log-Eintrag formatieren
            string logEntry = $"{timestamp}\nBenutzer: {userMessage}\nBot: {botResponse}\n\n";

            // An die Log-Datei anhängen
            File.AppendAllText(logFilePath, logEntry);
        }


        /// <summary>
        /// Verarbeitet die Benutzereingabe und gibt die Antwort im Chatverlauf aus.
        /// </summary>
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string userMessage = userInput.Text;
            string botResponse = botEngine.GetAnswer(userMessage);

            // Benutzereingabe und Bot-Antwort im Chatverlauf anzeigen
            chatHistory.Text += $"Benutzer: {userMessage}\n";
            chatHistory.Text += $"Bot: {botResponse}\n";
            LogConversation(userMessage, botResponse);
            chatHistory.ScrollToEnd();

            // Eingabefeld nach der Nachricht leeren
            userInput.Clear();
        }

        private void userInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string userMessage = userInput.Text;
                string botResponse = botEngine.GetAnswer(userMessage);

                // Benutzereingabe und Bot-Antwort im Chatverlauf anzeigen
                chatHistory.Text += $"Benutzer: {userMessage}\n";
                chatHistory.Text += $"Bot: {botResponse}\n";
                LogConversation(userMessage, botResponse);
                chatHistory.ScrollToEnd();

                // Eingabefeld nach der Nachricht leeren
                userInput.Clear();

                e.Handled = true;
            }
        }


        private void ShowKeywordsButton_Click(object sender, RoutedEventArgs e)
        {
            // Liste der Schlüsselwörter aus der Storage-Klasse abrufen
            List<string> keywords = storage.GetAllKeywords();

            // KeywordsWindow öffnen und Schlüsselwörter sowie Verweis auf MainWindow übergeben
            KeywordsWindow keywordsWindow = new KeywordsWindow(keywords, this);
            keywordsWindow.Show();  // Fenster anzeigen
        }

        /// <summary>
        /// Setzt das Schlüsselwort im Eingabefeld des Hauptfensters.
        /// </summary>
        /// <param name="keyword"></param>
        public void SetKeywordInInputBox(string keyword)
        {
            userInput.Text = keyword;
            userInput.Focus();
        }

        private void CloseProgramButton_Click(object sender, RoutedEventArgs e)
        {
            // Zeigt beim Schliessen des Fensters eine Bestätigungsmeldung an
            var result = MessageBox.Show("Möchtest du den Chatlog speichern bevor du die Anwendung schliesst ?",
                                         "Schliessbestätigung",
                                         MessageBoxButton.YesNoCancel,
                                         MessageBoxImage.Warning);

            if (result == MessageBoxResult.Cancel)
            {
                // Wenn der Benutzer den Vorgang abbricht, wird die Anwendung nicht geschlossen
                return;
            }

            if (result == MessageBoxResult.No)
            {
                this.Close();
            }

            if (result == MessageBoxResult.Yes)
            {
                // Vor dem Beenden den Chatverlauf speichern
                SaveLogBeforeExit();
            }

            // Anwendung schliessen
            this.Close();
        }


        private void SaveLogButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Textdatei (*.txt)|*.txt",
                Title = "Chatverlauf speichern"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                File.Copy(logFilePath, saveFileDialog.FileName);  // Aktuellen Log in die ausgewählte Datei speichern
            }
        }
    }
}
