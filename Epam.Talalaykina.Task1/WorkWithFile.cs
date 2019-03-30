using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Talalaykina.Task1
{
    public class WorkWithFile
    {
        private const string fileInName = "test.txt";
        private const string fileOutName = "Auto.txt";

        public List<Point> Read()
        {
            List<double> coordinates = new List<double>();
            List<Point> points = new List<Point>();
            using (StreamReader file = new StreamReader(fileInName))
            {
                string[] StrFromFile = file.ReadToEnd().Split(new[] {" ", "\n"}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(item => item.Trim())
                    .Where(item => !string.IsNullOrEmpty(item)).ToArray();
                foreach (var s in StrFromFile)
                {
                    if (!double.TryParse(s, out var value))
                    {
                        throw new ArgumentException("ERROR in reading coordinate");
                    }
                    else
                    {
                        coordinates.Add(value);
                    }
                }
                
                    for (int i = 0; i < coordinates.Count; i += 3)
                        points.Add(new Point(coordinates[i], coordinates[i + 1], coordinates[i + 2]));
                    return points;
                
            }
        }
        
        public void Save(double area, double volume)
        {
            using (StreamWriter sw = new StreamWriter(fileOutName))
            {
                sw.WriteLine("Pyramid`s area {0}", area);
                sw.WriteLine("Pyramid`s volume {0}", volume);
            }
        }
    }
}