using NC.GameStore.CommonRepository.Interfaces;
using NC.GameStore.Domain;
using NC.GameStore.Exception;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NC.GameStorre.MockRepository
{
    public class BoardGameMockRepository : MockRopository, IBoardGameRepository
    {
        private readonly List<BoardGame> _data;

        public BoardGameMockRepository()
        {
            _data = _boardGameData.ToList();
        }

        public void Add(BoardGame entity)
        {
            try
            {
                entity.Id = entity.Id <= Decimal.Zero ? _data.Select(s => s.Id).Max() + 1 : entity.Id;
                _data.Add(entity);
            }
            catch (Exception ex)
            {
                throw new MockRepositoryException(ex);
            }
        }

        public void Remove(BoardGame entity)
        {
            try
            {
                _data.Remove(entity);
            }
            catch (Exception ex)
            {
                throw new MockRepositoryException(ex);
            }
        }

        public void Update(BoardGame entity)
        {
            try
            {
                Remove(Get(entity.Id));
                Add(entity);
            }
            catch (Exception ex)
            {
                throw new MockRepositoryException(ex);
            }
        }

        public BoardGame Get(int id)
        {
            try
            {
                return _data.FirstOrDefault(f => f.Id == id);
            }
            catch (Exception ex)
            {
                throw new MockRepositoryException(ex);
            }
        }

        public BoardGame Get(string title)
        {
            try
            {
                return _data.FirstOrDefault(f => f.Title == title);
            }
            catch (Exception ex)
            {
                throw new MockRepositoryException(ex);
            }
        }

        public IEnumerable<BoardGame> GetAll()
        {
            try
            {
                return _data;
            }
            catch (Exception ex)
            {
                throw new MockRepositoryException(ex);
            }
        }
    }
}