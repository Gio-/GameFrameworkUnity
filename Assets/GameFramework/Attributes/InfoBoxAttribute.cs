using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum InfoBoxMessageType { None, Info, Warning, Error }

[AttributeUsage(AttributeTargets.Field, Inherited = true)]
public class InfoBoxAttribute : PropertyAttribute
{
    public readonly string text;
#if UNITY_EDITOR
    public readonly InfoBoxMessageType type;
#endif

    /// <summary>
    /// Adds a HelpBox to the Unity property inspector above this field.
    /// </summary>
    /// <param name="text">The help text to be displayed in the HelpBox.</param>
    /// <param name="type">The icon to be displayed in the HelpBox.</param>
    public InfoBoxAttribute(string text , InfoBoxMessageType type = InfoBoxMessageType.Info)
    {
        this.text = text;
        this.type = type;
    }
}