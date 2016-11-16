using UnityEngine;
using System.Collections;


namespace VacuumShaders
    {
        
        public class Flyer_Debuff : MonoBehaviour
        {
			//////////////////////////////////////////////////////////////////////////////
			//                                                                          // 
			//Variables                                                                 //                
			//                                                                          //               
			//////////////////////////////////////////////////////////////////////////////

			Rigidbody rigidBody;
			public float speed = 1;
		private float spawnYLocation = 0.0f;

			//////////////////////////////////////////////////////////////////////////////
			//                                                                          // 
			//Unity Functions                                                           //                
			//                                                                          //               
			//////////////////////////////////////////////////////////////////////////////
			void Start()
			{
				rigidBody = GetComponent<Rigidbody>();

			}

		void FixedUpdate ()
		{
    
			if (transform.position.z < -Flyer_SceneManager.BackDistance) {
				Flyer_SceneManager.get.DestroyDebuff (this);
			}
                    
			Vector3 newPos = transform.position;



			if ( transform.position.y < ( spawnYLocation + Flyer_SceneManager.spawnYHeight ) ) {

				// half of the gravity
				Vector3 force = Vector3.up * 25.0f;  // (Flyer_SceneManager.spawnYHeight * 10);

				GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);

			} 


				rigidBody.MovePosition ( newPos +  ( Flyer_SceneManager.moveVector * Flyer_SceneManager.speed * Time.deltaTime * speed )   );  

            }

  
        }

}
