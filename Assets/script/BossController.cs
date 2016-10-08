using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class BossController : MonoBehaviour {

	Rigidbody2D EnemyRd;
    Animator animator;
        
	enum PATTERN{
        USUALLY,
        SKILL
    };

	private float atk = 2;
    public Slider hpSlider;
	public Slider playerSlider;

    // base layerで使われる、アニメーターの現在の状態の参照
    private AnimatorStateInfo currentBaseState;
    private SpriteRenderer spriteRenderer;
    private GameObject target;

	float TargetX;
	float EnemyX;
	private float distanceCheck;
    private float passivenessCount = 0.0f;
	float moveSpeed = 1.0f;
	const float HIT_DISTANCE = 1.5f;
    const float FIND_DISTANCE = 6.5f;
    float hitCount = 10.0f;
	float timeCount = 0.0f;
    private int pattern;
    private bool skill;
    private bool a = false;
    private bool skillSuitchi = false;
	public GameObject Hp;

    static int attack = Animator.StringToHash ( "Base Layer.Attack" );
    static int attack2 = Animator.StringToHash ( "Base Layer.Rush" );
    static int damage = Animator.StringToHash ( "Base Layer.Damage" );
    static int jump = Animator.StringToHash ( "Base Layer.Jump" );
    static int dead = Animator.StringToHash( "Base Layer.Dead" );


	void Start() {
		animator = GetComponent<Animator> ();
		target = GameObject.Find ("Cguy");
		EnemyRd = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
        pattern = (int) PATTERN.USUALLY;
	}

	void Update() {

        currentBaseState = animator.GetCurrentAnimatorStateInfo (0);
		TargetX = target.transform.position.x;
		EnemyX = transform.position.x;
		distanceCheck = EnemyX - TargetX;

		if ((distanceCheck > 0 ? distanceCheck : -distanceCheck) <= FIND_DISTANCE) {
			Hp.SetActive (true);
		}
		if (hpSlider.value == 0 || playerSlider.value >= 100) {
	      
		} else {
			switch (pattern) {
			case 0:
				Usually ();
				break;
			case 1:
				Skill ();
				break;
			}
		}

	}

	public void Damage( float damage ) {

        hpSlider.value -= damage;
        if ( hpSlider.value <= 50.0f && skillSuitchi == false ) {  
            pattern = ( int )PATTERN.SKILL;
            skillSuitchi = true;
        } else {
	        animator.SetTrigger ( "Damage" );
            passivenessCount += 1;
			if( passivenessCount >= 3.0f && hpSlider.value > 0 ) {
		        EnemyRd.velocity = new Vector2 ( distanceCheck > 0 ? 5.0f : -5.0f, 5.0f);
                passivenessCount = 0.0f;
            }
			if ( hpSlider.value == 0 ) {
				animator.SetTrigger ("Dead");
			}
        }
	}

    void Usually( ) {

        if (  currentBaseState.fullPathHash != damage && currentBaseState.fullPathHash != attack ) {

            if ( distanceCheck > 0.0f ) {
                spriteRenderer.flipX = true ;
            } else { 
                spriteRenderer.flipX = false ;
            }

            if ( ( distanceCheck > 0 ? distanceCheck : -distanceCheck ) > HIT_DISTANCE && 
                 ( distanceCheck > 0 ? distanceCheck : -distanceCheck ) <= FIND_DISTANCE && 
                   currentBaseState.fullPathHash != attack2 &&
                   currentBaseState.fullPathHash != jump ) {

                EnemyRd.transform.position += new Vector3( ( spriteRenderer.flipX == true ?  -moveSpeed : moveSpeed ) * Time.deltaTime, 0 );
                animator.SetBool( "Walk", true );

            } else if ( ( distanceCheck > 0 ? distanceCheck : -distanceCheck ) <= HIT_DISTANCE ) {
                animator.SetTrigger( "Attack" );
                animator.SetBool( "Walk", false );
            } else {
                animator.SetBool( "Walk", false );
            }
        }

		if (hpSlider.value == 0) {
			animator.SetTrigger ("Dead");
		}

    }

    void Skill( ) {

        if( a == false ) {
            animator.SetTrigger( "Jump" );
            EnemyRd.velocity = new Vector2 ( distanceCheck > 0 ? 3.0f : -3.0f, 5.0f);
            skill = true;
            a = true;
        }

        if( EnemyRd.velocity.y == 0.0f && skill == true ) {
            animator.SetBool( "Rush", true );
            EnemyRd.velocity = new Vector2( distanceCheck > 0 ? -7.0f : 7.0f, 0.0f );
            skill = false;
        }


        if ( EnemyRd.velocity.x == 0.0f && EnemyRd.velocity.y == 0.0f ) {
            animator.SetBool( "Rush", false );
            pattern = (int)PATTERN.USUALLY;
        }

    }
	void AttackDecision( ) {
        distanceCheck = target.transform.position.x - transform.position.x;
        if ( ( distanceCheck > 0 ? distanceCheck : -distanceCheck ) <= HIT_DISTANCE && 
            spriteRenderer.flipX != target.GetComponent< SpriteRenderer >().flipX ) {
		    target.GetComponent<PlayerController> ().Damage (atk);
        }
	}

    void RushDecision( ) {

        distanceCheck = target.transform.position.x - transform.position.x;
        if ( ( distanceCheck > 0 ? distanceCheck : -distanceCheck ) <= HIT_DISTANCE - 0.2f  ) { 
            target.GetComponent<PlayerController> ().Passiveness ( );
            target.GetComponent<PlayerController> ().Damage (atk + 5);
        }
    }
}