using System;
using System.ComponentModel.Design;
using System.Linq;



/* Challenge 1 (23/24) - Cifrado Cesar
 * 
 * Crea un programa que realize el cifrado Cesar de un texto y lo imprima.
 * Tambien debe ser capaz de descifrarlo cuando asi se lo indiquemos.
 *
 * Te recomiendo que busques informacion para conocer en profundidad como
 * realizar el cifrado. Esto tambien forma parte del reto.
 * 
 * 
 * La metodologia que he seguido ha sido crear una clase [CesarCipher] que contiene dos metodos: [encrypt] y [decrypt]
 * 
 * Para controlar las excepciones he creado un array de caracteres [excludedChars] que contiene las letras que no se cifraran.
 * incluyendo las letras con tilde y la letra n tanto en mayusculas como en minusculas.
 * 
 * Se puede probar el codigo introduciendo una mensaje y una clave de desplazamiento para cifrarlo y descifrarlo.
 * o bien se puede probar el test unitario basico que he creado para comprobar que el cifrado y descifrado funciona correctamente.
 * 
 * EJEMPLOS DE USO:
 *       
 * Como Clase en un proyecto:
 * 
 *       Cipher.encrypt("zizu", 2); // se espera [bkxb]
 *       Cipher.decrypt("bkxb", 2); // se espera [zizu]
 *       
 * Como comando en linea de Consola (CLI):
 * 
 *       ChipherCesar.exe [sin argumentos] // se muestra el menu
 *       
 *       ChipherCesar.exe encrypt zizu, 2  // se espera [bkxb]
 *       
 *       ChipherCesar.exe decrypt bkxb, 2  // se espera [zizu]
 *
 * 
 */

class Program

{
    static void Main(string[] args)
    {
        switch (args.Length)
        {
            case 0:
                // No se han introducido argumentos por lo que se muestra el menu
                showMenu();
                break;
            case 3:
                // Se han introducido 3 argumentos por lo que se procede a comprobar si se quiere cifrar o descifrar
                if ((args[0] == "encrypt" || args[0] == "decrypt") && args.Length >= 3)
                {
                    // Comprobamos que la clave sea un numero entero positivo
                    if (int.TryParse(args[2], out int key) && key >= 0)
                    {
                        // Creamos una instancia de la clase CesarCipher
                        CesarCipher Cipher = new CesarCipher();
                        // Comprobamos si se quiere cifrar o descifrar
                        if (args[0] == "encrypt")
                            Cipher.encrypt(args[1], key); // ciframos el mensaje
                        else
                            Cipher.decrypt(args[1], key); // desciframos el mensaje
                    }
                    else
                        Console.WriteLine("La clave debe ser un numero entero positivo"); // si la clave no es valida
                }
                else
                    help(); // si los argumentos no son validos mostramos la ayuda

                break;
            default:
                help(); // si el numero de argumentos no es valido mostramos la ayuda
                break;
        }
    }
    public static void help()
    {
        Console.WriteLine("\n\nInstrucciones: ");
        Console.WriteLine("--------------");
        Console.WriteLine("Para encriptar: ChipherCesar.exe encrypt [texto] [clave]");
        Console.WriteLine("Para desencriptar: ChipherCesar.exe decrypt [textoEncriptado] [clave]");
        Console.WriteLine("Si quieres ver el menu: ChipherCesar.exe");
        Console.WriteLine("-- Enter para salir.");
        Console.ReadKey();

    }

    public static void showMenu()
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
                    Console.Write("Mensaje cifrado: ");
                    Cipher.encrypt(msg, keyEncrypt);
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
                    Console.Write("Mensaje descifrado: ");
                    Cipher.decrypt(msgDecrypt, keyDecrypt);
                    Console.ReadKey();
                    break;
                case "3":
                    Console.Clear();
                    string msgTest = "En criptografía, el cifrado Cesar, tambien conocido como cifrado por desplazamiento," +
                                   " código de Cesar o desplazamiento de Cesar, es una de las técnicas de cifrado mas simples y mas usadas";

                    string msgTestEncrypt = "Hq fulswrjudild, ho fliudgr Fhvdu, wdpelhq frqrflgr frpr fliudgr sru ghvsodcdplhqwr, frgljr gh " +
                        "Fhvdu r ghvsodcdplhqwr gh Fhvdu, hv xqd gh odv whfqlfdv gh fliudgr pdv vlpsohv b pdv xvdgdv";


                    int keyUnitTest = 3;
                    // Mensaje original
                    Console.WriteLine($"\n\n Mensaje original:\n\n {msgTest}");

                    // Mensaje cifrado
                    Console.WriteLine($"\n\n Mensaje cifrado con desplazamiento {keyUnitTest} :\n\n");
                    Cipher.encrypt(msgTest, keyUnitTest);

                    // Mensaje descifrado
                    Console.WriteLine($"\n\n Mensaje descifrado con desplazamiento {keyUnitTest} :\n\n");
                    Cipher.decrypt(msgTestEncrypt, keyUnitTest);
                    Console.ReadKey();
                    break;
                case "x":
                case "X":
                    // Salir
                    return;
                default:
                    Console.WriteLine("Por favor, seleccione una opción válida.");
                    break;
            }
        }
    }

    public class CesarCipher
    {
        public void encrypt(string msg, int key)
        {
            // variable para almacenar el mensaje cifrado
            string result = "";

            // reemplazar caracteres especiales
            msg = replaceChars(msg);

            // caracteres que no se cifran
            char[] excludedChars = { 'ñ', 'Ñ' };

            // recorremos cada letra del mensaje
            foreach (char c in msg)
            {
                // si no es una letra o es una de las letras '<ñ> o <Ñ>' se devuelve tal cual
                if (!char.IsLetter(c) || excludedChars.Contains(c))
                {
                    result += c;
                    continue;
                }
                // obtener si es mayuscula o minuscula para obtener el indice del codigo ASCII desde donde desplazaremos cada letra 
                char CharInit = char.IsUpper(c) ? 'A' : 'a';

                // indice de la letra [c] restando el codigo ASCII desde [A] o [a] 
                int Index = (int)c - (int)CharInit;
                /* 
                 * Formula aplicada por el metodo Cesar.(extraida de Wikipedia)  
                 * el modulo 26 es para que si [index] se pasa de la 'z' o 'Z' vuelva a empezar desde la 'a' o 'A' segun sea el caso
                 */
                int newIndex = (Index + key + 26) % 26;

                // obtenemos el nuevo codigo ASCII ya convertido de la letra desplazada [c]
                char AsciiChar = (char)(newIndex + CharInit);
                result += AsciiChar;
            }
            Console.WriteLine(result);
        }

        // El metodo [decrypt] es simplemente el metodo [encrypt] pero con la clave negativa
        public void decrypt(string msg, int key)
        {
            encrypt(msg, -key);
        }

        private string replaceChars(string msg)
        {
            return msg
                  .Replace('á', 'a').Replace('Á', 'A')
                  .Replace('é', 'e').Replace('É', 'E')
                  .Replace('í', 'i').Replace('Í', 'I')
                  .Replace('ó', 'o').Replace('Ó', 'O')
                  .Replace('ú', 'u').Replace('Ú', 'U');
        }

    }

}
