using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveController : MonoBehaviour {

    void Awake() {

    }

	void Start() {
		Handheld.PlayFullScreenMovie( "credit03.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);
	}

    void Update() {
		SceneManager.LoadScene( "TitleMenu" );
    }
}
