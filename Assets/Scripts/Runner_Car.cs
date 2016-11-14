using UnityEngine;
using System.Collections;


namespace VacuumShaders
    {
       
        public class Runner_Car : MonoBehaviour
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

                float roadWidth = Runner_SceneManager.RoadWidth;

           	transform.position = new Vector3(Random.Range(-roadWidth, roadWidth), 1, Random.Range(140, 240));
                speed = Random.Range(2f, 6f);

            }

            void FixedUpdate()
            {
                rigidBody.MovePosition(transform.position + Runner_SceneManager.moveVector * Runner_SceneManager.speed * Time.deltaTime * speed);


                if (transform.position.y < -10)
                {
                    Runner_SceneManager.get.DestroyCar(this);
                }
            }

            //////////////////////////////////////////////////////////////////////////////
            //                                                                          // 
            //Custom Functions                                                          //
            //                                                                          //               
            //////////////////////////////////////////////////////////////////////////////
        }
    }
