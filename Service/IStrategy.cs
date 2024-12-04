using ConsoleHome.Model;

namespace ConsoleHome.Service
{
    public interface IStrategy
    {
        void Init();
        void ShowInfo();
        Order Trade(decimal oldPrice, decimal newPrice);
    }
}
