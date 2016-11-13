using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using VR = UnityEngine.VR;
using UnityEngine.VR;
using VacuumShaders.CurvedWorld;


    namespace VacuumShaders
    {
   
        public class Runner_SceneManager : MonoBehaviour
        {
            //////////////////////////////////////////////////////////////////////////////
            //                                                                          // 
            //Variables                                                                 //                
            //                                                                          //               
            //////////////////////////////////////////////////////////////////////////////
            static public Runner_SceneManager get;

            public GameObject[] chunks;

			public GameObject[] chunksLeft;

			public GameObject[] chunksRight;

            public GameObject[] cars;

			public GameObject[] buffs;
			public GameObject[] debuffs;

			public AudioClip SpawnSound;

			public CurvedWorld_Controller curvedWorld_Controller;

            static public float chunkSize = 60;
            static public Vector3 moveVector = new Vector3(0, 0, -1);
            static public GameObject lastChunk;
			static public GameObject lastChunkRight;
			static public GameObject lastChunkLeft;

        static public float RoadWidth = 10.0f;
           static public float speed = 8.0f;

            List<Material> listMaterials;
            //////////////////////////////////////////////////////////////////////////////
            //                                                                          // 
            //Unity Functions                                                           //                
            //                                                                          //               
            //////////////////////////////////////////////////////////////////////////////
            void Awake()
            { 
                get = this;

                
                //Instantiate chunks
                for (int i = 0; i < chunks.Length; i++)
                {
                    GameObject obj = (GameObject)Instantiate(chunks[i]);

                    obj.transform.position = new Vector3(0, 0, i * chunkSize);

                    lastChunk = obj;
                }

				//Instantiate chunks left
				for (int i = 0; i < chunksLeft.Length; i++)
				{
					GameObject obj = (GameObject)Instantiate(chunksLeft[i]);

					obj.transform.position = new Vector3(-30, -5, i * chunkSize);

					lastChunkLeft = obj;
				}

				//Instantiate chunks Right
				for (int i = 0; i < chunksRight.Length; i++)
				{
					GameObject obj = (GameObject)Instantiate(chunksRight[i]);

					obj.transform.position = new Vector3(30, -5, i * chunkSize);

					lastChunkRight = obj;
				}

                //Instantiate cars
                for (int i = 0; i < cars.Length; i++)
                {
                    Instantiate(cars[i]);
                }


				//Instantiate buffs
				for (int i = 0; i < buffs.Length; i++)
				{
					Instantiate(buffs[i]);
				}

				//Instantiate debuffs
				for (int i = 0; i < debuffs.Length; i++)
				{
					Instantiate(debuffs[i]);
				}
            } 

            // Use this for initialization
            void Start()
            {
                Renderer[] renderers = FindObjectsOfType(typeof(Renderer)) as Renderer[];

                listMaterials = new List<Material>();
                foreach (Renderer _renderer in renderers)
                {
                    listMaterials.AddRange(_renderer.sharedMaterials);
                }

                listMaterials = listMaterials.Distinct().ToList();
            }

			// Update is called once per frame
			void Update ()
			{

				Vector3 headPosition = InputTracking.GetLocalPosition (VRNode.Head);

				float headY = Mathf.Clamp (   (headPosition.y * 44.0f), -1.0f, Mathf.Infinity);

				float headX = Mathf.Clamp (   (headPosition.x * 30.0f), -1.0f, Mathf.Infinity);

				Vector3 worldBend = new Vector3 ( headY, headX, 0   );

				curvedWorld_Controller.SetBend (worldBend);

				/*
				if (Input.GetKey (KeyCode.UpArrow)) {

					speed += 0.01f;
				}
				if (speed > 0.0f) {
					
					if (Input.GetKey (KeyCode.DownArrow)) {

						speed--;
					}
				}
				*/

			}

            //////////////////////////////////////////////////////////////////////////////
            //                                                                          // 
            //Custom Functions                                                          //
            //                                                                          //               
            //////////////////////////////////////////////////////////////////////////////

		static public void IncreaseSpeed(){

			speed++;

		}
            public void DestroyChunk(Runner_Chunk moveElement)
            {
                Vector3 newPos = lastChunk.transform.position;
                newPos.z += chunkSize;


                lastChunk = moveElement.gameObject;
                lastChunk.transform.position = newPos;
            }

			public void DestroyChunkLeft(Runner_ChunkLeft moveElement)
			{
				Vector3 newPos = lastChunkLeft.transform.position;
				newPos.z += chunkSize;


				lastChunkLeft = moveElement.gameObject;
				lastChunkLeft.transform.position = newPos;
			}


			public void DestroyChunkRight(Runner_ChunkRight moveElement)
			{
				Vector3 newPos = lastChunkRight.transform.position;
				newPos.z += chunkSize;


				lastChunkRight = moveElement.gameObject;
				lastChunkRight.transform.position = newPos;
			}

            public void DestroyCar(Runner_Car car)
            {
                GameObject.Destroy(car.gameObject);

                Instantiate(cars[Random.Range(0, cars.Length)]);
            }

			public void DestroyBuff(Runner_Buff buff)
			{
				GameObject.Destroy(buff.gameObject);

				Instantiate(buffs[Random.Range(0, buffs.Length)]);
			}

			public void DestroyDebuff(Runner_Debuff debuff)
			{
				GameObject.Destroy(debuff.gameObject);

				//SpawnSound.

				Instantiate(debuffs[Random.Range(0, debuffs.Length)]);
			}



        }
    }