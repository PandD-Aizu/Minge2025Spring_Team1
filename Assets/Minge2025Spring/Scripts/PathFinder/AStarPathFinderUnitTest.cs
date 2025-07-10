using NUnit.Framework;
using UnityEngine;

namespace PathFinder
{
    public class AStarPathFinderUnitTest
    {
        private Graph graph;
        private AStarPathFinder pathFinder;

        [SetUp]
        public void Setup()
        {
            graph = new Graph();
            pathFinder = new AStarPathFinder(graph);
        }

        [Test]
        public void TestFindPath()
        {
            // Arrange
            var blockA = new NormalBlock("A", new Vector3(0, 0, 0));
            var blockB = new NormalBlock("B", new Vector3(1, 0, 0));
            var blockC = new NormalBlock("C", new Vector3(2, 0, 0));
            graph.AddBlock(blockA);
            graph.AddBlock(blockB);
            graph.AddBlock(blockC);
            graph.AddEdge("A", "B", 1);
            graph.AddEdge("B", "C", 1);
            
            // Act
            var path = pathFinder.FindPath("A", "C");

            // Assert
            Assert.IsNotNull(path);
            Assert.AreEqual(3, path.path.Count);
            Assert.AreEqual("A", path.path[0]);
            Assert.AreEqual("B", path.path[1]);
            Assert.AreEqual("C", path.path[2]);
        }
    }
}