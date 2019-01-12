/**
 * @author Fabrizio Coppolecchia
 *
 * Component used to Execute action on any Monobehaviour cicle. Usage:
 * 1 - Attach to a object, select when you want to execute an action using executeWhen dropdown
 * 2 - make sure to attach right collider and eventually attach rigidbody to detect collision if you use trigger/trigger2D/collision/collision2d
 * 3 - add any action to actionToExecute UnityEvent (by inspector)
 * 
 * 
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
        protected ExecuteWhen executeWhen; //WHEN THE UnityEvent is invoked
        [ShowIf("executeWhen", 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17)]
        [SerializeField]
        protected LayerMask allowedLayers; //ALLOWED LAYERS (Only for collisions and triggers)
        [ShowIf("executeWhen", 1)]
        public EventsID eventToListen; //Event 

        //Used do determine when subscribe and unsubscribe execute action on EventManager in case di executeWhen = Event 
        [SerializeField]
        [InfoBox("REMEMBER TO SET TRUE IF OBJECT IS PART OF POOL", UnityEditor.MessageType.Warning)]
        protected bool isPooledObject = false;

        #endregion
        
        #region EVENTS
        [Space]
        public UnityEngine.Events.UnityEvent actionToExecute;
        #endregion

        #region MONOBEHAVIOUR METHODS
        protected virtual void Awake(){
            //If is not pooled object and must be executed on event i need to StartListening in Awake (because non pooled object are never re-utilized for different action and can be destroyed)
            if (executeWhen.Equals(ExecuteWhen.EVENT) && !isPooledObject)
                EventManager.StartListening(eventToListen, Execute);
        }
        protected virtual void Start()
        {
            if (executeWhen.Equals(ExecuteWhen.START))
                Execute();
        }
        protected virtual void OnEnable()
        {
            //If is pooled object and must be executed on event i need to StartListening OnEnable (because pooled object are never destroyed but only disabled and enabled on use/re-use)
            if (executeWhen.Equals(ExecuteWhen.EVENT) && isPooledObject)
                EventManager.StartListening(eventToListen, Execute);
            //Execute action
            if (executeWhen.Equals(ExecuteWhen.ENABLE))
                Execute();
        }
        protected virtual void OnDisable()
        {
            if (executeWhen.Equals(ExecuteWhen.DISABLE))
                Execute();

            //If is pooled object and must be executed on event i need to StopListening OnDisable (because pooled object are never destroyed but only disabled for re-use)
            if (executeWhen.Equals(ExecuteWhen.EVENT) && isPooledObject)
                EventManager.StopListening(eventToListen, Execute);
        }
        protected virtual void OnDestroy()
        {
            if (executeWhen.Equals(ExecuteWhen.DESTROY))
                Execute();
            actionToExecute.RemoveAllListeners();
            //If is not pooled object and must be executed on event i need to StopListening OnDestroy 
            if (executeWhen.Equals(ExecuteWhen.EVENT) && !isPooledObject)
                EventManager.StopListening(eventToListen, Execute);

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

        /// <summary>
        /// Execute action (invoke UnityEvent)
        /// </summary>
        public virtual void Execute()
        {
            actionToExecute?.Invoke();
        }
       
        #endregion

    }

}