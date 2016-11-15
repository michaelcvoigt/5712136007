using UnityEngine;
using System.Collections;

namespace VacuumShaders
{
    
       
        public class Runner_Buff : MonoBehaviour
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
					Runner_SceneManager.get.DestroyBuff(this);
				}
			}

			//////////////////////////////////////////////////////////////////////////////
			//                                                                          // 
			//Custom Functions                                                          //
			//                                                                          //               
			//////////////////////////////////////////////////////////////////////////////
		}

}