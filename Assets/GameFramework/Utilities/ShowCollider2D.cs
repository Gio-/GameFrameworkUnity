using UnityEngine;
namespace MyCustomFramework
{
    [RequireComponent(typeof(Collider2D))]
    public class ShowCollider2D : MonoBehaviour
    {
        [Header("ONLY FOR CUBE AND SPHERE FOR NOW")]

        [SerializeField]
        private Collider2D col;
        [SerializeField]
        private Color gizmoColor = Color.blue;



        public static void DrawLocalLine(Transform tr, Vector3 p1, Vector3 p2)
        {
            Gizmos.DrawLine(tr.TransformPoint(p1), tr.TransformPoint(p2));
        }


        void OnDrawGizmos()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            if (!this.isActiveAndEnabled)
                return;
            //to use real obj position
            Gizmos.matrix = transform.localToWorldMatrix;

            Gizmos.color = gizmoColor;
            col = col ?? GetComponent<Collider2D>();

            System.Type colType = col.GetType();


            if (col == null)
                return;
            if (colType == typeof(BoxCollider2D))
            {
                Vector3 size = col.bounds.size;
                BoxCollider2D boxCol = (BoxCollider2D)col;
                Gizmos.DrawWireCube(transform.InverseTransformPoint(boxCol.bounds.center), boxCol.size);
            }
            /*else if (colType == typeof(SphereCollider))
            {
                SphereCollider spCol = (SphereCollider)col;
                Vector3 size = col.bounds.size;
                Gizmos.DrawWireSphere(spCol.center, spCol.radius);
            }*/
            else if (colType == typeof(PolygonCollider2D))
            {
                Vector2[] points = ((PolygonCollider2D)col).points;
                Gizmos.color = Color.blue;

                // for every point (except for the last one), draw line to the next point
                for (int i = 0; i < points.Length - 1; i++)
                {
                    DrawLocalLine(transform, (Vector3)points[i], (Vector3)points[i + 1]);
                }
                // for polygons, close with the last segment
                DrawLocalLine(transform, (Vector3)points[points.Length - 1], (Vector3)points[0]);

            }
        }
    }
}