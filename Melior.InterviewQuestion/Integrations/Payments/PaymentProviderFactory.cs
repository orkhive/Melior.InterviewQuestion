using Melior.InterviewQuestion.Types;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melior.InterviewQuestion.Integrations.Payments
{
    public class PaymentProviderFactory : IPaymentProviderFactory
    {
        public void Dispose()
        {
            //Do Nothing
        }

        public IPaymentProvider GetPaymentProvider(PaymentScheme PaymentScheme)
        {
            switch (PaymentScheme)
            {
                case PaymentScheme.Bacs:
                    return new Bacs();

                case PaymentScheme.FasterPayments:
                    return new FasterPayments();

                case PaymentScheme.Chaps:
                    return new Chaps();

                default:
                    return null;
            }
        }
    }
}
