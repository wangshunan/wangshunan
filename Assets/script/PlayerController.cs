using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    Animator animator;
    Rigidbody2D rig2d;
    LayerMask groundMask;
	public SoundManager soundManager;

    private float speed;
    private float jumpPower;
    public float gravity;
	public float stamina;
	public float bloodPressure;
    public Slider staminaSlider;
    public Slider booldPressureSlider;
	GameObject[] enemy;
    GameObject[] block;
    GameObject[] item;

    private const float HITDIRETION = 1.1f;
    private const float STAMINACONSUME = 10.0f;

	private SpriteRenderer spriteRenderer;
	private float distanceCheckX = 0.0f;
	private float distanceCheckY = 0.0f;
    // base layerで使われる、アニメーターの現在の状態の参照
    private AnimatorStateInfo currentBaseState;
    private float axis = 0.0f;
    private bool hypertension = false;
    private bool reduceBloodPress = false;
    private float hypertensionSpeed;
    private float healthSpeed;
	private float hypertensionJumpPower;
	private float healthJumpPower;

    // アニメーター各ステートへの参照
    static int attack = Animator.StringToHash ( "Base Layer.Attack" );
    static int run = Animator.StringToHash ( "Base Layer.Run" );
    static int jump = Animator.StringToHash( "Base Layer.Jump" );


	// Use this for initialization
	void Start () {
		enemy = GameObject.FindGameObjectsWithTag("Enemy");
        spriteRenderer = GetComponent<SpriteRenderer> ();
        animator = GetComponent<Animator>();
        rig2d = GetComponent<Rigidbody2D> ();
		Initialization ();
	}
	
	// Update is called once per frame
	void Update () {
		
		// 移動
        axis = Input.GetAxisRaw ( "Horizontal" );
        currentBaseState = animator.GetCurrentAnimatorStateInfo (0);
            //　キャラを移動させる
		if ( axis != 0 && currentBaseState.fullPathHash != attack ) {
            rig2d.transform.position += new Vector3(axis * speed * Time.deltaTime, 0);
            animator.SetBool( "Run", true );
        } else {
            animator.SetBool( "Run", false );
        }
		// 攻撃
		if ( Input.GetKeyDown( KeyCode.Z ) && rig2d.velocity.y == 0 && currentBaseState.fullPathHash != attack ) {
			if (staminaSlider.value > 0) {
				animator.SetTrigger ("Attack");
				staminaSlider.value -= STAMINACONSUME;
			}
        }      

		//　ジャンプ
        if ( Input.GetButtonDown( "Jump" ) && currentBaseState.fullPathHash != jump )
        {
            rig2d.velocity = new Vector2(rig2d.velocity.x, jumpPower + 5);
            animator.SetBool( "Jump", true );
			//soundManager.PlaySeJump ();
        }
		if ( rig2d.velocity.y == 0 )
        {
            animator.SetBool( "Jump", false );
        }
        
        if ( booldPressureSlider.value >= 50.0f && hypertension == false ) {
            hypertension = true;
            BloodPressureSystem();
        }
        if (booldPressureSlider.value >= 70.0f && hypertension == true )
        {
            hypertension = false;
        }

        if(  booldPressureSlider.value < 50.0f && reduceBloodPress == false ) {
			Initialization ();
        }

        ItemDecision( );


		if ( axis != 0 && currentBaseState.fullPathHash != attack ) {
			spriteRenderer.flipX = axis < 0;
        }

        
    }

	private void AttackDecision() {

		block = GameObject.FindGameObjectsWithTag ( "Block" );
		enemy = GameObject.FindGameObjectsWithTag ( "Enemy" );
		for ( int i = 0; i < enemy.Length; i++ ) {        
            distanceCheckX = enemy[ i ].transform.position.x - transform.position.x;
            distanceCheckY = enemy[ i ].transform.position.y - transform.position.y;
			if ( ( ( distanceCheckX > 0 ? distanceCheckX : -distanceCheckX ) <= HITDIRETION ) &&
                 ( ( distanceCheckY > 0 ? distanceCheckY : -distanceCheckY ) <= HITDIRETION ) ) {
                if ( spriteRenderer.flipX != enemy[i].GetComponent<SpriteRenderer>().flipX ) {
                    enemy[i].GetComponent<EnemyController>().Damage();
                }
			}
		}

		for ( int i = 0; i < block.Length; i++ ) {
            distanceCheckX = block[ i ].transform.position.x - transform.position.x;
            distanceCheckY = block[ i ].transform.position.y - transform.position.y;
			if ( ( ( distanceCheckX > 0 ? distanceCheckX : -distanceCheckX ) <= HITDIRETION + 0.5f ) && 
                 ( ( distanceCheckY > 0 ? distanceCheckY : -distanceCheckY ) <= HITDIRETION ) ) {
                if ( ( distanceCheckX > 0 && spriteRenderer.flipX == false ) || 
                     ( distanceCheckX < 0 && spriteRenderer.flipX == true ) ) {
				    block[i].GetComponent<BlockController> ().BlockDestroyAni ();
                }
			}
		}

	}

    private void BloodPressureSystem() {

        if( hypertension == true ) {
            speed = hypertensionSpeed;

            if ( booldPressureSlider.value >= 70.0f ) {
                jumpPower = 0;
            } else {
                jumpPower = hypertensionJumpPower;
            }
        }

    }

    private void ItemDecision() {
        item = GameObject.FindGameObjectsWithTag("item");
        for ( int i = 0; i < item.Length; i++ ) {
            distanceCheckX = item[ i ].transform.position.x - transform.position.x;
			distanceCheckY = transform.position.y - item[ i ].transform.position.y;
			if( ( ( distanceCheckX > 0 ? distanceCheckX : -distanceCheckX ) <= HITDIRETION - 0.5f ) &&
				    distanceCheckY <= 0.9f ) {
				if ( item [i].gameObject.layer == 12 ) {
					booldPressureSlider.value -= 50;
					Initialization ();
				}
				if ( item [i].gameObject.layer == 13 ) {
					booldPressureSlider.value += 50;
					staminaSlider.value += 50;
				}
				Destroy( item[ i ].gameObject );
            }
        }
    }

	private void Initialization( ) {
		hypertensionSpeed = 1.5f;
		healthSpeed = 3.0f;
		hypertensionJumpPower = 3.5f;
		healthJumpPower = 7.0f;
		speed = healthSpeed;
		jumpPower = healthJumpPower;
        hypertension = false;
	}

	public void Damage( float damage ) {
		booldPressureSlider.value += damage;
        if ( hypertension == true ) {
		    staminaSlider.value -= damage;
        }
	}
}
