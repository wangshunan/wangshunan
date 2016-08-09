using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{

	Animator anime;

	void Awake ()
	{
		anime = GetComponent<Animator> ();
	}

	void Update ()
	{

            
	}

    public void Damage() {

		anime.SetTrigger ("Damage");

		SceneManager.LoadScene ("Result");

    }

}
