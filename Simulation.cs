using UnityEngine;
using System.Collections;

namespace Monastry 
{
	public class Simulation : MonoBehaviour 
	{
		public float SimulationTimeStepInSec;

		public bool SimulationRunning = false;

		private WorldController WorldController;
		private int IterationCount;

		void Start () 
		{
			WorldController = this.GetComponent<WorldController> ();
			IterationCount = 0;
			SimulationRunning = true;
			SimulationTimeStepInSec = SimulationTimeStepInSec > 0.0f ? SimulationTimeStepInSec : 1.0f;
			Invoke ("SimulationStep", SimulationTimeStepInSec);	
		}

		void SimulationStep() 
		{
			if (!SimulationRunning) {
				return;
			}
			IterationCount += 1;

			WorldController.SendCitizensToMass (IterationCount);
			Invoke ("SimulationStep", SimulationTimeStepInSec);	
		}
			
		void Update () 
		{
		
		}
	}
}