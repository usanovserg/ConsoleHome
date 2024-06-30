using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Capital_Test.Entity;

public class CheckBoxes: ObservableObject
{
    public CheckBoxes()
    {
        //GetCheckBoxes = new ObservableCollection<bool>(IsCheckedFix, IsCheckedCap, IsCheckedProg, IsCheckedDown);

        CheckBoxesCollection = [isCheckedFix, isCheckedCap, isCheckedProg, isCheckedDown];

    }

    public ObservableCollection<bool> CheckBoxesCollection = [];

    public ObservableCollection<bool> GetCheckBoxesCollection( )
    {           
        return CheckBoxesCollection;
    }

    public bool isCheckedFix { get; set; }
    public bool isCheckedCap;
    public bool isCheckedProg;
    public bool isCheckedDown;

   // ObservableCollection<bool> GetCheckBoxes = [IsCheckedFix, IsCheckedCap, IsCheckedProg, IsCheckedDown];
}


