using UnityEngine;
using System.Collections;

public class ItemController : MonoBehaviour {

    SpriteRenderer spriteRenderer;
    Animator animator;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();	
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {           
	}

    public void BlockDestroy() {
        animator.SetTrigger( "Destroy" );
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
