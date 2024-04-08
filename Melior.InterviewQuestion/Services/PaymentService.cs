using Melior.InterviewQuestion.Data;
using Melior.InterviewQuestion.Integrations.Payments;
using Melior.InterviewQuestion.Types;

using System;

namespace Melior.InterviewQuestion.Services
{
    public class PaymentService : IPaymentService
    {
        private IDataStore dataStore { get; set; }
        private IPaymentProviderFactory paymentProviderFactory { get; set; }

        public PaymentService(IDataStore dataStore, IPaymentProviderFactory paymentProviderFactory)
        {
            this.dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));

            //You could change this to provide the actual payment provider if you know it before creation, but the factory pattern works well for when you don't know it at creation.
            this.paymentProviderFactory = paymentProviderFactory ?? throw new ArgumentNullException(nameof(paymentProviderFactory));
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            try
            {
                using (var provider = paymentProviderFactory.GetPaymentProvider(request.PaymentScheme))
                {
                    if (provider == null)
                        return new MakePaymentResult() { Success = false };

                    Account account = dataStore.GetAccount(request.DebtorAccountNumber);
                    var result = provider.MakePayment(request, account);
                    if (result.Success)
                    {
                        account.Balance -= request.Amount;
                        dataStore.UpdateAccount(account);
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                //TODO Error Logging
            }
            return new MakePaymentResult() { Success = false };
        }

        public void Dispose()
        {
            dataStore.Dispose();
            dataStore = null;

            paymentProviderFactory.Dispose();
            paymentProviderFactory = null;
        }
    }
}
