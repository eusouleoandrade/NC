using NC.GameStore.Exception;
using Xunit;

namespace NC.GameStore.Test
{
    public class BoardGameServiceDelete : BoardGameServiceTest
    {
        [Fact]
        public void CheckDelete()
        {
            // Arranje
            var boardGame = _boardGameService.Get(1);

            // Act
            _boardGameService.Delete(boardGame);

            // Assert
            string expectedMessage = "Game not available.";
            void actAssert() => _boardGameService.Get(boardGame.Id);
            BoardGameServiceException exception = Assert.Throws<BoardGameServiceException>(actAssert);
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}