using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
        struct Triangle
        {

            public double SideA { get; set; }
            public double SideB { get; set; }
            public double SideC { get; set; }
            public double AnglebetweenAandB { get; set; }


            public Triangle(double sideA = 5, double sideB = 4, double sideC = 3, double angleBetweenAandB = 37)
            {
                try
                {
                    if (sideA <= 0 || sideB <= 0 || sideC <= 0 || angleBetweenAandB <= 0 || angleBetweenAandB >= 180)
                    {
                        throw new ArgumentException("Довжини сторін і кут повинні бути додатними, а кут меншим за 180 градусів.");
                    }

                    SideA = sideA;
                    SideB = sideB;
                    SideC = sideC;
                    AnglebetweenAandB = angleBetweenAandB;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Помилка в конструкторі: {ex.Message}");
                    SideA = SideB = SideC = AnglebetweenAandB = 0;
                }
                finally
                {
                    Console.WriteLine("Ініціалізація трикутника завершена.");
                }
            }

            public double CalculateArea()
            {
                try
                {
                    double angleRadians = AnglebetweenAandB * (Math.PI / 180);
                    return 0.5 * SideA * SideB * Math.Sin(angleRadians);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка обчислення площі: {ex.Message}");
                    return 0;
                }
                finally
                {
                    Console.WriteLine("Обчислення площі завершено.");
                }
            }

            public double CalculatePerimeter()
            {
                try
                {
                    return SideA + SideB + SideC;
                }
                finally
                {
                    Console.WriteLine("Обчислення периметра завершено.");
                }
            }


            public void CalculateHeight()
            {
                try
                {
                    double area = CalculateArea();
                    if (area == 0)
                    {
                        Console.WriteLine("Не можна обчислити висоту через некоректні сторони або кут.");
                        return;
                    }

                    double heightA = (2 * area) / SideA;
                    double heightB = (2 * area) / SideB;
                    double heightC = (2 * area) / SideC;

                    Console.WriteLine($"Висота, опущена на сторону A: {heightA}");
                    Console.WriteLine($"Висота, опущена на сторону B: {heightB}");
                    Console.WriteLine($"Висота, опущена на сторону C: {heightC}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка обчислення висоти: {ex.Message}");
                }
                finally
                {
                    Console.WriteLine("Обчислення висоти завершено.");
                }
            }

            public string GetTriangleType()
            {
                try
                {
                    if (SideA == SideB && SideB == SideC)
                        return "Рівносторонній";
                    else if (SideA == SideB || SideB == SideC || SideA == SideC)
                        return "Рівнобедрений";
                    else if (AnglebetweenAandB == 90 || CalculateArea() == 90)
                        return "Прямокутний";
                    else
                        return "Різносторонній";
                }
                finally
                {
                    Console.WriteLine("Визначення типу трикутника завершено.");
                }
            }

            public override string ToString()
            {
                return $"Трикутник зі сторонами A: {SideA}, B: {SideB}, C: {SideC}, кут між A і B: {AnglebetweenAandB}. Тип: {GetTriangleType()}";
            }
        }

        class Program
        {
            static Triangle[] ReadTriangles(int n)
            {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Triangle[] triangles = new Triangle[n];
                for (int i = 0; i < n; i++)
                {
                    try
                    {
                        Console.WriteLine($"Введіть сторони та кут трикутника {i + 1}:");
                        Console.Write("SideA: ");
                        double sideA = Convert.ToDouble(Console.ReadLine());

                        Console.Write("SideB: ");
                        double sideB = Convert.ToDouble(Console.ReadLine());

                        Console.Write("SideC: ");
                        double sideC = Convert.ToDouble(Console.ReadLine());

                        Console.Write("Angle between A and B: ");
                        double angleBetweenAandB = Convert.ToDouble(Console.ReadLine());

                        triangles[i] = new Triangle(sideA, sideB, sideC, angleBetweenAandB);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Помилка введення: {ex.Message}");
                        i--; 
                    }
                }
                return triangles;
            }

            static void PrintTriangle(Triangle triangle)
            {
                Console.WriteLine(triangle.ToString());
                Console.WriteLine($"Периметр трикутника: {triangle.CalculatePerimeter()}");
                triangle.CalculateHeight();
            }

 
            static void SortTriangles(Triangle[] triangles)
            {
                Array.Sort(triangles, (x, y) => x.CalculateArea().CompareTo(y.CalculateArea()));
            }

            static void ModifyTriangle(ref Triangle triangle, double newSideA, double newSideB, double newSideC, double newAngleBetweenAandB)
            {
                triangle = new Triangle(newSideA, newSideB, newSideC, newAngleBetweenAandB);
            }

            static void FindMinMaxArea(Triangle[] triangles, out double minArea, out double maxArea)
            {
                minArea = double.MaxValue;
                maxArea = double.MinValue;

                foreach (var triangle in triangles)
                {
                    double area = triangle.CalculateArea();
                    if (area < minArea)
                        minArea = area;
                    if (area > maxArea)
                        maxArea = area;
                }
            }

            static void Main()
            {
                Console.Write("Введіть кількість трикутників: ");
                int n = int.Parse(Console.ReadLine());

                Triangle[] triangles = ReadTriangles(n);

                Console.WriteLine("\nВведені трикутники:");
                foreach (var triangle in triangles)
                {
                    PrintTriangle(triangle);
                    Console.WriteLine($"Площа трикутника: {triangle.CalculateArea()}");
                }

                SortTriangles(triangles);
                Console.WriteLine("\nТрикутники після сортування за площею:");
                foreach (var triangle in triangles)
                {
                    PrintTriangle(triangle);
                    Console.WriteLine($"Площа трикутника: {triangle.CalculateArea()}");
            }

                FindMinMaxArea(triangles, out double minArea, out double maxArea);
                Console.WriteLine($"\nМінімальна площа: {minArea}, Максимальна площа: {maxArea}");

                if (triangles.Length > 0)
                {
                    Console.WriteLine("\nМодифікація першого трикутника:");
                    ModifyTriangle(ref triangles[0], 6, 7, 8, 60);
                    PrintTriangle(triangles[0]);
                    Console.WriteLine($"Площа трикутника: {triangles[0].CalculateArea()}"); 
                }

                Console.WriteLine("Натисніть будь-яку клавішу, щоб закрити програму...");
                Console.ReadKey();
            }
        }
    }
