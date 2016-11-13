using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using UnityEngine.Networking;

namespace VacuumShaders
{
    public class ApiManager : MonoBehaviour {
        #region Singleton
        private static ApiManager mInstance;
        public static ApiManager Instance {
            get {
                if (mInstance == null) {
                    mInstance = new GameObject().AddComponent<ApiManager>();
                }
                return mInstance;
            }
        }
        #endregion

        #region MonoBehaviour functions
        // Use this for initialization
        void Start() {
        }

        // Update is called once per frame
        void Update() {
        }
        #endregion

    

        public IEnumerator PostScore(int score, Action<string> onSuccess, Action<string, string> onFail = null) {
			
            WWWForm form = new WWWForm();

            form.AddField("score", score);
           

            UnityWebRequest www = UnityWebRequest.Post("http://grantvoigt.com/michael/highscore.php", form);
            yield return www.Send();

            if (www.isError || www.responseCode == 500) {
               

                if (onFail != null) { onFail(www.downloadHandler.text, www.error); }
            } else {
               

                onSuccess(www.downloadHandler.text);
            }
        }



		public IEnumerator Get( Action<string> onSuccess, Action<string, string> onFail = null ) {
			
			UnityWebRequest www = UnityWebRequest.Get("http://grantvoigt.com/michael/GetHighscore.php");

			yield return www.Send();


			if (www.isError || www.responseCode == 500) {
				
				if (onFail != null) { onFail(www.downloadHandler.text, www.error); }
			} else {


				onSuccess(www.downloadHandler.text);
			}
		}


    }
}
