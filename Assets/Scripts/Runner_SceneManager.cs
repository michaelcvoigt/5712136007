using UnityEngine;
using System.Linq;
using System.Collections.Generic;
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

public Runner_Player MyRunner_Player;

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

static public float sideChunkSize= 60.0f;
static public float sideChunkWidth= 195.0f;
static public float BackDistance = 180.0f;

static public float RoadWidth = 15.0f;
static public float speed;

static private float increaseFactor = 1.001f;

static private int currentSpawnRow = 0;
static private string[] levels = new string[8];
static private float spawnSpacingX = 3.40f;
static private float spawnStartX = -15.5f;
		static private float distanceBetweenSpawns = 0.5f;
		static private float distanceSinceLastSpawn = 0.0f;


//List<Material> listMaterials;

//////////////////////////////////////////////////////////////////////////////
//                                                                          //
//Unity Functions                                                           //
//                                                                          //
//////////////////////////////////////////////////////////////////////////////

void Start(){

			levels [0] = "1--2-----0";
			levels [1] = "-----5----";
			levels [2] = "-2-----3--";
			levels [3] = "---a------";
			levels [4] = "------1-6-";
			levels [5] = "3-b-3----1";
			levels [6] = "-----!3---";
			levels [7] = "--12----!-";
			//Instantiate cars
/*for (int i = 0; i < cars.Length; i++)
{

SpawnRow (0);

//GameObject spawnedCar = Instantiate(  cars[i] , GetRandomSpawnVector() , Quaternion.identity ) as GameObject;

//Runner_Car spawnedRunner_Car = spawnedCar.GetComponent<Runner_Car> ();
//spawnedRunner_Car.speed = Random.Range(2f, 6f);


}

Renderer[] renderers = FindObjectsOfType(typeof(Renderer)) as Renderer[];

listMaterials = new List<Material>();
foreach (Renderer _renderer in renderers)
{
listMaterials.AddRange(_renderer.sharedMaterials);
}

listMaterials = listMaterials.Distinct().ToList();

*/

}

void SpawnRow (int rowNumber)
		{


			var chars = levels [rowNumber].ToCharArray ();

			float offsetX = 0.0f;

			foreach (char character in chars) {

				string tryCharacter = character.ToString ();
				Vector3 spawnLocation = new Vector3 (   spawnStartX + offsetX , 0, 170 );

				if (tryCharacter != "-") {

					int i;
					if (!int.TryParse (tryCharacter, out i)) {
					

						if (tryCharacter == "a") {
							
							
							GameObject spawnedBuff = Instantiate(  buffs[0] , spawnLocation, Quaternion.identity ) as GameObject;
							
							Runner_Buff spawnedRunner_Buff = spawnedBuff.GetComponent<Runner_Buff> ();
							spawnedRunner_Buff.speed = 2.0f;  //Random.Range(2f, 2f);
						}
						
						if (tryCharacter == "b") {
							
							
							GameObject spawnedBuff = Instantiate(  buffs[1] , spawnLocation, Quaternion.identity ) as GameObject;
							
							Runner_Buff spawnedRunner_Buff = spawnedBuff.GetComponent<Runner_Buff> ();
							spawnedRunner_Buff.speed = 2.0f;  //Random.Range(2f, 2f);
						}
						
						if (tryCharacter == "!") {
							
							
							GameObject spawnedDeBuff = Instantiate(  debuffs[0] , spawnLocation, Quaternion.identity ) as GameObject;
							
							Runner_Debuff spawnedRunner_Debuff = spawnedDeBuff.GetComponent<Runner_Debuff> ();
							spawnedRunner_Debuff.speed = 2.0f;  //Random.Range(2f, 2f);
						}
					
						break;
					
					}


					int carIndex = System.Int32.Parse( tryCharacter );

					GameObject spawnedCar = Instantiate(  cars[carIndex] , spawnLocation, Quaternion.identity ) as GameObject;

					Runner_Car spawnedRunner_Car = spawnedCar.GetComponent<Runner_Car> ();
					spawnedRunner_Car.speed = 2.0f;  //Random.Range(2f, 2f);
					
					
					
					
					
					
				}

				offsetX = offsetX + spawnSpacingX;

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
speed = 8.0f;
get = this;
Physics.gravity = new Vector3(0, -50, 0);


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

obj.transform.position = new Vector3(-sideChunkWidth, -5, i * sideChunkSize);

lastChunkLeft = obj;
}

//Instantiate chunks Right
for (int i = 0; i < chunksRight.Length; i++)
{
GameObject obj = (GameObject)Instantiate(chunksRight[i]);

obj.transform.position = new Vector3(20.0f, -5, i * sideChunkSize);

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
newPos.z += sideChunkSize;


lastChunkLeft = moveElement.gameObject;
lastChunkLeft.transform.position = newPos;
}


public void DestroyChunkRight(Runner_ChunkRight moveElement)
{
Vector3 newPos = lastChunkRight.transform.position;
newPos.z += sideChunkSize;

lastChunkRight = moveElement.gameObject;
lastChunkRight.transform.position = newPos;
}

public void DestroyCar(Runner_Car car, bool scoreCounts)
{
GameObject.Destroy(car.gameObject);


if (scoreCounts) {
MyRunner_Player.Score ();
}

}

public void DestroyBuff(Runner_Buff buff)
{
GameObject.Destroy(buff.gameObject);


}

public void DestroyDebuff(Runner_Debuff debuff)
{
GameObject.Destroy(debuff.gameObject);



}


}
}