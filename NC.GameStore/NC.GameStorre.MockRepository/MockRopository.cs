using NC.GameStore.Domain;
using System.Collections.Generic;

namespace NC.GameStorre.MockRepository
{
    public abstract class MockRopository
    {
        protected readonly IEnumerable<BoardGame> _boardGameData;

        public MockRopository()
        {
            _boardGameData = GetBoardGameData();
        }

        private IEnumerable<BoardGame> GetBoardGameData()
        {
            return new List<BoardGame>
            {
                new BoardGame
                    {
                        Id = 1,
                        Title = "Candy Land",
                        PublishingCompany = "Hasbro",
                        MinPlayers = 2,
                        MaxPlayers = 4,
                        Price = 1.0m
                    },
                    new BoardGame
                    {
                        Id = 2,
                        Title = "Sorry!",
                        PublishingCompany = "Hasbro",
                        MinPlayers = 2,
                        MaxPlayers = 4,
                        Price = 2.0m
                    },
                    new BoardGame
                    {
                        Id = 3,
                        Title = "Ticket to Ride",
                        PublishingCompany = "Days of Wonder",
                        MinPlayers = 2,
                        MaxPlayers = 5,
                        Price = 3.0m
                    },
                    new BoardGame
                    {
                        Id = 4,
                        Title = "The Settlers of Catan (Expanded)",
                        PublishingCompany = "Catan Studio",
                        MinPlayers = 2,
                        MaxPlayers = 6,
                        Price = 4.45m
                    },
                    new BoardGame
                    {
                        Id = 5,
                        Title = "Carcasonne",
                        PublishingCompany = "Z-Man Games",
                        MinPlayers = 2,
                        MaxPlayers = 5,
                        Price = 5.50m
                    },
                    new BoardGame
                    {
                        Id = 6,
                        Title = "Sequence",
                        PublishingCompany = "Jax Games",
                        MinPlayers = 2,
                        MaxPlayers = 6,
                        Price = 6.0m
                    }
            };
        }
    }
}