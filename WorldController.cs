using UnityEngine;
using System.Collections.Generic;

using UnityEngine.Assertions;

namespace Monastry
{
	[System.Serializable]
	public class MassVisit
	{
		// Was the visitor blessed, or did he have to wait too long and went away
		public bool GotBlessed;

		// how much was the visitor impressed by the temple?
		[Range(0.0f, 1.0f)]
		public float Impression;

		// what did he pay
		[Range(0, 10)] 
		public int PayedAmount;

		public MassVisit(bool gotBlessed, float impression)
		{
			GotBlessed = gotBlessed;
			Impression = impression;
		}

		// total score of the visit
		// ranges from -1.0f to 1.4f;
		public float GetTotalScore()
		{
			float blessFactor = GotBlessed ? 0.4f : 0.2f;
			return (Impression + blessFactor - 0.7f) * 2.0f;
		}
	}

	[System.Serializable]
	public class Citizen
	{
		[Range(0.0f, 100.0f)]
		public float BaseAttitude;
		public List<MassVisit> MassesVisited;

		public Citizen(float baseAttitude)
		{
			BaseAttitude = baseAttitude;
			MassesVisited = new List<MassVisit> ();
		}

		public void GoToMass()
		{
			MassVisit visit = new MassVisit (true, 0.6f);
			MassesVisited.Add (visit);

			BaseAttitude += visit.GetTotalScore ();
		}
	}

	public class WorldController : MonoBehaviour
	{
		public int PopulationCount;
		public List<Citizen> Citizens;

		// Use this for initialization
		void Start ()
		{
			PopulationCount = PopulationCount > 0 ? PopulationCount : 100;

			for (int i = 0; i < PopulationCount; i++) {
				Citizen citizen = new Citizen (20.0f);
				Citizens.Add (citizen);
				citizen.GoToMass ();
			}
		}

		// Randomly send some citizen to mass
		public int SendCitizensToMass(int iterationCount) {
			float one_percent = PopulationCount / 100.0f;

			// Every step 1% +- 0.3% of the population goes to mass
			one_percent += Random.Range(-1 * one_percent * 0.3f, one_percent * 0.3f); 

			int people_send = Mathf.RoundToInt (one_percent);
			print ("day " + iterationCount + " -> People send to mass: " + people_send);

			return people_send;
		}
	}
}
			
