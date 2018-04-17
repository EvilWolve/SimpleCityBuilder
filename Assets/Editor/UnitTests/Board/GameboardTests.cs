using NUnit.Framework;
using Board;
using Configuration.Board;
using UnityEngine;

namespace UnitTests.Board
{
    public class GameboardTests
    {
        IGameboard gameboard;

        [SetUp]
        public void Setup()
        {
            this.gameboard = new Gameboard();
        }

        [Test]
        public void TestInitWidthAndHeight()
        {
            int width = 25;
            int height = 30;
            
            this.gameboard.Initialise(this.CreateFakeConfig(width, height));
            
            Assert.AreEqual(width, this.gameboard.Width);
            Assert.AreEqual(height, this.gameboard.Height);
        }

        [Test]
        public void TestSetAndCheckOccupy()
        {
            int width = 4;
            int height = 4;
            
            this.gameboard.Initialise(this.CreateFakeConfig(width, height));

            GridRect targetRect = new GridRect(0, 0, 1, 1);
            this.gameboard.SetOccupied(targetRect, true);
            
            Assert.IsTrue(this.gameboard.IsOccupied(targetRect));
            Assert.IsFalse(this.gameboard.IsOccupied(new GridRect(1, 1, 1, 1)));
        }

        [Test]
        public void TestSetReleaseAndCheckOccupyFully()
        {
            int width = 4;
            int height = 4;
            
            this.gameboard.Initialise(this.CreateFakeConfig(width, height));

            GridRect targetRect = new GridRect(0, 0, 4, 4);
            this.gameboard.SetOccupied(targetRect, true);
            this.gameboard.SetOccupied(targetRect, false);
            
            Assert.IsFalse(this.gameboard.IsOccupied(targetRect));
        }

        [Test]
        public void TestSetReleaseAndCheckOccupyPartially()
        {
            int width = 4;
            int height = 4;
            
            this.gameboard.Initialise(this.CreateFakeConfig(width, height));

            GridRect targetRect = new GridRect(0, 0, 4, 4);
            this.gameboard.SetOccupied(targetRect, true);
            
            GridRect freeRect = new GridRect(1, 1, 1, 1);
            this.gameboard.SetOccupied(freeRect, false);
            
            Assert.IsTrue(this.gameboard.IsOccupied(targetRect));
            Assert.IsFalse(this.gameboard.IsOccupied(freeRect));
            Assert.IsTrue(this.gameboard.IsOccupied(new GridRect(0, 0, 1, 1)));
        }

        [Test]
        public void TestClear()
        {
            int width = 4;
            int height = 4;
            
            this.gameboard.Initialise(this.CreateFakeConfig(width, height));

            GridRect targetRect = new GridRect(0, 0, 4, 4);
            this.gameboard.SetOccupied(targetRect, true);
            
            this.gameboard.Clear();
            
            Assert.IsFalse(this.gameboard.IsOccupied(targetRect));
        }

        GameboardConfiguration CreateFakeConfig(int width, int height)
        {
            GameboardConfiguration fakeConfig = ScriptableObject.CreateInstance<GameboardConfiguration>();
            fakeConfig.dimensions = new Vector2Int(width, height);

            return fakeConfig;
        }
    }
}