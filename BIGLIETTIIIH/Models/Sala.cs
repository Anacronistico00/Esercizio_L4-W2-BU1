namespace BIGLIETTIIIH.Models
{
    public class Sala
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int PostiTotali { get; set; } = 120;
        public int BigliettiVenduti { get; set; } = 0;
        public int BigliettiRidotti { get; set; } = 0;
    }
}
