using NC.GameStore.Domain;
using System.Collections.Generic;

namespace NC.GameStore.Service.Interfaces
{
    public interface IBoardGameService
    {
        BoardGame Get(int id);

        IEnumerable<BoardGame> GetAll();

        void Delete(BoardGame entity);

        void Edit(BoardGame entity);

        void Create(BoardGame entity);
    }
}
