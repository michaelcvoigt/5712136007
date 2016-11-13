using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


namespace VacuumShaders
{

public class GameOver : MonoBehaviour {

		public TextMesh GameScore;
        public TextMesh HighScore;

	// Use this for initialization
	void Start () {
	
		int Score = PlayerPrefs.GetInt ("CurrentScore");

			int currentHighScore = PlayerPrefs.GetInt ("HighScore");

            if (Score > currentHighScore)
            {
                PlayerPrefs.SetInt ("HighScore", Score);
            }

			GameScore.text = "Your Score =" + Score.ToString();
            HighScore.text = "High Score =" + PlayerPrefs.GetInt ("HighScore");
	}
	
		// Update is called once per frame
		void Update ()
		{


			if (Input.GetKey (KeyCode.Return)) {

				SceneManager.LoadScene (0);
			}

		}
}

}
