using GridBotMVVM.Entity;
using GridBotMVVM.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using GridBotMVVM.ViewModels;

namespace GridBotMVVM.Utils;

public class Emulator :ObservableObject
{
    private DispatcherTimer _dispatcherTimer = new ();

    public static List<Candle> CandlesEm = [];

    private List<Candle> loadCandles;
    
    public static event Action<Candle> NewTradeEvent;

    //public delegate void newTradeEvent(Candle candle);
    //public static event newTradeEvent? NewTradeEvent;

    private static int EmCandleCount;

    public static void AddCandleEm (List<Candle> loadCandles)
    {
        //EmCandleCount = 0;
        if (loadCandles.Count == 0)
        {
            Vm vm = new ();
            vm.LoadFile(null);
        }


        Candle candleEm = loadCandles[EmCandleCount];

        CandlesEm.Add(candleEm);
        NewTradeEvent?.Invoke(candleEm);
        EmCandleCount +=1;
    }
    
    //========================Таймер=====================================================================

    private void dispatcherTimer_Tick(object? sender, EventArgs e)
    {
        //_connector.Connect(Tb_ExChange);
        AddCandleEm(loadCandles);
    }

    //================Кнопка включения генерации котировок==========================================

    public void TimerStart(List<Candle> loadCandles, int timerInterval)
    {
        _dispatcherTimer.Tick += dispatcherTimer_Tick;
        _dispatcherTimer.Interval = new TimeSpan(0, 0, timerInterval);
        _dispatcherTimer.Start();

        this.loadCandles = loadCandles;

        //_connector.NewTradeEvent += GridTrading;    

    }

    

    //================Кнопка выключения генерации котировок==========================================

    private void Btn_OffExChange_Click()
    {
        _dispatcherTimer.Tick -= dispatcherTimer_Tick;
    }

}
