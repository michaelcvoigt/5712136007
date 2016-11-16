
using UnityEngine;
using System.Collections;


namespace VacuumShaders
{

       
        public class Flyer_Chunk : MonoBehaviour
        {
            //////////////////////////////////////////////////////////////////////////////
            //                                                                          // 
            //Unity Functions                                                           //                
            //                                                                          //               
            //////////////////////////////////////////////////////////////////////////////

            void Update()
            {
                transform.Translate(Flyer_SceneManager.moveVector * Flyer_SceneManager.speed * Time.deltaTime);
            }

            void FixedUpdate()
            {
			if (transform.position.z < -  Flyer_SceneManager.BackDistance)
                    		Flyer_SceneManager.get.DestroyChunk(this);
            }
        }
    }