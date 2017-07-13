using RevStack.Payment.Model;
using RevStack.Payment.Service;
using RevStack.AuthorizeDotNet.Repository;

namespace RevStack.AuthorizeDotNet.Service
{
    public class AuthorizeDotNetSubscriptionService : ISubscriptionService
    {
        private readonly AuthorizeDotNetSubscriptionRepository _repository;

        public AuthorizeDotNetSubscriptionService(AuthorizeDotNetSubscriptionRepository repository)
        {
            _repository = repository;
        }

        public T Subscribe<T>(ISubscribe subscribe) where T : ISubscription
        {
            return _repository.Subscribe<T>(subscribe);
        }

        public T Update<T>(IUpdateSubscription subscription) where T : ISubscription
        {
            return _repository.Update<T>(subscription);
        }

        public T Cancel<T>(ICancelSubscription subscription) where T : ISubscription
        {
            return _repository.Cancel<T>(subscription);
        }
    }
}
