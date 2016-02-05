using UnityEngine;
using System.Collections;
using System.Collections.Generic;     
using Random = UnityEngine.Random; 

public class BoardManager : MonoBehaviour {

	public static BoardManager instance = null;   

	private Transform boardHolder;      //A variable to store a reference to the transform of our Board object.
										// use  instance.transform.SetParent (boardHolder); to put all board objects under the BoardManager

	// Use this for initialization
	void Awake () {
		
		//Check if instance already exists
		if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);    

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);

		boardHolder = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
