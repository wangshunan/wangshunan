using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class BossController : MonoBehaviour {

	Rigidbody2D EnemyRd;
    Animator animator;
        
	enum PATTERN{
        USUALLY,
        SKILL,
		DEAD,
		CLEAR
    };

	private float atk = 2;
	public Slider playerSlider;

    // base layerで使われる、アニメーターの現在の状態の参照
    private AnimatorStateInfo currentBaseState;
    private SpriteRenderer spriteRenderer;
    private GameObject target;

	float TargetX;
	float EnemyX;
	private float distanceCheck;
    private float passivenessCount = 0.0f;
	private float BossHp;
	float moveSpeed = 1.0f;
	const float HIT_DISTANCE = 1.5f;
    const float FIND_DISTANCE = 6.5f;
	const float HP_MAX = 100;
	const float HP_HALF = 50;
    private int pattern;
    private bool skill;
    private bool a = false;
    private bool skillSuitchi = false;
	public GameObject HpSlider;
	public bool isDead;

    static int attack = Animator.StringToHash ( "Base Layer.Attack" );
    static int attack2 = Animator.StringToHash ( "Base Layer.Rush" );
    static int damage = Animator.StringToHash ( "Base Layer.Damage" );
    static int jump = Animator.StringToHash ( "Base Layer.Jump" );

	void Awake() {
		animator = GetComponent<Animator> ();
		target = GameObject.Find ("Cguy");
		EnemyRd = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		pattern = (int)PATTERN.USUALLY;
		isDead = false;
	}
		

	void Start() {
		/*if (HpSlider.GetComponent<Slider>().value <= 0) {
			pattern = (int)PATTERN.DEAD;
		}*/
	}

	void Update() {
		
		statusUpDate ();

		switch ( pattern ) {
		case (int)PATTERN.USUALLY:
				Usually ();
				break;
		case (int)PATTERN.SKILL:
				Skill ();
				break;
		case (int)PATTERN.DEAD:
				Dead ();
				break;
		}

	}

	public void Damage( float damage ) {
		
		// UI表示
		if ( ( distanceCheck > 0 ? distanceCheck : -distanceCheck) <= FIND_DISTANCE ) {
			HpSlider.SetActive (true);
		}

		HpSlider.GetComponent<Slider>().value -= damage;
		if ( HpSlider.GetComponent<Slider>().value <= HP_HALF && skillSuitchi == false ) {  
            pattern = ( int )PATTERN.SKILL;
            skillSuitchi = true;
        } else {
	        animator.SetTrigger ( "Damage" );
            passivenessCount += 1;
			if ( passivenessCount >= 3.0f && HpSlider.GetComponent<Slider>().value > 0 ) {
		        EnemyRd.velocity = new Vector2 ( distanceCheck > 0 ? 5.0f : -5.0f, 5.0f);
                passivenessCount = 0.0f;
            }
        }
	}

	void Dead() {		
		animator.SetTrigger ("DEAD");
		isDead = true;
		pattern = ( int )PATTERN.CLEAR;
	}

    void Usually( ) {

		currentBaseState = animator.GetCurrentAnimatorStateInfo (0);
		TargetX = target.transform.position.x;
		EnemyX = transform.position.x;
		distanceCheck = EnemyX - TargetX;

        if ( currentBaseState.fullPathHash != damage && currentBaseState.fullPathHash != attack ) {

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
			

    }

	void statusUpDate() {
		if (HpSlider.GetComponent<Slider> ().value == 0) {
			animator.SetTrigger ("Dead");
			pattern = (int)PATTERN.DEAD;
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