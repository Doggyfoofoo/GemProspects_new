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

	enum Gems {emerald, ruby, sapphire, amethyst, citrine, opal, topaz, morganite}; 

	public class MineralDeposit
	{
		int GemType = -1;
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

	public class Gem
	{
		private float gemSize = 0; // in Carats
		private float gemValue = 0; 

		public Gem(){
			gemSize = 1/Random.Range(.1f, 20f);
			gemValue = (float) (gemSize * 1000 + System.Math.Pow(4,gemSize)); 
		}

		public float GetGemSize(){
			return (float) System.Math.Round (gemSize, 2); 
		}

		public float GetGemValue(){
			return (float) System.Math.Round (gemValue, 2);
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
