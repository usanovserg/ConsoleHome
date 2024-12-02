using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


namespace ConsoleTrainingBot
{
    /// <summary>
    /// Интерфейс для классов, которые могут быть переданы в IntervalTimerManager
    /// TimerElapsed - метод, который будет вызываться с заданным в IntervalTimerManager интервалом
    /// </summary>
    public interface ITimerInterface
    {
        void TimerElapsed(object sender, ElapsedEventArgs e);
    }
    /// <summary>
    /// Менеджер времени. Принимает в конструктор класс и интервал через который должен запускаться метод TimerElapsed
    /// TimerElapsed наследуется от интерфейса ITimerInterface
    /// Класс, который передается в шаблон должен также наследовать от ITimerInterface с определением метода TimerElapsed
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class IntervalTimerManager <T> : System.Timers.Timer where T : class, ITimerInterface
    {
        System.Timers.Timer timer_;
        //public T ExternalClass { get; private set; }
        public IntervalTimerManager(T external_class, double interval_timer)   
        {
            timer_ = new Timer(interval_timer);
            //ExternalClass = external_class;
            timer_.Elapsed += (sender, e) => external_class.TimerElapsed(sender, e);
            timer_.Start();
        }
    }
}
