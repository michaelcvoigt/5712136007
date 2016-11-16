using UnityEngine;
using System.Collections;


namespace VacuumShaders
    {
       
        public class Flyer_Plane : MonoBehaviour
        {
            //////////////////////////////////////////////////////////////////////////////
            //                                                                          // 
            //Variables                                                                 //                
            //                                                                          //               
            //////////////////////////////////////////////////////////////////////////////

            Rigidbody rigidBody;
            public float speed = 1;

		public bool Dirty = false;


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

		public void Hit()
		{
			scoreCounts = false;
		}

            void FixedUpdate()
            {
                rigidBody.MovePosition(    transform.position + Flyer_SceneManager.sideMoveVector * Flyer_SceneManager.speed * Time.deltaTime * speed  );

				if (transform.position.z < -  Flyer_SceneManager.BackDistance    )
                {
					Flyer_SceneManager.get.DestroyPlane(this, scoreCounts);
                }
            }

            //////////////////////////////////////////////////////////////////////////////
            //                                                                          // 
            //Custom Functions                                                          //
            //                                                                          //               
            //////////////////////////////////////////////////////////////////////////////
        }
    }
