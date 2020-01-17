using System;
using System.Collections.Generic;
using System.Text;

namespace Conways.Library
{
    public enum CellState
    {
        Alive,
        Dead
    }
    public class LifeRules
    {
        // The approach here is functional programming rather than OOP
        public static CellState GetNewState(CellState currentState, int liveNeighbors)
        {
            if (!Enum.IsDefined(typeof(CellState), currentState))
                throw new ArgumentOutOfRangeException(nameof(currentState));

            if (liveNeighbors < 0 || liveNeighbors > 8)
                throw new ArgumentOutOfRangeException(nameof(liveNeighbors));

            switch(currentState)
            {
                case CellState.Alive:
                    if(liveNeighbors < 2 || liveNeighbors > 3)
                        return CellState.Dead;
                    break;
                case CellState.Dead:
                    if(liveNeighbors == 3)
                        return CellState.Alive;
                    break;
            }
            return currentState;
        }
    }
}
