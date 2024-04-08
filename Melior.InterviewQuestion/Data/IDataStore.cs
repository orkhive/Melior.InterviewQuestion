using Melior.InterviewQuestion.Types;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melior.InterviewQuestion.Data
{
    public interface IDataStore : IDisposable
    {
        Account GetAccount(string accountNumber);
        void UpdateAccount(Account account);
    }
}
