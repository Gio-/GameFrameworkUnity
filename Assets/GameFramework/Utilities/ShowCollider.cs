using UnityEngine;

namespace MyCustomFramework
{
    [RequireComponent(typeof(Collider))]
    public class ShowCollider : MonoBehaviour
    {

        [Header("ONLY FOR CUBE AND SPHERE FOR NOW")]

        [SerializeField]
        private Collider col;
        [SerializeField]
        private Color gizmoColor = Color.blue;
        [Header("Mesh for complex colliders")]
        [SerializeField]
        private Mesh mesh;






        void OnDrawGizmos()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            if (!this.isActiveAndEnabled)
                return;
            //to use real obj position
            Gizmos.matrix = transform.localToWorldMatrix;

            Gizmos.color = gizmoColor;
            col = col ?? GetComponent<Collider>();

            System.Type colType = col.GetType();


            if (col == null)
                return;
            if (colType == typeof(BoxCollider))
            {
                Vector3 size = col.bounds.size;
                BoxCollider boxCol = (BoxCollider)col;
                Gizmos.DrawWireCube(boxCol.center, boxCol.size);
            }
            else if (colType == typeof(SphereCollider))
            {
                SphereCollider spCol = (SphereCollider)col;
                Vector3 size = col.bounds.size;
                Gizmos.DrawWireSphere(spCol.center, spCol.radius);
            }
            else if (colType == typeof(MeshCollider))
            {
                MeshCollider meshCollider;
                if (mesh == null)
                {
                    meshCollider = (MeshCollider)col;
                    mesh = meshCollider.sharedMesh;
                }


                //TODO ROTATION NOT UPDATED CORRECTLY
                Gizmos.DrawWireMesh(mesh, Vector3.zero, Quaternion.identity, Vector3.one);
            }
        }

    }
}