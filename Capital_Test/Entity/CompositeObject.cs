using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Capital_Test.ViewModels;
using CommunityToolkit.Mvvm.Input;

namespace Capital_Test.Entity;

public class CompositeObject
{
    public PnlHour First { get; set; }
    public PnlHour Second { get; set; }

    public ICommand FirstCommand { get; private set; }
    public ICommand SecondCommand { get; private set; }

    public CompositeObject()
    {
        FirstCommand = new RelayCommand(FirstAction);
        SecondCommand = new RelayCommand(SecondAction);
    }

    private void FirstAction()
    {
        
    }

    private void SecondAction()
    {
        // Действие для второй команды
    }

}
