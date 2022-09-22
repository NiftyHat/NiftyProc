using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using DelaunatorSharp;

namespace NiftyProcLibrary.Debug
{
    [Tool]
    public class DebugDelauny : Node2D
    {
        private DebugPoissonDiscSampler _poissonDisc;
        private Delaunator _delaunator;

        private Vector2[] _points;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _poissonDisc = GetNode<DebugPoissonDiscSampler>("DebugPoissonDiscSampler");
            if (_poissonDisc != null)
            {
                if (_poissonDisc.Points != null && _poissonDisc.Points.Count > 3)
                {
                    _points = _poissonDisc.Points.ToArray();
                }

                _poissonDisc.OnChange += HandlePoissonDiscChange;
            }

            Regenerate();
        }

        private void HandlePoissonDiscChange(List<Vector2> points)
        {
            _points = points.ToArray();
        }

        private void Regenerate()
        {
            if (_points != null && _points.Length > 3)
            {
                _delaunator = new Delaunator(_points);
            }
        }

        public override void _Draw()
        {
            if (Engine.EditorHint && _delaunator != null)
            {
                var hullPoints = _delaunator.GetHullPoints();
                DrawPolyline(hullPoints, Colors.White, 1f, false);
                Color color = new Color();
                Random rnd = new Random();
                _delaunator.ForEachVoronoiEdge(edge => { DrawLine(edge.P, edge.Q, Colors.Black); });
                _delaunator.ForEachVoronoiCell(cell =>
                {
                    if (cell.Points.Length > 2)
                    {
                        color.h = (float)rnd.NextDouble();
                        color.s = 0.4f;
                        color.v = 0.8f;
                        color.a = 0.7f;
                        DrawColoredPolygon(cell.Points.ToArray(), color);
                    }
                });
            }

            base._Draw();
        }
    }
}