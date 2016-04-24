using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Monastry
{
	public enum Attitude
	{
		negative,
		neutral,
		positive

	}

	/**
	 * Main WorldController Monobehavior 
	 */
	public class WorldController : MonoBehaviour
	{
		[Range (0, 10000)]
		public int PopulationCount;

		[Range (0.0f, 1.0f)]
		public float PopulationGrowthRatePerYear;

		public float InitialCitizenBaseAttitude;

		public List<Citizen> Citizens;

		public Queue<Citizen> CitizenMassQueue;

		Rect UIWindowRect = new Rect (250, 10, 200, 200);

		int CurrentMassVisitors = 0;
		float RealPopulation;


		void OnGUI ()
		{	
			UIWindowRect = GUILayout.Window (1, UIWindowRect, UIRenderWindow, "World Control");
		}

		void UIRenderWindow (int windowID)
		{			
			GUILayout.Label ("Pop: " + PopulationCount);
			GUILayout.HorizontalSlider (RealPopulation, PopulationCount, PopulationCount + 1);
			GUILayout.Label ("Todays # of Visitors: " + CurrentMassVisitors);
			GUI.DragWindow (new Rect (0, 0, 200, 20));
		}

		// Use this for initialization
		void Start ()
		{
			PopulationCount = PopulationCount > 0 ? PopulationCount : 100;
			PopulationGrowthRatePerYear = PopulationGrowthRatePerYear > 0 ? PopulationGrowthRatePerYear : 0.05f;
			InitialCitizenBaseAttitude = InitialCitizenBaseAttitude > 0 ? InitialCitizenBaseAttitude : 35.0f;
			RealPopulation = PopulationCount;

			for (int i = 0; i < PopulationCount; i++) {
				Citizen citizen = new Citizen (InitialCitizenBaseAttitude);
				Citizens.Add (citizen);
			}
		}

		// Citizen go to mass following some rules:
		// @see pages 21, 23, 33
		public int OLDSendCitizensToMassOld (int iterationCount)
		{
			float one_percent = PopulationCount / 100.0f;

			// Every step 1% +- 0.3% of the population goes to mass
			one_percent += Random.Range (-1 * one_percent * 0.3f, one_percent * 0.3f); 

			int people_to_send = Mathf.RoundToInt (one_percent);

			CurrentMassVisitors = people_to_send;
			return people_to_send;
		}

		/**
		 * sending a citizien to mass means:
		 * add to a queue
		 * the queue gets processed over time
		 * the result is added to the citzien
		 * the citzizen becomes available to be queued another time
		 */
		public int SendCitizensToMass (int iterationCount)
		{
			foreach (Citizen citizen in Citizens) {
				Attitude attitude = citizen.GetAttitude ();

				if (attitude == Attitude.negative) {
					continue;
				}

				float citizen_score = citizen.GetBaseAttitude ();
				int last_visit_days_ago = iterationCount - citizen.LastVisit;
				float last_visit_score = citizen.GetLastVisitScore ();

				// Range(-100 / 2000)
				float go_to_mass_score = (last_visit_days_ago * citizen_score) + (last_visit_score * 100.0f);
				// Range 0...1
				float chance = (go_to_mass_score + 100.0f) / 2100.0f;
				print (citizen + " / " + chance);
			}
			return 0;
		}


		// Called on every Iteration
		// @see page 27
		// TODO belive factor is not respected
		// TODO negative growth
		public int GrowPopulation (int iterationCount)
		{
			float growth_per_day = PopulationGrowthRatePerYear / 365;

			RealPopulation += RealPopulation * growth_per_day;

			int new_population = Mathf.FloorToInt (RealPopulation);

			if (new_population > PopulationCount) {
				// Somebody got born
				Citizen citizen = new Citizen (InitialCitizenBaseAttitude);
				Citizens.Add (citizen);
			} else if (new_population < PopulationCount) {
				// Somebody should die
				// TODO
				print ("A citizen must die, not implemented");
			}

			PopulationCount = new_population;

			return PopulationCount;
		}
	}
}
			
