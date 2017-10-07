using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods {

    private const float FloatComparition = .1f;
    public static bool IsNear(this float value, float other)
    {
        return Mathf.Abs(value - other) < FloatComparition;
    }
	
}
