using NC.GameStore.CommonRepository.Interfaces;
using NC.GameStore.Service;
using NC.GameStore.Service.Interfaces;
using NC.GameStorre.MockRepository;

namespace NC.GameStore.Test
{
    public abstract class BoardGameServiceTest
    {
        protected readonly IBoardGameService _boardGameService;
        protected readonly IBoardGameRepository _mockRepository;

        public BoardGameServiceTest()
        {
            _mockRepository = new BoardGameMockRepository();
            _boardGameService = new BoardGameService(_mockRepository);
        }
    }
}
