using System;
using Xunit;
using RedBlackTree;
using System.Collections.Generic;

namespace RedBlackTreeTest
{
    public class UnitTest1
    {
        private (RedBlackTree<int> Tree, int[] Pool) RandomTree(int size)
        {
            Random random = new Random(new Guid().GetHashCode());
            RedBlackTree<int> tree = new RedBlackTree<int>();

            //pool setup
            List<int> pool = new List<int>();
            for (int i = 0; i < size; i++)
            {
                pool.Add(i);
            }
            int[] returnPool = pool.ToArray();

            //tree building
            while (pool.Count != 0)
            {
                int index = random.Next(0, pool.Count);
                tree.Add(pool[index]);
                AssertInvariants(tree);
                pool.RemoveAt(index);
            }

            return (tree, returnPool);
        }

        private void AssertInvariants(RedBlackTree<int> tree)
        {
            // Root is black
            Assert.IsTrue((null == tree.Root) || tree._rootNode.IsBlack, "Root is not black");
            // Every path contains the same number of black nodes
            Dictionary<Node, Node> parents = new Dictionary<Node, Node>();
            foreach (Node node in tree.Traverse(tree._rootNode, n => true, n => n))
            {
                if (null != node.Left)
                {
                    parents[node.Left] = node;
                }
                if (null != node.Right)
                {
                    parents[node.Right] = node;
                }
            }
            if (null != tree._rootNode)
            {
                parents[tree._rootNode] = null;
            }
            int treeCount = -1;
            foreach (Node node in tree.Traverse(tree._rootNode, n => (null == n.Left) || (null == n.Right), n => n))
            {
                int pathCount = 0;
                Node current = node;
                while (null != current)
                {
                    if (current.IsBlack)
                    {
                        pathCount++;
                    }
                    current = parents[current];
                }
                Assert.IsTrue((-1 == treeCount) || (pathCount == treeCount), "Not all paths have the same number of black nodes.");
                treeCount = pathCount;
            }
            // Verify node properties...
            foreach (Node node in tree.Traverse(tree._rootNode, n => true, n => n))
            {
                // Left node is less
                if (null != node.Left)
                {
                    Assert.IsTrue(0 < node.Value.CompareTo(node.Left.Value), "Left node is greater than its parent.");
                }
                // Right node is greater
                if (null != node.Right)
                {
                    Assert.IsTrue(0 > node.Value.CompareTo(node.Right.Value), "Right node is less than its parent.");
                }
                // Both children of a red node are black
                Assert.IsTrue(!tree.IsRed(node) || (!tree.IsRed(node.Left) && !tree.IsRed(node.Right)), "Red node has a red child.");
                // Always left-leaning
                Assert.IsTrue(!tree.IsRed(node.Right) || tree.IsRed(node.Left), "Node is not left-leaning.");
            }
        }

        [Fact]
        public void Test1()
        {

        }
    }
}
