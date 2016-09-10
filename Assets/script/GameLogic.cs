using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {
	
    public Slider bossHpSlider;


	void Start() {

	}
	
	// Update is called once per frame
	void Update () {
		if ( bossHpSlider.value <= 0.0f ) {
			Application.LoadLevel ("Result");
		}
	}

	public void SceneManager( ) {
		
	}
}
