namespace ConsoleHome
{
    internal class Program
    {
        static void Main(string[] args)
        {


            Position position = new();
            position.PositionChanged += Notify;
           
            Console.ReadLine();

            /*Load();


            priceHi = ReadDcm("Задайте верхнюю цену: ");
            priceLow = ReadDcm("Задайте нижнюю цену: ");
            priceStep = ReadDcm("Задайте шаг цены: ");

            List<Level> LevelList = Level.GetLevels(priceHi, priceLow, priceStep);

            WrtLst = WriteList;

            WrtLst(LevelList);

            Save();
            */
        }
        #region Fields     //==================================================================================//
        static decimal priceHi;
        static decimal priceLow;
        static decimal priceStep;
        static string filePath = "params.txt";
        
        
        delegate void WriteListDelegate(List<Level> lst);
        static WriteListDelegate WrtLst;
        #endregion

        #region Properties //==================================================================================//


        #endregion

        #region Methods    //==================================================================================//
        static void WriteList(List<Level> levels)
        {
            Console.WriteLine($"Количество элементов в списке: {levels.Count.ToString()}");
            foreach (var level in levels) Console.Write(level.PriceLevel.ToString() + "\t");
        }
        static decimal ReadDcm(string promptStr)
        {
            Console.Write(promptStr);
            return decimal.Parse(Console.ReadLine());
        }

        static void Load()
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string? line;
                
                if ((line = reader.ReadLine()) != null) priceHi = decimal.Parse(line);
                if ((line = reader.ReadLine()) != null) priceLow = decimal.Parse(line);
                if ((line = reader.ReadLine()) != null) priceStep = decimal.Parse(line);
                /*while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }*/
            }
        }

        static void Save()
        {
           using (StreamWriter writer = new StreamWriter(filePath, false))
           {
                writer.WriteLine(priceHi.ToString());
                writer.WriteLine(priceLow.ToString());
                writer.WriteLine(priceStep.ToString());
            }


        }

        static void Notify(decimal position)
        {
            Console.WriteLine("Позиция изменилась! " + position.ToString() + "\n");
        }
        #endregion

    }
}
