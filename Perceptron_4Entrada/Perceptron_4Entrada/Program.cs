using LinearAlgebra;
using System;

namespace Perceptron_4Entrada
{

        class Program
        {
            const double Epsilon = 0.01;

            static void Main(string[] args)
            {
                
                int entrada = 2;
                int pesos = 5;
                int salida = 4;
                int umbral = 4;
                double recalcular = 0.1;
                Random r = new Random();

              
                Matrix x = new double[,] { { 0, 0 },
                                       { 0, 1 },
                                       { 1, 0 },
                                       { 1, 1 }};

                Matrix y = new double[,] { { 1, 0, 0, 0 },
                                           { 0, 0, 1, 1 },
                                           { 0, 1, 1, 1 },
                                           { 1, 1, 1, 0 }};


                Matrix w1 = (Matrix.Random(entrada + 1, pesos, r) - 0.5) * 2.0;
                Matrix w2 = (Matrix.Random(pesos + 1, salida, r) - 0.5) * 2.0;
                Matrix w3 = (Matrix.Random(pesos + 1, salida, r) - 0.5) * 2.0;
                Matrix w4 = (Matrix.Random(pesos + 1, salida, r) - 0.5) * 2.0;


            for (int l = 0; l < 5001; l++)
                {

                    Matrix z1 = x.AddColumn(Matrix.Ones(umbral, 1));
                    Matrix a1 = z1;
                    Matrix z2 = (a1 * w1).AddColumn(Matrix.Ones(umbral, 1));
                    Matrix a2 = siguiente(z2);
                    Matrix z3 = a2 * w2;
                    Matrix a3 = siguiente(z3);

                    Matrix a3error = a3 - y;
                    Matrix Delta3 = a3error * siguiente(z3, true);

                    Matrix a2error = Delta3 * w2.T;
                    Matrix Delta2 = a2error * siguiente(z2, true);
                    Delta2 = Delta2.Slice(0, 1, Delta2.x, Delta2.y);

                    w2 -= (a2.T * Delta3) * recalcular;
                    w1 -= (a1.T * Delta2) * recalcular;

                    double perdidas = a3error.abs.average * umbral;
                    Console.WriteLine("Entrenando: " + perdidas);

                    if (l % 1000 == 0)
                    {
                        Console.WriteLine("---" + l + "---");
                        Console.WriteLine("X: " + x.size.ToString());
                        Console.WriteLine(x.ToString());

                        Console.WriteLine("Repesos: " + a3.size.ToString());
                        Console.WriteLine(a3);
                    }
                }
                Console.ReadKey();
            }

            static Matrix siguiente(Matrix m, bool derivada = false)
            {
                double[,] a = m;
                Matrix.MatrixLoop((i, j) =>
                {
                    if (derivada)
                    {
                        double sig = 1.0 / (1.0 + Math.Exp(-a[i, j]));
                        a[i, j] = sig * (1.0 - sig);
                    }
                    else
                    {
                        a[i, j] = 1.0 / (1.0 + Math.Exp(-a[i, j]));
                    }
                }, m.x, m.y);

                return a;
            }
            static Matrix recalcular(Matrix m, bool derivada = false)
            {
                double[,] a = m;
                Matrix.MatrixLoop((i, j) =>
                {
                    if (derivada)
                    {
                        a[i, j] = a[i, j] > 0 ? 1 : Epsilon;
                    }
                    else
                    {
                        a[i, j] = a[i, j] > 0 ? a[i, j] : 0;
                    }
                }, m.x, m.y);

                return a;
            }
        }
    }
