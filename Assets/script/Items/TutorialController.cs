using UnityEngine;
using System.Collections;

public class TutorialController : MonoBehaviour {

	public GameObject[] tutorialImage;
	private int i;
	private float count;

	// Use this for initialization
	void Start () {
		i = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (i == 5 && Input.GetMouseButtonDown (0) ) {
			Application.LoadLevel ("Battle");
		}
		
		if (Input.GetMouseButtonDown (0) && i < 5 ) {	
			if (i > 0) {
				tutorialImage [i - 1].SetActive (false);
			}
			tutorialImage [i].SetActive (true);
			i += 1;
		}
			

	}
}
