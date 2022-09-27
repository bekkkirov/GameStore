using System.Collections.Generic;
using GameStore.Application.Models;

namespace GameStore.UnitTests.Comparers;

public class CommentModelComparer : IEqualityComparer<CommentModel>
{
    public bool Equals(CommentModel x, CommentModel y)
    {
        if (x is null && y is null)
        {
            return true;
        }

        if (x is null || y is null)
        {
            return false;
        }

        return x.Body == y.Body;
    }

    public int GetHashCode(CommentModel obj)
    {
        return obj.Body.GetHashCode();
    }
}