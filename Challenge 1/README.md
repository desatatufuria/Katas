
  Challenge 1 (23/24) - Cifrado César
 --
  Crea un programa que realize el cifrado César de un texto y lo imprima.
  También debe ser capaz de descifrarlo cuando así se lo indiquemos.
 
  Te recomiendo que busques información para conocer en profundidad cómo
  realizar el cifrado. Esto también forma parte del reto.
  

  Metodología
  --
 He creado una clase [CesarCipher] que contiene dos métodos: [encrypt] y [decrypt]
  
 Para controlar las excepciones he creado un array de caracteres [excludedChars] que contiene las letras que no se cifrarán.
 incluyendo las letras con tilde y la letra ñ tanto en mayúsculas como en minúsculas.
 
 Se puede probar el codigo introduciendo una mensaje y una clave de desplazamiento para cifrarlo y descifrarlo,
 o bien se puede probar el test unitario básico que he creado para comprobar que el cifrado y descifrado funciona correctamente.
  
   
