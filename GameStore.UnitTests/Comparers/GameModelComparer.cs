using System.Collections.Generic;
using GameStore.Application.Models;

namespace GameStore.UnitTests.Comparers;

public class GameModelComparer : IEqualityComparer<GameModel>
{
    public bool Equals(GameModel x, GameModel y)
    {
        if (x is null && y is null)
        {
            return true;
        }

        if (x is null || y is null)
        {
            return false;
        }

        return x.Key == y.Key;
    }

    public int GetHashCode(GameModel obj)
    {
        return obj.Key.GetHashCode();
    }
}
