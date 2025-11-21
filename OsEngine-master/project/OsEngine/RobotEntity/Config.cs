using ControlzEx.Theming;
using MahApps.Metro.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace OSEngine.RobotEntity
{
    public class Config //класс для хранения настроек робота
    {
        public Config() { } //создаем конструктор


        #region Properties ================================================================
        /*список роботов (их конфигураций)*/
        public List<ConfigRobot> ConfigRobots { get; set; } = new List<ConfigRobot>();

        /*координаты окна*/
        public double Left { get; set; } = 50;
        public double Top { get; set; } = 50;
        public double Width { get; set; } = 800;
        public double Height { get; set; } = 600;

        /*тема окна*/
        public string? Theme { get; set; }


        #endregion

        #region Methods ================================================================

        public void SaveConfig(MetroWindow window, Theme? theme)
        {
            Width = window.Width;
            Height = window.Height;
            Left = window.Left;
            Top = window.Top;

            Theme = theme?.Name ?? "";
            SaveConfig();
        }

        public static Config LoadConfig() //загрузка сохраненной конфигурации робота
        {
            if (!Directory.Exists(@"Paraments")||!File.Exists(@"Paraments\paraments.json"))
            {
                return new Config();
            }

            try
            {
                using (StreamReader reader = new StreamReader(@"Paraments\paraments.json"))
                {
                    string str = reader.ReadToEnd();

                    if (str != null)
                    {
                        Config config = JsonConvert.DeserializeObject<Config>(str);

                        if (config != null)
                        {
                            return config;
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return new Config();
        }

        private void SaveConfig() //сохранение конфигурации робота
        {
            if (!Directory.Exists(@"Paraments"))
            {
                Directory.CreateDirectory(@"Paraments");
            }
            try
            {
                using (StreamWriter writer = new StreamWriter(@"Paraments\paraments.json", false))
                {
                    string json = JsonConvert.SerializeObject(this);
                    writer.WriteLine(json);
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
    }
}