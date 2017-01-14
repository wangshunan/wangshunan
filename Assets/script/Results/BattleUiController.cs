using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleUiController : MonoBehaviour {


    private GameObject playerBloodPressure;
    private GameObject bossHp;
    private GameObject playerStamina;

    public GameObject retryBtn;
    public GameObject titleBtn;



	// Use this for initialization
	void Start () {
        playerBloodPressure = GameObject.Find( "BloodPressure" );
        bossHp = GameObject.Find( "BossHp" );
        playerStamina = GameObject.Find( "Stamina" );
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OverUIController() {

    }

    void ClearUIController() {

    }

    void BossUIController() {

    }

    void PlayerControllerUI( bool touchCtr ) {
        
    }


    public void OnRetryBtnClick() {
        //SceneManager.LoadScene ("Battle");
    }

    public void OnOverBtnClick() {
        //SceneManager.LoadScene ("Title");
    }

    public void OnMenuBtnClick() {

    }
}
