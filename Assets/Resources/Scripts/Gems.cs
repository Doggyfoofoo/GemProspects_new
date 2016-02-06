using UnityEngine;
using System.Collections;

public class Gems : MonoBehaviour
{

	private float gemSize = 0;
	// in Carats
	private float gemValue = 0;


	public float GetGemSize ()
	{
		return (float)System.Math.Round (gemSize, 2); 
	}

	public float GetGemValue ()
	{
		return (float)System.Math.Round (gemValue, 2);
	}

	// Use this for initialization
	void Start ()
	{
		gemSize = 1 / Random.Range (.1f, 20f);
		gemValue = (float)(gemSize * 1000 + System.Math.Pow (4, gemSize)); 
	}
	
	// Update is called once per frame
	//void Update () {
	//
	//}
}
