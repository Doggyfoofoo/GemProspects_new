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
	private int boardSize = 20;  // 20x20 board
	private int minDeposits = 20; 
	private int maxDeposits = 40; 

	private List<Vector2> emptyGridPositions = new List <Vector2> (); 

	private GameObject[,] boardTiles; 
	private Dictionary<Vector2, MineralDeposit> mineralDepositsOnBoard = new Dictionary<Vector2, MineralDeposit>(); 

	enum Gems {emerald, ruby, sapphire, amethyst, citrine, opal, topaz, morganite}; 

	public class MineralDeposit
	{
		public int GemType = -1;
		bool hasMine = false; 
		bool isOperational = false; //being mined by miners

		public MineralDeposit(){
			GemType = (int) Random.Range(0, System.Enum.GetNames(typeof(Gems)).Length);
		}

		public void BuildMine(){
			hasMine = true; 
		}

		public bool OpenMine(){
			if (hasMine == false) {
				return false; 
			} else {
				isOperational = true; 
				return true;
			}
		}

		public bool CloseMine(){
			if (isOperational == false) {
				return false; 
			} else {
				isOperational = true; 
				return true; 
			}
		}
	}
		
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

		emptyGridPositions.Clear (); 
		for (int i = 0; i< boardSize; i++) {

			for (int j = 0; j< boardSize; j++) {

				//floortiles are populated through the heirarchy 
				GameObject toInstantiate = floorTiles[Random.Range (0,floorTiles.Length)];

				GameObject instance =
					Instantiate (toInstantiate, new Vector3 (i, j, 0f), Quaternion.identity) as GameObject;

				boardTiles [i,j] = instance; 
				instance.transform.SetParent (boardHolder);

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
			Instantiate (deposit, randomPosition, Quaternion.identity); 

			MineralDeposit mineralDeposit = new MineralDeposit (); 
			mineralDepositsOnBoard.Add (randomPosition, mineralDeposit); 
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
