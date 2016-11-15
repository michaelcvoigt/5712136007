using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VacuumShaders
{


public class GameScoreFont : MonoBehaviour {

		public Vector3 scale = new Vector3 (1, 1, 1);

	private string score = null;

	private float offsetAmount = 0.8f;
	private float offset = 0.0f;
	

		private List<GameObject> numbers = new List<GameObject>();



		void Start(){

			offsetAmount = offsetAmount * scale.x;


			if (score != null) {
				CreateText ();
			}
		}

		public void SetScore ( string myScore ) {


			//float moveBack = transform.localPosition.x - ( offset * 0.5f ) + ( offsetAmount * 0.5f ) ;
			transform.position = new Vector3 (0.0f, transform.localPosition.y , transform.localPosition.z);
		
			offset = 0.0f;

			foreach (GameObject number in numbers) {

				Destroy (number);

			}
				
			numbers.Clear ();

			score = myScore;
			if (score != null) {
				CreateText ();
			}

		}



	void CreateText () {
	
			var chars = score.ToCharArray();

			foreach (char character in chars)
			{

				if( character != null && Resources.Load("Text/"+ character )!=null ) {

					Vector3 letterPosition = new Vector3 (offset, 0, 0);
					Quaternion letterRotation = new Quaternion (0, 180.0f, 0,0);

					GameObject letterGo = Instantiate(Resources.Load("Text/"+character), transform.position + letterPosition , letterRotation) as GameObject;
					letterGo.transform.parent = transform;
					letterGo.transform.localScale = scale;
					numbers.Add (letterGo);
				}
			
			offset += offsetAmount;

			}

			float moveLeft = ( transform.localPosition.x - ( offset * 0.5f ) + ( offsetAmount * 0.5f ) )  ;
			transform.position = new Vector3 (moveLeft, transform.localPosition.y , transform.localPosition.z);
	}

	}
}
