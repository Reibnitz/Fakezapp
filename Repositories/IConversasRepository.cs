using Fakezapp.Models;

namespace Fakezapp.Repositories
{
    public interface IConversasRepository
    {
        bool AddMensagem(string idConversa, Mensagem mensagem);
        IEnumerable<Mensagem> GetMensagens(string idConversa);
    }
}