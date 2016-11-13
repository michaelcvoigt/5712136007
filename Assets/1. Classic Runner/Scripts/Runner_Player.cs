
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


namespace VacuumShaders
    {
        
        public class Runner_Player : MonoBehaviour
        {
            public enum SIDE { Left,MiddleLeft,MiddleRight, Right }
            //////////////////////////////////////////////////////////////////////////////
            //                                                                          // 
            //Variables                                                                 //                
            //                                                                          //               
            //////////////////////////////////////////////////////////////////////////////
            static public Runner_Player get;

            Vector3 newPos;
            SIDE side;

            Animation animationComp;
            public AnimationClip moveLeft;
            public AnimationClip moveRight;
			public GameObject HealthBar;
		    public TextMesh ScoreText;

			private int playerHeath = 20;
			private int damagePerHit = 10;
			private int damagePerDebuff = 10;
			private int gainPerBuff = 10;
			private int playerScore = 0;

		private float laneWidth = 7.0f;

            //////////////////////////////////////////////////////////////////////////////
            //                                                                          // 
            //Unity Functions                                                           //                
            //                                                                          //               
            //////////////////////////////////////////////////////////////////////////////
            void Awake()
            {
                get = this;
            }

            // Use this for initialization
            void Start()
            {
                side = SIDE.MiddleLeft;
			transform.position = new Vector3(- laneWidth   , 0, 0); 

                newPos = transform.position;

                animationComp = GetComponent<Animation>();


                Physics.gravity = new Vector3(0, -50, 0);
            }

            void OnDisable()
            {
                //Restor gravity
                Physics.gravity = new Vector3(0, -9.8f, 0);
            }

            // Update is called once per frame
            void Update()
            {

				if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) ||   OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).x < 0  )
                {

				switch ( side ){


				case  SIDE.Right:
		                    

					newPos = new Vector3 (laneWidth, 0, 0);
					side = SIDE.MiddleRight;

		                        //animationComp.Play(moveLeft.name);

					break;

				case SIDE.MiddleRight:
		                
					newPos = new Vector3(  -laneWidth   , 0, 0);
					side = SIDE.MiddleLeft;

		                    //animationComp.Play(moveLeft.name);
					break;


				case SIDE.MiddleLeft:

					newPos = new Vector3(  -laneWidth * 2.0f   , 0, 0);
					side = SIDE.Left;

					//animationComp.Play(moveLeft.name);
					break;

				}

                }

		if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) ||   OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).x > 0)
                {
	                   
					switch ( side ){

					case SIDE.Left:
				
					newPos = new Vector3(  -laneWidth  , 0, 0);
					side = SIDE.MiddleLeft;

					//animationComp.Play(moveRight.name);
					break;

					case SIDE.MiddleLeft:
				
					newPos = new Vector3(  laneWidth  , 0, 0);
					side = SIDE.MiddleRight;

					//animationComp.Play(moveRight.name);
							break;

					case SIDE.MiddleRight:
				
					newPos = new Vector3(  laneWidth * 2.0f, 0, 0);
					side = SIDE.Right;

					//animationComp.Play(moveRight.name);
					break;

					}



                }
		

                transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 10);

          
            }



		public void Score(){

			playerScore++;
			ScoreText.text = playerScore.ToString();

			Runner_SceneManager.IncreaseSpeed ();

		}

            void OnCollisionEnter(Collision collision)
            {
                Vector3 force = (Vector3.forward + Vector3.up + Random.insideUnitSphere).normalized * Random.Range(100, 150);
                collision.rigidbody.AddForce(force, ForceMode.Impulse);

                Runner_Car car = collision.gameObject.GetComponent<Runner_Car>();
				Runner_Debuff debuff = collision.gameObject.GetComponent<Runner_Debuff>();
				Runner_Buff buff = collision.gameObject.GetComponent<Runner_Buff>();




				if (debuff != null) {

					playerHeath = playerHeath - damagePerDebuff;
							Runner_SceneManager.get.DestroyDebuff(debuff);
				}

				if (buff != null) {

					playerHeath = playerHeath + gainPerBuff;
							Runner_SceneManager.get.DestroyBuff(buff);
				}

                if (car != null)
                {
					playerHeath = playerHeath - damagePerHit;
							Runner_SceneManager.get.DestroyCar(car);
				}


				if (playerHeath <= 0) {

					//car.speed = 0;

				PlayerPrefs.SetInt ("CurrentScore", playerScore);
				SceneManager.LoadScene (2);

				}else{


					Vector3 barScale = new Vector3 (  playerHeath / 100.0f, 1.0f, 1.0f);

					HealthBar.transform.localScale = barScale;


                }
            }

            //////////////////////////////////////////////////////////////////////////////
            //                                                                          // 
            //Custom Functions                                                          //
            //                                                                          //               
            //////////////////////////////////////////////////////////////////////////////
        }
    }