using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjects : MonoBehaviour
{
    [Header("Object To Follow")]
    public Transform Target;

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
        XYZ = ~0
    }

    [Header("Sync Position")]
    [SerializeField]
    private SyncDirection m_position;

    [SerializeField]
    private Vector3       m_positionOffset;
    
    [Header("Sync Rotation")]
    [SerializeField]
    private SyncDirection m_rotation;

    [SerializeField]
    private Vector3       m_rotationOffset;

    [Header("Speed and Delay Settings")]
    [Tooltip("The lower the value, the faster it will follow the target.")]
    [SerializeField]
    private float         m_smooth           = 0.5f;

    private Vector3       m_velocity;
    private bool          m_stopFollow;
    private Vector3       m_resultPosition;
    private Vector3       m_resultRotation;
    
    // Update is called once per frame
    void Update()
    {
        if(Target == null || m_stopFollow)
            return;

        GetSyncedDirection(ref m_position, Target.position, out m_resultPosition);
        GetSyncedDirection(ref m_rotation, Target.rotation.eulerAngles, out m_resultRotation);

        m_resultPosition = m_resultPosition + m_positionOffset;
        m_resultRotation = m_resultRotation + m_rotationOffset;
         
        if(m_position != SyncDirection.None)
            transform.position = Vector3.SmoothDamp(transform.position, m_resultPosition, ref m_velocity, m_smooth);     

        if(m_rotation != SyncDirection.None)
            transform.rotation = Quaternion.Euler(Vector3.SmoothDamp(transform.rotation.eulerAngles, m_resultRotation, ref m_velocity, m_smooth));    
    }

    void GetSyncedDirection(ref SyncDirection direction, Vector3 targetPosition, out Vector3 result)
    {
        /* switch(direction)
        {
            case SyncDirection.X:
                result = new Vector3(targetPosition.x, transform.position.y, transform.position.z);
                break;
            case SyncDirection.Y:
                result = new Vector3(transform.position.x, targetPosition.y, transform.position.z);
                break;
            case SyncDirection.Z:
                result = new Vector3(transform.position.x, transform.position.y, targetPosition.z);
                break;
            case SyncDirection.XY:
                result = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
                break;
            case SyncDirection.XZ:
                result = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
                break;
            case SyncDirection.YZ:
                result = new Vector3(transform.position.x, targetPosition.y, targetPosition.z);
                break;
            case SyncDirection.XYZ:
                result = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);
                break;
            default:
                result = transform.position;
                break;
        }*/

        if(direction == SyncDirection.None)
        {
            result = transform.position;
        }
        else
        {
            result = new Vector3(direction.HasFlag(SyncDirection.X) ? targetPosition.x : transform.position.x,
                                 direction.HasFlag(SyncDirection.Y) ? targetPosition.y : transform.position.y, 
                                 direction.HasFlag(SyncDirection.Z) ? targetPosition.z : transform.position.z);
        }
    }
}
