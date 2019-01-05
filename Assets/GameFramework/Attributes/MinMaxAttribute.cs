using UnityEngine;

public class MinMaxAttribute : PropertyAttribute
{
    public float MinLimit = 0;
    public float MaxLimit = 1;
    public bool ShowEditRange = true;
    public bool ShowDebugValues = true;

    public MinMaxAttribute(int min, int max)
    {
        MinLimit = min;
        MaxLimit = max;
    }
}