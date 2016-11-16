using UnityEngine;
using System.Collections;


namespace VacuumShaders
    {
       
        public class Flyer_Car : MonoBehaviour
        {
            //////////////////////////////////////////////////////////////////////////////
            //                                                                          // 
            //Variables                                                                 //                
            //                                                                          //               
            //////////////////////////////////////////////////////////////////////////////

            Rigidbody rigidBody;
            public float speed = 1;

		public bool Dirty = false;
		private float spawnYLocation = 0.0f;


		private bool scoreCounts = true;

            //////////////////////////////////////////////////////////////////////////////
            //                                                                          // 
            //Unity Functions                                                           //                
            //                                                                          //               
            //////////////////////////////////////////////////////////////////////////////
            
		void Awake()
		{

			Dirty = false;
		}
		
		void Start()
        {
                rigidBody = GetComponent<Rigidbody>();

        }
        
		public void MarkDirty()
		{
			Dirty = true;
		}

		public void SetSpawnLocation( Vector3 vector )
		{
			spawnYLocation = vector.y;
		}

		public void Hit()
		{
			scoreCounts = false;
		}

    void FixedUpdate ()
		{
    
			if (transform.position.z < -Flyer_SceneManager.BackDistance) {
				Flyer_SceneManager.get.DestroyCar (this, scoreCounts);
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
