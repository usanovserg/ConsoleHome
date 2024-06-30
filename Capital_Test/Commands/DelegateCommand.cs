using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
// ReSharper disable SuggestVarOrType_SimpleTypes

namespace Capital_Test.Commands;

public class DelegateCommand: ICommand
{
    public DelegateCommand(DelegateFunction function)
    {
        _function = function;
    }


    private readonly DelegateFunction _function;
    public delegate void DelegateFunction(object sender);
    public event EventHandler? CanExecuteChanged;


    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        _function?.Invoke(parameter);
    }
}
