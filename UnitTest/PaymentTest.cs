using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RevStack.AuthorizeDotNet.Service;
using RevStack.AuthorizeDotNet.Repository;
using RevStack.AuthorizeDotNet.Context;
using RevStack.Payment;
using RevStack.Payment.Model;
using RevStack.AuthorizeDotNet.Model;
using System.Collections.Generic;
using System.Linq;
using RevStack.AuthorizeDotNet.Model.Gateway;

namespace UnitTest
{
    [TestClass]
    public class PaymentTest
    {
        private readonly ServiceMode serviceMode;
        private readonly AuthorizeDotNetContext context;
        private readonly AuthorizeDotNetPaymentRepository repository;
        private readonly AuthorizeDotNetPaymentService service;

        private readonly string apiLoginId = "";
        private readonly string transactionKey = "";

        public PaymentTest()
        {
            serviceMode = ServiceMode.Sandbox;
            context = new AuthorizeDotNetContext(apiLoginId, transactionKey, "USD", serviceMode);
            repository = new AuthorizeDotNetPaymentRepository(context);
            service = new AuthorizeDotNetPaymentService(repository);
        }
        
        [TestMethod]
        public void Charge()
        {
            ICustomer customer = new Customer();
            customer.Address = "555 Main St.";
            customer.City = "Charlotte";
            customer.Country = "US";
            customer.Email = "***@****.com";
            customer.FirstName = "Bob";
            customer.LastName = "Biggs";
            customer.Phone = "5555555555";
            customer.StateOrProvince = "NC";
            customer.Zipcode = "28202";

            ICreditCard creditCard = new CreditCard();
            creditCard.CardNumber = "4111111111111111";
            creditCard.CVV = "123";
            creditCard.ExpirationMonth = "02";
            creditCard.ExpirationYear = "2020";

            IShipping shipping = new Shipping();
            shipping.Address = "555 Main St.";
            shipping.City = "Charlotte";
            shipping.Country = "US";
            shipping.Email = "***@****.com";
            shipping.FirstName = "Bog";
            shipping.LastName = "Biggs";
            shipping.Phone = "5555555555";
            shipping.StateOrProvince = "NC";
            shipping.Zipcode = "28202";

            ICharge charge = new Charge();
            charge.Customer = customer;
            charge.Shipping = shipping;
            charge.CreditCard = creditCard;
            charge.Amount = 10;

            var response = service.Charge<GatewayResponse>(charge);
            Assert.AreEqual(true, response.Approved);
        }

        [TestMethod]
        public void AuthorizeAndCapture()
        {
            ICustomer customer = new Customer();
            customer.Address = "555 Main St.";
            customer.City = "Charlotte";
            customer.Country = "US";
            customer.Email = "***@****.com";
            customer.FirstName = "Bob";
            customer.LastName = "McDougle";
            customer.Phone = "5555555555";
            customer.StateOrProvince = "NC";
            customer.Zipcode = "28202";

            ICreditCard creditCard = new CreditCard();
            creditCard.CardNumber = "4111111111111111";
            creditCard.CVV = "123";
            creditCard.ExpirationMonth = "02";
            creditCard.ExpirationYear = "2020";

            IShipping shipping = new Shipping();
            shipping.Address = "555 Main St.";
            shipping.City = "Charlotte";
            shipping.Country = "US";
            shipping.Email = "***@****.com";
            shipping.FirstName = "Bog";
            shipping.LastName = "Biggs";
            shipping.Phone = "5555555555";
            shipping.StateOrProvince = "NC";
            shipping.Zipcode = "28202";

            IAuthorize authorize = new Authorize();
            authorize.Customer = customer;
            authorize.Shipping = shipping;
            authorize.CreditCard = creditCard;
            authorize.Amount = 10;

            var response = service.Authorize<GatewayResponse>(authorize);
            Assert.AreEqual(true, response.Approved);

            ICapture capture = new Capture();
            capture.Amount = 10;
            capture.Id = response.Id;

            response = service.Capture<GatewayResponse>(capture);
            Assert.AreEqual(true, response.Approved);
        }

