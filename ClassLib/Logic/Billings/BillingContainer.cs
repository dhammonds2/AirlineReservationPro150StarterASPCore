using ClassLib.Data;
using ClassLib.Interface;
using System.Collections.Generic;
using System.Linq;

namespace ClassLib.Logic
{
    public class BillingContainer
    {
        private readonly FitchData<BillingObject> billingContainer;

        public BillingContainer(FitchData<BillingObject> billingContainer)
        {
            this.billingContainer = billingContainer;
        }
        public Bill getBillingById(int id) => new Bill(billingContainer.getById(id));
        public Bill verifyLogin(string email, string password) => new Bill(billingContainer.verifyLogin(email, new PasswordHashing().passwordHash256(password)));
        public IEnumerable<Bill> getAllBillings() => billingContainer.getAll().Select(billingDto => new Bill(billingDto));
        public IEnumerable<Bill> getAllBillingsById(int id) => billingContainer.getAllByCustomer(id).Select(billingDto => new Bill(billingDto));
    }
}
