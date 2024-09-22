using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;


namespace WPFCHATBOT
{
    /// <summary>
    /// Die BotEngine Klasse verarbeitet Benutzeranfragen und gibt passende Antworten zurück.
    /// </summary>
    public class BotEngine
    {
        private Storage storage { get;  set; }

        /// <summary>
        /// Konstruktor der BotEngine Klasse. Richtet die Storage Klasse ein, um die Daten zu laden.
        /// </summary>
        public BotEngine()
        {
            storage = new Storage();  // Assoziation: BotEngine verwendet Storage
        }

        /// <summary>
        /// Verarbeitet das vom Benutzer eingegebene Schlüsselwort und gibt die passende Antwort zurück.
        /// </summary>
        /// <param name="keyword">Das vom Benutzer eingegebene Schlüsselwort.</param>
        /// <returns>Die Antwort, falls das Schlüsselwort gefunden wurde, oder eine Standardmeldung.</returns>
        public string GetAnswer(string keyword)
        {
            string response = storage.FindAnswer(keyword);
            if (response == null)
            {
                return "Schlüsselwort nicht gefunden.";  // Standardmeldung, falls das Schlüsselwort nicht gefunden wird
            }
            return response;
        }

    }
}

