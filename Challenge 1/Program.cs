using System;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;


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
 */

class Program

{
    static void Main(string[] args)
    {
        CesarCipher Cipher = new CesarCipher();
        string msg = "En criptografía, el cifrado Cesar, tambien conocido como cifrado por desplazamiento," +
                       " código de Cesar o desplazamiento de Cesar, es una de las técnicas de cifrado mas simples y mas usadas";
                      
        string msgEncrypt = "Gp etkrvqitchkc, gn ekhtcfq Eguct, vcodkgp eqpqekfq eqoq ekhtcfq rqt fgurncbcokgpvq," +
                       " eqfkiq fg Eguct q fgurncbcokgpvq fg Eguct, gu wpc fg ncu vgepkecu fg ekhtcfq ocu ukorngu a ocu wucfcu";


        Cipher.encrypt(msg, 2); // se espera [msgEncrypt]
        Cipher.decrypt(msgEncrypt, 2); // se espera [msg]
        
        Cipher.encrypt("zizu", 2); // se espera [bkxb]
        Cipher.decrypt("bkxb", 2); // se espera [zizu]

        
        Cipher.encrypt("supercalifragilisticoespialidoso", 20); // se espera [mojylwufczluacfcmncwiymjcufcximi]
        Cipher.decrypt("mojylwufczluacfcmncwiymjcufcximi", 20); // se espera [supercalifragilisticoespialidoso]


        Console.ReadKey();

    }

    public class CesarCipher
    {
        public void encrypt(string msg, int key)
        {
            // variable para almacenar el mensaje cifrado
            string result = "";

            // reemplazar caracteres especiales
            msg = replaceChars(msg);

            // recorremos cada letra del mensaje
            foreach (char c in msg)
            {
                // si no es una letra o es una de las letras '<ñ> o <Ñ>' se devuelve tal cual
                if (!char.IsLetter(c))
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
                  .Replace('ú', 'u').Replace('Ú', 'U')
                  .Replace('ñ', 'n').Replace('Ñ', 'N');
        }

    }

}
