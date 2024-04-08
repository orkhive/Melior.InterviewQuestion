using Microsoft.VisualStudio.TestTools.UnitTesting;

using Melior.InterviewQuestion.Types;

namespace Melior.InterviewQuestion.Integrations.Payments.Tests
{
    [TestClass()]
    public class BacsTests
    {
        private Bacs Bacs;

        [TestInitialize] 
        public void TestInitialize()
        {
            Bacs = new Bacs();
        }

        [TestMethod()]
        public void DisposeTest()
        {
            //Arrange

            //Act
            Bacs.Dispose();

            //Assert
            //Nothing To Assert As Providing No Exception We're Good
        }

        [TestMethod()]
        public void MakePaymentTest()
        {
            //Arrange
            var request = new MakePaymentRequest();
            var account = new Account() { AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs };

            //Act
            var res = Bacs.MakePayment(request, account);

            //Assert
            Assert.IsNotNull(res);
            Assert.IsTrue(res.Success);
        }

        [TestMethod()]
        public void MakePayment_NoAccountTest()
        {
            //Arrange
            var request = new MakePaymentRequest();

            //Act
            var res = Bacs.MakePayment(request, null);

            //Assert
            Assert.IsNotNull(res);
            Assert.IsFalse(res.Success);
        }

        [TestMethod()]
        public void MakePayment_BadPaymentSchemeTest()
        {
            //Arrange
            var request = new MakePaymentRequest();
            var account = new Account() { AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments };

            //Act
            var res = Bacs.MakePayment(request, account);

            //Assert
            Assert.IsNotNull(res);
            Assert.IsFalse(res.Success);
        }
    }
}