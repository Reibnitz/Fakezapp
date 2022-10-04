using Fakezapp.Models;
using Fakezapp.Repositories;

namespace Fakezapp.Subscriptions
{
    [ExtendObjectType(OperationTypeNames.Subscription)]
    public class ConversaSubscription
    {
        private readonly IMensagensRepository _repository;

        public ConversaSubscription(IMensagensRepository repository)
        {
            _repository = repository;
        }

        [Subscribe]
        public Mensagem ListenConversa([EventMessage] Mensagem mensagem)
        {
            return mensagem;
        }
    }
}
