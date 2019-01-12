using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[AttributeUsage(AttributeTargets.Field, Inherited = true)]
public class InfoBoxAttribute : PropertyAttribute
{
    public readonly string text;
#if UNITY_EDITOR
    public readonly MessageType type;
#endif

    /// <summary>
    /// Adds a HelpBox to the Unity property inspector above this field.
    /// </summary>
    /// <param name="text">The help text to be displayed in the HelpBox.</param>
    /// <param name="type">The icon to be displayed in the HelpBox.</param>
    public InfoBoxAttribute(string text
#if UNITY_EDITOR
, MessageType type = MessageType.Info
#endif
)
    {
        this.text = text;
#if UNITY_EDITOR
        this.type = type;
#endif
    }
}