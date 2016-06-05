using UnityEngine;
using System.Collections;

//!  Classa dla laserów 
/*!
  movement and destroying lasers
*/
public class LaserScript : MonoBehaviour {

	///Czas istnienia laseru
	public float lifetime = 2.5f;
	
	///predkosc
	public float speed = 5.5f;
	
	///ilosc demage przy kolizji z przeciwnikami
	public int damage = 1;

	/// Use this for initialization
	void Start () {
		/*
		\zniszczenie zaseru po uplynieciu czasu timeru
		*/
		Destroy (gameObject, lifetime);
	}
	
	/// Update is called once per frame
	void Update () {
		/*
		\przemieśczenie lasera
		*/
		transform.Translate(Vector3.up * Time.deltaTime * speed);	
	}
}
