using NC.GameStore.Domain;
using NC.GameStore.Exception;
using Xunit;

namespace NC.GameStore.Test
{
    public class BoardGameServiceCreate : BoardGameServiceTest
    {
        [Fact]
        public void CheckCreate()
        {
            // Arranje
            BoardGame newBoardGame = new BoardGame
            {
                Title = "New Tile",
                PublishingCompany = "New Company",
                MinPlayers = 1,
                MaxPlayers = 4,
                Price = 1.49m
            };

            // Act
            _boardGameService.Create(newBoardGame);

            // Assert
            var expectedBoardGame = _mockRepository.Get(newBoardGame.Title);
            Assert.NotNull(expectedBoardGame);
            Assert.Equal(expectedBoardGame.Title, newBoardGame.Title);
            Assert.Equal(expectedBoardGame.PublishingCompany, newBoardGame.PublishingCompany);
            Assert.Equal(expectedBoardGame.MinPlayers, newBoardGame.MinPlayers);
            Assert.Equal(expectedBoardGame.MaxPlayers, newBoardGame.MaxPlayers);
            Assert.Equal(expectedBoardGame.Price, newBoardGame.Price);
        }

        [Fact]
        public void CheckCreateWithExceptionReturn()
        {
            // Arranje
            BoardGame newBoardGame = new BoardGame
            {
                Title = "Candy Land",
                PublishingCompany = "Hasbro",
                MinPlayers = 2,
                MaxPlayers = 4,
                Price = 1.0m
            };

            // Act
            void act() => _boardGameService.Create(newBoardGame);

            // Assert
            string expectedMessage = "It is not possible to register a game with the same title.";
            BoardGameServiceException exception = Assert.Throws<BoardGameServiceException>(act);
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
