using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NC.GameStore.ViewModel
{
    public class BoardGameViewModel
    {
        [DisplayName("Cod.")]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [DisplayName("Publishing Company")]
        public string PublishingCompany { get; set; }

        [Required]
        [DisplayName("Min Players")]
        public int? MinPlayers { get; set; }

        [Required]
        [DisplayName("Max Players")]
        public int? MaxPlayers { get; set; }

        [Required]
        [DisplayName("Price $")]
        public Decimal? Price { get; set; }
    }
}