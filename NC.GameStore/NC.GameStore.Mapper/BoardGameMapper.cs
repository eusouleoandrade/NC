using AutoMapper;
using NC.GameStore.Domain;
using NC.GameStore.ViewModel;

namespace NC.GameStore.Mapper
{
    public static class BoardGameMapper
    {
        public static BoardGameViewModel ToBoardGameViewModel(this BoardGame model)
        {
            IMapper mapper = BoardGameForBoardGameViewModelConfig().CreateMapper();
            return mapper.Map<BoardGameViewModel>(model);
        }

        public static BoardGame ToEntity(this BoardGameViewModel viewModel)
        {
            IMapper mapper = BoardGameForBoardGameViewModelConfig().CreateMapper();
            return mapper.Map<BoardGame>(viewModel);
        }

        private static MapperConfiguration BoardGameForBoardGameViewModelConfig()
        {
            return new MapperConfiguration(m =>
            {
                m.CreateMap<BoardGame, BoardGameViewModel>();
                m.CreateMap<BoardGameViewModel, BoardGame>();
            });
        }
    }
}