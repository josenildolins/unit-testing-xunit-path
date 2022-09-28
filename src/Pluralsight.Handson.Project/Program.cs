namespace Pluralsight.Handson.Project.Calculator
{
    public class Program
    {
        static void Main(string[] args)
        {
            string TipoOperacao = "";
            bool isNumber = false;

            Console.WriteLine("Choose the operation type");
            Console.WriteLine("");
            Console.WriteLine("Choose 1 to SUM");
            Console.WriteLine("Choose 2 to SUBTRACTION");
            Console.WriteLine("Chosse 3 to MULTIPLY");
            Console.WriteLine("Choose 4 to DIVIDE");
            Console.WriteLine("");
            Console.WriteLine("");
            TipoOperacao = Console.ReadLine();
            isNumber = double.TryParse(TipoOperacao, out double result);

            if (isNumber)
            {

                Console.Clear();

                Console.WriteLine("Digite o valor1");
                double clienteInput = double.Parse(Console.ReadLine());
                Console.WriteLine();

                Console.WriteLine("Digite o valor2");
                double clienteInput2 = double.Parse(Console.ReadLine());


                switch (TipoOperacao)
                {
                    case "1":
                        Console.WriteLine("você escolheu a primeira opção");
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                }

            }
            else
            {
                Console.WriteLine("Valor digitado é invalido");
            }


            Console.WriteLine("Resultado" + " = " + result);
        }
    }
}