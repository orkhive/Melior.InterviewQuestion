using Microsoft.VisualStudio.TestTools.UnitTesting;
using Melior.InterviewQuestion.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melior.InterviewQuestion.Data;
using Moq;
using Melior.InterviewQuestion.Integrations.Payments;
using Melior.InterviewQuestion.Types;

namespace Melior.InterviewQuestion.Services.Tests
{
    [TestClass()]
    public class PaymentServiceTests
    {
        private Mock<IDataStore> dataStoreMock;
        private Mock<IPaymentProvider> paymentProviderMock;
        private Mock<IPaymentProviderFactory> paymentProviderFactoryMock;
        private PaymentService service;

        [TestInitialize]
        public void Init()
        {
            dataStoreMock = new Mock<IDataStore>();
            paymentProviderMock = new Mock<IPaymentProvider>();

            paymentProviderFactoryMock = new Mock<IPaymentProviderFactory>();
            paymentProviderFactoryMock.Setup(f => f.GetPaymentProvider(It.IsAny<PaymentScheme>())).Returns(paymentProviderMock.Object);

            service = new PaymentService(dataStoreMock.Object, paymentProviderFactoryMock.Object);
        }

        [TestMethod()]
        public void PaymentServiceTest()
        {
            //Given I create a PaymentService object
            //When the object is created
            //Then the object should create without error
            Assert.IsNotNull(service);
        }

        [TestMethod()]
        public void MakePaymentTest()
        {
            //Given I have a PaymentService object
            //When I attempt to take a BACS payment
            //Then the payment should be taken without error

            //Arrange
            var paymentRequest = new Types.MakePaymentRequest() { CreditorAccountNumber = "TEST1", DebtorAccountNumber = "TEST2", Amount = 10m, PaymentDate = DateTime.Now, PaymentScheme = Types.PaymentScheme.Bacs };
            var account = new Account() { Balance = 1000m };
            dataStoreMock.Setup(f => f.GetAccount(paymentRequest.DebtorAccountNumber)).Returns(account);
            var accounts = new List<Account>();
            dataStoreMock.Setup(f => f.UpdateAccount(Capture.In(accounts)));
            paymentProviderMock.Setup(f => f.MakePayment(It.IsAny<MakePaymentRequest>(), account)).Returns(new MakePaymentResult() { Success = true });
            var expected = account.Balance - paymentRequest.Amount;

            //Act
            var res = service.MakePayment(paymentRequest);

            //Assert
            Assert.IsNotNull(res);
            Assert.IsTrue(res.Success);
            paymentProviderFactoryMock.Verify(f => f.GetPaymentProvider(paymentRequest.PaymentScheme), Times.Once);
            dataStoreMock.Verify(f => f.GetAccount(paymentRequest.DebtorAccountNumber), Times.Once);

            Assert.AreEqual(1, accounts.Count());
            Assert.AreEqual(expected, accounts[0].Balance);
        }


        [TestMethod()]
        public void MakePayment_BadPaymentProviderTest()
        {
            //Given I have a PaymentService object
            //When I attempt to take a payment that isn't recognised
            //Then the payment shouldn't be taken

            //Arrange
            var paymentRequest = new Types.MakePaymentRequest() { CreditorAccountNumber = "TEST1", DebtorAccountNumber = "TEST2", Amount = 10m, PaymentDate = DateTime.Now, PaymentScheme = Types.PaymentScheme.Bacs };
            paymentProviderFactoryMock.Setup(f => f.GetPaymentProvider(It.IsAny<PaymentScheme>()));

            //Act
            var res = service.MakePayment(paymentRequest);

            //Assert
            Assert.IsNotNull(res);
            Assert.IsFalse(res.Success);
        }

        [TestMethod()]
        public void DisposeTest()
        {
            //Given I have a PaymentService object
            //When I dispose of that object
            //Then all of the depencanies should also be cleared down

            //Arrange

            //Act
            service.Dispose();

            //Assert
            dataStoreMock.Verify(f => f.Dispose(), Times.Once);
            paymentProviderFactoryMock.Verify(f => f.Dispose(), Times.Once);
        }
    }
}