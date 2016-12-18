using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    Animator animator;
    Rigidbody2D rig2d;

    GameObject[] enemy;
    GameObject[] block;
    GameObject[] item;
    GameObject boss;
    public GameObject TextController;


	public bool isGrounded = false;
	Transform tagGround;
	public LayerMask playerMask;

    private float speed;
    private float jumpPower;
    private int atk = 10;
	public float stamina;
    public Slider staminaSlider;
    public Slider booldPressureSlider;
	public Slider bossHp;
	public bool isDead = false;

    private const float HITDIRETION = 1.1f;
    private const float STAMINA_CONSUME = 10.0f;
    private const float BOOLD_PRESSURE_MAX = 100.0f;
    private const float BOOLD_PRESSURE_HALF = 50.0f;
    private const float HYPERTENSION = 70.0f;
    private const float ATTACK_CONSUME = 10.0f;


	private SpriteRenderer spriteRenderer;
	private float distanceCheckX;
	private float distanceCheckY;
    private AnimatorStateInfo currentBaseState;  // base layerで使われる、アニメーターの現在の状態の参照
    private float axis;
    private bool hypertension = false;
    private bool reduceBloodPress = false;
    private float hypertensionSpeed;
    private float healthSpeed;
	private float hypertensionJumpPower;
	private float healthJumpPower;
	private GameObject balloonController;

	//　タッチ操作
	public float minSwipeDistY;
	public float minSwipeDistX;
	public float tapDis;
	private Vector2 startPos;
	public Text text;

    // アニメーター各ステートへの参照
    static int attack = Animator.StringToHash ( "Base Layer.Attack" );
    static int jump = Animator.StringToHash( "Base Layer.Jump" );
    static int passive = Animator.StringToHash( "Base Layer.Passiveness" );

	void Awake () {
		tagGround = GameObject.Find (this.name + "/tag_ground").transform;
		enemy = GameObject.FindGameObjectsWithTag ("Enemy");
		boss = GameObject.Find ("Vodke");
		spriteRenderer = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
		rig2d = GetComponent<Rigidbody2D> ();
		StatasInit ();
	}
		

	// Use this for initialization
	void Start () {		
	}
	
	void Update () {
            SwipeAction();
            Controller();
            PlayerStatasUpDate();
            ItemDecision();
    }

    // プレイヤーコントローラ
    private void Controller( ) {

        if ( isDead || currentBaseState.fullPathHash == passive ) {
            return;
        }

        // keyBoardController
        axis = Input.GetAxisRaw ( "Horizontal" );

        // TouchController
		//axis = CrossPlatformInputManager.GetAxisRaw ( "Horizontal" );

        if ( currentBaseState.fullPathHash != passive ) {

		    //　キャラを移動させる
		    if ( axis != 0 && currentBaseState.fullPathHash != attack ) {
			    rig2d.transform.position += new Vector3 ( ( axis > 0 ? 1 : -1 ) * speed * Time.deltaTime, 0 );
			    animator.SetBool ( "Run", true );
		    } else {
			    animator.SetBool ( "Run", false );
		    }

            if ( isGrounded && currentBaseState.fullPathHash != attack ) {
		        // 攻撃
		        if ( Input.GetKeyDown( KeyCode.Z ) ) {
			        if ( staminaSlider.value > ATTACK_CONSUME ) {
				        animator.SetTrigger ("Attack");
				        staminaSlider.value -= STAMINA_CONSUME;
				        //soundManager.PlaySePunch ();
			        }
		        }

		        if ( Input.GetButtonDown ("Jump") ) {
				    rig2d.velocity = new Vector2( rig2d.velocity.x, jumpPower );
				  
		        }
            }
        }

			
}

    // プレイヤー状態更新
    private void PlayerStatasUpDate() {

        // 接地判定
        isGrounded = Physics2D.Linecast( rig2d.position, tagGround.position, playerMask );

        // 現在ANIMATOR状態を取得
        currentBaseState = animator.GetCurrentAnimatorStateInfo (0);

        if ( !isDead ) {
            staminaSlider.value += 2 * Time.deltaTime;
        }


        // 接地判定
		if ( isGrounded ) {
			animator.SetBool( "Jump", false );
		} else {
            animator.SetBool( "Jump", true );
        }


		if ( axis != 0 && currentBaseState.fullPathHash != attack ) {
			spriteRenderer.flipX = axis < 0;
		}

		if ( booldPressureSlider.value >= BOOLD_PRESSURE_HALF && hypertension == false ) {
			hypertension = true;
			BloodPressureSystem ();
		}
		if ( booldPressureSlider.value >= HYPERTENSION && hypertension == true ) {
			hypertension = false;
		}

		if (booldPressureSlider.value < BOOLD_PRESSURE_HALF && reduceBloodPress == false) {
			StatasInit();
		}

        if ( booldPressureSlider.value == BOOLD_PRESSURE_MAX ) {
				animator.SetTrigger ("Dead");
                isDead = true;
		}

        if ( gameObject.transform.position.y <= -4 ) {
           isDead = true;
        }
    }

    // 攻撃判定
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
					//soundManager.PlaySeBlock ();
                }
			}
		}

	}

    // 血圧システム
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

    // アイテム判定
    private void ItemDecision() {
        item = GameObject.FindGameObjectsWithTag("item");
        for ( int i = 0; i < item.Length; i++ ) {
            distanceCheckX = item[ i ].transform.position.x - transform.position.x;
			distanceCheckY = transform.position.y - item[ i ].transform.position.y;
			if( ( ( distanceCheckX > 0 ? distanceCheckX : -distanceCheckX ) <= HITDIRETION - 0.5f ) &&
				  ( distanceCheckY > 0 ? distanceCheckY : -distanceCheckY ) <= 0.9f ) {
				if ( item [i].gameObject.layer == 12 ) {
					booldPressureSlider.value -= 50;
					StatasInit ();
				}
				if ( item [i].gameObject.layer == 13 ) {
					booldPressureSlider.value += 25;
					staminaSlider.value += 50;
				}
				Destroy( item[ i ].gameObject );
            }
        }
    }

    // プレイヤーステータス初期化
	private void StatasInit( ) {
		hypertensionSpeed = 3.0f;
		healthSpeed = 4.0f;
		hypertensionJumpPower = 3.5f;
		healthJumpPower = 12.0f;
		speed = healthSpeed;
		jumpPower = healthJumpPower;
        hypertension = false;
	}

    // 受けるダメージ
	public void Damage( float damage ) {
		booldPressureSlider.value += damage;
        if ( hypertension == true ) {
		    staminaSlider.value -= damage;
        }
	}

    // ダメージアニメション
    public void Passiveness( ) {

		distanceCheckX = boss.transform.position.x - transform.position.x;
        animator.SetBool( "Passiveness", true );
		rig2d.velocity = new Vector2( distanceCheckX > 0 ? -4.0f : 4.0f, 2.0f );
    }

    // タッチ操作
	void SwipeAction()
	{
		if (Input.touchCount > 0) {
			for (int i = 0; i < Input.touchCount; i++) {
				Touch touch = Input.touches [i];

				switch (touch.phase) {

				case TouchPhase.Began:

					startPos = touch.position;

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
							if (staminaSlider.value > 10 ) {
								//soundManager.PlaySePunch ();
								animator.SetTrigger ("Attack");
								staminaSlider.value -= STAMINA_CONSUME;
							}
						}
						text.text = "tap";
					}
					break;

				}
			}
		}
	}

    void BallonDestroy() {
		balloonController = GameObject.Find ("TextController");
		balloonController.GetComponent<BalloonController> ().BalloonDestroy ();
	}
		
}