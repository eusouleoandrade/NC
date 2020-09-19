using NC.GameStore.Domain;
using System.Collections.Generic;

namespace NC.GameStore.CommonRepository.Interfaces
{
    public interface IBoardGameRepository
    {
        BoardGame Get(int id);

        BoardGame Get(string title);

        IEnumerable<BoardGame> GetAll();

        void Update(BoardGame entity);

        void Remove(BoardGame entity);

        void Add(BoardGame entity);
    }
}
