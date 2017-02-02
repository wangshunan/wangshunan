using UnityEngine;
using System.Collections;

public class EnemyFactory : MonoBehaviour {

	[SerializeField]
	GameLogic gamelogic;

	GameObject enemyApperPoint;
	GameObject enemyApperPoint2;
    GameObject[] enemys;
    public GameObject capsules;
	public GameObject enemy;
    
	private const float ENEMY_MAX = 20;
	private bool apperController = true;
	private bool enemyApper;
	private float eventPoint;
	private float timeCount = 0;
	private float enemyCount = 0;
    public int count;
	private GameObject target;

	void Awake() {
		gamelogic = GameObject.Find ("GameLogic").GetComponent<GameLogic> ();
		enemyApperPoint = GameObject.Find ("EnemyApperPoint");
		enemyApperPoint2 = GameObject.Find ("EnemyApperPoint2");
		target = GameObject.Find ("Cguy");
		eventPoint = ( enemyApperPoint2.transform.position.x - enemyApperPoint.transform.position.x ) / 2;
	}
	
	// Update is called once per frame
	void Update () {
        if (gamelogic.gameStatus != GameLogic.GAME_STATUS.Start) {
			return;
		}

        if ( enemyCount >= ENEMY_MAX ) {
            enemyApper = false;
        }
		

		if( enemyApper ) {
			timeCount += Time.deltaTime;
			switch ((int)timeCount % 2) {
			case 0:
				if (apperController) {
					Instantiate (enemy, enemyApperPoint.transform.position, Quaternion.identity);
                    enemy.gameObject.layer = 14;
					apperController = false;
					enemyCount++;
				}
				break;
			case 1:
				if (!apperController) {
					Instantiate (enemy, enemyApperPoint2.transform.position, Quaternion.identity);
                    enemy.gameObject.layer = 14;
					apperController = true;
					enemyCount++;
				}
				break;
			}			
		}

        if ( count == ENEMY_MAX ) {
            Instantiate (capsules, transform.position, Quaternion.identity);
            count = 0;
        }

	}

    void OnTriggerEnter2D(Collider2D coll) {

        if ( coll.gameObject.tag == "Player" ) {
            enemyApper = true;
        }

        Destroy( GetComponent<BoxCollider2D>( ) );
    }
}
