using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour {
	
	GameObject[] enemy;

	void Start() {

	}
	
	// Update is called once per frame
	void Update () {
		enemy = GameObject.FindGameObjectsWithTag("Enemy");
		if (enemy.Length == 0) {
			//Application.LoadLevel ("Result");
		}
	}

	public void SceneManager( ) {
		
	}
}
