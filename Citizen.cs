using UnityEngine;
using System.Collections.Generic;

namespace Monastry
{
	[System.Serializable]
	public class Citizen
	{
		[Range (0.0f, 100.0f)]
		public float BaseAttitude;

		public List<MassVisit> MassesVisited;

		public string Name;
		public bool IsMale;

		public int LastVisit { get; set; }

		const float NegativeAttitudeThreshold = 30.0f;
		const float PositiveAttitudeThreshold = 50.0f;

		public Citizen (float baseAttitude)
		{
			BaseAttitude = baseAttitude;
			LastVisit = 0;
			MassesVisited = new List<MassVisit> ();
			NameAndGender ();
		}

		void NameAndGender ()
		{
			bool is_male;
			string name = NamesGenerator.Instance.GetName (out is_male);
			Name = name;
			IsMale = is_male;
		}

		public void GoToMass ()
		{
			MassVisit visit = new MassVisit (true, 0.6f);
			MassesVisited.Add (visit);

			BaseAttitude += visit.GetTotalScore ();
		}

		public Attitude GetAttitude ()
		{
			if (BaseAttitude < NegativeAttitudeThreshold) {
				return Attitude.negative;
			}
			if (BaseAttitude > PositiveAttitudeThreshold) {
				return Attitude.positive;
			}
			return Attitude.neutral;
		}

		public float GetBaseAttitude ()
		{
			return BaseAttitude;
		}

		public float GetLastVisitScore ()
		{
			if (MassesVisited.Count == 0) {
				return 0;
			}
			return MassesVisited [MassesVisited.Count - 1].GetTotalScore ();
		}
	}
}

