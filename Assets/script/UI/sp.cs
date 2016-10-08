using UnityEngine;
using System.Collections;

public class sp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetMouseButtonDown (0)){
			Application.LoadLevel ("Main");
		}
      //  Invoke("TitleMenu", 4.0f );

	}
}
