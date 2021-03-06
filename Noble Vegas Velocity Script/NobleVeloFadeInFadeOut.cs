using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Reflection;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using ScriptPortal.Vegas;
public class EntryPoint
{
Vegas myVegas;
public void FromVegas(Vegas vegas)
{
myVegas = vegas;
Timecode fourF = Timecode.FromMilliseconds(133);
foreach (Track track in myVegas.Project.Tracks)
{
if (track.IsVideo())
{
foreach (TrackEvent evnt in track.Events)
{
if (evnt.Selected) {

VideoEvent videoEvent = (VideoEvent)evnt;

Envelope VelEnv = new Envelope(EnvelopeType.Velocity);
videoEvent.Envelopes.Add(VelEnv);

Timecode t = evnt.Start-evnt.Start;

EnvelopePoint a = VelEnv.Points.GetPointAtX(t);
if (a != null){a.Y = 4;};

EnvelopePoint bTest = VelEnv.Points.GetPointAtX(fourF);
if (bTest == null){
EnvelopePoint b = new EnvelopePoint(fourF, 0.50);
VelEnv.Points.Add(b);
};

a.Curve = CurveType.Fast;

EnvelopePoint cTest = VelEnv.Points.GetPointAtX(evnt.Length-fourF);
if (cTest == null){
EnvelopePoint c = new EnvelopePoint(evnt.Length-fourF, 0.50);
VelEnv.Points.Add(c);
c.Curve = CurveType.Slow;
};

EnvelopePoint dTest = VelEnv.Points.GetPointAtX(evnt.Length);
if (dTest == null){
EnvelopePoint d = new EnvelopePoint(evnt.Length, 4);
VelEnv.Points.Add(d);
};


}
}
}
}
}
}
//Orginally by Str0kd 
//Velocity settings by Noble
