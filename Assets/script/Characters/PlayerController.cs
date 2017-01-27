using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    GameLogic gameLogic;

	[SerializeField]
	BalloonController balloonController;

    GameObject[] enemy;
    GameObject[] block;
    GameObject[] item;
    GameObject boss;
    public GameObject TextController;

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

	private Color damageColor = new Color( 255, 0, 0, 255 );
    private float damageCount;

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

	//　タッチ操作
	public float minSwipeDistY;
	public float minSwipeDistX;
	public float tapDis;
	private Vector2 startPos;

    [SerializeField] private float characterHeightOffset = 0.2f;
	[SerializeField] LayerMask groundMask;

	[SerializeField, HideInInspector]Animator animator;
	[SerializeField, HideInInspector]SpriteRenderer spriteRenderer;
	[SerializeField, HideInInspector]Rigidbody2D rig2d;

    static int hashSpeed = Animator.StringToHash ("Speed");
	static int hashFallSpeed = Animator.StringToHash ("FallSpeed");
	static int hashGroundDistance = Animator.StringToHash ("GroundDistance");
	static int hashAttack = Animator.StringToHash ("Attack");
    static int hashDamage = Animator.StringToHash ("Damage");
	static int hashDead = Animator.StringToHash("IsDead");

    // アニメーター各ステートへの参照
    static int attack = Animator.StringToHash ( "Base Layer.Attack" );
    static int jump = Animator.StringToHash( "Base Layer.Jumping.Jumping" );
	static int damage = Animator.StringToHash( "Base Layer.Damaged");
	static int grounded = Animator.StringToHash( "Base Layer.Ground.Stand" );

	void Awake () {
        gameLogic = GameObject.Find( "GameLogic" ).GetComponent<GameLogic>();
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

        if ( gameLogic.gameStatus != GameLogic.GAME_STATUS.Start ) {
            return;
        }

        SwipeAction();
        Controller();
        ItemDecision();
		PlayerStatasUpDate();
    }

    // プレイヤーコントローラ
    private void Controller( ) {

        if ( isDead || currentBaseState.fullPathHash == hashDamage ) {
            return;
        }
        
        // keyBoardController
        axis = Input.GetAxisRaw ("Horizontal");
        
        // TouchController
		//axis = CrossPlatformInputManager.GetAxisRaw ( "Horizontal" );

		if ( currentBaseState.fullPathHash != damage ) {

		    //　キャラを移動させる
		    if ( axis != 0 && currentBaseState.fullPathHash != attack ) {
			   rig2d.transform.position += new Vector3( axis * speed, 0, 0 );
            }

			if ( currentBaseState.fullPathHash != attack && currentBaseState.fullPathHash == grounded ) {
		        // 攻撃
		        if ( Input.GetKeyDown( KeyCode.Z ) ) {
			        if ( staminaSlider.value > ATTACK_CONSUME ) {
				        animator.SetTrigger (hashAttack);
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

        // 現在ANIMATOR状態を取得
        currentBaseState = animator.GetCurrentAnimatorStateInfo (0);
        var distanceFromGround = Physics2D.Raycast (transform.position, Vector3.down, 10, groundMask);
        //Debug.DrawRay(transform.position, distanceFromGround.point - new Vector2( transform.position.x, transform.position.y ), Color.blue, 3, true);
        if ( !isDead ) {
            staminaSlider.value += 2 * Time.deltaTime;
        }
        //Debug.Log( distanceFromGround.distance );
		// update animator parameters
		animator.SetFloat (hashGroundDistance, distanceFromGround.distance == 0 ? 99 : distanceFromGround.distance - characterHeightOffset);
		animator.SetFloat (hashFallSpeed, rig2d.velocity.y);
		animator.SetFloat (hashSpeed, Mathf.Abs (axis) );

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

        if ( booldPressureSlider.value >= BOOLD_PRESSURE_MAX ) {
			animator.SetBool (hashDead, true);
			isDead = true;
		}

        // 落下死亡判定
        /*if ( gameObject.transform.position.y <= -4 ) {
           isDead = true;
        }*/

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
		hypertensionSpeed = 1.0f;
		healthSpeed = 0.1f;
		hypertensionJumpPower = 3.5f;
		healthJumpPower = 12.0f;
		speed = healthSpeed;
		jumpPower = healthJumpPower;
        hypertension = false;
	}

    // 受けるダメージ
	public void Damage( float damage ) {
		if (isDead) {
			return;
		}
		booldPressureSlider.value += damage;
        if ( hypertension == true ) {
		    staminaSlider.value -= damage;
        }
        animator.SetTrigger( hashDamage ); 
        
	}

    // ダメージアニメション
    public void Passiveness( ) {
		distanceCheckX = boss.transform.position.x - transform.position.x;
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
						}

						//jumpEnd

						else if (swipeValue < 0) {//down swipe
							//Shrink ();
						}
					}

					float swipeDistHorizontal = (new Vector3 (touch.position.x, 0, 0) - new Vector3 (startPos.x, 0, 0)).magnitude;

					if (swipeDistHorizontal > minSwipeDistX && (touch.position.x > (Screen.width / 2))) {

						float swipeValue = Mathf.Sign (touch.position.x - startPos.x);

						if (swipeValue > 0) {//right swipe
							//MoveRight ();
						} else if (swipeValue < 0) {//left swipe
							//MoveLeft ();
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
					}
					break;

				}
			}
		}
	}

    public void DamageColor() {
         GetComponent<SpriteRenderer>().color = new Color( 255, 0, 0, 255 );
    }
		
    public void NormalColor() {
         GetComponent<SpriteRenderer>().color = new Color( 255, 255, 255, 255 );
    }
	public void Dead() {
		gameLogic.gameStatus = GameLogic.GAME_STATUS.Over;
	}
}