using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Conways.Library.Tests
{
    // Any live cell with fewer than two live neighbours dies.
    // Any live cell with two or three live neighbours lives.
    // Any live cell with more than three live neighbours dies.
    // Any dead cell with exactly three live neighbours becomes a live cell.

    [TestFixture]
    class LifeRulesTests
    {
        [Test]
        public void LiveCell_FewerThan2LiveNeighbors_Dies(
            [Values(0, 1)] int liveNeighbors)
        {
            //Arrange
            var currentState = CellState.Alive;

            //Act
            CellState newState = LifeRules.GetNewState(currentState, liveNeighbors);

            //Assert
            Assert.That(newState, Is.EqualTo(CellState.Dead));
        }

        [Test]
        public void LiveCell_2Or3LiveNeighbors_Lives(
            [Values(2, 3)] int liveNeighbors)
        {
            //Arrange
            var currentState = CellState.Alive;

            //Act
            CellState newState = LifeRules.GetNewState(currentState, liveNeighbors);

            //Assert
            Assert.That(newState, Is.EqualTo(CellState.Alive));
        }

        [Test]
        public void LiveCell_MoreThan3LiveNeighbors_Dies(
            [Range(4, 8)] int liveNeighbors)
        {
            //Arrange
            var currentState = CellState.Alive;

            //Act
            CellState newState = LifeRules.GetNewState(currentState, liveNeighbors);

            //Assert
            Assert.That(newState, Is.EqualTo(CellState.Dead));
        }

        [Test]
        public void DeadCell_Exactly3LiveNeighbors_Lives() 
        {
            //Arrange
            var currentState = CellState.Dead;
            var liveNeighbors = 3;

            //Act
            CellState newState = LifeRules.GetNewState(currentState, liveNeighbors);

            //Assert
            Assert.That(newState, Is.EqualTo(CellState.Alive));

        }

        [Test]
        public void DeadCell_FewerThan3LiveNeighbors_StaysDead([Range(0, 2)] int liveNeighbors)
        {
            //Arrange
            var currentState = CellState.Dead;

            //Act
            CellState newState = LifeRules.GetNewState(currentState, liveNeighbors);

            //Assert
            Assert.That(newState, Is.EqualTo(CellState.Dead));

        }

        [Test]
        public void DeadCell_MoreThan3LiveNeighbors_StaysDead([Range(4, 8)] int liveNeighbors)
        {
            //Arrange
            var currentState = CellState.Dead;

            //Act
            CellState newState = LifeRules.GetNewState(currentState, liveNeighbors);

            //Assert
            Assert.That(newState, Is.EqualTo(CellState.Dead));

        }

        [Test]
        
        public void CurrentState_When2_ThrowsArgumentException()
        {
            var currentState = (CellState)2;
            var liveNeighbors = 0;
            Assert.Throws<ArgumentOutOfRangeException>(() => LifeRules.GetNewState(currentState, liveNeighbors));
        }

        [Test]
        public void LiveNeighbors_MoreThan8_ThrowsArgumentException()
        {
            var currentState = CellState.Alive;
            var liveNeighbors = 9;
            var paramName = "liveNeighbors";
            try
            {
                CellState newState = LifeRules.GetNewState(currentState, liveNeighbors);
            }
            catch (ArgumentOutOfRangeException ex) 
            {
                if (ex.ParamName != paramName)
                    Assert.Fail($"Wrong Parameter. Expected: '{paramName}', Actual:'{ex.ParamName}'");

                Assert.Pass();
            }
        }

        [Test]
        public void LiveNeighbors_LessThan0_ThrowsArgumentException()
        {
            var currentState = CellState.Alive;
            var liveNeighbors = -1;
            var paramName = "liveNeighbors";

            Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>()
                .And.Property("ParamName")
                .EqualTo(paramName),
                () => LifeRules.GetNewState(currentState, liveNeighbors));
        }
    }
}
