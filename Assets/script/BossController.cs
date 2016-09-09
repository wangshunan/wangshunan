using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class BossController : MonoBehaviour {

	Rigidbody2D EnemyRd;
    Animator animator;
        
	
	public float atk = 1;
    public Slider hpSlider;

    // base layerで使われる、アニメーターの現在の状態の参照
    private AnimatorStateInfo currentBaseState;
    private SpriteRenderer spriteRenderer;
    private GameObject target;

	float TargetX;
	float EnemyX;
	private float distanceCheck;
	float moveSpeed = 1.0f;
	const float HIT_DISTANCE = 1.5f;
    const float FIND_DISTANCE = 5.0f;
    float hitCount = 10.0f;
	float timeCount = 0.0f;


    static int attack = Animator.StringToHash ( "Base Layer.Attack" );
    static int attack2 = Animator.StringToHash ( "Base Layer.Attack2" );
    static int damage = Animator.StringToHash ( "Base Layer.Damage" );
    static int jump = Animator.StringToHash ( "Base Layer.Jump" );
    //static int dead = Animator.StringToHash( "Base Layer.Dead" );


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

        if ( /*currentBaseState.fullPathHash != dead &&*/ currentBaseState.fullPathHash != damage ) {
            if ( distanceCheck > 0.0f ) {
                spriteRenderer.flipX = true ;
            } else { 
                spriteRenderer.flipX = false ;
            }

            if ( ( distanceCheck > 0 ? distanceCheck : -distanceCheck ) > HIT_DISTANCE && 
                 ( distanceCheck > 0 ? distanceCheck : -distanceCheck ) <= FIND_DISTANCE && 
                   currentBaseState.fullPathHash != attack2 ) {

                EnemyRd.transform.position += new Vector3( ( spriteRenderer.flipX == true ?  -moveSpeed : moveSpeed ) * Time.deltaTime, 0 );
                animator.SetBool( "Walk", true );

            } else if ( ( distanceCheck > 0 ? distanceCheck : -distanceCheck ) <= HIT_DISTANCE ) {
                animator.SetTrigger( "Attack2" );
                animator.SetBool( "Walk", false );
            } else {
                animator.SetBool( "Walk", false );
            }
        }

        if( Input.GetKeyDown( "b" ) ) {
            animator.SetTrigger( "Jump" );
            EnemyRd.velocity = new Vector2 ( distanceCheck > 0 ? 3.0f : -3.0f, 5.0f);
            if( currentBaseState.fullPathHash != jump ) {
                animator.SetBool( "Rush", true );
                EnemyRd.velocity = new Vector2( -10.0f, 0.0f );
            }
        }

		/*if ( currentBaseState.fullPathHash == dead ) {
			timeCount++;
		}
		if ( timeCount == 60 ) {
			Destroy ( gameObject );
		}*/
	}

	public void Damage( float damage ) {
		animator.SetTrigger ( "Damage" );
        //if( currentBaseState.fullPathHash != damage && currentBaseState.fullPathHash != dead ) {
		    EnemyRd.velocity = new Vector2 ( distanceCheck > 0 ? 3.0f : -3.0f, 2.0f);
        //}
        hpSlider.value -= damage;
	}

	void AttackDecision() {
        distanceCheck = target.transform.position.x - transform.position.x;
        if ( ( distanceCheck > 0 ? distanceCheck : -distanceCheck ) <= HIT_DISTANCE && 
               spriteRenderer.flipX != target.GetComponent< SpriteRenderer >().flipX ) {
		    target.GetComponent<PlayerController> ().Damage (atk);
        }
	}
}