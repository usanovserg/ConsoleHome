using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OsEngine.Commands
{
    public class DelegateCommand : ICommand
    /*Класс для реализации интерфейса ICommand,который используется в MVVM 
     * для привязки действий кнопок и других элементов к методам во ViewModel. 
     * Обеспечивает способ вызывать методы из ViewModel напрямую из XAML, 
     * без обработчиков событий в code-behind */
    {
        public DelegateCommand(DelegateFunction function)
        {
            _function = function;
        }

        public delegate void DelegateFunction(object obj);

        public event EventHandler CanExecuteChanged; /*Событие, которое уведомляет о том, 
                                                      * что результат метода CanExecute изменился */

        //=====================================================
        private DelegateFunction _function;

        //=====================================================

        /// <summary>
        /// Определяет, может ли команда быть выполнена
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Вызывает метод (делегат _function), который был передан в конструктор
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _function?.Invoke(parameter);
        }


    }
}
