using System.Collections.Generic;
using Godot;

namespace DelaunatorSharp
{
    public interface IVoronoiCell
    {
        Vector2[] Points { get; }
        int Index { get; }
    }
}
