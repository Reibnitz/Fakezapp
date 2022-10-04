namespace Fakezapp.Models
{
    public class Mensagem
    {
        public string Texto { get; }
        public string Sender { get; }
        public DateTime Timestamp { get; }
        public bool Lido { get; }

        public Mensagem(string texto, string sender, DateTime timestamp, bool lido = false)
        {
            Texto = texto;
            Sender = sender;
            Timestamp = timestamp;
            Lido = lido;
        }
    }
}
