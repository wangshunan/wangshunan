using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    Animator animator;
    Rigidbody2D rig2d;
    LayerMask groundMask;
	public SoundManager soundManager;

    public float speed;
    public float jumpPower;
    public float gravity;
	public float stamina;
	public float bloodPressure;
    public Slider staminaSlider;
    public Slider booldPressureSlider;
	GameObject[] enemy;
    GameObject[] block;
    GameObject[] item;

	private SpriteRenderer spriteRenderer;
	private float distanceCheck = 0.0f;
	private const float HITDIRETION = 1.1f;
    // base layerで使われる、アニメーターの現在の状態の参照
    private AnimatorStateInfo currentBaseState;
    private float axis = 0.0f;
    private const float STAMINACONSUME = 1.0f;
    private bool hypertension = false;
    private bool reduceBloodPress = false;
    private float hypertensionSpeed;
    private float healthSpeed;

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
        hypertensionSpeed = 1.5f;
        healthSpeed = 3.0f;
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
			animator.SetTrigger ( "Attack" );
            staminaSlider.value -= STAMINACONSUME;
        }      
		if (Input.GetKeyDown (KeyCode.Z) && rig2d.velocity.y == 0) {
			animator.SetTrigger ("Attack");
			//soundManager.PlaySePunch ();
		}

		//　ジャンプ
        if ( Input.GetButtonDown( "Jump" ) && currentBaseState.fullPathHash != jump )
        {
            rig2d.velocity = new Vector2(rig2d.velocity.x, jumpPower);
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

        if(  booldPressureSlider.value < 50.0f && reduceBloodPress == false ) {
            reduceBloodPress = true;
            BloodPressureSystem();
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
            distanceCheck = enemy[ i ].transform.position.x - transform.position.x;
			if ( ( distanceCheck > 0 ? distanceCheck : -distanceCheck ) <= HITDIRETION && 
                   spriteRenderer.flipX != enemy[ i ].GetComponent< SpriteRenderer >().flipX ) {
				enemy[i].GetComponent<EnemyController> ().Damage ();
			}
		}

		for ( int i = 0; i < block.Length; i++ ) {
            distanceCheck = block[ i ].transform.position.x - transform.position.x;         
			if ( ( distanceCheck > 0 ? distanceCheck : -distanceCheck ) <= HITDIRETION + 0.5f ) {
                if ( ( distanceCheck > 0 && spriteRenderer.flipX == false ) || 
                     ( distanceCheck < 0 && spriteRenderer.flipX == true ) ) {
				    block[i].GetComponent<BlockController> ().BlockDestroy ();
                }
			}
		}

	}

    private void BloodPressureSystem() {
        if( hypertension == true ) {
            speed = hypertensionSpeed;
            jumpPower = jumpPower / 2.0f;
            reduceBloodPress = false;
        }

        if( reduceBloodPress == true ) {
            speed = healthSpeed;
            jumpPower = jumpPower + jumpPower;
            hypertension = false;
        }
    }

    private void ItemDecision() {
        item = GameObject.FindGameObjectsWithTag("medicine");
        for ( int i = 0; i < item.Length; i++ ) {
            distanceCheck = item[ i ].transform.position.x - transform.position.x;   
            if( ( distanceCheck > 0 ? distanceCheck : -distanceCheck ) <= HITDIRETION ) {
                Destroy( item[ i ].gameObject );
                booldPressureSlider.value -= 50;
            }
        }
    }


	public void Damage( float damage ) {
		booldPressureSlider.value += damage;
        if ( hypertension == true ) {
		    staminaSlider.value -= damage;
        }
	}
}
