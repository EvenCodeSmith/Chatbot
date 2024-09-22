namespace WPFCHATBOT.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using Xunit;

    namespace WPFCHATBOT.Tests
    {
        public class StorageTests
        {
            [Fact]
            public void Load_InitializesMessages_FromFile()
            {
                // Arrange
                var storage = new Storage();

                // Act
                var keywords = storage.GetAllKeywords();

                // Assert
                Assert.NotEmpty(keywords);
                Assert.Contains("Hallo", keywords);
            }

            [Fact]
            public void FindAnswer_Returns_CorrectAnswer_ForKeyword()
            {
                // Arrange
                var storage = new Storage();

                // Act
                var answer = storage.FindAnswer("Hallo");

                // Assert
                Assert.Equal("Hallo! Wie kann ich Ihnen helfen?", answer);
            }
        }
    }
}