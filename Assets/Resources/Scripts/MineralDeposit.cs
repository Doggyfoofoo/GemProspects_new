using UnityEngine;
using System.Collections;
using System.Collections.Generic;  




public class MineralDeposit : MonoBehaviour
{

	public int gemType = -1;
	Player owner = null;
	bool hasMine = false;
	public bool isOperational = false;
	//being mined by miners

	List<Gem> gemBag = new List<Gem> (); 

	public void SetOwner (Player newOwner)
	{
		owner = newOwner; 
	}

	public Player GetOwner ()
	{
		return owner; 
	}

	public void BuildMine ()
	{
		hasMine = true; 
	}

	public bool OpenMine ()
	{
		if (hasMine == false) {
			return false; 
		} else {
			isOperational = true; 

			return true;
		}
	}

	public bool CloseMine ()
	{
		if (isOperational == false) {
			return false; 
		} else {
			isOperational = true; 
			return true; 
		}
	}



	// Use this for initialization
	void Start ()
	{
		gemType = (int)Random.Range (0, System.Enum.GetNames (typeof(GemTypes)).Length);
		StartCoroutine (CreateGems()); 
	}
	
	// Update is called once per frame
	void Update ()
	{
		

	}




	IEnumerator CreateGems(){
	
		while (isOperational && (gemType != -1)) {

			Gem newGem = ScriptableObject.CreateInstance <Gem> ().Init(gemType); 
			gemBag.Add (newGem); 

			Debug.Log (System.Enum.GetName (typeof(GemTypes), gemType)); 
			yield return new WaitForSeconds(3);
		}	
	
	}

 }
