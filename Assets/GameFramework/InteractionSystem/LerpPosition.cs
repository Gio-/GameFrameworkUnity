using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpPosition : Interactable
{
    public LoopController LoopController;

    public bool Active;

    public Vector3 start = -Vector3.forward;
    public Vector3 end   = Vector3.forward;

    float m_evaluation;
    #pragma warning disable  CS0414
    float m_timer = 0.0f;

    public override void OnInteract()
    {
        Active = true;
    }

    public void FixedUpdate()
    {
        if(!Active) return;

        TranslateToPoint(LoopController.UpdateLoop());
    }

    void TranslateToPoint(float time)
    {
        m_evaluation = LoopController.accelCurve.Evaluate(time / LoopController.duration);
        transform.position = Vector3.Lerp(start, end, m_evaluation); 
    }
 
}
