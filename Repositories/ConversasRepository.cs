using Fakezapp.Models;

namespace Fakezapp.Repositories
{
    public class ConversasRepository : IConversasRepository
    {
        public IList<Conversa> Conversas = new List<Conversa>
        {
            new Conversa(idConversa: "1", mensagens: new List<Mensagem>()),
            new Conversa(idConversa: "2", mensagens: new List<Mensagem>()),
            new Conversa(idConversa: "3", mensagens: new List<Mensagem>())
        };

        public bool AddMensagem(string idConversa, Mensagem mensagem)
        {
            Conversa? conversa = Conversas.FirstOrDefault(conversa => conversa.Id == idConversa);

            if (conversa == null)
                return false;

            conversa.Mensagens.Add(mensagem);

            return true;
        }

        public IEnumerable<Mensagem> GetMensagens(string idConversa)
        {
            Conversa? conversa = Conversas.FirstOrDefault(conversa => conversa.Id == idConversa);
            if (conversa == null)
                return new List<Mensagem>();

            return conversa.Mensagens;
        }
    }
}
