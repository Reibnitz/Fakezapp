using Fakezapp.Models;
using Fakezapp.Repositories;

namespace Fakezapp.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class ConversaQuery
    {
        private readonly IConversasRepository _repository;

        public ConversaQuery(IConversasRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Mensagem> GetConversa(string idConversa)
        {
            IEnumerable<Mensagem> mensagens = _repository.GetMensagens(idConversa);
            
            return mensagens;
        }
    }
}
