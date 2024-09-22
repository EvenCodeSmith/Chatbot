using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCHATBOT.Tests
{
    public class BotEngineTests
    {
        [Fact]
        public void GetAnswer_Returns_CorrectResponse_ForKnownKeyword()
        {
            // Arrange
            var storage = new Storage();
            var botEngine = new BotEngine();

            // Act
            var response = botEngine.GetAnswer("Hallo");

            // Assert
            Assert.Equal("Hallo! Wie kann ich Ihnen helfen?", response);
        }

        [Fact]
        public void GetAnswer_Returns_NotFoundMessage_ForUnknownKeyword()
        {
            // Arrange
            var storage = new Storage();
            var botEngine = new BotEngine();

            // Act
            var response = botEngine.GetAnswer("UnbekanntesKeyword");

            // Assert
            Assert.Equal("Schlüsselwort nicht gefunden.", response);
        }
    }
}
