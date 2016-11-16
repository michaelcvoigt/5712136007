using UnityEngine;
using VR = UnityEngine.VR;
using System.Collections;
using UnityEngine.VR;
using UnityEngine.SceneManagement;

public class FlyerHeadController : MonoBehaviour 
{

	[SerializeField]
	public bool AllowMovement = false;
	public GameObject myPlayer;

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
				Quaternion headRotation = InputTracking.GetLocalRotation (VRNode.Head);

				myPlayer.transform.localRotation = headRotation;

			float headY = Mathf.Clamp (   (headPosition.y * 12.0f), -1.0f, Mathf.Infinity);

			float headX = headPosition.x * 18.0f;

			float headZ = Mathf.Clamp (   (headPosition.z * 24.0f), 3.0f, Mathf.Infinity);

			myPlayer.transform.position = new Vector3 (   ( headX* 2.0f ) , headY * 4.0f, headZ );

			transform.position = new Vector3 (headX* 2.0f, headY * 2.0f, headZ );
		}

		}
	}
}
