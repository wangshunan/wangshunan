using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	EnemyController enemyController;


    Animator animator;
    CharacterController controller;

    public float speedZ;
    public float jumpPower;
    public float gravity;
    // キャラの方向状態の参照
    private bool Right;
    private Vector3 moveDiretion = Vector3.zero;
	public Slider hpSlider;
	private int maxHp = 10;
	private float damage = 1.0f;
    // base layerで使われる、アニメーターの現在の状態の参照
    private AnimatorStateInfo currentBaseState;
	public GameObject target;

    // アニメーター各ステートへの参照
    static int jabState = Animator.StringToHash ( "Base Layer.Jab" );
    static int runState = Animator.StringToHash ( "Base Layer.Run" );
	static int jumpState = Animator.StringToHash("Base Layer.Land");


	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        Right = true;
	}
	
	// Update is called once per frame
	void Update () {

        currentBaseState = animator.GetCurrentAnimatorStateInfo (0);

		//　キャラを移動させる
		if( currentBaseState.fullPathHash != jabState ) {

			/*if( (Input.GetKey ( "right" ) && Right == true) ||
				(Input.GetKey ( "left" ) && Right != true) ) {    
				moveDiretion.z = speedZ;
			} else {
    			moveDiretion.z = 0;
			}*/
			moveDiretion.z = CrossPlatformInputManager.GetAxis("Horizontal") * speedZ;


		}


		// キャラの方向を変える
		if( currentBaseState.fullPathHash != jabState ) {
			if( CrossPlatformInputManager.GetAxis("Horizontal") > 0.0f && Right != true ) {
				Right = true;
				transform.Rotate (0, 180, 0);
				speedZ *= -1;
			} 
			if( CrossPlatformInputManager.GetAxis("Horizontal") < 0.0f && Right == true ) {
				Right = false;
				transform.Rotate (0, 180, 0);
				speedZ *= -1;
			}        
		}

        // 接地状態でしかできない動き
        if( controller.isGrounded ) {
			if( currentBaseState.fullPathHash != jabState ) {
				if ( CrossPlatformInputManager.GetButtonDown( "Attack" ) ) {
					animator.SetTrigger ("Jab");
					if (Mathf.Abs (target.transform.position.z - transform.position.z) <= 1.5f) {
						hpSlider.value -= damage;
						enemyController.Damage ();
					}
					if (hpSlider.value == 0) {
						Destroy(target.gameObject);
					}
					moveDiretion.z = 0;
				}
			}

			if( CrossPlatformInputManager.GetButtonDown( "Jump" ) && currentBaseState.fullPathHash != jabState ) {
				moveDiretion.y = jumpPower;
				animator.SetTrigger( "Land" );
			}
		}

		moveDiretion.y -= gravity* Time.deltaTime;
			
        Vector3 globaDirection = transform.TransformDirection( moveDiretion );
        controller.Move( globaDirection * Time.deltaTime );

		if( controller.isGrounded ) {
			moveDiretion.y = 0;
		}

        animator.SetBool( "Run", moveDiretion.z != 0.0f );

		if( hpSlider.value == 0 ) {

			Application.LoadLevel ("TitleMenu");

		}
	}


}
