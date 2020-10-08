using System;
using Xunit;
using RedBlackTree;
using System.Collections.Generic;

namespace RedBlackTreeTest
{
    public class UnitTest1
    {
        internal Random rand = new Random(26);
        List<int> CreateTree(int length, (int, int) range)
        {
            List<int> datatype = new List<int>();
            for (int i = 0; i < length; i++)
            {

                datatype.Add(i);

            }

            return datatype;

        }



        [Fact]
        public void Add()
        {
            List<int> meow = CreateTree(5, (5, 6));

        }
    }
}
