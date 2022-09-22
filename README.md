# NiftyProc
General Purpose Procedrual Generation Library for Godot

# Current tools

## Voroni/Delaunay Triangulation
Port of the Delaunator Sharp https://github.com/nol1fe/delaunator-sharp. Updated to use Godot Vector2's for compatibility at a low accuracy due to using floats instead of doubles
## Poisson Disc Sampler
Adaption from http://theinstructionlimit.com/fast-uniform-poisson-disk-sampling-in-c
Changed to use Godot.Vector2 and added SampleRect using a Rect2
