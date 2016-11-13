using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace VacuumShaders
{

	public class StartGame : MonoBehaviour {

		public TextMesh GameScore;
        public TextMesh Message;

		// Use this for initialization
		void Start () {

            PlayerPrefs.SetInt ("CurrentScore",0);
			int Score = PlayerPrefs.GetInt ("HighScore");

			GameScore.text = "High Score =" + Score.ToString();

            Message.text = "Press Enter To Start";
		}

		// Update is called once per frame
		void Update ()
		{


			if (Input.GetKey (KeyCode.Return)) {

				SceneManager.LoadScene (1);
			}

		}
	}

}
