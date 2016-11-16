using UnityEngine;
using VR = UnityEngine.VR;
using System.Collections;
using UnityEngine.VR;
using UnityEngine.SceneManagement;

public class OculusHeadController : MonoBehaviour 
{

	[SerializeField]
	public bool AllowMovement = false;


	void Awake()
	{
		VR.InputTracking.Recenter ();
	}
		


	void Update ()
	{

			if (Input.GetKey(KeyCode.Escape) ) {

				SceneManager.LoadScene (0);
			}


		if( !OVRPlugin.userPresent ){

			Time.timeScale = 0.0f;
			VR.InputTracking.Recenter ();

		}else{

			Time.timeScale = 1.0f;

		if ( AllowMovement   )
		{

			Vector3 headPosition = InputTracking.GetLocalPosition (VRNode.Head);

			float headY = Mathf.Clamp (   (headPosition.y * 24.0f), -1.0f, Mathf.Infinity);

			float headX = headPosition.x * 24.0f;

			float headZ = Mathf.Clamp (   (headPosition.z * 24.0f), 3.0f, Mathf.Infinity);

			transform.position = new Vector3 (headX* 2.0f, headY * 2.0f, headZ );

		
		}

		}
	}
}
