using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {


    Animator animator;
    Rigidbody2D rig2d;
	public SoundManager soundManager;

    private float speed;
    private float jumpPower;
    private int atk = 10;
    public float gravity;
	public float stamina;
	public float bloodPressure;
    public Slider staminaSlider;
    public Slider booldPressureSlider;
	public Slider bossHp;
	GameObject[] enemy;
    GameObject[] block;
    GameObject[] item;
    GameObject boss;

    private const float HITDIRETION = 1.1f;
    private const float STAMINACONSUME = 10.0f;

	private SpriteRenderer spriteRenderer;
	private float distanceCheckX = 0.0f;
	private float distanceCheckY = 0.0f;
    // base layerで使われる、アニメーターの現在の状態の参照
    private AnimatorStateInfo currentBaseState;
    private float axis1 = 0.0f;
    private bool hypertension = false;
    private bool reduceBloodPress = false;
	public bool game = true;
    private float hypertensionSpeed;
    private float healthSpeed;
	private float hypertensionJumpPower;
	private float healthJumpPower;
	private GameObject gameLogic;

	//swipeActions
	public float minSwipeDistY;

	public float minSwipeDistX;

	public float tapDis;

	private Vector2 startPos;

	public Text text;

    // アニメーター各ステートへの参照
    static int attack = Animator.StringToHash ( "Base Layer.Attack" );
    static int run = Animator.StringToHash ( "Base Layer.Run" );
    static int jump = Animator.StringToHash( "Base Layer.Jump" );
    static int passive = Animator.StringToHash( "Base Layer.Passiveness" );


	// Use this for initialization
	void Start () {		
		enemy = GameObject.FindGameObjectsWithTag("Enemy");
		boss = GameObject.Find( "Vodke" );
        spriteRenderer = GetComponent<SpriteRenderer> ();
        animator = GetComponent<Animator>();
        rig2d = GetComponent<Rigidbody2D> ();
		Initialization ();
	}
	
	// Update is called once per frame
	void Update () {
		//axis = Input.GetAxisRaw ( "Horizontal" );
		axis1 = CrossPlatformInputManager.GetAxisRaw ( "Horizontal" );
        currentBaseState = animator.GetCurrentAnimatorStateInfo (0);
        staminaSlider.value += 1 * Time.deltaTime;
        if ( currentBaseState.fullPathHash == passive && rig2d.velocity.y == 0.0f ) {
            animator.SetBool( "Passiveness", false );
        }

		if ( bossHp.value <= 0 || booldPressureSlider.value == 100 ) {
			game = false;
		}

		if ( currentBaseState.fullPathHash != passive && game == true ) {
            Controller( );
			SwipeAction ();
        }

        ItemDecision( );

        
    }

    void Controller( ) {

        //　キャラを移動させる
		if ( axis1 != 0 && currentBaseState.fullPathHash != attack ) {
            rig2d.transform.position += new Vector3(axis1 * speed * Time.deltaTime, 0);
            animator.SetBool( "Run", true );
        } else {
            animator.SetBool( "Run", false );
        }
		/*// 攻撃
		if ( Input.GetKeyDown( KeyCode.Z ) && currentBaseState.fullPathHash != jump && currentBaseState.fullPathHash != attack ) {
            Debug.Log( "!!!!" );
			if ( staminaSlider.value > 10.0f ) {
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
        }*/
        
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

		if ( axis1 != 0 && currentBaseState.fullPathHash != attack ) {
			spriteRenderer.flipX = axis1 < 0;
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
                    enemy[ i ].GetComponent<EnemyController>().Damage();
                }
			}
		}
      
            distanceCheckX = boss.transform.position.x - transform.position.x;
            distanceCheckY = boss.transform.position.y - transform.position.y;
			if ( ( ( distanceCheckX > 0 ? distanceCheckX : -distanceCheckX ) <= HITDIRETION ) &&
                 ( ( distanceCheckY > 0 ? distanceCheckY : -distanceCheckY ) <= HITDIRETION ) ) {
                if ( spriteRenderer.flipX != boss.GetComponent<SpriteRenderer>().flipX ) {
                    boss.GetComponent<BossController>().Damage( atk );
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
                    Debug.Log( "" + i );
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
					booldPressureSlider.value += 25;
					staminaSlider.value += 50;
				}
				Destroy( item[ i ].gameObject );
            }
        }
    }

	private void Initialization( ) {
		hypertensionSpeed = 3.0f;
		healthSpeed = 4.0f;
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

    public void Passiveness( ) {

		distanceCheckX = boss.transform.position.x - transform.position.x;
        animator.SetBool( "Passiveness", true );
		rig2d.velocity = new Vector2( distanceCheckX > 0 ? -4.0f : 4.0f, 2.0f );
    }

	void SwipeAction()
	{
		if ( rig2d.velocity.y == 0 )
		{
			animator.SetBool( "Jump", false );
		}

		if (Input.touchCount > 0) {
			for (int i = 0; i < Input.touchCount; i++) {
				Touch touch = Input.touches [i];

				switch (touch.phase) {

				case TouchPhase.Began:

					startPos = touch.position;

					if (rig2d.velocity.y == 0) {
						animator.SetBool ("Jump", false);
					}

					break;

				case TouchPhase.Ended:

					float swipeDistVertical = (new Vector3 (0, touch.position.y, 0) - new Vector3 (0, startPos.y, 0)).magnitude;

					if (swipeDistVertical > minSwipeDistY && (touch.position.x > (Screen.width / 2))) {

						float swipeValue = Mathf.Sign (touch.position.y - startPos.y);

						//jump
						if (swipeValue > 0) {//up swipe
							//Jump ();
							if (currentBaseState.fullPathHash != jump) {
								rig2d.velocity = new Vector2 (rig2d.velocity.x, jumpPower + 5);
								animator.SetBool ("Jump", true);
								//soundManager.PlaySeJump ();
							}
							text.text = "Up Swipe";
						}

						//jumpEnd

						else if (swipeValue < 0) {//down swipe
							//Shrink ();
							text.text = "Down Swipe";
						}
					}

					float swipeDistHorizontal = (new Vector3 (touch.position.x, 0, 0) - new Vector3 (startPos.x, 0, 0)).magnitude;

					if (swipeDistHorizontal > minSwipeDistX && (touch.position.x > (Screen.width / 2))) {

						float swipeValue = Mathf.Sign (touch.position.x - startPos.x);

						if (swipeValue > 0) {//right swipe
							//MoveRight ();
							text.text = "Right Swipe";
						} else if (swipeValue < 0) {//left swipe
							//MoveLeft ();
							text.text = "Left Swipe";
						}
					}

					//tap攻撃
					if (swipeDistVertical <= tapDis && swipeDistHorizontal <= tapDis && (touch.position.x > (Screen.width / 2))) {
						if (currentBaseState.fullPathHash != jump && currentBaseState.fullPathHash != attack) {
							Debug.Log ("!!!!");
							if (staminaSlider.value > 0) {
								//soundManager.PlaySePunch ();
								animator.SetTrigger ("Attack");
								staminaSlider.value -= STAMINACONSUME;
							}
						}
						text.text = "tap";
					}
					break;

				}
			}
		}
	}
		
		
}
