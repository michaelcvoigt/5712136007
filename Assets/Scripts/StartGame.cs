using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;




// turn off keys in fly mode

// get airplane
// find cool effect


// side tweaked
// cop cars 

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

           		 Message.text = "Press < to Drive \n or > to Fly!";
		}

		// Update is called once per frame
		void Update ()
		{

			if (Input.GetKey(KeyCode.LeftArrow) ) {

				SceneManager.LoadScene (1);
			}

			if (Input.GetKey(KeyCode.RightArrow) ){

				SceneManager.LoadScene (3);
			}
		}
	

	public void GetScore() {


		Action<string> success = (string data) => {


				GameScore.SetText("High Score");
				GameScoreFont.SetScore( data);
		};

		Action<string, string> fail = (string data, string error) => {

			string msg = data + (string.IsNullOrEmpty(error) ? "" : " : "  + error);

		};


			StartCoroutine (  ApiManager.Instance.Get( success, fail)  );


	}

	}
}
