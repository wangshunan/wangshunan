using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResultController : MonoBehaviour {

	public void OnRetryButtonClicked() {
		SceneManager.LoadScene ("Battle");
	}

	public void OnOverButtonClicked() {
		SceneManager.LoadScene ("Title");
	}
}
