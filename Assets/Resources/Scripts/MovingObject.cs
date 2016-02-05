using UnityEngine;
using System.Collections;

public abstract class MovingObject : MonoBehaviour {


	//add functions here that would be useful for numerous things

	private BoxCollider2D boxCollider;      //The BoxCollider2D component attached to this object.
	private Rigidbody2D rb2D;               //The Rigidbody2D component attached to this object.

	// Use this for initialization
	protected virtual void Start () {
	
		//Get a component reference to this object's BoxCollider2D
		boxCollider = GetComponent <BoxCollider2D> ();

		//Get a component reference to this object's Rigidbody2D
		rb2D = GetComponent <Rigidbody2D> ();
	}
	

}
