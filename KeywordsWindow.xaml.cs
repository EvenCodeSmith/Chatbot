using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFCHATBOT
{
    /// <summary>
    /// Stellt das Fenster für Schlüsselwörter dar.
    /// </summary>
    public partial class KeywordsWindow : Window
    {
        private MainWindow mainWindow;

        /// <summary>
        /// Konstruktor der KeywordsWindow Klasse.
        /// </summary>
        /// <param name="keywords">Die Liste der Schlüsselwörter.</param>
        /// <param name="mainWindow">Das Hauptfenster.</param>
        public KeywordsWindow(List<string> keywords, MainWindow mainWindow)
        {
            InitializeComponent();
            KeywordsListBox.ItemsSource = keywords;  // Füllt die ListBox mit den Schlüsselwörtern
            this.mainWindow = mainWindow;  // Behält eine Referenz zum Hauptfenster
        }

        /// <summary>
        /// Wird aufgerufen, wenn ein Schlüsselwort in der ListBox doppelt angeklickt wird.
        /// </summary>
        /// <param name="sender">Der Auslöser des Ereignisses.</param>
        /// <param name="e">Die Ereignisargumente.</param>
        private void KeywordsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Das ausgewählte Schlüsselwort abrufen
            string selectedKeyword = (string)KeywordsListBox.SelectedItem;

            if (selectedKeyword != null)
            {
                // Das ausgewählte Schlüsselwort an das Hauptfenster senden
                mainWindow.SetKeywordInInputBox(selectedKeyword);
                this.Close();  // Das Schlüsselwortfenster schließen, nachdem das Schlüsselwort ausgewählt wurde
            }
        }
    }


}
