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
		public AnimationClip moveForward;
		public AnimationClip moveBackwards;
		public AnimationClip bounceUp;

		public AudioSource DamageSound;
		public AudioSource BuffSound;
		public AudioSource DebuffSound;
		public AudioSource HornSound;
		//public GameObject HealthBar;
		public GameScoreFont ScoreText;
		public GameScoreFont LivesText;

		public ParticleSystem DamageEmitter;
		public ParticleSystem DamageChildEmitter1;
		public ParticleSystem DamageChildEmitter2;
		public ParticleSystem DamageChildEmitter3;
		public ParticleSystem DamageChildEmitter4;


		public ParticleSystem BuffEmitter;
		public ParticleSystem BuffChildEmitter1;
		public ParticleSystem BuffChildEmitter2;
		public ParticleSystem BuffChildEmitter3;
		public ParticleSystem BuffChildEmitter4;

			private int playerHealth = 1;
			private int damagePerHit = 1;
			private int damagePerDebuff = 1;
			private int gainPerBuff = 1;
			private int playerScore = 0;

		private static float strifeAmount = 1.0f;
		private static float laneMax = 15.0f * strifeAmount;

		private static GameObject lastHitObject = null;

            //////////////////////////////////////////////////////////////////////////////
            //                                                                          // 
            //Unity Functions                                                           //                
            //                                                                          //               
            //////////////////////////////////////////////////////////////////////////////
            void Awake()
            {
                get = this;

			ScoreText.SetScore( "0" );
            }

            // Use this for initialization
            void Start()
            {

               	side = SIDE.MiddleLeft;
		transform.position = new Vector3(- strifeAmount   , 0, 0); 
                newPos = transform.position;

                animationComp = GetComponent<Animation>();

                //Physics.gravity = new Vector3(0, -50, 0);
            }
		/*
            void OnDisable()
            {
                //Restore gravity
                Physics.gravity = new Vector3(0, -9.8f, 0);
            }*/

            // Update is called once per frame
            void Update()
            {

			if (Input.GetKeyDown (KeyCode.Space)) {

				//newPos = new Vector3 (transform.position.x, transform.position.y + strifeAmount  , transform.position.z);
				animationComp.Play(bounceUp.name);
				HornSound.Play ();
			}


			// the left thumbstick
			//float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
			//float v = CrossPlatformInputManager.GetAxisRaw("Vertical");


			if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W) || Input.GetAxis("Xbox360ControllerDPadY") > 0   ) {

				if (transform.position.z < 36) {

					newPos = new Vector3 (transform.position.x, 0, transform.position.z + strifeAmount);
					animationComp.Play (moveForward.name);
				}
			}

			if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S) ||  Input.GetAxis("Xbox360ControllerDPadY") < 0    ) {

				newPos = new Vector3 (transform.position.x,0, transform.position.z - strifeAmount);
				animationComp.Play(moveBackwards.name);
			}


			if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetAxis("Xbox360ControllerDPadX") < 0  )
                	{
				float x = Mathf.Clamp( transform.position.x - strifeAmount,  - (strifeAmount* 15.0f), (strifeAmount* 15.0f)  );

				newPos = new Vector3 (x  , 0.0f,  transform.position.z);
				animationComp.Play(moveLeft.name);
				/*
				switch ( side ){


				case  SIDE.Right:
		                    

					newPos = new Vector3 (strifeAmount, 0, newPosZ);
					side = SIDE.MiddleRight;

		                        animationComp.Play(moveLeft.name);

					break;

				case SIDE.MiddleRight:
		                
					newPos = new Vector3(  -strifeAmount   , 0, newPosZ);
					side = SIDE.MiddleLeft;

		                    animationComp.Play(moveLeft.name);
					break;


				case SIDE.MiddleLeft:

					newPos = new Vector3(  -strifeAmount * 2.0f   , 0, newPosZ);
					side = SIDE.Left;

					animationComp.Play(moveLeft.name);
					break;

				}
				*/
                }

			if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.GetAxis("Xbox360ControllerDPadX") > 0 )
                {
				float x =      Mathf.Clamp( transform.position.x + strifeAmount, -  (strifeAmount* 15.0f) , (strifeAmount* 15.0f));

				newPos = new Vector3 ( x, 0,  transform.position.z);
				animationComp.Play(moveRight.name);

				/*
					switch ( side ){

					case SIDE.Left:
				
					newPos = new Vector3(  -strifeAmount  , 0, newPosZ);
					side = SIDE.MiddleLeft;

					animationComp.Play(moveRight.name);
					break;

					case SIDE.MiddleLeft:
				
					newPos = new Vector3(  strifeAmount  , 0, newPosZ);
					side = SIDE.MiddleRight;

					animationComp.Play(moveRight.name);
							break;

					case SIDE.MiddleRight:
				
					newPos = new Vector3(  strifeAmount * 2.0f, 0, newPosZ);
					side = SIDE.Right;

					animationComp.Play(moveRight.name);
					break;

					}

					*/

                }
		

                transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 10);

         
            }



		public void Score(){

			playerScore++;
			ScoreText.SetScore( playerScore.ToString());

			Runner_SceneManager.IncreaseSpeed ();

		}

            public void OnCollisionEnter(Collision collision)
            {

			Runner_Chunk chunk = collision.gameObject.GetComponent<Runner_Chunk>();

			if (chunk != null) {

				return;
			}

			print ( "collision = " +  collision.gameObject.name + " playerH = " + playerHealth );


                	Vector3 force = (Vector3.forward + Vector3.up + Random.insideUnitSphere).normalized * Random.Range(100, 150);
                	collision.rigidbody.AddForce(force, ForceMode.Impulse);

			if (collision.gameObject == lastHitObject) {


				lastHitObject = null;
				return;

			}

			lastHitObject = collision.gameObject;
		
                		Runner_Car car = collision.gameObject.GetComponent<Runner_Car>();
				Runner_Debuff debuff = collision.gameObject.GetComponent<Runner_Debuff>();
				Runner_Buff buff = collision.gameObject.GetComponent<Runner_Buff>();


				if (debuff != null) {

					playerHealth = playerHealth - damagePerDebuff;
							Runner_SceneManager.get.DestroyDebuff(debuff);
				DamageEmitter.Emit (1000);
				DamageChildEmitter1.Emit (1000);
				DamageChildEmitter2.Emit (1000);
				DamageChildEmitter3.Emit (1000);
				DamageChildEmitter4.Emit (1000);

				DebuffSound.Play ();
				}

				if (buff != null) {

					playerHealth = playerHealth + gainPerBuff;
							Runner_SceneManager.get.DestroyBuff(buff);
				BuffSound.Play();

				BuffEmitter.Emit (1000);
				BuffChildEmitter1.Emit (1000);
				BuffChildEmitter2.Emit (1000);
				BuffChildEmitter3.Emit (1000);
				BuffChildEmitter4.Emit (1000);

				}

			if (car != null && car.Dirty == false )
                	{
					playerHealth = playerHealth - damagePerHit;
					//Runner_SceneManager.get.DestroyCar(car);

				car.Hit ();
				car.MarkDirty ();

				DamageEmitter.Emit (1000);
				DamageChildEmitter1.Emit (1000);
				DamageChildEmitter2.Emit (1000);
				DamageChildEmitter3.Emit (1000);
				DamageChildEmitter4.Emit (1000);

				DamageSound.Play();

				}


				if (playerHealth <= 0) {

					//car.speed = 0;

					PlayerPrefs.SetInt ("CurrentScore", playerScore);
					SceneManager.LoadScene (2);

				}else{
				
					string lives = "";
					for (int i = 0; i < playerHealth; i++) {
					lives = lives + "-";
				}

				LivesText.SetScore (lives);

		
                }
                
            }

            //////////////////////////////////////////////////////////////////////////////
            //                                                                          // 
            //Custom Functions                                                          //
            //                                                                          //               
            //////////////////////////////////////////////////////////////////////////////
        }
    }