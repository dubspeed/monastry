using System;

namespace Monastry
{
	/**
	 * generate a random name and gender for a character
	 */
	public class NamesGenerator
	{
		static NamesGenerator _Instance;

		public static NamesGenerator Instance {
			get {
				if (_Instance == null) {
					_Instance = new NamesGenerator ();
				}
				return _Instance;
			}
		}

		string[] NamesDatabase;

		// index in array of the first female Name
		const int FIRST_FEMALE_NAME = 558;

		public NamesGenerator ()
		{
			string names = System.IO.File.ReadAllText ("Assets/shared/medival_german_names.txt");
			NamesDatabase = names.Split ('\n');
		}

		public string GetName (out bool is_male)
		{
			int random_index;
			is_male = Convert.ToBoolean (UnityEngine.Random.Range (0, 2));

			if (is_male) {
				random_index = UnityEngine.Random.Range (0, FIRST_FEMALE_NAME);
			} else {
				random_index = UnityEngine.Random.Range (FIRST_FEMALE_NAME, NamesDatabase.Length);
			}

			return NamesDatabase [random_index];
		}
	}
}

