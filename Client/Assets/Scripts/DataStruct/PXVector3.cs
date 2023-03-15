using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public struct PXVector3
{
    public float x;
    public float y;
    public float z;
    public PXVector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public static implicit operator PXVector3(Vector3 v) { return new PXVector3(v.x, v.y, v.z); }
    public static implicit operator Vector3(PXVector3 v) { return new Vector3(v.x, v.y, v.z); }
}

