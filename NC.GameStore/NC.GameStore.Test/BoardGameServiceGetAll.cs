using NC.GameStore.CommonRepository.Interfaces;
using NC.GameStore.Service;
using NC.GameStore.Service.Interfaces;
using NC.GameStorre.MockRepository;
using System;
using System.Linq;
using Xunit;

namespace NC.GameStore.Test
{
    public class BoardGameServiceGetAll : BoardGameServiceTest
    {
        [Fact]
        public void ReturnAll()
        {
            // Arranje / Act
            var boardGameList = _boardGameService.GetAll();

            // Assert
            Assert.NotNull(boardGameList);
            Assert.True(boardGameList.Count() > Decimal.Zero);
        }
    }
}
