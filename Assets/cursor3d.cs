using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class cursor3d : MonoBehaviour {


	public GameObject KeyRightOver;
	public GameObject KeyLeftOver;

	public Animation KeyEdgeRight;
	public Animation KeyEdgeLeft;

	private Collider currentCollider = null;


	// Use this for initialization
	void Start () {
	
	}


	public void OnTriggerEnter (Collider c)
	{

		currentCollider = c;

		KeyLeftOver.SetActive(false);
		KeyRightOver.SetActive(false);

		if(  c.name == "keyright" ){

			KeyRightOver.SetActive(true);
			KeyEdgeRight.Play();

		}


			if(  c.name == "keyleft" ){

			KeyLeftOver.SetActive(true);
			KeyEdgeLeft.Play();



		}

	}

	public void OnTriggerExit ()
	{
		KeyLeftOver.SetActive(false);
		KeyRightOver.SetActive(false);



	}

	public void AnimationEnded ()
	{

		if (currentCollider.name == "keyright") {

			SceneManager.LoadScene (3);
		}
		if (currentCollider.name == "keyleft") {

			SceneManager.LoadScene (1);
		}

	
}

	
}
