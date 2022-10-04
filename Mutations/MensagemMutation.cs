using Fakezapp.Models;
using Fakezapp.Repositories;
using Fakezapp.Subscriptions;
using HotChocolate.Subscriptions;

namespace Fakezapp.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    public class MensagemMutation
    {
        private readonly IMensagensRepository _repository;
        private ITopicEventSender _eventSender;

        public MensagemMutation(IMensagensRepository repository, ITopicEventSender eventSender)
        {
            _repository = repository;
            _eventSender = eventSender;
        }

        public bool SendMensagem(string texto, string sender, string? idConversa)
        {
            Mensagem mensagem = new(texto, sender, DateTime.Now);
            bool result = _repository.AddMensagem(mensagem);

            if (result == true)
            {
                _eventSender.SendAsync(nameof(ConversaSubscription.ListenConversa), mensagem)
                    .ConfigureAwait(false);
            }

            return result;
        }
    }
}
