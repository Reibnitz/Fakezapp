using Fakezapp.Models;
using Fakezapp.Repositories;

namespace Fakezapp.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class ConversaQuery
    {
        private readonly IMensagensRepository _repository;

        public ConversaQuery(IMensagensRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Mensagem> GetConversa()
        {
            IEnumerable<Mensagem> mensagens = _repository.GetMensagens();
            
            return mensagens;
        }
    }
}
