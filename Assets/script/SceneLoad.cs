using UnityEngine;
using System.Collections;

public class SceneLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetMouseButtonDown (0)){
			Application.LoadLevel ("TitleMenu");
		}
        Invoke("ReturnToTitle", 4.0f );
	}
    void ReturnToTitle() {

        Application.LoadLevel ("TitleMenu");

    }
}
