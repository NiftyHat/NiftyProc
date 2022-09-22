using System.Collections.Generic;
using Godot;

namespace DelaunatorSharp
{
    public interface ITriangle
    {
        IEnumerable<Vector2> Points { get; }
        int Index { get; }
    }
}
