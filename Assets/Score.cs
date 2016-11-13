using UnityEngine;
using System.Collections;

namespace VacuumShaders
{
	
public class Score : MonoBehaviour {


	public Runner_Player MyRunner_Player;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

		void OnCollisionEnter(Collision collision)
		{
			Vector3 force = (Vector3.forward + Vector3.up + Random.insideUnitSphere).normalized * Random.Range (100, 150);
			collision.rigidbody.AddForce (force, ForceMode.Impulse);

			Runner_Car car = collision.gameObject.GetComponent<Runner_Car> ();
			Runner_Debuff debuff = collision.gameObject.GetComponent<Runner_Debuff> ();
			Runner_Buff buff = collision.gameObject.GetComponent<Runner_Buff> ();

			if (debuff != null) {


				Runner_SceneManager.get.DestroyDebuff(debuff);
			}

			if (buff != null) {


				Runner_SceneManager.get.DestroyBuff(buff);
			}

			if (car != null)
			{
				MyRunner_Player.Score ();
				Runner_SceneManager.get.DestroyCar(car);
			}


		}


}

}