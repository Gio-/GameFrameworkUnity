using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionType
{
    Open,
    Close,
    Activate,
    Destroy
}

[SelectionBase]
public abstract class Interactable : MonoBehaviour
{
    [SerializeField]
    protected InteractionType m_interactionType = InteractionType.Activate;
    [SerializeField]
    protected float m_coolDown                  = 0.0f;
    [SerializeField]
    protected bool  m_isOneShot                  = false;
    [SerializeField]
    protected bool  m_isActivated                = false;
    [SerializeField]
    protected bool  m_canUse                     = true;

    private float   m_lastUse                    = 0.0f;

    public void Interact(InteractionType interactionReceived)
    {
        if(!CanInteract() || interactionReceived != m_interactionType)  
            return;

        m_lastUse       = Time.time;
        m_isActivated   = true;
        
        OnInteract();
    }

    public abstract void OnInteract();

    public bool CanInteract()
    {
        if(!m_canUse)                    return false;
        if(m_isOneShot && m_isActivated) return false;
        if(!IsCoolDownEnded())           return false;

        return true;
    }

    public bool IsCoolDownEnded()
    {
        return (Time.time - m_lastUse >= m_coolDown);
    }
}
