using System.Collections.Generic;
using Godot;
using Godot.Collections;
using NiftyProc;

namespace NiftyProcLibrary.Debug
{
    [Tool]
    public class DebugPoissonDiscSampler : Node2D
    {
        public delegate void Changed(List<Vector2> points);
        
        protected Rect2 _region;
        
        [Export()]
        public Rect2 Region
        {
            get => _region;
            set
            {
                _region = value;
                Sample(true);
            }
        }

        protected float _minimumDistance;
        
        [Export()]
        public float MinimumDistance
        {
            get => _minimumDistance;
            set
            {
                _minimumDistance = value;
                Sample(true);
            }
        }
        
        protected List<Vector2> _points;
        public List<Vector2> Points => _points;

        protected bool _isDirty;

        public event Changed OnChange;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            Sample();
        }
        
        public void Sample(bool forceUpdate = false)
        {
            GD.Print("Sampling");
            _points = UniformPoissonDiskSampler.SampleRectangle(Region, MinimumDistance);
            if (forceUpdate)
            {
                _isDirty = true;
            }
            OnChange?.Invoke(_points);
        }

        public override void _Draw()
        {
            if (Engine.EditorHint && _points != null)
            {
                DrawRect(Region, Colors.Green, false, 1f, false);
                int len = _points.Count;
                for (int i = 0; i < len; i++)
                {
                    Vector2 point = _points[i];
                    DrawRect(new Rect2(point, Vector2.One), Colors.Aqua, true);
                }
            }
            base._Draw();
        }

        public override bool _Set(string property, object value)
        {
            GD.Print("Set Test " + property);
            GD.Print("nameof" + property == "_points");
            if (property == nameof(Region) || property == nameof(MinimumDistance) || property ==nameof(_points))
            {
                Sample();
                _isDirty = true;
            }
            return base._Set(property, value);
        }

        public override Array _GetPropertyList()
        {
            return base._GetPropertyList();
        }

        public override void _Process(float delta)
        {
            if (_isDirty)
            {
                Update();
            }
            base._Process(delta);
        }
    }
}