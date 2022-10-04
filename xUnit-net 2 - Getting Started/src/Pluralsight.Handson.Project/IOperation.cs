namespace Pluralsight.Handson.Project.Calculator
{
    public interface IOperation
    {

        double Add(double firstValue, double secondValue);

        double Subtract(double firstValue, double secondValue);

        double Multiply(double firstValue, double secondValue);

        double Divide(double firstValue, double secondValue);
    }
}