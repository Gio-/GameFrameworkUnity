using UnityEngine;

namespace GameFramework
{
	[CreateAssetMenu]
	public class FloatVariable : ScriptableObject 
	{
		[SerializeField]
		private float m_baseValue;

		[ReadOnlyAttribute]
		[SerializeField]
		private float m_value;
		
		public float value 
		{ 
			get { return m_value;  } 
			set { m_value = value; }
		}
		
		public void OnEnable()
		{
			value = m_baseValue;	
		}
	}
}