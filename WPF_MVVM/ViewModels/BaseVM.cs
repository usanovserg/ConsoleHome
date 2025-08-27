using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVVM.ViewModels
{
    public class BaseVM : INotifyPropertyChanged //Реализует интерфейс INotifyPropertyChanged
    {
        /// <summary>
        /// Впомогательный метод, который вызывает событие,сообщая имя изменившегося свойства
        /// </summary>
        /// <param name="prop"></param>
        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        //====================== Event ==============================

        public event PropertyChangedEventHandler PropertyChanged;
        /*Когда свойство в ViewModel изменяется, оно должно уведомить об этом View, 
         * чтобы обновить привязанные элементы интерфейса (например, обновить текст в TextBox).
         * Это уведомление происходит через событие PropertyChanged */
    }
}
