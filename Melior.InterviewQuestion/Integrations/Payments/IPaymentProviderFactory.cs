using Melior.InterviewQuestion.Types;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melior.InterviewQuestion.Integrations.Payments
{
    public interface IPaymentProviderFactory : IDisposable
    {
        IPaymentProvider GetPaymentProvider(PaymentScheme PaymentScheme);
    }
}
