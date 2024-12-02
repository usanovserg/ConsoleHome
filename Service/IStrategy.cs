using ConsoleHome.Model;

namespace ConsoleHome.Service
{
    public interface IStrategy
    {
        void Init();
        void ShowInfo();
        Trade Trade(decimal oldPrice, decimal newPrice);
    }
}
