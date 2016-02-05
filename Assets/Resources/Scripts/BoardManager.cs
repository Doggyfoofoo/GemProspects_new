using UnityEngine;
using System.Collections;
using System.Collections.Generic;     
using Random = UnityEngine.Random; 

public class BoardManager : MonoBehaviour {

	public static BoardManager instance = null;   

	private Transform boardHolder;      //A variable to store a reference to the transform of our Board object.
										// use  instance.transform.SetParent (boardHolder); to put all board objects under the BoardManager

	public GameObject[] floorTiles;

	private int tileOffset; 
	private int boardSize = 20; 

	private GameObject[,] boardTiles; 


	public int  GetBoardSize(){
		return boardSize;
	}

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

		boardTiles = new GameObject[boardSize, boardSize];

		SetUpBoard ();
	}


	private void SetUpBoard(){

		for (int i = 0; i< boardSize; i++) {

			for (int j = 0; j< boardSize; j++) {

				//floortiles are populated through the heirarchy 
				GameObject toInstantiate = floorTiles[Random.Range (0,floorTiles.Length)];

				GameObject instance =
					Instantiate (toInstantiate, new Vector3 (i, j, 0f), Quaternion.identity) as GameObject;

				boardTiles [i,j] = instance; 
				instance.transform.SetParent (boardHolder);

			}
		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
