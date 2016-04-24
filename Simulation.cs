using UnityEngine;
using System.Collections.Generic;

namespace Monastry
{
	public class Simulation : MonoBehaviour
	{
		public float SimulationTimeStepInSec;

		public bool SimulationRunning = false;

		WorldController WorldController;
		int IterationCount;

		Rect UIWindowRect = new Rect (10, 10, 200, 150);

		Queue<Citizen> CitizenMassQueue = new Queue<Citizen> ();

		void OnGUI ()
		{	
			UIWindowRect = GUILayout.Window (0, UIWindowRect, UIRenderWindow, "Simulation Control");
		}

		void UIRenderWindow (int windowID)
		{			
			if (GUILayout.Button ("Sim Running: " + SimulationRunning)) {
				SimulationRunning = !SimulationRunning;
				if (SimulationRunning) {
					Invoke ("SimulationStep", SimulationTimeStepInSec);	
				}
			}
			GUILayout.Label ("Step seconds: " + SimulationTimeStepInSec);
			GUILayout.Label ("Simulation Day: " + IterationCount);
			if (GUILayout.Button ("Reset")) {
				IterationCount = 0;
			}
			GUI.DragWindow (new Rect (0, 0, 200, 20));
		}

		void Start ()
		{
			WorldController = this.GetComponent<WorldController> ();
			WorldController.CitizenMassQueue = CitizenMassQueue;
			IterationCount = 0;
			SimulationRunning = true;
			SimulationTimeStepInSec = SimulationTimeStepInSec > 0.0f ? SimulationTimeStepInSec : 1.0f;
			Invoke ("SimulationStep", SimulationTimeStepInSec);	
		}

		void SimulationStep ()
		{
			if (!SimulationRunning) {
				return;
			}
			IterationCount += 1;

			WorldController.SendCitizensToMass (IterationCount);
			WorldController.GrowPopulation (IterationCount);
			Invoke ("SimulationStep", SimulationTimeStepInSec);	
		}

		void Update ()
		{
			// check the queue and create GameObjects from queued citizen
		}
	}
}