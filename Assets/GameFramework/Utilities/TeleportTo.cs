using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TeleportTo : MonoBehaviour
{
    [System.Serializable]
    public class TeleportToEvents
    {     
        [Header("On Teleport")]
        public UnityEvent OnTeleport;
        
        [Header("On Teleport Denied")]
        public UnityEvent OnTeleprotDenied;
        
        [Header("On Teleport Lock down")]
        public UnityEvent OnTeleportLock;  
        
        [Header("On Teleport Unlock")]
        public UnityEvent OnTeleportUnlock;   
    }

    [Header("Target setting")]
    [SerializeField]
    private Transform m_target          = null;

    [Header("Teleport setting")]
    [SerializeField]
    private bool      m_isLocked        = false;

    [SerializeField]
    private bool      m_oneShot         = false;

    [Space(10), Header("Teleport Events")]
    public TeleportToEvents Events;

    public Transform  Target    { get { return m_target;   } set { m_target = value; } }
    public bool       IsLocked  { get { return m_isLocked; } }
    public bool       IsOneShot { get { return m_oneShot;  } }

    public void Teleport(Transform goTo)
    {
        Teleport(goTo.position);
    }

    public void Teleport(Vector3 position)
    {
        if(m_isLocked) 
        {
            if(Events.OnTeleprotDenied != null) Events.OnTeleprotDenied.Invoke();
            return;
        }

        if(m_target == null)
        {
            Debug.LogWarning("[TeleportTo]("+ this.gameObject.name +"): No Target Selected.");            
            if(Events.OnTeleprotDenied != null) Events.OnTeleprotDenied.Invoke();
            return;
        }

        m_target.position = position;

        if(m_oneShot && !m_isLocked)
            Lock();

        if(Events.OnTeleport != null) Events.OnTeleport.Invoke();
    }

    public void Lock()
    {
        m_isLocked = true;
        if(Events.OnTeleportLock != null) Events.OnTeleportLock.Invoke();
    }
    
    public void Unlock()
    {
        m_isLocked = false;
        if(Events.OnTeleportUnlock != null) Events.OnTeleportUnlock.Invoke();
    }
}
