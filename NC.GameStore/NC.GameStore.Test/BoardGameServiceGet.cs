using Xunit;

namespace NC.GameStore.Test
{
    public class BoardGameServiceGet : BoardGameServiceTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ReturnValue(int id)
        {
            // Arranje / Act
            var boardGame = _boardGameService.Get(id);

            // Assert
            Assert.NotNull(boardGame);
        }

        [Fact]
        public void ReturnCandLandValue()
        {
            // Arranje
            int id = 1;

            // Act
            var boardGame = _boardGameService.Get(id);

            // Assert
            string expectTitle = "Candy Land";
            Assert.Equal(expectTitle, boardGame.Title);
        }

        [Fact]
        public void ReturnSorryValue()
        {
            // Arranje
            int id = 2;

            // Act
            var boardGame = _boardGameService.Get(id);

            // Assert
            string expectedTitle = "Sorry!";
            Assert.Equal(expectedTitle, boardGame.Title);
        }
    }
}