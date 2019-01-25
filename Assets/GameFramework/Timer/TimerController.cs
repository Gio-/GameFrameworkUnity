using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerController : MonoBehaviour
{
    enum TimerPhase
    {
        READY,
        PLAY,
        STOP,
        PAUSE
    }

    [SerializeField]
    private float m_maxTime         = 1.0f;
    [SerializeField]
    private float m_startTime       = 0.0f;
    [SerializeField]
    private float m_speedMultiplier = 1.0f;
    [SerializeField]
    private bool  m_ignoreTimeScale = false; 
    [SerializeField]
    private bool  m_restartOnFinish = false;
    [SerializeField]
    private bool  m_disableOnFinish = false;

    private float       m_timer     = 0.0f;
    private TimerPhase  m_phase     = TimerPhase.READY;

    public UnityEvent OnTimerStart;
    public UnityEvent OnTimerEnd;

    public delegate void OnTimerHandler();
    public event OnTimerHandler OnTimerStartEvent;
    public event OnTimerHandler OnTimerEndEvent;

    // Update is called once per frame
    void Update()
    {
        if(m_phase != TimerPhase.PLAY)
            return;

        UpdateTimer();
    }

    public void StartTimer()
    {
        m_timer         = m_startTime;
        m_phase         = TimerPhase.PLAY;
    
        ThrowStartEvent();
    }

    public void ToggleTimer()
    {
        m_phase = (m_phase == TimerPhase.PLAY) ? TimerPhase.PAUSE : TimerPhase.PLAY;
    }

    public void TestTimeScale0()
    {
        Time.timeScale = (Time.timeScale == 0) ? 1 : 0;
    }

    private void UpdateTimer()
    {
        if(m_timer < m_maxTime)
        {
            m_timer += ((m_ignoreTimeScale) ? Time.unscaledDeltaTime : Time.deltaTime) * m_speedMultiplier;
        }
        else
        {
            if(!m_restartOnFinish)
            {
                m_phase = TimerPhase.STOP;
            }
            else
            {
                m_timer = m_startTime;
            }

            ThrowEndEvent();
        }
    }
    private void ThrowStartEvent()
    {
        if(OnTimerStart != null)
            OnTimerStart.Invoke();

        if(OnTimerStartEvent != null)
            OnTimerStartEvent();
    }

    private void ThrowEndEvent()
    {
        if(OnTimerEnd != null)
            OnTimerEnd.Invoke();

        if(OnTimerEndEvent != null)
            OnTimerEndEvent();
    }
}
