using Fakezapp.Models;

namespace Fakezapp.Repositories
{
    public interface IMensagensRepository
    {
        bool AddMensagem(Mensagem mensagem);
        IEnumerable<Mensagem> GetMensagens();
    }
}