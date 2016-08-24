using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	Rigidbody2D EnemyRd;
    Animator animator;
        
	public GameObject target;
	public int atk = 1;

    // base layerで使われる、アニメーターの現在の状態の参照
    private AnimatorStateInfo currentBaseState;
    private SpriteRenderer spriteRenderer;

	float TargetX;
	float EnemyX;
	float distanceCheck;
	float moveSpeed = 1.0f;
	const float HIT_DISTANCE = 1.0f;
    const float FIND_DISTANCE = 5.0f;
    float hitCount = 10.0f;
	float timeCount = 0.0f;

    static int attack = Animator.StringToHash ( "Base Layer.Attack" );
    static int damage = Animator.StringToHash ( "Base Layer.Damage" );
    static int dead = Animator.StringToHash( "Base Layer.Dead" );


	void Start() {
		animator = GetComponent<Animator> ();
		target = GameObject.Find ("Cguy");
		EnemyRd = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void Update() {

        currentBaseState = animator.GetCurrentAnimatorStateInfo (0);
		TargetX = target.transform.position.x;
		EnemyX = transform.position.x;
		distanceCheck = EnemyX - TargetX;

        if ( currentBaseState.fullPathHash != dead ) {
            if ( distanceCheck > 0.0f ) {
                spriteRenderer.flipX = true ;
            } else { 
                spriteRenderer.flipX = false ;
            }

            if ( ( distanceCheck > 0 ? distanceCheck : -distanceCheck ) > HIT_DISTANCE && 
                 ( distanceCheck > 0 ? distanceCheck : -distanceCheck ) <= FIND_DISTANCE && 
                   currentBaseState.fullPathHash != attack ) {

                EnemyRd.transform.position += new Vector3( ( spriteRenderer.flipX == true ?  -moveSpeed : moveSpeed ) * Time.deltaTime, 0 );
                animator.SetBool( "Run", true );

            } else if ( ( distanceCheck > 0 ? distanceCheck : -distanceCheck ) <= HIT_DISTANCE ) {
                animator.SetTrigger( "Attack" );
                animator.SetBool( "Run", false );
            } else {
                animator.SetBool( "Run", false );
            }
        }

		if ( currentBaseState.fullPathHash == dead ) {
			timeCount++;
		}
		if ( timeCount == 60 ) {
			Destroy ( gameObject );
		}
	}

	public void Damage() {
		animator.SetTrigger ( "Damage" );
		EnemyRd.velocity = new Vector2 (3.0f, 2.0f);
	}

	void AttackDecision() {
		target.GetComponent<PlayerController> ().Damage (atk);
	}
}