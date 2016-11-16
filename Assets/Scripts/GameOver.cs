using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using System.IO;

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

		/*
		public void Share()
     {

					FB.Init();
					//FB.Login("public_profile,email,user_friends", LoginCallback);

         if(!ShareImage)
         {
             StartCoroutine(ShareImageShot());
         }
     }
 
     IEnumerator ShareImageShot()
     {
         ShareImage = true;
 
         yield return new WaitForEndOfFrame();
         Texture2D screenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
         
         screenTexture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height),0,0);
         
         screenTexture.Apply();
         
         byte[] dataToSave = screenTexture.EncodeToPNG();
         
         string destination = Path.Combine(Application.persistentDataPath, Screenshot_Name);
         
         File.WriteAllBytes(destination, dataToSave);
         
         var wwwForm = new WWWForm();
         wwwForm.AddBinaryData("image", dataToSave, "InteractiveConsole.png");
         
         FB.API("me/photos", Facebook.HttpMethod.POST, Callback, wwwForm);
 
     }
 
     private Texture2D lastResponseTexture;
     private string lastResponse = "";
     private string ApiQuery = "";
     void Callback(FBResult result)
     {
         lastResponseTexture = null;
         if (result.Error != null)
             lastResponse = "Error Response:\n" + result.Error;
         else if (!ApiQuery.Contains("/picture"))
             lastResponse = "Success Response:\n" + result.Text;
         else
         {
             lastResponseTexture = result.Texture;
             lastResponse = "Success Response:\n";
         }
     }
     */

}

}
