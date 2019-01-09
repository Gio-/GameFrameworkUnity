using UnityEngine;

namespace GameFramework
{
    public class ContactChecker2D : MonoBehaviour
    {
        #region VARIABLES
        public bool debugCheckers = true;
        [ShowIf("debugCheckers", true)]
        public Color CheckedObstacleDebugColor = Color.red;
        [ShowIf("debugCheckers", true)]
        public Color leftDebugColor = Color.green;
        [ShowIf("debugCheckers", true)]
        public Color topDebugColor = Color.green;
        [ShowIf("debugCheckers", true)]
        public Color rightDebugColor = Color.green;
        [ShowIf("debugCheckers", true)]
        public Color bottomDebugColor = Color.green;
        #endregion

        #region SIDES CHECKS
        /// <summary>
        /// Check Area on Left of player and return all collider founded
        /// </summary>
        /// <param name="col">The ref collider used to get bounds of area</param>
        /// <param name="layerMask">filter mask</param>
        /// /// <param name="areaSize">Size of checker</param>
        /// <param name="marginTop">Margin from collider top</param>
        /// <param name="marginBottom">Margin from collider bottom</param>
        /// <returns></returns>
        public Collider2D[] CheckLeftArea(Collider2D col, LayerMask layerMask, float areaSize, float marginTop = 0, float marginBottom = 0)
        {
            Vector2 bottomLeft = new Vector2(col.bounds.min.x, col.bounds.min.y);
            Vector2 topRight = new Vector2(col.bounds.max.x, col.bounds.max.y);
            Vector2 topLeft = new Vector2(bottomLeft.x, topRight.y);

            bottomLeft.y += marginBottom;
            topLeft.y -= marginTop;

            Vector2 pointA = topLeft;
            Vector2 pointB = new Vector2(bottomLeft.x - areaSize, bottomLeft.y);
            Collider2D[] check = CheckArea(pointA, pointB, layerMask);
            if (debugCheckers)
            {
                Color debugColor = check.Length > 0 ? CheckedObstacleDebugColor : bottomDebugColor;

                Vector3 point1 = pointA;
                Vector3 point2 = new Vector3(point1.x - areaSize, point1.y, 0);
                Vector3 point3 = new Vector3(point2.x, pointB.y, 0);
                Vector3 point4 = pointB;
                Vector3 point5 = new Vector3(pointB.x + areaSize, pointB.y, 0);
                Vector3[] debugPoints = { point1, point2, point3, point4, point5 };

                DebugUtils.DrawPoly(debugPoints, debugColor);
            }

            return check;
        }
        /// <summary>
        /// Check Area on Right of player and return all collider founded
        /// </summary>
        /// <param name="col">The ref collider used to get bounds of area</param>
        /// <param name="layerMask">filter mask</param>
        /// <param name="areaSize">Size of checker</param>
        /// <param name="marginTop">Margin from collider top</param>
        /// <param name="marginBottom">Margin from collider bottom</param>
        /// <returns></returns>
        public Collider2D[] CheckRightArea(Collider2D col,LayerMask layerMask, float areaSize, float marginTop = 0, float marginBottom = 0)
        {
            Vector2 bottomLeft = new Vector2(col.bounds.min.x, col.bounds.min.y);
            Vector2 topRight = new Vector2(col.bounds.max.x, col.bounds.max.y);
            Vector2 bottomRight = new Vector2(topRight.x, bottomLeft.y);
            bottomRight.y += marginBottom;
            topRight.y -= marginTop;

            Vector2 pointA = topRight;
            Vector2 pointB = new Vector2(bottomRight.x + areaSize, bottomRight.y);

            Collider2D[] check = CheckArea(pointA, pointB, layerMask);
            if (debugCheckers)
            {
                Color debugColor = check.Length > 0 ? CheckedObstacleDebugColor : bottomDebugColor;

                Vector3 point1 = pointA;
                Vector3 point2 = new Vector3(point1.x + areaSize, point1.y, 0);
                Vector3 point3 = new Vector3(point2.x, pointB.y, 0);
                Vector3 point4 = pointB;
                Vector3 point5 = new Vector3(pointB.x - areaSize, pointB.y, 0);
                Vector3[] debugPoints = { point1, point2, point3, point4, point5 };

                DebugUtils.DrawPoly(debugPoints, debugColor);
            }

            return check;
        }
        /// <summary>
        /// Check Area on Top of player and return all collider founded
        /// </summary>
        /// <param name="col">The ref collider used to get bounds of area</param>
        /// <param name="layerMask">filter mask</param>
        /// /// <param name="areaSize">Size of checker</param>
        /// <param name="marginLeft">Margin from collider left</param>
        /// <param name="marginRight">Margin from collider right</param>
        public Collider2D[] CheckTopArea(Collider2D col, LayerMask layerMask, float areaSize, float marginLeft = 0, float marginRight = 0)
        {
            Vector2 bottomLeft = new Vector2(col.bounds.min.x, col.bounds.min.y);
            Vector2 topRight = new Vector2(col.bounds.max.x, col.bounds.max.y);
            Vector2 topLeft = new Vector2(bottomLeft.x, topRight.y);

            topLeft.x += marginLeft;
            topRight.x -= marginRight;

            Vector2 pointA = topLeft;
            Vector2 pointB = new Vector2(topRight.x, topRight.y + areaSize);

            Collider2D[] check = CheckArea(pointA, pointB, layerMask);
            if (debugCheckers)
            {
                Color debugColor = check.Length > 0 ? CheckedObstacleDebugColor : bottomDebugColor;

                Vector3 point1 = pointA;
                Vector3 point2 = new Vector3(point1.x, point1.y + areaSize, 0);
                Vector3 point3 = new Vector3(pointB.x, point2.y, 0);
                Vector3 point4 = pointB;
                Vector3 point5 = new Vector3(pointB.x, pointB.y - areaSize, 0);
                Vector3[] debugPoints = { point1, point2, point3, point4, point5 };

                DebugUtils.DrawPoly(debugPoints, debugColor);
            }

            return check;

        }
        /// <summary>
        /// Check Area on BOTTOM of player and return all collider founded
        /// </summary>
        /// <param name="col">The ref collider used to get bounds of area</param>
        /// <param name="layerMask">filter mask</param>
        /// /// <param name="areaSize">Size of checker</param>
        /// <param name="marginLeft">Margin from collider left</param>
        /// <param name="marginRight">Margin from collider right</param>
        public Collider2D[] CheckBottomArea(Collider2D col, LayerMask layerMask, float areaSize, float marginLeft = 0, float marginRight = 0)
        {
            Vector2 bottomLeft = new Vector2(col.bounds.min.x, col.bounds.min.y);
            Vector2 topRight = new Vector2(col.bounds.max.x, col.bounds.max.y);
            Vector2 bottomRight = new Vector2(topRight.x, bottomLeft.y);

            bottomRight.x -= marginRight;
            bottomLeft.x += marginLeft;

            Vector2 pointA = bottomLeft;
            Vector2 pointB = new Vector2(bottomRight.x, bottomRight.y - areaSize);
            Collider2D[] check = CheckArea(pointA, pointB, layerMask);
            if (debugCheckers)
            {
                
                Color debugColor = check.Length > 0 ? CheckedObstacleDebugColor : bottomDebugColor;

                Vector3 point1 = pointA;
                Vector3 point2 = new Vector3(point1.x, point1.y - areaSize, 0);
                Vector3 point3 = new Vector3(pointB.x, point2.y, 0);
                Vector3 point4 = pointB;
                Vector3 point5 = new Vector3(pointB.x, pointB.y + areaSize, 0);
                Vector3[] debugPoints = { point1, point2, point3, point4, point5 };

                DebugUtils.DrawPoly(debugPoints, debugColor);
            }
            Rigidbody2D rb;
            
            return check;
        }
        #endregion

        #region ISGROUNDED CHECK
        public bool IsGrounded(Collider2D col, LayerMask groundMask,float checkerSize =.3f)
        {
            return (CheckBottomArea(col, groundMask, checkerSize).Length > 0);

        }
        #endregion

        #region GENERIC CHECKS
        public Collider2D[] CheckArea(Vector2 topLeft, Vector2 bottomRight, LayerMask layerMask)
        {
            return Physics2D.OverlapAreaAll(topLeft, bottomRight, layerMask);
        }
        public Collider2D[] CheckSphere(Vector2 sphereCenter, float sphereRadius, LayerMask layerMask)
        {
            return Physics2D.OverlapCircleAll(sphereCenter, sphereRadius, layerMask);
        }
        #endregion

    }
}
