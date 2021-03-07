﻿using UnityEngine;

public static class VectorExtension
{
    public static Vector2 IgnoreZ(this Vector3 vec)
    {
        return new Vector2(vec.x, vec.y);
    }
}