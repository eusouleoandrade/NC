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

            if(entity is null)
                throw new BoardGameServiceException("Game not available.");

            return entity;
        }

        private void CreateOrEdit(BoardGame entity, bool isUpdate)
        {
            if (entity is null || !entity.IsValid())
            {
                throw new BoardGameServiceException("Game not available.");
            }

            var entityOld = _boardGameRepository.Get(entity.Title);

            if (!(entityOld is null) && entity.Id != entityOld.Id)
            {
                throw new BoardGameServiceException("It is not possible to register a game with the same title.");
            }

            if (isUpdate)
            {
                if(entityOld is null)
                    throw new BoardGameServiceException("register not found.");

                _boardGameRepository.Update(entity);
            }  
            else
                _boardGameRepository.Add(entity);
        }
    }
}