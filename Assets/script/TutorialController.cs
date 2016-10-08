using UnityEngine;
using System.Collections;

public class TutorialController : MonoBehaviour {

	public GameObject[] tutorialImage;
	private int i;
	private float count;

	// Use this for initialization
	void Start () {
		i = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (i == 4 && Input.GetMouseButtonDown (0) ) {
			Application.LoadLevel ("Battle");
		}
		
		if (Input.GetMouseButtonDown (0) && i < 4 ) {
			tutorialImage [i].SetActive (true);
			i += 1;
		}
			

	}
}
