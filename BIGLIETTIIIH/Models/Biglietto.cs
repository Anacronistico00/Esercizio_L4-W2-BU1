namespace BIGLIETTIIIH.Models
{
    public class Biglietto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public Sala Sala { get; set; }
        public bool isRidotto { get; set; }
    }
}
