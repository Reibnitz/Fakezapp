using Fakezapp.Models;
using Fakezapp.Repositories;

namespace Fakezapp.Subscriptions
{
    [ExtendObjectType(OperationTypeNames.Subscription)]
    public class ConversaSubscription
    {
        private readonly IConversasRepository _repository;

        public ConversaSubscription(IConversasRepository repository)
        {
            _repository = repository;
        }

        //[Subscribe]
        //public Mensagem ListenConversa([EventMessage] Mensagem mensagem)
        //{
        //    return mensagem;
        //}

        [Subscribe]
        public Mensagem ListenConversa([Topic] string idConversa, [EventMessage] Mensagem mensagem)
        {
            return mensagem;
        }
    }
}
