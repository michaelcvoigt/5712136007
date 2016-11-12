using UnityEngine;
using System.Collections;

namespace VacuumShaders
{
    namespace CurvedWorld
    {
        [AddComponentMenu("VacuumShaders/Curved World/Example/Runner/Buff")]
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

				transform.position = new Vector3(Random.Range(-3.5f, 3.5f), 1, Random.Range(140, 240));
				speed = Random.Range(2f, 6f);

			}

			void FixedUpdate()
			{
				rigidBody.MovePosition(transform.position + Runner_SceneManager.moveVector * Runner_SceneManager.get.speed * Time.deltaTime * speed);


				if (transform.position.y < -10)
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
}