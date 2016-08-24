using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	[SerializeField]
	PlayerController playerController;

	Rigidbody2D EnemyRd;
    Animator animator;
        
	public GameObject target;

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

    static int attack = Animator.StringToHash ( "Base Layer.Attack" );
    static int damage = Animator.StringToHash ( "Base Layer.Damage" );


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

        if ( currentBaseState.fullPathHash != damage ) {
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
            } else {
                animator.SetBool( "Run", false );
            }
        }

        if ( Input.GetKeyDown( "x" ) ) {
            animator.SetTrigger( "Damage" );
            EnemyRd.velocity = new Vector2( 3.0f , 2.0f );
        }









		/*if (TargetX > EnemyX) {
			spriteRenderer.flipX = false;
		} else {
			spriteRenderer.flipX = true;

		}*/

		/*if (DistanceCheck > DISTANCE || DistanceCheck < -DISTANCE) {
			EnemyRd.MovePosition (Vector2.Lerp (EnemyMove, target.transform.position, Time.deltaTime));
			animator.SetBool ("Run", true);
		} else {
			animator.SetBool( "Run", false );
			animator.SetTrigger ("Attack");
		}*/

	}
}

