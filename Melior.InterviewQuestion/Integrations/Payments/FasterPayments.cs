using Melior.InterviewQuestion.Types;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melior.InterviewQuestion.Integrations.Payments
{
    internal class FasterPayments : IPaymentProvider
    {
        public void Dispose()
        {
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request, Account account)
        {
            var result = new MakePaymentResult();
            if (account == null)
            {
                result.Success = false;
            }
            else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
            {
                result.Success = false;
            }
            else if (account.Balance < request.Amount)
            {
                result.Success = false;
            }
            else
            {
                result.Success = true;
            }
            return result;
        }
    }
}
