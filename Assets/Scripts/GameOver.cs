using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Collections.Generic;


namespace VacuumShaders
{

public class GameOver : MonoBehaviour {

		public GameScoreFont GameScore;
        public TextMesh HighScore;

		private Texture ShareImage;

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

				if (Input.anyKey) {

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


				if (Score > serverHighScore)
				{

					PlayerPrefs.SetInt ("HighScore", Score);
					PostScore (Score);
					HighScore.text = "You Got the World's HighScore!!! " + Score;
				}
				else{

					HighScore.text = "Server HighScore :" + serverHighScore + " \n Your HighScore : " + Score;

				}


				GameScore.SetScore(Score.ToString());
				// = "Your Score :" + 
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
