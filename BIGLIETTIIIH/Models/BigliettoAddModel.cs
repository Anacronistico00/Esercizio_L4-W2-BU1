using System.ComponentModel.DataAnnotations;

namespace BIGLIETTIIIH.Models
{
    public class BigliettoAddModel
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Il nome è obbligatorio!")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Il nome deve contenere minimo 2 e massimo 20 caratteri")]
        public string? Nome { get; set; }

        [Display(Name = "Cognome")]
        [Required(ErrorMessage = "Il Cognome è obbligatorio!")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Il nome deve contenere minimo 2 e massimo 20 caratteri")]
        public string? Cognome { get; set; }

        [Display(Name = "Sala")]
        [Required(ErrorMessage = "Seleziona una sala!")]
        public Sala? Sala { get; set; }

        public bool IsRidotto { get; set; }
    }
}
