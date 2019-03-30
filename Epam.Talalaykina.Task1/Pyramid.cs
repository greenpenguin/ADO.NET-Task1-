
using System;
using System.Collections.Generic;
using System.IO;

namespace Epam.Talalaykina.Task1
{
    public class Pyramid
    {
        
        private Point a;
        private Point b;
        private Point c;
        private Point d;
        private Point h;
        
        public Pyramid(List <Point> coordinates)
        {
            Assert(coordinates);
        }
        
        public Point A {
            get
            {
                return a;
            }
            set
            {
                if (!IsQuadrilateralSelfIntersecting(value, b, c, d))
                {
                    a = value;
                }
                else
                {
                    throw new ArgumentException("ERROR the base of the pyramid is a self-intersecting quadrilateral!");
                }
            } 
        }

        public Point B
        {
            get
            {
                return b;
            }
            set
            {
                if (!IsQuadrilateralSelfIntersecting(a, value, c, d))
                {
                    b = value;
                }
                else
                {
                    throw new ArgumentException("ERROR the base of the pyramid is a self-intersecting quadrilateral!");
                }
            }
        }

        public Point C
        {
            get
            {
                return c;
            }
            set
            {
                if (!IsQuadrilateralSelfIntersecting(a, b, value, d))
                {
                    c = value;
                }
                else
                {
                    throw new ArgumentException("ERROR the base of the pyramid is a self-intersecting quadrilateral!");
                }
            }
        }
        
        public Point D
        {
            get
            {
                return d;
            }
            set
            {
                if (!IsQuadrilateralSelfIntersecting(a, b, c, value))
                {
                    d = value;
                }
                else
                {
                    throw new ArgumentException("ERROR the base of the pyramid is a self-intersecting quadrilateral!");
                }
            }
        }
        
        public Point H
        {
            get
            {
                return h;
            }
            set
            {
                if (!IsQuadrilateralSelfIntersecting(a, b, c, d))
                {
                    h = value;
                }
                else
                {
                    throw new ArgumentException("ERROR the base of the pyramid is a self-intersecting quadrilateral!");
                }
            }
        }
        
        public void Assert(List<Point> coordinates)
        { 
            if (!IsQuadrilateralSelfIntersecting(coordinates[0], coordinates[1], coordinates[2], coordinates[3]))
            {
                a = coordinates[0];
                b = coordinates[1];
                c = coordinates[2];
                d = coordinates[3];
                h = coordinates[4];
            }
            else
            {
                throw new ArgumentException("ERROR the base of the pyramid is a self-intersecting quadrilateral!");
            }
        }

        private double AreaOfATriangle(Point point1, Point point2, Point point3)
        {
            double res1 = Math.Pow(((point2.Y - point1.Y) * (point3.Z - point1.Z)
                                    - (point2.Z - point1.Z) * (point3.Y - point1.Y)), 2);
            double res2 = Math.Pow(((point2.X - point1.X) * (point3.Z - point1.Z)
                                    - (point2.Z - point1.Z) * (point3.X - point1.X)), 2);
            double res3 = Math.Pow(((point2.X - point1.X) * (point3.Y - point1.Y)
                                    - (point2.Y - point1.Y) * (point3.X - point1.X)), 2);

            double res = (Math.Sqrt(res1 + res2 + res3)) / 2;

            return Math.Abs(res);
        }

        public double Area()
        {
            int trigger = IsConvex(A, B, C, D);
            if ((trigger == 0) || (trigger == 1) || (trigger == 3))
            {
                return AreaOfATriangle(A, B, C) + AreaOfATriangle(A, C, D);
            }
            else
            {
                return AreaOfATriangle(A, B, D) + AreaOfATriangle(B, C, D);
            }
        }

