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
	private static int boardSize = 20;  // 20x20 board
	private int minDeposits = 20; 
	private int maxDeposits = 40; 

	private List<Vector2> emptyGridPositions = new List <Vector2> (); 

	private GameObject[,] boardTiles = new GameObject[boardSize, boardSize];
	private GameObject[,] fogTiles = new GameObject[boardSize, boardSize];

	private Dictionary<Vector2, GameObject> mineralDepositsOnBoard = new Dictionary<Vector2, GameObject>(); 


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

		SetUpBoard ();
	}

	public void DispelFog(Transform playerPosition){

		for (int i = -1; i < 2; i++) {
			for (int j = -1; j < 2; j++) {

				int xPos = i + (int) playerPosition.position.x; 
				int yPos = j + (int) playerPosition.position.y;

				if ((fogTiles[xPos, yPos] != null)) {
					Destroy (fogTiles [xPos, yPos]); 
					fogTiles [xPos, yPos] = null; 
				}
			}
		}
	}


	private void SetUpBoard(){

		emptyGridPositions.Clear (); 
		for (int i = 0; i< boardSize; i++) {

			for (int j = 0; j< boardSize; j++) {

				//floortiles are populated through the heirarchy 
				GameObject floorTile = floorTiles[Random.Range (0,floorTiles.Length)];

				GameObject instance =
					Instantiate (floorTile, new Vector3 (i, j, 0f), Quaternion.identity) as GameObject;

				boardTiles [i,j] = instance; 
				instance.transform.SetParent (boardHolder);

				GameObject fogTile = (GameObject) Resources.Load("Prefabs/black_fog"); 
				GameObject fogInstance = Instantiate (fogTile, new Vector3 (i, j, 0f), Quaternion.identity) as GameObject;
				fogTiles [i, j] = fogInstance;
				fogInstance.transform.SetParent (boardHolder); 

				emptyGridPositions.Add (new Vector2 (i,j));

			}
		}

		//create mineral deposits
		LayoutMineralDepositsOnBoard(minDeposits, maxDeposits); 
	}

	Vector2 RandomBoardPosition(){

		int randomIndex = Random.Range (0, emptyGridPositions.Count); 
		Vector2 randomPosition = emptyGridPositions [randomIndex];
		emptyGridPositions.RemoveAt(randomIndex);
		return randomPosition; 
	}

	void LayoutMineralDepositsOnBoard(int minimum, int maximum){

		int numDeposits = Random.Range (minimum, maximum + 1);
		Debug.Log (numDeposits);
		GameObject deposit = (GameObject) Resources.Load ("Prefabs/mineral_deposit1");

		for (int i = 0; i < numDeposits; i++) {
			Vector2 randomPosition = RandomBoardPosition (); 
			GameObject instance = (GameObject) Instantiate (deposit, randomPosition, Quaternion.identity); 

			instance.transform.SetParent (boardHolder); 
			mineralDepositsOnBoard.Add (randomPosition, instance);

		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
