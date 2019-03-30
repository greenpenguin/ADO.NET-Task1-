using System;
using System.Collections.Generic;

namespace Epam.Talalaykina.Task1
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            WorkWithFile workWithFile = new WorkWithFile();
            List<Point> listOfCoordinates = new List<Point>();

            listOfCoordinates = workWithFile.Read();

            Pyramid test = new Pyramid(listOfCoordinates);
            double area = test.Area();
            double volume = test.Volume();

            workWithFile.Save(area, volume);
            
        }
    }
}