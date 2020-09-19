using NC.GameStore.CommonRepository.Interfaces;
using NC.GameStore.Domain;
using NC.GameStore.Exception;
using NC.GameStore.Service.Interfaces;
using System;
using System.Collections.Generic;

namespace NC.GameStore.Service
{
    public class BoardGameService : IBoardGameService
    {
        private readonly IBoardGameRepository _boardGameRepository;

        public BoardGameService(IBoardGameRepository boardGameRepository)
        {
            _boardGameRepository = boardGameRepository;
        }

        public IEnumerable<BoardGame> GetAll()
        {
            return _boardGameRepository.GetAll();
        }

        public void Delete(BoardGame entity)
        {
            if (entity is null)
                throw new BoardGameServiceException("Game not available.");

            _boardGameRepository.Remove(entity);
        }

        public void Edit(BoardGame entity)
        {
            CreateOrEdit(entity, true);
        }

        public void Create(BoardGame entity)
        {
            CreateOrEdit(entity, false);
        }

        public BoardGame Get(int id)
        {
            if (id <= Decimal.Zero)
                throw new BoardGameServiceException("Invalid identifier.");

            var entity = _boardGameRepository.Get(id);

            if (entity is null)
                throw new BoardGameServiceException("Game not available.");

            return entity;
        }

        private void CreateOrEdit(BoardGame entity, bool isUpdate)
        {
            // Checks domain validation
            if (entity is null || !entity.IsValid())
                throw new BoardGameServiceException("Game not available.");

            // Checks if the title already exists
            var entityByName = _boardGameRepository.Get(entity.Title);
            if (!(entityByName is null) && entity.Id != entityByName.Id)
                throw new BoardGameServiceException("It is not possible to register a game with the same title.");

            if (isUpdate)
            {
                // Checks if it still exists
                if (_boardGameRepository.Get(entity.Id) is null)
                    throw new BoardGameServiceException("Register not found.");

                // Updates
                _boardGameRepository.Update(entity);
            }
            else
            {
                // Adds
                _boardGameRepository.Add(entity);
            }
                
        }
    }
}