        [TestMethod]
        public void ChargeAndCredit()
        {
            ICustomer customer = new Customer();
            customer.Address = "555 Main St.";
            customer.City = "Charlotte";
            customer.Country = "US";
            customer.Email = "***@****.com";
            customer.FirstName = "Bob4";
            customer.LastName = "McDougle4";
            customer.Phone = "5555555555";
            customer.StateOrProvince = "NC";
            customer.Zipcode = "28202";

            ICreditCard creditCard = new CreditCard();
            creditCard.CardNumber = "4111111111111111";
            creditCard.CVV = "234";
            creditCard.ExpirationMonth = "02";
            creditCard.ExpirationYear = "2020";

            IShipping shipping = new Shipping();
            shipping.Address = "444 Main St.";
            shipping.City = "Charlotte";
            shipping.Country = "US";
            shipping.Email = "***@****.com";
            shipping.FirstName = "Bog";
            shipping.LastName = "Biggs";
            shipping.Phone = "5555555555";
            shipping.StateOrProvince = "NC";
            shipping.Zipcode = "28202";

            ICharge charge = new Charge();
            charge.Customer = customer;
            charge.Shipping = shipping;
            charge.CreditCard = creditCard;
            charge.Amount = 10;

            var response = service.Charge<GatewayResponse>(charge);
            Assert.AreEqual(true, response.Approved);

            Credit credit = new Credit();
            credit.Amount = 8;
            credit.Id = response.Id;
            credit.CardNumber = creditCard.CardNumber;
            credit.ExpirationDate = creditCard.ExpirationMonth + creditCard.ExpirationYear;

            response = service.Credit<GatewayResponse>(credit);
            Assert.AreEqual(true, response.Approved);
        }

        [TestMethod]
        public void ChargeAndVoid()
        {
            ICustomer customer = new Customer();
            customer.Address = "555 Main St.";
            customer.City = "Charlotte";
            customer.Country = "US";
            customer.Email = "***@****.com";
            customer.FirstName = "Bob3";
            customer.LastName = "McDougle3";
            customer.Phone = "5555555555";
            customer.StateOrProvince = "NC";
            customer.Zipcode = "28202";

            ICreditCard creditCard = new CreditCard();
            creditCard.CardNumber = "4111111111111111";
            creditCard.CVV = "123";
            creditCard.ExpirationMonth = "02";
            creditCard.ExpirationYear = "2020";

            IShipping shipping = new Shipping();
            shipping.Address = "555 Main St.";
            shipping.City = "Charlotte";
            shipping.Country = "US";
            shipping.Email = "***@****.com";
            shipping.FirstName = "Bog";
            shipping.LastName = "Biggs";
            shipping.Phone = "5555555555";
            shipping.StateOrProvince = "NC";
            shipping.Zipcode = "28202";

            ICharge charge = new Charge();
            charge.Customer = customer;
            charge.Shipping = shipping;
            charge.CreditCard = creditCard;
            charge.Amount = 10;

            var response = service.Charge<GatewayResponse>(charge);
            Assert.AreEqual(true, response.Approved);

            IVoid @void = new RevStack.AuthorizeDotNet.Model.Void();
            @void.Amount = 10;
            @void.Id = response.Id;

            response = service.Void<GatewayResponse>(@void);
            Assert.AreEqual(true, response.Approved);
        }

        [TestMethod]
        public void GetById()
        {
            var transaction = new Transaction();
            transaction.Id = "60026525577";

            var response = service.GetById<Payment>(transaction);
            Assert.AreEqual(true, response.Approved);
        }

        [TestMethod]
        public void Get()
        {
            ITransactions batch = new Transactions();
            batch.BatchId = "";

            var transactions = service.Get<IEnumerable<Payment>>(batch);
            Assert.AreNotEqual(0, transactions.Count());
        }
    }
}
