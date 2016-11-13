using UnityEngine;
using System.Collections;

namespace VacuumShaders
{


public class CreateFont : MonoBehaviour {



	public string text = null;

	public float offsetAmount = 0.0f;

	private float offset = 0.0f;

		void Start(){
			if (text != null) {
				CreateText ();
			}
		}

		public void SetText ( string myText ) {

			text = myText;
			if (text != null) {
				CreateText ();
			}

		}

	void CreateText () {
	
			var chars = text.ToCharArray();

			foreach (char character in chars)
			{

				if( character != null && Resources.Load("Text/"+ character )!=null ) {

					Vector3 letterPosition = new Vector3 (offset, 0, 0);
					Quaternion letterRotation = new Quaternion (0, 180.0f, 0,0);

					GameObject letterGo = Instantiate(Resources.Load("Text/"+character), transform.position + letterPosition , letterRotation) as GameObject;
					letterGo.transform.parent = transform;
				}
			
			offset += offsetAmount;
			}

			float moveLeft = transform.localPosition.x - ( offset * 0.5f ) + ( offsetAmount * 0.5f ) ;


			transform.position = new Vector3 (moveLeft, transform.localPosition.y , transform.localPosition.z);
	

	}

	}
}
