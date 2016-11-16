using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.VR;
using VacuumShaders.CurvedWorld;




namespace VacuumShaders
{

public class Flyer_SceneManager : MonoBehaviour
{
//////////////////////////////////////////////////////////////////////////////
//                                                                          //
//Variables                                                                 //
//                                                                          //
//////////////////////////////////////////////////////////////////////////////
static public Flyer_SceneManager get;

public Flyer_Player MyFlyer_Player;

public GameObject[] chunks;

public GameObject[] chunksLeft;
public GameObject[] chunksRight;
public GameObject[] cars;
public GameObject[] buffs;
public GameObject[] debuffs;
public GameObject[] planes;

public AudioClip SpawnSound;

public CurvedWorld_Controller curvedWorld_Controller;

static public float chunkSize = 60;
static public Vector3 moveVector = new Vector3(0, 0, -1);

static public Vector3 sideMoveVector = new Vector3(-1, 0, 0);

static public GameObject lastChunk;
static public GameObject lastChunkRight;
static public GameObject lastChunkLeft;

static public float sideChunkSize= 60.0f;
static public float sideChunkWidth= 195.0f;
static public float BackDistance = 180.0f;

static public float RoadWidth = 15.0f;
static public float speed = 4.0f;

static private float increaseFactor = 1.001f;

static private int currentSpawnRow = 0;

static private string[] levels = new string[16];
static private string[] height = new string[16];
static private string[] sides = new string[16];

static private float spawnSpacingX = 3.40f;
static private float spawnStartX = -15.5f;
static private float spawnSideStartX = 100;
static public float spawnYHeight = 2.5f;
		static public float spawnStartY = -10.0f;

		static private float distanceBetweenSpawns = 1.5f;
		static private float distanceSinceLastSpawn = 0.0f;

