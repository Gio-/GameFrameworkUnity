using System;
using System.Collections;
using UnityEngine;


[System.Serializable]
public class LoopController
{
    public enum LoopType
    {
        Once,
        PingPong,
        Repeat
    }

    public LoopType loopType;

    public float duration = 1;
    public AnimationCurve accelCurve;

    public float m_timer   = 0f;

    public float UpdateLoop()
    {

        switch(loopType)
        {
            case LoopType.Once:
                m_timer = Mathf.Clamp(m_timer + Time.deltaTime, 0, duration);
                break;
            case LoopType.PingPong:
                m_timer = Mathf.PingPong(Time.time, duration);
                break;
            case LoopType.Repeat:
                m_timer = Mathf.Repeat(m_timer + Time.deltaTime, duration);
                break;
        }

        return m_timer;
    }

}