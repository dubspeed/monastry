using UnityEngine;

namespace Monastry
{
	[System.Serializable]
	public class MassVisit
	{
		// Was the visitor blessed, or did he have to wait too long and went away
		public bool GotBlessed;

		// how much was the visitor impressed by the temple?
		[Range (0.0f, 1.0f)]
		public float Impression;

		// what did he pay
		[Range (0, 10)] 
		public int PayedAmount;

		public MassVisit (bool gotBlessed, float impression)
		{
			GotBlessed = gotBlessed;
			Impression = impression;
		}

		// total score of the visit
		// ranges from -1.0f to 1.4f;
		public float GetTotalScore ()
		{
			float blessFactor = GotBlessed ? 0.4f : 0.2f;
			return (Impression + blessFactor - 0.7f) * 2.0f;
		}
	}
}

