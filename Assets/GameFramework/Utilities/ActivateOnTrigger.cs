using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameFramework;

[RequireComponent(typeof(Collider))]
public class ActivateOnTrigger : MonoBehaviour
{
    [Header("Valid Layers")]
    [SerializeField]
    private LayerMask m_activateForLayers;
    private Collider  m_collider;

    [Space(10)]
    public UnityEvent OnEnterTrigger;

    [Space(10)]
    public UnityEvent OnExitTrigger;
    
    void Awake()
    {
        m_collider = GetComponent<Collider>();

        if(m_collider == null)
            this.enabled = false;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if(Utilities.LayerIsInLayerMask(m_activateForLayers, other.gameObject))
        {
            if(OnEnterTrigger != null)
            {
                OnEnterTrigger.Invoke();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(Utilities.LayerIsInLayerMask(m_activateForLayers, other.gameObject))
        {
            if(OnExitTrigger != null)
            {
                OnExitTrigger.Invoke();
            }
        }
    }

 
}
