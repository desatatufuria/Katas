using System;
using System.Linq;


/* Challenge 1 (23/24) - Cifrado César
 * 
 * Crea un programa que realize el cifrado César de un texto y lo imprima.
 * También debe ser capaz de descifrarlo cuando así se lo indiquemos.
 *
 * Te recomiendo que busques información para conocer en profundidad cómo
 * realizar el cifrado. Esto también forma parte del reto.
 * 
 * 
 * La metodología que he seguido ha sido crear una clase [CesarCipher] que contiene dos métodos: [encrypt] y [decrypt]
 * 
 * Para controlar las excepciones he creado un array de caracteres [excludedChars] que contiene las letras que no se cifrarán.
 * incluyendo las letras con tilde y la letra ñ tanto en mayúsculas como en minúsculas.
 * 
 * Se puede probar el codigo introduciendo una mensaje y una clave de desplazamiento para cifrarlo y descifrarlo.
 * o bien se puede probar el test unitario básico que he creado para comprobar que el cifrado y descifrado funciona correctamente.
 * 
 */

class Program

{
    static void Main(string[] args)
    {
        CesarCipher Cipher = new CesarCipher();


        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n\n Seleccione una opción:");
            Console.WriteLine("\r\r 1. Cifrar mensaje");
            Console.WriteLine("\r\r 2. Descifrar mensaje");
            Console.WriteLine("\r\r 3. Test Unitario Básico");
            Console.WriteLine("\r\r x. Salir");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Clear();
                    // Introducir mensaje [sin cifrar]
                    Console.Write("Ingrese el mensaje a cifrar:");
                    string msg = Console.ReadLine();

                    // Introducir clave de desplazamiento
                    Console.Write("Ingrese la clave:");
                    int keyEncrypt = int.Parse(Console.ReadLine());

                    // cifrar mensaje
                    string msgEncrypt = Cipher.encrypt(msg, keyEncrypt);
                    Console.WriteLine("Mensaje cifrado: " + msgEncrypt);
                    Console.ReadKey();
                    break;
                case "2":
                    Console.Clear();
                    // Introducir mensaje [cifrado]
                    Console.WriteLine("Ingrese el mensaje a descifrar:");
                    string msgDecrypt = Console.ReadLine();

                    // Introducir clave de desplazamiento
                    Console.WriteLine("Ingrese la clave:");
                    int keyDecrypt = int.Parse(Console.ReadLine());

                    // descifrar mensaje
                    string decryptMsg = Cipher.decrypt(msgDecrypt, keyDecrypt);
                    Console.WriteLine("Mensaje descifrado: " + decryptMsg);
                    Console.ReadKey();
                    break;
                case "3":
                    Console.Clear();
                    string msgUnitTest = "En criptografía, el cifrado César, también conocido como cifrado por desplazamiento," +
                        " código de César o desplazamiento de César, es una de las técnicas de cifrado más simples y más usadas." +
                        " Es un tipo de cifrado por sustitución en el que una letra en el texto original es reemplazada por otra " +
                        "letra que se encuentra un número fijo de posiciones más adelante en el alfabeto.";


                    int keyUnitTest = 3;
                    string msgEncryptUnitTest = Cipher.encrypt(msgUnitTest, keyUnitTest);
                    string msgDecryptUnitTest = Cipher.decrypt(msgEncryptUnitTest, keyUnitTest);

                    Console.WriteLine($"\n\n Mensaje original:\n\n {msgUnitTest}");
                    Console.WriteLine($"\n\n Mensaje cifrado con desplazamiento {keyUnitTest} :\n\n {msgEncryptUnitTest}");
                    Console.WriteLine($"\n\n Mensaje descifrado con desplazamiento {keyUnitTest} :\n\n {msgDecryptUnitTest}");
                    Console.ReadKey();
                    break;


                case "x":
                case "X":
                    Console.WriteLine("Saliendo del programa...");
                    return;
                default:
                    Console.WriteLine("Opción no válida. Por favor, seleccione una opción válida.");
                    break;
            }
        }
    }

    public class CesarCipher
    {
        public string encrypt(string msg, int key)
        {
            // variable para almacenar el mensaje cifrado
            string result = "";
            // caracteres que no se cifrarán
            char[] excludedChars = { 'Ñ', 'ñ', 'á', 'é', 'í', 'ó', 'ú', 'Á', 'É', 'Í', 'Ó', 'Ú' };

            // recorremos cada letra del mensaje
            foreach (char c in msg)
            {
                // si no es una letra o es una de las letras '<ñ> o <Ñ>' se devuelve tal cual
                if (!char.IsLetter(c) || excludedChars.Contains(c))
                {
                    result += c;
                    continue;
                }
                // obtener si es mayúscula o minúscula para obtener el índice del código ASCII desde donde desplazaremos cada letra 
                char CharInit = char.IsUpper(c) ? 'A' : 'a';

                // indice de la letra [c] restando el codigo ASCII desde [A] o [a] 
                int Index = (int)c - (int)CharInit;
                /* 
                 * Fórmula aplicada por el método Cesar.(extraída de Wikipedia)  
                 * el modulo 26 es para que si [index] se pasa de la 'z' o 'Z' vuelva a empezar desde la 'a' o 'A' según sea el caso
                 */
                int newIndex = (Index + key + 26) % 26;

                // obtenemos el nuevo código ASCII ya convertido de la letra desplazada [c]
                char AsciiChar = (char)(newIndex + CharInit);
                result += AsciiChar;
            }
            return result;
        }

        // El método [decrypt] es simplemente el método [encrypt] pero con la clave negativa
        public string decrypt(string msg, int key)
        {
            return encrypt(msg, -key);
        }
    }

}
