using System;

using AVL;
using RedBlackTree;
using BenchmarkRunner;

namespace BenchmarkTests
{
    class Program
    {

        class Tests
        {
            static Random random = new Random(26);
            static int size = 1000000;

            [Benchmark]
            public static void AVLTest()
            {
                AVL<int> avl = new AVL<int>();
                for (int i = 0; i < size; i++)
                {
                    avl.Add(random.Next());
                }
            }

            [Benchmark]
            public static void RedBlackTest()
            {
                RedBlackTree<int> redBlack = new RedBlackTree<int>();
                for (int i = 0; i < size; i++)
                {
                    redBlack.Add(random.Next());
                }
            }


        }


        static void Main(string[] args)
        {
            BenchmarkRunner.BenchmarkRunner.Run<Tests>();
        }
    }
}
