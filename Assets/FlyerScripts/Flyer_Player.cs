using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using MorePPEffects;


namespace VacuumShaders
    {
        
        public class Flyer_Player : MonoBehaviour
        {
            public enum SIDE { Left,MiddleLeft,MiddleRight, Right }
            //////////////////////////////////////////////////////////////////////////////
            //                                                                          // 
            //Variables                                                                 //                
            //                                                                          //               
            //////////////////////////////////////////////////////////////////////////////
            static public Flyer_Player get;

            Vector3 newPos;
            SIDE side;

	            Animation animationComp;
	            public AnimationClip moveLeft;
	            public AnimationClip moveRight;
			public AnimationClip moveForward;
			public AnimationClip moveBackwards;
			public AnimationClip moveDeath;
			public AnimationClip bounceUp;
			public AnimationClip moveHit;

		public AnimationClip flyLeft;
		public AnimationClip flyRight;
	
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

			public LowResolution MyLowResolution;

			private int playerHealth = 3;
			private int damagePerHit = 1;
			private int damagePerDebuff = 2;
			private int gainPerBuff = 1;
			private int scorePerBuff = 1;
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
				UpdateUI();
				ScoreText.SetScore( playerScore.ToString());
            }

            // Use this for initialization
            void Start()
            {

               	side = SIDE.MiddleLeft;
				transform.position = new Vector3(- strifeAmount   , 0, 0); 
                newPos = transform.position;

                animationComp = GetComponent<Animation>();

                Physics.gravity = new Vector3(0, -50.0f, 0);
            }
		/*
            void OnDisable()
            {
                //Restore gravity
                Physics.gravity = new Vector3(0, -9.8f, 0);
            }*/

            // Update is called once per frame
            void Update ()
		{


			if (Input.GetKeyDown (KeyCode.Space)) {

				//newPos = new Vector3 (transform.position.x, transform.position.y + strifeAmount  , transform.position.z);
				animationComp.Play (bounceUp.name);
				HornSound.Play ();
			}

			if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W) || Input.GetAxis ("Xbox360ControllerDPadY") > 0) {

				if (transform.position.z < 36) {

					newPos = new Vector3 (transform.position.x, 0, transform.position.z + strifeAmount);
					animationComp.Play (moveForward.name);
				}
			}

			if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S) || Input.GetAxis ("Xbox360ControllerDPadY") < 0) {

				if (transform.position.z > -10) {
					newPos = new Vector3 (transform.position.x, 0, transform.position.z - strifeAmount);
					animationComp.Play (moveBackwards.name);
				}
			}


			if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetAxis("Xbox360ControllerDPadX") < 0  ){
                	
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

			if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.GetAxis("Xbox360ControllerDPadX") > 0 ){
			
				float x = Mathf.Clamp( transform.position.x + strifeAmount, -  (strifeAmount* 15.0f) , (strifeAmount* 15.0f));

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
		

                transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 500);
				transform.rotation = Quaternion.Lerp( transform.rotation, Quaternion.identity  , Time.deltaTime * 1000 );
         
            }



		public void Score(){

			playerScore++;
			ScoreText.SetScore( playerScore.ToString());
			Flyer_SceneManager.IncreaseSpeed ();

		}

            public void OnCollisionEnter(Collision collision)
            {

			Flyer_Chunk chunk = collision.gameObject.GetComponent<Flyer_Chunk>();

			if (chunk != null) {

				return;
			}


                	Vector3 force = (Vector3.forward + Vector3.up + Random.insideUnitSphere).normalized * Random.Range(100, 150);
                	collision.rigidbody.AddForce(force, ForceMode.Impulse);

			if (collision.gameObject == lastHitObject) {


				lastHitObject = null;
				return;

			}

			lastHitObject = collision.gameObject;
		
                		Flyer_Car car = collision.gameObject.GetComponent<Flyer_Car>();
				Flyer_Debuff debuff = collision.gameObject.GetComponent<Flyer_Debuff>();
				Flyer_Buff buff = collision.gameObject.GetComponent<Flyer_Buff>();


				if (debuff != null) {

					playerHealth = 0;
							Flyer_SceneManager.get.DestroyDebuff(debuff);
				DamageEmitter.Emit (1000);
				DamageChildEmitter1.Emit (1000);
				DamageChildEmitter2.Emit (1000);
				DamageChildEmitter3.Emit (1000);
				DamageChildEmitter4.Emit (1000);

				DebuffSound.Play ();
				
				animationComp.Play(moveHit.name);
				}

				if (buff != null) {

					playerScore = playerScore + scorePerBuff;
					//playerHealth = playerHealth + gainPerBuff;
					Flyer_SceneManager.get.DestroyBuff(buff);
				BuffSound.Play();

				BuffEmitter.Emit (1000);
				BuffChildEmitter1.Emit (1000);
				BuffChildEmitter2.Emit (1000);
				BuffChildEmitter3.Emit (1000);
				BuffChildEmitter4.Emit (1000);
				
				animationComp.Play(moveHit.name);
				}

			if (car != null && car.Dirty == false )
                	{
					playerHealth = playerHealth - damagePerHit;
					//Flyer_SceneManager.get.DestroyCar(car);

				StartCoroutine(DamageCoroutine());
				

				car.Hit ();
				car.MarkDirty ();

				DamageEmitter.Emit (1000);
				DamageChildEmitter1.Emit (1000);
				DamageChildEmitter2.Emit (1000);
				DamageChildEmitter3.Emit (1000);
				DamageChildEmitter4.Emit (1000);

				DamageSound.Play();
				
				animationComp.Play(moveHit.name);

				}


				if (playerHealth <= 0) {

					//car.speed = 0;

					PlayerPrefs.SetInt ("CurrentScore", playerScore);
					
					newPos = new Vector3 (transform.position.x, 0, transform.position.z - strifeAmount);
					animationComp.Play (moveDeath.name);
					
					LivesText.SetScore ("GameOver");
					
					StartCoroutine(OnCollisionCoroutine());
					
					
					return;
				}
				
					UpdateUI ();
		
            }
            
            
            public void UpdateUI()
			{
                string lives = "";
                
				for (int i = 0; i < playerHealth; i++) {
					lives = lives + "-";
				};
				
                LivesText.SetScore (lives);
			}


        IEnumerator OnCollisionCoroutine ()
		{
			yield return new WaitForSeconds (2); 
			
			SceneManager.LoadScene (2);
		}

		IEnumerator DamageCoroutine ()
		{
			int start = 0;
			MyLowResolution.enabled = true;
				
			while (start < 200) {
				
				start = start + 15;
				
				MyLowResolution.resolutionX = start;
				MyLowResolution.resolutionY = start;
				
				yield return null;
			}
		
			
			StartCoroutine(DamageDoneCoroutine());
			
		}
		
		IEnumerator DamageDoneCoroutine ()
		{
			int start = 200;
				
			while (start > 200) {
				
				start = start - 20;
				
				MyLowResolution.resolutionX = start;
				MyLowResolution.resolutionY = start;
				
				yield return null;
			}
			
		
			
			MyLowResolution.enabled = false;
			
		}

        }
        
        
     
    }