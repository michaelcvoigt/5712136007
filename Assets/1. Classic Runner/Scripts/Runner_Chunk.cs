
using UnityEngine;
using System.Collections;


namespace VacuumShaders
{

       
        public class Runner_Chunk : MonoBehaviour
        {
            //////////////////////////////////////////////////////////////////////////////
            //                                                                          // 
            //Unity Functions                                                           //                
            //                                                                          //               
            //////////////////////////////////////////////////////////////////////////////

            void Update()
            {
                transform.Translate(Runner_SceneManager.moveVector * Runner_SceneManager.speed * Time.deltaTime);
            }

            void FixedUpdate()
            {
                if (transform.position.z < -100)
                    Runner_SceneManager.get.DestroyChunk(this);
            }
        }
    }