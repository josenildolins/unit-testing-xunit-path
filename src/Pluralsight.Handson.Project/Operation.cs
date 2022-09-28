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

        public int Divide(int firstValue, int secondValue)
        {
            try
            {
                return  firstValue / secondValue;
               
            }
            catch (Exception)
            {

                throw;
            }   
           
        }   
    }
}