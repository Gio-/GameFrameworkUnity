
namespace GameFramework
{
	[System.Serializable]
	public class FloatReference {

		[UnityEngine.SerializeField]
		private bool m_useConstant = true;
		
		[UnityEngine.SerializeField]
		private float m_constantValue = 0;

		[UnityEngine.SerializeField]
    	#pragma warning disable CS0649
		private FloatVariable m_floatVariable;

		public float Value 
		{
			get { return (m_useConstant) ? m_constantValue : m_floatVariable.value; }
			set 
			{
				if(m_useConstant)
				{
					m_constantValue = value;
				}
				else 
				{
					m_floatVariable.value = value;
				}
			}
		}
	}
}