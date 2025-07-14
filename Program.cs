
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.StreamWriter;


namespace Console_урок1_3
{
    public class Program
    {
         void Main(string[] args)
        {

            
            numder = WriteLine;

            levels = new List<Level>();

            Load();

            numder();
        
            string str = ReadLine("Введите колличество уровней:");

             contLevels = Convert.ToInt32(str);

            str = ReadLine("Задайте верхнюю цену:");

             priceUp = decimal.Parse(str);

            str = ReadLine("Введите шаг уровня:" );

            StepLevel = decimal.Parse(str);


            str = ReadLine("Введите лот на уровень:");

            lotLevel = decimal.Parse(str);

            numder();

            Save();

            Console.ReadLine();
        }
        //=============================================Filds============================ 

        #region Filds

        List<Level> levels;

        decimal priceUp;

        int contLevels;

        decimal lotLevel;

        //=====================================

        Trade  trade = new Trade();


        #endregion


        //==============================================Properts========================
        #region Properts

        decimal StepLevel
        {
            get
            {
                return levels.Count;

            }

            set
            {
                StepLevel = value;

                levels =  Level.ColculateLevel(priceUp, stepLevel, contLevels);
            }
        }
        decimal stepLevel;


        #endregion

        //==============================================Metod===============================
        #region Metod

        void WriteLine()
        {
            Console.WriteLine("Колличество элементов в списке:" + levels.Count.ToString());

            for (int i = 0; i < levels.Count; i++)
            {
                Console.WriteLine(levels[i].PriceLevel);
            }
        }
        string ReadLine(string message)
        {
            Console.WriteLine(message);

            return Console.ReadLine();

        }

        void Save() 
        {
            using (StreamWriter writer = new StreamWriter("params.txt", false));
            {
                writer.WriteLine(priceUp.ToString());
                writer.WriteLine(contLevels.ToString());
                writer.WriteLine(stepLevel.ToString());
            }

            void Load() 
            {
                try
                {
                    StreamReader reader = new StreamReader("params.txt");

                    int index = 0;

                    while (true) 
                    {
                        string line = reader.ReadLine();

                        index++;

                     
                        switch (index) 
                        { 
                            case 1:
                                priceUp = decimal.Parse(line);
                                break;

                            case 2:
                                contLevels = int.Parse(line);
                                break;

                            case 3:
                                StepLevel = decimal.Parse(line);
                                break;



                        }
                        if (line == null) 
                        { 
                            break;
                        }
                    }
                }
                catch (Exception e)
                {

                }
            }
        }

       

        #endregion

        delegate void Numder();

        Numder numder;
    }

}
