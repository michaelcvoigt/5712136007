﻿using UnityEngine;
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

		private bool scoreCounts = true;

            //////////////////////////////////////////////////////////////////////////////
            //                                                                          // 
            //Unity Functions                                                           //                
            //                                                                          //               
            //////////////////////////////////////////////////////////////////////////////
            void Start()
            {
                rigidBody = GetComponent<Rigidbody>();

                float roadWidth = Runner_SceneManager.RoadWidth;

           	//transform.position = new Vector3(Random.Range(-roadWidth, roadWidth), 1, Random.Range(140, 240));


		transform.position = new Vector3(Random.Range(-roadWidth, roadWidth), 0.0f, Random.Range(140, 240));

                speed = Random.Range(2f, 6f);

            }

		public void Hit()
		{
			scoreCounts = false;
		}

            void FixedUpdate()
            {
                rigidBody.MovePosition(transform.position + Runner_SceneManager.moveVector * Runner_SceneManager.speed * Time.deltaTime * speed);

		
		if (transform.position.z < -  Runner_SceneManager.BackDistance    )
                {
			Runner_SceneManager.get.DestroyCar(this, scoreCounts);
                }
            }

            //////////////////////////////////////////////////////////////////////////////
            //                                                                          // 
            //Custom Functions                                                          //
            //                                                                          //               
            //////////////////////////////////////////////////////////////////////////////
        }
    }
