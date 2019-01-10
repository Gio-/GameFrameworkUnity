using UnityEngine;

namespace GameFramework
{
    public struct DamageInfo
    {
        private float      m_damage;
        private GameObject m_damager;
        private Vector3    m_direction;

        public float      Damage
        {
            get { return m_damage; }
            set { m_damage = value;}
        }

        public GameObject Damager
        {
            get { return m_damager; }
            set { m_damager = value;}
        }

        public Vector3    Direction
        {
            get { return m_direction; }
            set { m_direction = value;}
        }    

        public DamageInfo(float damage, GameObject damager, Vector3 direction)
        {
            m_damage    = damage;
            m_damager   = damager;
            m_direction = direction;
        }
    }
}
