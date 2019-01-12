/**
 * @author Fabrizio Coppolecchia
 *
 * Component used to play audio. Usage:
 * 1 - Attach to a object, select when you want to play audio from playEvent dropdown
 * 2 - make sure to attach right collider and eventually attach rigidbody to detect collision if you use trigger/trigger2D/collision/collision2d
 * 3 - define audio parameters
 * 
 * This class also provide a simple pool system to improve speed
 * 
 * @date - 2019/01/03
 */

using System;
using UnityEngine;

namespace GameFramework
{
    public class ExecuteAction : MonoBehaviour
    {
        #region VARIABLES
        [SerializeField]
        protected ExecuteWhen executeWhen;
        [ShowIf("executeWhen", 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17)]
        [SerializeField]
        protected LayerMask allowedLayers;
        [ShowIf("executeWhen", 1)]
        public EventsID eventToListen;

 
        #endregion
        #region EVENTS
        public UnityEngine.Events.UnityEvent actionToExecute;
        #endregion

        protected virtual void Awake()
        {
        }

        #region MONOBEHAVIOUR METHODS
        protected virtual void Start()
        {
            if (executeWhen.Equals(ExecuteWhen.START))
                Execute();
        }
        protected virtual void OnEnable()
        {
            if (executeWhen.Equals(ExecuteWhen.ENABLE))
                Execute();
        }
        protected virtual void OnDisable()
        {


            if (executeWhen.Equals(ExecuteWhen.DISABLE))
                Execute();
        }
        protected virtual void OnDestroy()
        {
            if (executeWhen.Equals(ExecuteWhen.DESTROY))
                Execute();
            actionToExecute.RemoveAllListeners();
        }

        #region COLLISION/TRIGGER DETECTIONS
        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (executeWhen.Equals(ExecuteWhen.COLLISIONENTER) && Utilities.LayerIsInLayerMask(collision.gameObject.layer, allowedLayers))
                Execute();
        }
        protected virtual void OnCollisionStay(Collision collision)
        {
            if (executeWhen.Equals(ExecuteWhen.COLLISIONSTAY) && Utilities.LayerIsInLayerMask(collision.gameObject.layer, allowedLayers))
                Execute();
        }
        protected virtual void OnCollisionExit(Collision collision)
        {
            if (executeWhen.Equals(ExecuteWhen.COLLISIONEXIT) && Utilities.LayerIsInLayerMask(collision.gameObject.layer, allowedLayers))
                Execute();
        }
        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (executeWhen.Equals(ExecuteWhen.COLLISIONENTER2D) && Utilities.LayerIsInLayerMask(collision.gameObject.layer, allowedLayers))
                Execute();
        }
        protected virtual void OnCollisionStay2D(Collision2D collision)
        {
            if (executeWhen.Equals(ExecuteWhen.COLLISIONSTAY2D) && Utilities.LayerIsInLayerMask(collision.gameObject.layer, allowedLayers))
                Execute();
        }
        protected virtual void OnCollisionExit2D(Collision2D collision)
        {
            if (executeWhen.Equals(ExecuteWhen.COLLISIONEXIT2D) && Utilities.LayerIsInLayerMask(collision.gameObject.layer, allowedLayers))
                Execute();
        }
        protected virtual void OnTriggerEnter(Collider other)
        {
            if (executeWhen.Equals(ExecuteWhen.TRIGGERENTER) && Utilities.LayerIsInLayerMask(other.gameObject.layer, allowedLayers))
                Execute();
        }
        protected virtual void OnTriggerStay(Collider other)
        {
            if (executeWhen.Equals(ExecuteWhen.TRIGGERSTAY) && Utilities.LayerIsInLayerMask(other.gameObject.layer, allowedLayers))
                Execute();
        }
        protected virtual void OnTriggerExit(Collider other)
        {
            if (executeWhen.Equals(ExecuteWhen.TRIGGEREXIT) && Utilities.LayerIsInLayerMask(other.gameObject.layer, allowedLayers))
                Execute();
        }
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (executeWhen.Equals(ExecuteWhen.TRIGGERENTER2D) && Utilities.LayerIsInLayerMask(collision.gameObject.layer, allowedLayers))
                Execute();
        }
        protected virtual void OnTriggerStay2D(Collider2D collision)
        {
            if (executeWhen.Equals(ExecuteWhen.TRIGGERSTAY2D)  && Utilities.LayerIsInLayerMask(collision.gameObject.layer, allowedLayers))
                Execute();
        }
        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            if (executeWhen.Equals(ExecuteWhen.TRIGGEREXIT2D) && Utilities.LayerIsInLayerMask(collision.gameObject.layer, allowedLayers))
                Execute();
        }
        #endregion
        #endregion

        #region GENERIC METHODS

        public virtual void Execute()
        {
            //if (gameObject.activeSelf) { 
                actionToExecute?.Invoke();
                Debug.Log("EXECUTED ACTION");
            //}
        }
       
        #endregion

    }

}