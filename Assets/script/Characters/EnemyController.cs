using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	[SerializeField]
	GameLogic gameLogic;

    [SerializeField]
    EnemyFactory enemyFactory;

	Rigidbody2D EnemyRd;
    Animator animator;
	public float atk;


    // base layerで使われる、アニメーターの現在の状態の参照
    private AnimatorStateInfo currentBaseState;
    private SpriteRenderer spriteRenderer;
    private GameObject target;

	private float TargetX;
	private float EnemyX;
	private float distanceCheck;
	float moveSpeed = 1.0f;
	private const float HIT_DISTANCE = 2f;
    private  const float FIND_DISTANCE = 10.0f;
	private const int SECOND = 60;
	private const float PASSIVE_X = 3.0f;
	private const float PASSIVE_Y = 2.0f;
	private float timeCount = 0.0f;
    private float eventEnemyCount;

	bool isGround = false;

    static int attack = Animator.StringToHash ( "Base Layer.Attack" );
    static int damage = Animator.StringToHash ( "Base Layer.Damage" );
    static int dead = Animator.StringToHash( "Base Layer.Dead" );


	void Start() {
        enemyFactory= GameObject.Find ("EnemyApperController").GetComponent<EnemyFactory> ();
		gameLogic = GameObject.Find ("GameLogic").GetComponent<GameLogic> ();
		animator = GetComponent<Animator> ();
		target = GameObject.Find ("Cguy");
		EnemyRd = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void Update () {
		if (gameLogic.gameStatus != GameLogic.GAME_STATUS.Start) {
			return;
		}
		ActionController ();

        /*if ( gameObject.layer == 14 ) {
            if ( target.transform.position.x - gameObject.transform.position.x > 25 ) {
                Destroy( gameObject );
            }
        }*/
	}

	private void ActionController() {
		currentBaseState = animator.GetCurrentAnimatorStateInfo (0);
		TargetX = target.transform.position.x;
		EnemyX = transform.position.x;
		distanceCheck = EnemyX - TargetX;
		if ( isGround ) {
			if (currentBaseState.fullPathHash != dead && currentBaseState.fullPathHash != damage) {
				if (distanceCheck > 0) {
					spriteRenderer.flipX = true;
				} else { 
					spriteRenderer.flipX = false;
				}
				float distanceCheckY = target.transform.position.y - transform.position.y;
				if ( Mathf.Abs( distanceCheckY ) <= 0.8f) {
					if (Mathf.Abs (distanceCheck) > HIT_DISTANCE &&
					   Mathf.Abs (distanceCheck) <= FIND_DISTANCE &&
					   currentBaseState.fullPathHash != attack) {

						EnemyRd.transform.position += new Vector3 ((spriteRenderer.flipX == true ? -moveSpeed : moveSpeed) * Time.deltaTime, 0);
						animator.SetBool ("Run", true);

					} else if (Mathf.Abs (distanceCheck) <= HIT_DISTANCE) {
						
						animator.SetTrigger ("Attack");
						animator.SetBool ("Run", false);
					} else {
						animator.SetBool ("Run", false);
					}
				}
			}
		}

		if (currentBaseState.fullPathHash == dead) {
			timeCount++;
		}
		if (timeCount == SECOND) {
            if( gameObject.layer == 14 ) {
                EventEnemyCount( );
            }
			Destroy (gameObject);
		}
	}

	public void Damage() {
        if ( spriteRenderer.flipX == target.GetComponent<SpriteRenderer>().flipX ) {
            spriteRenderer.flipX = true ? false : true;
        }
		animator.SetTrigger ( "Damage" );
        if( currentBaseState.fullPathHash != damage && currentBaseState.fullPathHash != dead ) {
			EnemyRd.velocity = new Vector2 ( distanceCheck > 0 ? PASSIVE_X : -PASSIVE_X, PASSIVE_Y);
        }

	}

	void AttackDecision() {
        distanceCheck = target.transform.position.x - transform.position.x;
		float distanceCheckY = target.transform.position.y - transform.position.y;
		if ( Mathf.Abs( distanceCheck ) <= HIT_DISTANCE &&  Mathf.Abs( distanceCheckY ) <= 0.8f ) {
             if (  ( spriteRenderer.flipX == false && distanceCheck >= 0 ) ||
                   ( spriteRenderer.flipX == true && distanceCheck <= 0 ) ) {
		        target.GetComponent<PlayerController> ().Damage (atk);
            }
        }
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ground") {
			isGround = true;
		}
	}

    void EventEnemyCount( ) {
        enemyFactory.count++;
    }
}