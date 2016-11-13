using UnityEngine;
using VR = UnityEngine.VR;
using System.Collections;
using UnityEngine.VR;


public class MichaelHeadController : MonoBehaviour 
{

	[SerializeField]
	public bool AllowMovement = false;


	void Awake()
	{
		VR.InputTracking.Recenter ();
	}
		


	void Update ()
	{

		if( !OVRPlugin.userPresent ){

			VR.InputTracking.Recenter ();

		}else{



		if ( AllowMovement   )
		{

			Vector3 headPosition = InputTracking.GetLocalPosition (VRNode.Head);

			float headY = Mathf.Clamp (   (headPosition.y * 24.0f), -1.0f, Mathf.Infinity);

			float headX = headPosition.x * 24.0f;

			float headZ = Mathf.Clamp (   (headPosition.z * 24.0f), 3.0f, Mathf.Infinity);

			transform.position = new Vector3 (headX, headY, headZ );

		
		}

		}
	}
}
