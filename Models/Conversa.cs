namespace Fakezapp.Models
{
    public class Conversa
    {
        public string Id { get; set; }
        public IList<Mensagem> Mensagens { get; set; }

        public Conversa(string idConversa, IList<Mensagem> mensagens)
        {
            Mensagens = mensagens;
            Id = idConversa;
        }
    }
}
