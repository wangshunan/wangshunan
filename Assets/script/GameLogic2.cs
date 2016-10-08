using UnityEngine;
using System.Collections;

public class GameLogic2 : MonoBehaviour {
    private GameObject target;
	// Use this for initialization
	void Start () {
	    target = GameObject.Find ("Cguy");
	}
	
	// Update is called once per frame
	void Update () {
        if ( target.transform.position.y <= -4.0f ) {
	        Application.LoadLevel ("Battle");
        }
	}
}