        private double VolumeOfTetrahedron(Point point1, Point point2, Point point3, Point point4)
        {
            double res = ((point2.X - point1.X) * (point3.Y - point1.Y) * (point4.Z - point1.Z) +
                          (point4.X - point1.X) * (point2.Y - point1.Y) * (point3.Z - point1.Z) +
                          (point3.X - point1.X) * (point4.Y - point1.Y) * (point2.Z - point1.Z) -
                          (point4.X - point1.X) * (point3.Y - point1.Y) * (point2.Z - point1.Z) -
                          (point2.X - point1.X) * (point4.Y - point1.Y) * (point3.Z - point1.Z) -
                          (point3.X - point1.X) * (point2.Y - point1.Y) * (point4.Z - point1.Z)) / 6;
            return Math.Abs(res);
        }

        public double Volume()
        {
            int trigger = IsConvex(A, B, C, D);

            if ((trigger == 0) || (trigger == 1) || (trigger == 3))
            {
                return VolumeOfTetrahedron(H, A, B, C) + VolumeOfTetrahedron(H, A, C, D);
            }
            else
            {
                return VolumeOfTetrahedron(H, A, B, D) + VolumeOfTetrahedron(H, B, C, D);
            }
        }

        private int IsConvex(Point point1, Point point2, Point point3, Point point4)
        {
            double[] product = new double[4];
            product[0] = ((point2.Y - point1.Y) * (point3.Z - point2.Z) -
                          (point2.Z - point1.Z) * (point3.Y - point2.Y)) - //1
                         ((point2.X - point1.X) * (point3.Z - point2.Z) -
                          (point2.Z - point1.Z) * (point3.X - point2.X)) + //2
                         (point2.X - point1.X) * (point3.Y - point2.Y) -
                         (point2.Y - point1.Y) * (point3.X - point2.X);

            product[1] = ((point3.Y - point2.Y) * (point4.Z - point3.Z) -
                          (point3.Z - point2.Z) * (point4.Y - point3.Y)) - //1
                         ((point3.X - point2.X) * (point4.Z - point3.Z) -
                          (point3.Z - point2.Z) * (point4.X - point3.X)) + //2
                         (point3.X - point2.X) * (point4.Y - point3.Y) -
                         (point3.Y - point2.Y) * (point4.X - point3.X);

            product[2] = ((point4.Y - point3.Y) * (point1.Z - point4.Z) -
                          (point4.Z - point3.Z) * (point1.Y - point4.Y)) - //1
                         ((point4.X - point3.X) * (point1.Z - point4.Z) -
                          (point4.Z - point3.Z) * (point1.X - point4.X)) + //2
                         (point4.X - point3.X) * (point1.Y - point4.Y) -
                         (point4.Y - point3.Y) * (point1.X - point4.X);

            product[3] = ((point1.Y - point4.Y) * (point2.Z - point1.Z) -
                          (point1.Z - point4.Z) * (point2.Y - point1.Y)) - //1
                         ((point1.X - point4.X) * (point2.Z - point1.Z) -
                          (point1.Z - point4.Z) * (point2.X - point1.X)) + //2
                         (point1.X - point4.X) * (point2.Y - point1.Y) -
                         (point1.Y - point4.Y) * (point2.X - point1.X);

            int count = 0;
            for (int i = 0; i < 4; i++)
            {
                if (product[i] < 0)
                {
                    count++;
                }
            }

            if (count == 2)
            {
                return -1;
            }

            if ((product[0] > 0) && (product[1] < 0))
            {
                if ((product[2] < 0) && (product[3] < 0))
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
            else if ((product[1] > 0) && (product[2] < 0))
            {
                if ((product[0] < 0) && (product[3] < 0))
                {
                    return 3;
                }
                else
                {
                    return 4;
                }
            }
            else if ((product[2] > 0) && (product[3] < 0))
            {
                if ((product[0] < 0) && (product[1] < 0))
                {
                    return 4;
                }
                else
                {
                    return 1;
                }
            }
            else if ((product[3] > 0) && (product[0] < 0))
            {
                if ((product[1] < 0) && (product[2] < 0))
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            
            return 0;
        }

        public bool IsQuadrilateralSelfIntersecting(Point point1, Point point2, Point point3, Point point4)
        {
            if (IsConvex(point1, point2, point3, point4) == -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}