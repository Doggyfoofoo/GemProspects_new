using UnityEngine;
using System.Collections;

public class Player : MovingObject {

	private Animator animator; 

	//MovingObject has references boxCollider and rb2D

	// Use this for initialization
	protected override void Start () {
		animator = GetComponent<Animator>();
		base.Start (); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
