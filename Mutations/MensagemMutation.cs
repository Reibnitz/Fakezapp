using Fakezapp.Models;
using Fakezapp.Repositories;
using Fakezapp.Subscriptions;
using HotChocolate.Subscriptions;

namespace Fakezapp.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    public class MensagemMutation
    {
        private readonly IConversasRepository _repository;
        private ITopicEventSender _eventSender;

        public MensagemMutation(IConversasRepository repository, ITopicEventSender eventSender)
        {
            _repository = repository;
            _eventSender = eventSender;
        }

        public bool SendMensagem(string texto, string sender, string idConversa)
        {
            Mensagem mensagem = new(texto, sender, DateTime.Now);
            bool result = _repository.AddMensagem(idConversa, mensagem);

            if (result == true)
            {
                _eventSender.SendAsync(nameof(ConversaSubscription.TodasConversas), mensagem)
                    .ConfigureAwait(false);

                _eventSender.SendAsync(idConversa, mensagem)
                    .ConfigureAwait(false);
            }

            return result;
        }
    }
}
