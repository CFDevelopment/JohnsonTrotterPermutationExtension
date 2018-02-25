using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace johnsonTrotter
{
   class Program
   {
       private const bool LEFT_TO_RIGHT = true;
       private const bool RIGHT_TO_LEFT = false;
       
     /*
        Pass a word to GenerateUniquePermutations to generate all possible unique permutations
     */
     static void Main(string[] args)
       {
          string word = "man";
          Console.WriteLine("The first word is man, there should be all unique permutations");
          GenerateUniquePermutations(word.Length, word);
          string word2 = "maa";
          Console.WriteLine("\n The second word is maa, there should be duplicate values, but corrected with a unqiue list of permutations");
          GenerateUniquePermutations(word2.Length, word2);
          /*
            For computing power/time commented out, but you can check the extra character
            string word3 = "christoper";
            Console.WriteLine("\n The second word is my name, just a test with more characters, expected 11! =>  39,916,800 combinations");
            GenerateUniquePermutations(word3.Length, word3);
          */
       }

        /*
            Function that returns the non 0 index of the mobile integer in the array.
        */
        public static int SearchArr(int[] a, int n, int mobile) {
            for(int i = 0; i < n; i++) {
                if (a[i] == mobile) {
                    return i + 1;
                }
            }
            return 0;
        }

        /*
           Return mobile number - a directed integer that is greater than its immediate neigbout in the direction it is facing. Iterate through word length, check
           if the direction is right to left or left to right. (Right to Left) => check if value is larger than its left neigbour, if so set the mobile to this new
           large number, set mobile_prev to number being swapped. Repeat for (Left to Right) => but act in opposite direction. Return mobile at end.
        */
        public static int GetMobile(int[] a, bool[] dir, int n){
            int mobile_prev = 0, mobile = 0;
            for(int i = 0; i < n; i++) {
                if(dir[a[i] - 1] == RIGHT_TO_LEFT && i != 0) {
                    if(a[i] > a[i -1] && a[i] > mobile_prev) {
                        mobile = a[i];
                        mobile_prev = mobile;
                    }
                }
                if(dir[a[i] - 1] == LEFT_TO_RIGHT && i != n-1) {
                    if( a[i] > a[i+1] &&  a[i] > mobile_prev) {
                        mobile = a[i];
                        mobile_prev = mobile;
                    }
                }
            }
            if(mobile == 0 && mobile_prev == 0) {
                return 0;
            } else {
                return mobile;
            }
        }

        /*
           This method returns an individual permutation by using the mapped character value associated with the 
           character index assigned to the original string. This extension of the Johnson and Trotter algorithm allows
           the user to save all permutations and filter out non unique combinations. We get the position of the mobile
           integer in relation to the array of values and swap this value with the preceding index if (Right to
           Left) or the superseding index if (Left to Right). This model is used to alternate the corresponding char
           value from the inital word being processed. Lastly you will invert the direction for any value greater than
           the mobile integer.
        */
        public static string PrintOnePermumation(int[] a, bool[] dir, int n, string word, Dictionary<int, char> map) {
           char[] letterArray = word.ToCharArray();
           int mobile = GetMobile(a, dir, n);
           int pos = SearchArr(a, n, mobile);
           StringBuilder sb = new StringBuilder();
           
           if(dir[a[pos - 1] -1 ] == RIGHT_TO_LEFT) {
               int temp = a[pos - 1];
               a[pos - 1] = a[pos - 2];
               a[pos - 2] = temp;
           } else if ( dir[a[pos - 1] - 1 ] == LEFT_TO_RIGHT) {
               int temp = a[pos];
               a[pos] = a[pos - 1];
               a[pos - 1] = temp;
           }
           for(int i = 0; i < n; i++) {
               if (a[i] > mobile) {
                   if(dir[a[i] - 1 ] == LEFT_TO_RIGHT) {
                       dir[a[i] - 1] = RIGHT_TO_LEFT;
                   } else if (dir[a[i] - 1] == RIGHT_TO_LEFT) {
                        dir[a[i] - 1] = LEFT_TO_RIGHT;
                   }
               }
           }
            for( int i = 0; i < n; i++) {
                 char result = map[a[i]];
                 sb.Append(result);
            }
            return sb.ToString();
        }

        /* 
            Generate and return the number of permuations through n factorial. 
        */
        public static int Factorial(int n) {
            int totalPermuations = 1;
            for(int i = 1; i <= n; i++) {
                totalPermuations *= i;
            }
            return totalPermuations;
        }

        /*
           This method creates an integer array to model the string being processes, generates a bool array to
           hold the direction of the integers, false => Right to Left, true => Left to Right. A map is created to 
           associate characters in the word to an index to be process n factioral times. A new list is created from 
           the unique values set in the original list of all possible permutations. Bried summary is displayed.
        */
        public static void GenerateUniquePermutations(int n, string word) {
            int[] numericalPlaceholders = new int[n];
            Boolean[] dir = new Boolean[n]; 
            List<string> pList = new List<string>(); 
            var map = new Dictionary<int, char>();
            pList.Add(word);

            Console.WriteLine("There are : " + Factorial(n) + " number of permutations");   
            
            for(int i = 0; i < n; i++) {
                numericalPlaceholders[i] = i + 1;
                map.Add(numericalPlaceholders[i], word[i]);   
                dir[i] = RIGHT_TO_LEFT;
            }

            for(int i = 1; i < Factorial(n); i++) {
                pList.Add(PrintOnePermumation(numericalPlaceholders, dir, n, word, map));
            }

            List<string> uniqueList = pList.Distinct().ToList();
            foreach(string uniWords in uniqueList) {
               Console.WriteLine(uniWords);
            }
            Console.WriteLine("Your word : " + word + " has " + pList.Count + " possible permutations, but only + " + uniqueList.Count + " unique combinations");
        }
   }
}