		static public float chunkStartY = -8.0f;
		static public float chunkSidesStartY = -12.0f;

//List<Material> listMaterials;

//////////////////////////////////////////////////////////////////////////////
//                                                                          //
//Unity Functions                                                           //
//                                                                          //
//////////////////////////////////////////////////////////////////////////////

void Start(){

			levels [0] =    "1--2-----0";
			levels [1] =    "-----5----";
			levels [2] =    "-2-----3--";
			levels [3] =    "---12-----";
			levels [4] =    "------1-6-";
			levels [5] =    "3---3----1";
			levels [6] =    "-4---!3---";
			levels [7] =    "--0--6----";
			levels [8] =    "-3--5---!-";
			levels [9] =    "--02------";
			levels [10] =   "4-1-1-0---";
			levels [11] =   "--12--a-!-";
			levels [12] =   "1---6--3--";
			levels [13] =   "2-3-2--4-0";
			levels [14] =   "b---6---!-";
			levels [15] =   "-6146-2--4";
			
			height [0] =    "1--2-----0";
			height [1] =    "-----5----";
			height [2] =    "-2-----3--";
			height [3] =    "---12-----";
			height [4] =    "------1-6-";
			height [5] =    "3---3----1";
			height [6] =    "-4---23---";
			height [7] =    "--0--6----";
			height [8] =    "-3--5---2-";
			height [9] =    "--02------";
			height [10] =   "4-1-1-0---";
			height [11] =   "--12--5-2-";
			height [12] =   "1---6--3--";
			height [13] =   "2-3-2--4-0";
			height [14] =   "2---6---6-";
			height [15] =   "-6146-2--4";
			
			sides [0] =    "----------";
			sides [1] =    "----------";
			sides [2] =    "----------";
			sides [3] =    "----------";
			sides [4] =    "----------";
			sides [5] =    "----------";
			sides [6] =    "----------";
			sides [7] =    "----------";
			sides [8] =    "----------";
			sides [9] =    "----------";
			sides [10] =   "----------";
			sides [11] =   "----------";
			sides [12] =   "----------";
			sides [13] =   "----------";
			sides [14] =   "----------";
			sides [15] =   "----------";
			

}

void SpawnSide (int rowNumber)
		{


			var chars = sides [rowNumber].ToCharArray ();

			float offsetX = 0.0f;

			foreach (char character in chars) {

				string tryCharacter = character.ToString ();
				Vector3 spawnLocation = new Vector3 ( spawnSideStartX + offsetX ,1,0   );

				if (tryCharacter != "-") {

					int planeIndex = System.Int32.Parse( tryCharacter );

					GameObject spawnedPlane = Instantiate(  planes[planeIndex] , spawnLocation, Quaternion.identity ) as GameObject;

					Flyer_Plane spawnedFlyer_Plane = spawnedPlane.GetComponent<Flyer_Plane> ();
					spawnedFlyer_Plane.speed = speed;
					
				}

				offsetX = offsetX + spawnSpacingX;

			}

			distanceSinceLastSpawn = 0.0f;
}


void SpawnRow (int rowNumber)
		{
			SpawnSide(rowNumber);

			var chars = levels [rowNumber].ToCharArray ();
			var heightChars = height [rowNumber].ToCharArray ();
			
			float offsetX = 0.0f;

			int counter = 0;
			foreach (char character in chars) {

				string tryCharacter = character.ToString ();
				Vector3 spawnLocation = new Vector3 (   spawnStartX + offsetX , 0, 170 );

				if (tryCharacter != "-") {
					
					spawnLocation.y = spawnStartY + ( System.Int32.Parse( heightChars[counter].ToString()   )  * spawnYHeight );
					
					int i;
					if (!int.TryParse (tryCharacter, out i)) {
					
					
						if (tryCharacter == "a") {
							
							GameObject spawnedBuff = Instantiate(  buffs[0] , spawnLocation, Quaternion.identity ) as GameObject;
							
							Flyer_Buff spawnedFlyer_Buff = spawnedBuff.GetComponent<Flyer_Buff> ();
							spawnedFlyer_Buff.speed = speed; 
						}
						
						if (tryCharacter == "b") {
							
							
							GameObject spawnedBuff = Instantiate(  buffs[1] , spawnLocation, Quaternion.identity ) as GameObject;
							
							Flyer_Buff spawnedFlyer_Buff = spawnedBuff.GetComponent<Flyer_Buff> ();
							spawnedFlyer_Buff.speed = speed;  
						}
						
						if (tryCharacter == "!") {
							
							
							GameObject spawnedDeBuff = Instantiate(  debuffs[0] , spawnLocation, Quaternion.identity ) as GameObject;
							
							Flyer_Debuff spawnedFlyer_Debuff = spawnedDeBuff.GetComponent<Flyer_Debuff> ();
							spawnedFlyer_Debuff.speed = speed;
						}
					
						break;
					
					}


					int carIndex = System.Int32.Parse( tryCharacter );

					GameObject spawnedCar = Instantiate(  cars[carIndex] , spawnLocation, Quaternion.identity ) as GameObject;

					Flyer_Car spawnedFlyer_Car = spawnedCar.GetComponent<Flyer_Car> ();
					spawnedFlyer_Car.speed = speed;
					spawnedFlyer_Car.SetSpawnLocation (  spawnLocation ) ;
					
				}

				offsetX = offsetX + spawnSpacingX;
				counter++;
			}

			distanceSinceLastSpawn = 0.0f;
}

void Update ()
		{

			distanceSinceLastSpawn = distanceSinceLastSpawn + Time.deltaTime;


			if (distanceSinceLastSpawn >= distanceBetweenSpawns) {

				if ( currentSpawnRow >= levels.Length ) {
					currentSpawnRow = 0;
				}
				SpawnRow (currentSpawnRow);
				
				currentSpawnRow++;
				
				
	
			}


Vector3 headPosition = InputTracking.GetLocalPosition (VRNode.Head);

float headY = Mathf.Clamp (   (headPosition.y * 44.0f), -1.0f, Mathf.Infinity);
//float headX = Mathf.Clamp (   (headPosition.x * 30.0f), -1.0f, Mathf.Infinity);

float headX = headPosition.x * 30.0f;

Vector3 worldBend = new Vector3 ( headY, headX, 0   );

curvedWorld_Controller.SetBend (worldBend);


}

void Awake()
{

get = this;
Physics.gravity = new Vector3(0, -50, 0);


//Instantiate chunks
for (int i = 0; i < chunks.Length; i++)
{
GameObject obj = (GameObject)Instantiate(chunks[i]);

				obj.transform.position = new Vector3(0, chunkStartY, i * chunkSize);

lastChunk = obj;
}

//Instantiate chunks left
for (int i = 0; i < chunksLeft.Length; i++)
{
GameObject obj = (GameObject)Instantiate(chunksLeft[i]);

				obj.transform.position = new Vector3(-sideChunkWidth, chunkSidesStartY, i * sideChunkSize);

lastChunkLeft = obj;
}

//Instantiate chunks Right
for (int i = 0; i < chunksRight.Length; i++)
{
GameObject obj = (GameObject)Instantiate(chunksRight[i]);

				obj.transform.position = new Vector3(20.0f, chunkSidesStartY, i * sideChunkSize);

lastChunkRight = obj;
}


}

void CreateSpawnPoints(){

GameObject go = new GameObject ("Empty");

}


//////////////////////////////////////////////////////////////////////////////
//                                                                          //
//Custom Functions                                                          //
//                                                                          //
//////////////////////////////////////////////////////////////////////////////

static public void IncreaseSpeed(){

speed = speed++;

}

public void DestroyChunk(Flyer_Chunk moveElement)
{
Vector3 newPos = lastChunk.transform.position;
newPos.z += chunkSize;

lastChunk = moveElement.gameObject;
lastChunk.transform.position = newPos;


}

public void DestroyChunkLeft(Flyer_ChunkLeft moveElement)
{
Vector3 newPos = lastChunkLeft.transform.position;
newPos.z += sideChunkSize;


lastChunkLeft = moveElement.gameObject;
lastChunkLeft.transform.position = newPos;
}


public void DestroyChunkRight(Flyer_ChunkRight moveElement)
{
Vector3 newPos = lastChunkRight.transform.position;
newPos.z += sideChunkSize;

lastChunkRight = moveElement.gameObject;
lastChunkRight.transform.position = newPos;
}

public void DestroyPlane (Flyer_Plane plane, bool scoreCounts)
		{
			GameObject.Destroy (plane.gameObject);

		}

public void DestroyCar(Flyer_Car flyer, bool scoreCounts)
{
GameObject.Destroy(flyer.gameObject);


if (scoreCounts) {
MyFlyer_Player.Score ();
}

}

public void DestroyBuff(Flyer_Buff buff)
{
GameObject.Destroy(buff.gameObject);


}

public void DestroyDebuff(Flyer_Debuff debuff)
{
GameObject.Destroy(debuff.gameObject);



}


}
}