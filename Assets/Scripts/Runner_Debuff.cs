using UnityEngine;
using System.Collections;


namespace VacuumShaders
    {
        
        public class Runner_Debuff : MonoBehaviour
        {
			//////////////////////////////////////////////////////////////////////////////
			//                                                                          // 
			//Variables                                                                 //                
			//                                                                          //               
			//////////////////////////////////////////////////////////////////////////////

			Rigidbody rigidBody;
			public float speed = 1;

			//////////////////////////////////////////////////////////////////////////////
			//                                                                          // 
			//Unity Functions                                                           //                
			//                                                                          //               
			//////////////////////////////////////////////////////////////////////////////
			void Start()
			{
				rigidBody = GetComponent<Rigidbody>();

			}

			void FixedUpdate()
			{
				rigidBody.MovePosition(transform.position + Runner_SceneManager.moveVector * Runner_SceneManager.speed * Time.deltaTime * speed);


			if (transform.position.z < -  Runner_SceneManager.BackDistance    )
				{
					Runner_SceneManager.get.DestroyDebuff(this);
				}
			}

			//////////////////////////////////////////////////////////////////////////////
			//                                                                          // 
			//Custom Functions                                                          //
			//                                                                          //               
			//////////////////////////////////////////////////////////////////////////////
		}
	}
