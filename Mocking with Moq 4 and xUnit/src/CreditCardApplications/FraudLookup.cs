namespace CreditCardApplications
{
    public class FraudLookup
    {
        virtual public bool IsFraudRisk(CreditCardApplication application)
        {
            if (application.LastName == "smith")
            {
                return true;
            }

            return false;
        }
    }
}