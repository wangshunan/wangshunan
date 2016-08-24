using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    Animator animator;
    Rigidbody2D rig2d;
    LayerMask groundMask;

    public float speed;
    public float jumpPower;
    public float gravity;
	public int stamina;
	public int bloodPressure;
	GameObject[] enemy;

	private SpriteRenderer spriteRenderer;
	private float distanceCheck = 2.0f;
	private const float hitDiretion = 1.2f;
    // base layerで使われる、アニメーターの現在の状態の参照
    private AnimatorStateInfo currentBaseState;
    private float axis = 0.0f;

    
    [SerializeField]
	EnemyController enemyController;

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
	}
	
	// Update is called once per frame
	void Update () {
		
		// 移動
        axis = Input.GetAxisRaw ("Horizontal");
        currentBaseState = animator.GetCurrentAnimatorStateInfo (0);
            //　キャラを移動させる
            if (axis != 0 && currentBaseState.fullPathHash != attack)
            {
                rig2d.transform.position += new Vector3(axis * speed * Time.deltaTime, 0);
                animator.SetBool("Run", true);
            }
            else
            {
                animator.SetBool("Run", false);
            }
		// 攻撃
		if (Input.GetKeyDown (KeyCode.Z) && rig2d.velocity.y == 0) {
			animator.SetTrigger ("Attack");
		}

		//　ジャンプ
        if (Input.GetButtonDown("Jump") && currentBaseState.fullPathHash != jump )
        {
            rig2d.velocity = new Vector2(rig2d.velocity.x, jumpPower);
            animator.SetBool("Jump", true);
        }
		if (rig2d.velocity.y == 0)
        {
            animator.SetBool("Jump", false);
        }			


		if ( axis != 0 && currentBaseState.fullPathHash != attack ) {
			spriteRenderer.flipX = axis < 0;
        }
    }

	void AttackDecision() {
		
		enemy = GameObject.FindGameObjectsWithTag ("Enemy");
		for (int i = 0; i < enemy.Length; i++) {
			if (enemy [i].transform.position.x - transform.position.x < 1.0f) {
				enemy [i].GetComponent<EnemyController> ().Damage ();
			}
		}
	}

	public void Damage( int damage ) {
		bloodPressure += damage;
		stamina -= damage;
		Debug.Log ( "" + bloodPressure );
		Debug.Log ( "" + stamina );
	}
}
