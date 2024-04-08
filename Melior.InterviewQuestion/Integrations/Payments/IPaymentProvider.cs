using Melior.InterviewQuestion.Types;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melior.InterviewQuestion.Integrations.Payments
{
    public interface IPaymentProvider : IDisposable
    {
        /// <summary>
        /// Calls into the third party and actions the payment
        /// </summary>
        /// <param name="request"></param>
        /// <param name="Account"></param>
        /// <returns></returns>
        MakePaymentResult MakePayment(MakePaymentRequest request, Account account);
    }
}
