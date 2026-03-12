using System;
using System.Collections.Generic;

namespace Utils
{
    public class Randomizer
    {
        public static void RandomizeList<T>(ref List<T> list)
        {
            Random random = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];  
                list[k] = list[n];  
                list[n] = value;  
            }
        }
        
        public static void RandomizeArray<T>(ref T[] array)
        {
            Random random = new Random();
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = array[k];  
                array[k] = array[n];  
                array[n] = value;   
            }
        }
    }
}