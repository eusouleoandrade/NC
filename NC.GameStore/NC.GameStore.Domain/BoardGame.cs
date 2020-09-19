using System;

namespace NC.GameStore.Domain
{
    public class BoardGame
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string PublishingCompany { get; set; }

        public int MinPlayers { get; set; }

        public int MaxPlayers { get; set; }

        public Decimal Price { get; set; }

        public bool IsValid()
        {
            return !String.IsNullOrEmpty(Title) 
                && !String.IsNullOrEmpty(PublishingCompany)
                && MinPlayers > Decimal.Zero
                && MaxPlayers > Decimal.Zero
                && Price > Decimal.Zero;
        }
    }
}
