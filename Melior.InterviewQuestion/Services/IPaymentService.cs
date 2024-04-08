using Melior.InterviewQuestion.Types;

using System;

namespace Melior.InterviewQuestion.Services
{
    public interface IPaymentService : IDisposable
    {
        MakePaymentResult MakePayment(MakePaymentRequest request);
    }
}
