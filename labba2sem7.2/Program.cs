using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace labba2sem7._2
{
    public class MeteorologicalData
    {
        public DateTime Date;
        public string City = "";
        public float AtmosphericPressure = 0;
        public float AirTemperature = 0;
        public float WindSpeed = 0;
        public MeteorologicalData(string City = "", float AtmosphericPressure = 0, float AirTemperature = 0, float WindSpeed = 0)
        {
            this.City = City;
            this.AtmosphericPressure = AtmosphericPressure;
            this.AirTemperature = AirTemperature;
            this.WindSpeed = WindSpeed;
        }
        public int CompareTo(MeteorologicalData p)
        {
            return this.WindSpeed.CompareTo(p.WindSpeed);
        }
        public class SortByDate : IComparer<MeteorologicalData>
        {

            public int Compare(MeteorologicalData p1, MeteorologicalData p2)
            {
                if (p1.Date > p2.Date)
                    return 1;
                else if (p1.Date < p2.Date)
                    return -1;
                else
                    return 0;
            }
        }
        public class SortByTemperatureAndSpeed : IComparer<MeteorologicalData>
        {
            public int Compare(MeteorologicalData p1, MeteorologicalData p2)
            {
                if (p1.AirTemperature > p2.AirTemperature)
                {
                    return 1;
                }
                else if (p1.AirTemperature < p2.AirTemperature)
                {
                    return -1;
                }
                else if (p1.AtmosphericPressure > p2.AtmosphericPressure)
                {
                    return 1;
                }
                else if (p1.AtmosphericPressure < p2.AtmosphericPressure)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
    class Program
    {
        [STAThread]
        static void Main()
        {
            List<MeteorologicalData> data = new List<MeteorologicalData>();
            Console.WriteLine("Enter path to file C:/Program Files(x86)/Floader/databse.txt if you enter not exist path will be created new file");
            string Path = Console.ReadLine();
            data = ReadDate(Path);
            while (true)
            {
                Console.Clear();
                Table(data);
                var k = Console.ReadKey().Key;
                if (k == ConsoleKey.A)
                {
                    Add(data);
                }
                if (k == ConsoleKey.R)
                {
                    Remove(data);
                }
                if (k == ConsoleKey.C)
                {
                    ChangeData(data);
                }
                if (k == ConsoleKey.D)
                {
                    data.Sort(new MeteorologicalData.SortByDate());
                }
                if (k == ConsoleKey.T)
                {
                    data.Sort(new MeteorologicalData.SortByTemperatureAndSpeed());
                }
                SaveDate(data, Path);
            }
        }
        static void Table(List<MeteorologicalData> v)
        {
            string[] Texts = new string[5];
            Texts[0] = "    City    ";
            Texts[1] = " Atmospheric Pressure ";
            Texts[2] = "     Date     ";
            Texts[3] = " Air Temperature ";
            Texts[4] = " Wind Speed ";
            Console.WriteLine($"{Texts[0]}|{Texts[1]}|{Texts[2]}|{Texts[3]}|{Texts[4]}|");
            foreach (MeteorologicalData vg in v)
            {
                Console.WriteLine(vg.City + s(Texts[0].Length - vg.City.Length) + "|" +
                    vg.AtmosphericPressure + s(Texts[1].Length - vg.AtmosphericPressure.ToString().Length) + "|" +
                    vg.Date.Date.ToString("dd.MM.yyyy") + s(Texts[2].Length - vg.Date.Date.ToString("dd.MM.yyyy").Length) + "|" +
                    vg.AirTemperature + s(Texts[3].Length - vg.AirTemperature.ToString().Length) + "|" +
                    vg.WindSpeed + s(Texts[4].Length - vg.WindSpeed.ToString().Length) + "|"
                    );
            }
            Console.WriteLine("A) Add new\nR) Remove\nC) Change\nD) Sort By Date\nT) Sort by Time and Buget");
        }
        static void Add(List<MeteorologicalData> v)
        {
            MeteorologicalData New = new MeteorologicalData();
            try
            {
                Console.WriteLine("Enter City");
                New.City = Console.ReadLine();
                Console.WriteLine("Enter Atmospheric Pressure");
                New.AtmosphericPressure = (float)Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter Date");
                New.Date = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                Console.WriteLine("Enter Air Temperature");
                New.AirTemperature = (float)Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter Wind Speed");
                New.WindSpeed = (float)Convert.ToDouble(Console.ReadLine());
            }
            catch
            {

            }
            v.Add(New);
        }
        static void Remove(List<MeteorologicalData> v)
        {
            Console.WriteLine("Enter Date to delete");
            int name = Convert.ToInt32(Console.ReadLine());
            v.RemoveAt(v.FindIndex(f => f.WindSpeed == name));
        }
        static void ChangeData(List<MeteorologicalData> v)
        {
            Console.WriteLine("Enter name to change");
            string name = Console.ReadLine();
            if ((v.FindIndex(f => f.City == name) != -1))
            {
                MeteorologicalData Change = v[v.FindIndex(f => f.City == name)];
                Console.WriteLine("1)City\n2)City\n3)Date\n4)Air Temperature\n5)Wind Speed ");
                var res = Console.ReadKey().KeyChar;
                Console.WriteLine("Enter new value");
                if (res == '1')
                {
                    Change.City = Console.ReadLine();
                }
                if (res == '2')
                {
                    Change.AtmosphericPressure = (float)Convert.ToDouble(Console.ReadLine());
                }
                if (res == '3')
                {
                    Change.Date = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                }
                if (res == '4')
                {
                    Change.AirTemperature = Convert.ToInt16(Console.ReadLine());
                }
                if (res == '5')
                {
                    Change.WindSpeed = Convert.ToInt16(Console.ReadLine());
                }
            }
            else
            {
                Console.WriteLine("Entered name not found");
                Console.ReadKey();
            }

        }
        public static string s(int c)
        {
            try
            {
                return new String(' ', c);
            }
            catch
            {
                return "";
            }
        }
        public static void SaveDate(List<MeteorologicalData> Date, string path)
        {
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                foreach (MeteorologicalData g in Date)
                {

                    sw.WriteLine(g.City.Trim() + "|" + g.AtmosphericPressure + "|" + g.Date.Date.ToString("dd.MM.yyyy") + "|" + g.AirTemperature + "|" + g.WindSpeed + "/");

                }
            }
        }
        public static List<MeteorologicalData> ReadDate(string path)
        {
            List<MeteorologicalData> g = new List<MeteorologicalData>();
            string text = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    text = sr.ReadToEnd();
                }
                string[] Dates = text.Split('/');
                foreach (string s in Dates)
                {
                    string[] MetaDete = s.Split('|');
                    if (MetaDete.Length == 5)
                    {
                        MeteorologicalData d = new MeteorologicalData
                        {
                            City = MetaDete[0].Trim(),
                            AtmosphericPressure = (float)Convert.ToDouble(MetaDete[1]),
                            Date = DateTime.ParseExact(MetaDete[2], "dd.MM.yyyy", CultureInfo.InvariantCulture),
                            AirTemperature = (float)Convert.ToDouble(MetaDete[3]),
                            WindSpeed = Convert.ToInt32(MetaDete[4])
                        };
                        g.Add(d);
                    }
                }
            }
            catch
            {
                var flv = File.Create(path);
                flv.Close();
            }

            return g;
        }
    }
}
