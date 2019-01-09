using UnityEngine;
namespace GameFramework
{
    public static class DebugUtils
    {
        public static void DrawPoly(Vector3[] points, Color color)
        {
            for (int i = 0; i < points.Length - 1; i++)
            {
                Debug.DrawLine(points[i], points[i + 1], color);
            }
        }
    }
}



