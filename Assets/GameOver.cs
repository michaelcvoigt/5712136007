using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;


namespace VacuumShaders
{

public class GameOver : MonoBehaviour {

		public TextMesh GameScore;
        public TextMesh HighScore;


		private bool postingScore = false;

	// Use this for initialization
	void Start () {
	
			postingScore = true;

			int currentHighScore = PlayerPrefs.GetInt ("HighScore");
			GetScore ();


	}
	
		// Update is called once per frame
		void Update ()
		{
			if (!postingScore) {

				if (Input.GetKey (KeyCode.Return)) {

					SceneManager.LoadScene (0);
				}
			}

		}


		public void PostScore( int score) {


				Action<string> success = (string data) => {
					
					
				};

				Action<string, string> fail = (string data, string error) => {
					

					if (!string.IsNullOrEmpty(error)) {
						
					}

					

					string msg = data + (string.IsNullOrEmpty(error) ? "" : " : "  + error);
					

					
				};


			StartCoroutine (ApiManager.Instance.PostScore (score, success, fail));
				
		}

		public void GetScore( ) {


			Action<string> success = (string data) => {

				int serverHighScore = Int32.Parse(data);

				int Score = PlayerPrefs.GetInt ("CurrentScore");

				print ( Score + " : " + serverHighScore);


				if (Score > serverHighScore)
				{

					print (  "high score " );

					PlayerPrefs.SetInt ("HighScore", Score);
					PostScore (Score);
					HighScore.text = "Server High Score =" + Score;
				}
				else{

					HighScore.text = "Server High Score =" + serverHighScore;

				}


				GameScore.text = "Your Score =" + Score.ToString();

				postingScore = false;

			};

			Action<string, string> fail = (string data, string error) => {


				//if (!string.IsNullOrEmpty(error)) {

				//}

				string msg = data + (string.IsNullOrEmpty(error) ? "" : " : "  + error);

				postingScore = false;
			};


			StartCoroutine (  ApiManager.Instance.Get( success, fail)  );


		}


}

}
