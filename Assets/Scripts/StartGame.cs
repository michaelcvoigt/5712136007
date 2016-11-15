using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

// make controller work

// make cars not spawn on top of each other
// faster spawners
// side airplanes
// cop cars 
// defined spawning

// secure highScore
// hook up facebook


// balance game

// wrong way var logo
// 3d keys for intro
// make start screen look good 
// make end screen look good

namespace VacuumShaders
{

		public class StartGame : MonoBehaviour {

		public CreateFont GameScore;
		public GameScoreFont GameScoreFont;
        	public TextMesh Message;


		// Use this for initialization
		void Start () {

            		PlayerPrefs.SetInt ("CurrentScore",0);

			GetScore() ;

           		 Message.text = "Press Any Key To Start";
		}

		// Update is called once per frame
		void Update ()
		{


			if (Input.anyKey) {

				SceneManager.LoadScene (1);
			}

		}
	


	public void GetScore() {


		Action<string> success = (string data) => {


				GameScore.SetText("High Score");
				GameScoreFont.SetScore( data);
		};

		Action<string, string> fail = (string data, string error) => {


			//if (!string.IsNullOrEmpty(error)) {

			//}

			string msg = data + (string.IsNullOrEmpty(error) ? "" : " : "  + error);

		};


			StartCoroutine (  ApiManager.Instance.Get( success, fail)  );


	}

	}
}
