namespace Pluralsight.Handson.Project.Calculator
{
    public class Operation : IOperation
    {

        public double Add(double firstValue, double secondValue)
        {
            return firstValue + secondValue;
        }

        public double Subtract(double firstValue, double secondValue)
        {
            return firstValue - secondValue;
        }

        public double Multiply(double firstValue, double secondValue)
        {
            return firstValue * secondValue;
        }

        public double Divide(double firstValue, double secondValue)
        {
            try
            {
                if (secondValue == 0)
                    throw new DivideByZeroException();
                return firstValue / secondValue;

            }
            catch (DivideByZeroException)
            {

                throw;
            }

        }
    }
}