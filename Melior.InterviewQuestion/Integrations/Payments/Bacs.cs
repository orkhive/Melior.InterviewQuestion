using Melior.InterviewQuestion.Types;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Melior.InterviewQuestion.Integrations.Payments
{
    public class Bacs : IPaymentProvider
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
            else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
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
