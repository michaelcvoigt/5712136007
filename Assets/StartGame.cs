using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;


namespace VacuumShaders
{

	public class StartGame : MonoBehaviour {

		public TextMesh GameScore;
        public TextMesh Message;

		// Use this for initialization
		void Start () {

            PlayerPrefs.SetInt ("CurrentScore",0);

			GetScore() ;

           		 Message.text = "Press Enter To Start";
		}

		// Update is called once per frame
		void Update ()
		{


			if (Input.GetKey (KeyCode.Return)) {

				SceneManager.LoadScene (1);
			}

		}
	


	public void GetScore() {


		Action<string> success = (string data) => {


				GameScore.text = "Server High Score =" + data;

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
