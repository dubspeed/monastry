using UnityEngine;
using System.Collections;

using UnityEngine.Assertions;

namespace Monastry
{
	public class MassVisit
	{
		// Was the visitor blessed, or did he have to wait too long and went away
		public bool GotBlessed { get; set; }

		// was he impressed by the temple?
		public float WasImpressed { get; set; }

		// what did he pay
		public float PayedAmount { get; set; }

		public float TotalScore()
		{
			return 0.0f;
		}
	}

	public class Citizen
	{
		public int BaseAttitude { get; set; }

		public MassVisit[] MassesVisited { get; set; }

	}

	public class WorldController : MonoBehaviour
	{

		public int NumberOfCitizen;

		// Use this for initialization
		void Start ()
		{
			Assert.IsTrue (NumberOfCitizen > 0, "WorldController: NumberOFCitizen is 0");

			// create x citizen

		}

		// Update is called once per frame
		void Update ()
		{

		}
	}
}
			
