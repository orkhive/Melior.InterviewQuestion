﻿using Melior.InterviewQuestion.Types;

namespace Melior.InterviewQuestion.Data
{
    public class AccountDataStore : IDataStore
    {
        public void Dispose()
        {
            //Do Nothing
        }

        public Account GetAccount(string accountNumber)
        {
            // Access database to retrieve account, code removed for brevity 
            return new Account();
        }

        public void UpdateAccount(Account account)
        {
            // Update account in database, code removed for brevity
        }
    }
}
