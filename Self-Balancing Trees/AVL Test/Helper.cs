using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AVL_Test
{
    internal static class Helper
    {
        public static int[] Randomize(int size, Random rand)
        {
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = rand.Next(0, size + 1);
            }
            return array;
        }

        public static int[] UniqueRandomization(int size, Random rand)
        {
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                int temp = rand.Next(0, size * 2);
                while (array.Contains(temp))
                {
                    temp = rand.Next(0, size * 2);
                }
                array[i] = temp;
            }
            return array;
        }
    }
}
