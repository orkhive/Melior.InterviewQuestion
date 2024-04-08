using Melior.InterviewQuestion.Types;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melior.InterviewQuestion.Integrations.Payments
{
    public class Chaps : IPaymentProvider
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
            else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
            {
                result.Success = false;
            }
            else if (account.Status != AccountStatus.Live)
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
