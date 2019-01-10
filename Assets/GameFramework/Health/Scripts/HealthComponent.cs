using UnityEngine;
using UnityEngine.Events;
using GameFramework;

public class HealthComponent : MonoBehaviour, IDamageable<float>
{
    [Header("Health Data")]
    [SerializeField, ReadOnly]
    private float   m_health              = 0.0f;

    [Space(10), SerializeField]
    private float   m_startingHealth      = 100;

    [SerializeField]
    private Vector2 m_healthRange         = new Vector2(0, 100);

    [Header("Invulnerability Settings")]
    [SerializeField]
    private bool    m_enableGodMode             = false;
    [SerializeField]
    private bool    m_isInvulnerable            = false;

    [Space(10),SerializeField]
    private bool    m_isInvulnerableAfterEnabled= false;
    [SerializeField]
    private bool    m_invulnerableAfterDamage   = false;

    [SerializeField]
    private float   m_invulnerableDuration      = 5.0f;
    private float   m_currentInvulnerableTimer  = 0.0f;

    #region Events
    [Header("On Death Event")]
    public UnityEvent DeathEvent;
    [Header("On Gain Health Event")]
    public UnityEvent GainHealthEvent;
    [Header("On Take Damage Event")]
    public UnityEvent TakeDamageEvent;
    
    public delegate void DamagerDefinitionHandler(HealthComponent h, DamageInfo info);
    public delegate void HealthComponentHandler(HealthComponent h);
    public static event  DamagerDefinitionHandler OnDeathEvent;
    public event         DamagerDefinitionHandler OnTakeDamageEvent;
    public event         HealthComponentHandler   OnGainHealthEvent;
    #endregion

    /// <summary>
    /// Current Health.
    /// </summary>
    /// <value>Return a float with current health.</value>
    public float   Health                     { get { return m_health;                  } }
    /// <summary>
    /// Health Min and Max.
    /// </summary>
    /// <value>Return a Vector2 where x is Min and y is Max.</value>
    public Vector2 HealthRange                { get { return m_healthRange;             } }
    /// <summary>
    /// Starting Health Value.
    /// </summary>
    /// <value>Return the starting health value.</value>
    public float   StartingHealth             { get { return m_startingHealth;          } }
    /// <summary>
    /// GodMode make entity invulnerable, is persistent and
    /// is not affected by time.
    /// </summary>
    /// <value>Return true if is in god mode.</value>
    public bool    IsGodModeEnabled           { get { return m_enableGodMode;           } }
    /// <summary>
    /// Invulnerability make entity invulnerable, is not 
    /// persistent and has a cooldown timer.
    /// </summary>
    /// <value>Return true if is invulenrable or false if not.</value>
    public bool    IsInvulnerable             { get { return m_isInvulnerable;          } }
    /// <summary>
    /// Boolean to make entity invulnerable after hit.
    /// </summary>
    /// <value>Return a boolean.</value>
    public bool    IsInvulnerableAfterDamage  { get { return m_invulnerableAfterDamage; } }
    /// <summary>
    /// The duration of invulnerability.
    /// </summary>
    /// <value>return a float with timer duration.</value>
    public float   InvulnerableDuration       { get { return m_invulnerableDuration;    } }
    /// <summary>
    /// Current invulnerabile timer.
    /// </summary>
    /// <value></value>
    public float   InvulnerableTimer          { get { return m_currentInvulnerableTimer;} }

    /// <summary>
    /// Taking damage of value (value) from a entity (damager) 
    /// </summary>
    /// <param name="value">Value of damage</param>
    /// <param name="damager">Gameobject that make damage</param>
    public void Hit(DamageInfo info)
    {
        if(m_isInvulnerable || m_enableGodMode)
            return;

        Set(m_health - info.Damage);

        if(m_health <= m_healthRange.x)
        {
            ThrowOnDeathEvent(info);
        }
        else
        {
            if(m_invulnerableAfterDamage)
                EnableInvulnerability();

            ThrowTakeDamageEvent(info);
        }
    }

    /// <summary>
    /// Add a value of health.
    /// </summary>
    /// <param name="value">Value to add.</param>
    public void Gain(float value)
    {
        Set(m_health + value);
        
        ThrowGainHealthEvent();
    }

    /// <summary>
    /// Set a health value. Value is clamped in range 
    /// specified by HealthRange.
    /// </summary>
    /// <param name="value">Value of health.</param>
    public void Set(float value)
    {
        m_health = Mathf.Clamp(value, m_healthRange.x, m_healthRange.y);
    }

    /// <summary>
    /// Enable Invulnerability for a short time.
    /// </summary>
    public void EnableInvulnerability()
    {
        m_isInvulnerable = true;
        m_currentInvulnerableTimer = m_invulnerableDuration;
    }

    #region Privates
    private void ThrowTakeDamageEvent(DamageInfo info)
    {          
        /// Standard Event  
        if(OnTakeDamageEvent != null)
            OnTakeDamageEvent(this, info);

        /// Unity Event
        if(TakeDamageEvent != null)
            TakeDamageEvent.Invoke();
    }

    private void ThrowGainHealthEvent()
    {
        /// Standard Event
        if(OnGainHealthEvent != null)
            OnGainHealthEvent(this);

        /// Unity Event
        if(GainHealthEvent != null)
            GainHealthEvent.Invoke();
    }

    private void ThrowOnDeathEvent(DamageInfo info)
    {
        /// Standard Event
        if(OnDeathEvent != null)
            OnDeathEvent(this, info);

        /// Unity Event
        if(DeathEvent != null)
            DeathEvent.Invoke();
    }
    #endregion

    #region Unity Methods
    public void OnValidate()
    {
        /// Correct starting health if it is > of max range.
        if(m_startingHealth > m_healthRange.y)
        {
            m_startingHealth = m_healthRange.y;
            Debug.LogWarning("[HealthComponent](" + this.gameObject.name + "): You are trying to set a starting health > than its maximum. Increase the range!");
        }

        /// Correct starting health if it is < of min range.
        if(m_startingHealth < m_healthRange.x)
        {
            m_startingHealth = m_healthRange.x;
            Debug.LogWarning("[HealthComponent](" + this.gameObject.name + "): You are trying to set a starting health < than its minimum. Increase the range!");  
        }
    }

    public void Awake()
    {
        /// Set starting health
        Set(m_startingHealth);
    }

    public void OnEnable()
    {
        /// After spawning, is invulnerable for a short time
        if(m_isInvulnerableAfterEnabled)
            EnableInvulnerability();
    }

    public void Update()
    {
        /// if it is invulnerable and the current timer is > 0,
        /// decrease and take it to 0. Once to 0, reset invulnerability.   
        if(m_isInvulnerable)
        {
            if(m_currentInvulnerableTimer > 0)
            {
                m_currentInvulnerableTimer -= Time.deltaTime;
            }
            else
            {
                m_currentInvulnerableTimer = 0;
                m_isInvulnerable         = false;
            }
        }
    }
    #endregion
}
