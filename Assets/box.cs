using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour {


	public int sd;
	// Use this for initialization
	void Start () {
		sd = PlayerPrefs.GetInt ("capsulesCount");
		Debug.Log (sd);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
