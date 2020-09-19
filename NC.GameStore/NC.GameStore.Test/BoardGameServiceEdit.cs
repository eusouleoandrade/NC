using NC.GameStore.Domain;
using NC.GameStore.Exception;
using Xunit;

namespace NC.GameStore.Test
{
    public class BoardGameServiceEdit : BoardGameServiceTest
    {
        [Fact]
        public void CheckEdit()
        {
            // Arranje
            BoardGame updateBoardGame = new BoardGame
            {
                Id = 3,
                Title = "Ticket to Ride 2",
                PublishingCompany = "Days of Wonder 2",
                MinPlayers = 3,
                MaxPlayers = 4,
                Price = 5.0m
            };

            // Act
            _boardGameService.Edit(updateBoardGame);

            // Assert
            var expectedBoardGame = _boardGameService.Get(updateBoardGame.Id);
            Assert.NotNull(expectedBoardGame);
            Assert.Equal(expectedBoardGame.Title, updateBoardGame.Title);
            Assert.Equal(expectedBoardGame.PublishingCompany, updateBoardGame.PublishingCompany);
            Assert.Equal(expectedBoardGame.MinPlayers, updateBoardGame.MinPlayers);
            Assert.Equal(expectedBoardGame.MaxPlayers, updateBoardGame.MaxPlayers);
            Assert.Equal(expectedBoardGame.Price, updateBoardGame.Price);
        }

        [Fact]
        public void CheckEditWithExceptionReturn()
        {
            // Arranje
            BoardGame updateBoardGame = new BoardGame
            {
                Id = 5,
                Title = "Sequence",
                PublishingCompany = "Z-Man Games 2",
                MinPlayers = 1,
                MaxPlayers = 4,
                Price = 10.50m
            };

            // Act
            void act() => _boardGameService.Edit(updateBoardGame);

            // Assert
            string expectedMessage = "It is not possible to register a game with the same title.";
            BoardGameServiceException exception = Assert.Throws<BoardGameServiceException>(act);
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}