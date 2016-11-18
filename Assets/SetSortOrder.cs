using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]

public class SetSortOrder : MonoBehaviour {

	
	void Start ()
	{


		Renderer renderer = GetComponent<Renderer> ();

				renderer.material.renderQueue = -1;

	}
 
   void OnValidate() {
      Start();
   }
}
