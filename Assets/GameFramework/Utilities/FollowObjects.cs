using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjects : MonoBehaviour
{
    [System.Flags]
    public enum SyncDirection
    {
        None = 0,
        X = 1<<0,
        Y = 1<<1,
        Z = 1<<2,
        XY = X | Y,
        XZ = X | Z,
        YZ = Y | Z,
        XYZ = X | Y | Z
    }

    [Header("Object To Follow")]
    public Transform Target                          = null;

    [Header("Sync Position")]
    [SerializeField]
    private SyncDirection m_position                 = SyncDirection.None;

    [SerializeField]
    private Vector3       m_positionOffset           = new Vector3(0, 0, 0);
    
    [Header("Sync Rotation")]
    [SerializeField]
    private SyncDirection m_rotation                 = SyncDirection.None;

    [SerializeField]
    private Vector3       m_rotationOffset           = new Vector3(0, 0, 0);
    
    [Header("Speed and Delay Settings")]
    [Tooltip("The lower the value, the faster it will follow the target.")]
    [SerializeField]
    private float         m_smoothPosition           = 0.5f;
    [Tooltip("The lower the value, the slower it will follow the target.")]
    [SerializeField]
    private float         m_smoothRotation           = 2f;

    private Vector3       m_velocity                 = new Vector3(0, 0, 0);
    private bool          m_stopFollow               = false;
    private Vector3       m_resultPosition           = new Vector3(0, 0, 0);
    private Vector3       m_resultRotation           = new Vector3(0, 0, 0);

    public bool           StopFollow 
    { 
        get { return m_stopFollow; } 
        set { m_stopFollow = value; } 
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Target == null || m_stopFollow)
            return;

        GetSyncedDirection(ref m_position, transform.position, Target.position, out m_resultPosition);
        GetSyncedDirection(ref m_rotation, transform.rotation.eulerAngles, Target.rotation.eulerAngles, out m_resultRotation);

        m_resultPosition = m_resultPosition + m_positionOffset;
        m_resultRotation = m_resultRotation + m_rotationOffset;
        
        if(m_position != SyncDirection.None)
            transform.position = Vector3.SmoothDamp(transform.position, m_resultPosition, ref m_velocity, m_smoothPosition);     
            
        if(m_rotation != SyncDirection.None)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(m_resultRotation),  m_smoothRotation * Time.deltaTime);
   }

    void GetSyncedDirection(ref SyncDirection direction, Vector3 myPosition, Vector3 targetPosition, out Vector3 result)
    {
        if(direction == SyncDirection.None)
        {   
            result = myPosition;
            return;
        }
    
        if(direction == SyncDirection.XYZ)
        {
            result = targetPosition;
            return;
        }
        
        result = new Vector3(direction.HasFlag(SyncDirection.X) ? targetPosition.x : myPosition.x,
                             direction.HasFlag(SyncDirection.Y) ? targetPosition.y : myPosition.y, 
                             direction.HasFlag(SyncDirection.Z) ? targetPosition.z : myPosition.z);
    }
    
}
