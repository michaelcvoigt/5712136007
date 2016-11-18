using UnityEngine;
using System.Collections;

public class animEvent : MonoBehaviour {

	public cursor3d myCursor;



	public void AnimationEnded (float theValue)
	{

		myCursor.AnimationEnded();
	}
}
