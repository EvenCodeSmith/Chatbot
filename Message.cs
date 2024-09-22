using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace WPFCHATBOT
{
    /// <summary>
    /// Die Message Klasse speichert ein Schlüsselwort und die dazugehörige Antwort.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Das Schlüsselwort.
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// Die Antwort auf das Schlüsselwort.
        /// </summary>
        public string Answer { get; set; }

        /// <summary>
        /// Konstruktor der Message-Klasse.
        /// </summary>
        /// <param name="keyword">Das Schlüsselwort.</param>
        /// <param name="answer">Die Antwort auf das Schlüsselwort.</param>
        public Message(string keyword, string answer)
        {
            Keyword = keyword;
            Answer = answer;
        }
    }
}

