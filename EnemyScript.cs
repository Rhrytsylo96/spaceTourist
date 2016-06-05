using UnityEngine;
using System.Collections;
//!  Classa dla wrogów
/*!
klasa do sprawdzenia kolizji wrogów
*/
public class EnemyScript : MonoBehaviour {

	/// ilosc punktów życiowych przeciwnika(kazdego)
	public int health = 2;

	///Animacja przy znisczeniu
	public Transform explosion;

	///dzwiek przy kolizji lasera a przeciwnika
	public AudioClip hitSound;
    ///przy kolizji 
	void OnCollisionEnter2D(Collision2D theCollision)
	{
		/*!
		\param theCollision typu Collision2D
		*/
		///sprawdzamy kolizje z objektami typu laser
		if(theCollision.gameObject.name.Contains("laser"))
		{
			LaserScript laser = theCollision.gameObject.GetComponent("LaserScript") as LaserScript;
			health -= laser.damage;
			//znisc przeciwnika
			Destroy (theCollision.gameObject);
			///play audio sound
			GetComponent<AudioSource>().PlayOneShot(hitSound);
		}
		if (health <= 0)
		{
			///pracuje dopiero po zniszczeniu
			if(explosion);
			{
				GameObject exploder = ((Transform)Instantiate(explosion, this.transform.position, this.transform.rotation)).gameObject;
				Destroy(exploder, 2.0f);
			}
			Destroy (this.gameObject);
			GameController controller = GameObject.FindGameObjectWithTag("GameController").GetComponent("GameController") as GameController;
			controller.KilledEnemy();
			controller.IncreaseScore(10);

		}
	}
}
