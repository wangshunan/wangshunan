using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossController : MonoBehaviour {

	[SerializeField]
	GameLogic gameLogic;

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
	float moveSpeed = 2.0f;
	const float HIT_DISTANCE = 2f;
    const float FIND_DISTANCE = 20f;
	const float HP_MAX = 100;
	const float HP_HALF = 50;
    private int pattern;
    private bool skill;
    private bool a = false;
    private bool skillSuitchi = false;
	public Slider HpSlider;
	public bool isDead;

    static int attack = Animator.StringToHash ( "Base Layer.Attack" );
    static int attack2 = Animator.StringToHash ( "Base Layer.Rush" );
    static int damage = Animator.StringToHash ( "Base Layer.Damage" );
    static int jump = Animator.StringToHash ( "Base Layer.Jump" );

	void Awake() {
		gameLogic = GameObject.Find( "GameLogic" ).GetComponent<GameLogic>( );
		animator = GetComponent<Animator> ();
		target = GameObject.Find ("Cguy");
		EnemyRd = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		pattern = (int)PATTERN.USUALLY;
		isDead = false;
		HpSlider.value = 100;
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

		HpSlider.value -= damage;
		if ( HpSlider.value <= HP_HALF && skillSuitchi == false ) {  
            pattern = ( int )PATTERN.SKILL;
            skillSuitchi = true;
        } else {
	        animator.SetTrigger ( "Damage" );
            passivenessCount += 1;
			if ( passivenessCount == 3.0f && HpSlider.GetComponent<Slider>().value > 0 ) {
		        EnemyRd.velocity = new Vector2 ( distanceCheck > 0 ? 5.0f : -5.0f, 5.0f);
            }
			if ( passivenessCount == 4.0f && HpSlider.GetComponent<Slider>( ).value > 0 ) {
				pattern = ( int )PATTERN.SKILL;
				passivenessCount = 0.0f;
			}
        }
	}

	void Dead() {		
		animator.SetTrigger ("DEAD");
		isDead = true;
		pattern = ( int )PATTERN.CLEAR;
		gameLogic.gameStatus = GameLogic.GAME_STATUS.Clear;
	}

    void Usually( ) {

		currentBaseState = animator.GetCurrentAnimatorStateInfo (0);
		TargetX = target.transform.position.x;
		EnemyX = transform.position.x;
		distanceCheck = EnemyX - TargetX;

        if ( currentBaseState.fullPathHash != damage && currentBaseState.fullPathHash != attack ) {

            if ( distanceCheck > 0.0f ) {
				spriteRenderer.flipX = false ;
            } else { 
				spriteRenderer.flipX = true ;
            }

            if ( ( distanceCheck > 0 ? distanceCheck : -distanceCheck ) > HIT_DISTANCE && 
                 ( distanceCheck > 0 ? distanceCheck : -distanceCheck ) <= FIND_DISTANCE && 
                   currentBaseState.fullPathHash != attack2 &&
                   currentBaseState.fullPathHash != jump ) {

				EnemyRd.transform.position += new Vector3( ( spriteRenderer.flipX == false ?  -moveSpeed : moveSpeed ) * Time.deltaTime, 0 );
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
		if (HpSlider.value == 0) {
			animator.SetTrigger ("Dead");
			pattern = (int)PATTERN.DEAD;
		}
	}

    void Skill( ) {

        if( a == false ) {
            animator.SetTrigger( "Jump" );
            EnemyRd.velocity = new Vector2 ( 0, 5.0f);
            skill = true;
            a = true;
        }

        if( EnemyRd.velocity.y == 0.0f && skill == true ) {
			animator.SetTrigger( "Rush" );
            skill = false;
        }


		if ( currentBaseState.fullPathHash != attack2 && EnemyRd.velocity.y == 0.0f ) {
			a = false;
            pattern = (int)PATTERN.USUALLY;
        }

    }

	void AttackDecision( ) {
        distanceCheck = target.transform.position.x - transform.position.x;
		if ( Mathf.Abs( distanceCheck ) < HIT_DISTANCE ) {
			if ( ( distanceCheck < 0 && spriteRenderer.flipX == false ) ||
				 ( distanceCheck > 0 && spriteRenderer.flipX == true ) ) {
				target.GetComponent<PlayerController>( ).Damage( atk );
			}
        }
	}

    void RushDecision( ) {

        distanceCheck = target.transform.position.x - transform.position.x;
        if ( ( distanceCheck > 0 ? distanceCheck : -distanceCheck ) <= HIT_DISTANCE ) { 
            target.GetComponent<PlayerController> ().Passiveness ( );
            target.GetComponent<PlayerController> ().Damage (atk + 5);
        }
    }
}