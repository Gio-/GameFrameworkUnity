using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField]
    protected InteractionType   m_interactionType;

    [SerializeField]
    protected Interactable      m_interactable;

    protected void Send()
    {
        if(m_interactable != null)
            m_interactable.Interact(m_interactionType);
            
        OnSend();
    }

    public virtual void OnSend()
    {
        //
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Send();
        }
    }
}
