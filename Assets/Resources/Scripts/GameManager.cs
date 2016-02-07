using UnityEngine;
using System.Collections;
using System.Collections.Generic;     

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
	private GameObject mainCamera; 
	private GameObject mainPlayer; 

	void Awake()
	{
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
	}


	//initialize things after the awake functions are all called
	void Start () {
		//camera 
		mainCamera = GameObject.Find("Main Camera"); 
		int boardCenter = (BoardManager.instance.GetBoardSize ())/2;
		mainCamera.transform.position = new Vector3 (boardCenter, boardCenter, -10);

		mainPlayer = GameObject.Find ("MainPlayer");
	}
	
	// Update is called once per frame
	void Update () {
	
//		mainCamera.transform.position = new Vector3 (mainPlayer.transform.position.x, mainPlayer.transform.position.y, -10); 
	}
}
