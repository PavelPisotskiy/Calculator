using System.Threading.Tasks;

namespace Calculator.Model
{
    public interface ICalculator
    {
        string Calculate(string expression);
        Task<string> CalculateAsync(string expression);
    }
}