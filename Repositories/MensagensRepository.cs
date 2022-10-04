using Fakezapp.Models;

namespace Fakezapp.Repositories
{
    public class MensagensRepository : IMensagensRepository
    {
        public IList<Mensagem> Mensagens = new List<Mensagem>
        {
            new Mensagem("Oi", "João", DateTime.Now.AddMinutes(-2)),
            new Mensagem("Tudo bem?", "Bruno", DateTime.Now.AddMinutes(-1)),
            new Mensagem("Tudo!", "Gabriel", DateTime.Now)
        };

        public bool AddMensagem(Mensagem mensagem)
        {
            Mensagens.Add(mensagem);
            return true;
        }

        public IEnumerable<Mensagem> GetMensagens()
        {
            return Mensagens;
        }
    }
}
