using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.IO;


namespace WPFCHATBOT
{
    /// <summary>
    /// Die Storage Klasse verwaltet das Laden der Schlüsselwort Antwort Paare aus einer Datei.
    /// </summary>
    public class Storage
    {
        private List<Message> messages;  // Aggregation: Storage enthält viele Message-Objekte

        /// <summary>
        /// Konstruktor der Storage Klasse. Richtet die Nachrichtenliste ein und lädt die Daten.
        /// </summary>
        public Storage()
        {
            messages = new List<Message>();
            Load();
        }

        /// <summary>
        /// Liest die Schlüsselwort Antwort Paare aus einer Textdatei und speichert sie als Message Objekte.
        /// </summary>
        public void Load()
        {
            // Basisverzeichnis erhalten, in dem die ausführbare Datei ausgeführt wird
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Mit Path.Combine auf die keywords.txt-Datei im Ordner Resources verweisen
            string filePath = Path.Combine(baseDirectory, "Resources", "keywords.txt");

            try
            {
                if (File.Exists(filePath))
                {
                    foreach (var line in File.ReadLines(filePath))
                    {
                        var parts = line.Split('=');
                        if (parts.Length == 2)
                        {
                            messages.Add(new Message(parts[0].Trim(), parts[1].Trim()));  // Schlüsselwort-Antwort-Paare als Message-Objekte speichern
                        }
                    }
                }
                else
                {
                    throw new FileNotFoundException($"Die Datei {filePath} wurde nicht gefunden.");
                }
            }
            catch (Exception ex)
            {
                // Fehler behandeln und eine Meldung anzeigen
                Console.WriteLine($"Fehler beim Laden der Datei: {ex.Message}");
            }
        }

        /// <summary>
        /// Sucht die passende Antwort für das gegebene Schlüsselwort.
        /// </summary>
        /// <param name="keyword">Das vom Benutzer eingegebene Schlüsselwort.</param>
        /// <returns>Die passende Antwort oder eine Standardmeldung, falls das Schlüsselwort nicht gefunden wird.</returns>
        public string FindAnswer(string keyword)
        {
            foreach (var message in messages)
            {
                if (message.Keyword.Equals(keyword, StringComparison.OrdinalIgnoreCase))  // Groß-/Kleinschreibung ignorieren
                {
                    return message.Answer;
                }
            }
            return null;
        }

        /// <summary>
        /// Gibt alle Schlüsselwörter zurück.
        /// </summary>
        /// <returns>Eine Liste mit allen Schlüsselwörtern.</returns>
        public List<string> GetAllKeywords()
        {
            return messages.Select(m => m.Keyword).ToList();  // Extrahiert alle Schlüsselwörter aus den Message-Objekten
        }
    }
}
