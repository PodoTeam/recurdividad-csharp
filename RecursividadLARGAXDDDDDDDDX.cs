using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConversionRecursiva
{
    class Program
    {
        static void Imprimir(Stack<string> Posfijo, int aux)
        {
            string prueba = "";

            for (int j = 0; j < aux; j++)
                prueba += Posfijo.Pop();

            for (int j = prueba.Length; j != 0; j--)
                Console.Write(prueba.Substring(j - 1, 1));

        }
        static int Contador(string Infijo)
        {
            int cont = 0;

            for (int j = 0; j < Infijo.Length; j++)
            {
                if (Infijo.Substring(j, 1) == "(" || Infijo.Substring(j, 1) == ")")
                    cont++;
            }
            return cont;
        }
        static void Conversion(string Infijo, Stack<string> Posfijo, Stack<string> opstk, int i, int aux)
        {
            if (i < Infijo.Length)
            {
                if (Infijo.Substring(i, 1) == ")")
                {
                    Posfijo.Push(opstk.Pop());
                    i = i + 1;
                    Conversion(Infijo, Posfijo, opstk, i, aux);
                }
                else if ((Encoding.ASCII.GetBytes(Infijo.Substring(i, 1).ToString())[0] < 64 || Encoding.ASCII.GetBytes(Infijo.Substring(i, 1).ToString())[0] > 91) && Infijo.Substring(i, 1) != "(" && Infijo.Substring(i, 1) != ")")
                {
                    opstk.Push(Infijo.Substring(i, 1));
                    i = i + 1;
                    Conversion(Infijo, Posfijo, opstk, i, aux);
                }
                else if (Encoding.ASCII.GetBytes(Infijo.Substring(i, 1).ToString())[0] > 64 && Encoding.ASCII.GetBytes(Infijo.Substring(i, 1).ToString())[0] < 91)
                {
                    Posfijo.Push(Infijo.Substring(i, 1));
                    i = i + 1;
                    Conversion(Infijo, Posfijo, opstk, i, aux);
                }
                else if (Infijo.Substring(i, 1) == "(")
                {
                    i = i + 1;
                    Conversion(Infijo, Posfijo, opstk, i, aux);
                }
            }
            else
            {
                if (Posfijo.Count != 0 && opstk.Count != 0)
                {
                    Posfijo.Push(opstk.Peek());
                    opstk.Pop();
                    i = i + 1;
                    Conversion(Infijo, Posfijo, opstk, i, Posfijo.Count);
                }
                else if (opstk.Count == 0 && aux != Infijo.Length - Contador(Infijo))
                {
                    i = i + 1;
                    Conversion(Infijo, Posfijo, opstk, i, Posfijo.Count);
                }
                if (aux == Infijo.Length - Contador(Infijo))
                {
                    Imprimir(Posfijo, aux);
                }
            }
        }

        static void Main(string[] args)
        {
            int i = 0;
            string Infijo;
            string prueba = "";
            int aux = 0;

            Console.WriteLine("Ingrese su cadena infija");
            Infijo = Console.ReadLine().ToUpper();

            Stack<string> Posfijo = new Stack<string>();
            Stack<string> opstk = new Stack<string>();

            Console.WriteLine("\nConversión de infijo a posfijo: ");
            Conversion(Infijo, Posfijo, opstk, i, aux);

        }
    }
}
