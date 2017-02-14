using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonController : MonoBehaviour {

	[SerializeField]
	public Text itemText;

    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    GameObject itemButton;

    [SerializeField]
    GameLogic gameLogic;

	private const int ITEM_MAX = 3;
	private const int ITEM_MIN = 0;
	private int items = 1;

	void Awake() {
		itemText.text = "X" + items;
        playerController = GameObject.Find( "Cguy" ).GetComponent<PlayerController>();
        itemButton = GameObject.Find( "ItemButton" );
        gameLogic = GameObject.Find( "GameLogic" ).GetComponent<GameLogic>();
	}

	// Update is called once per frame
	void Update () {
        if ( gameLogic.gameStatus != GameLogic.GAME_STATUS.Start ) {
            itemButton.SetActive( false );
        } else {
            itemButton.SetActive( true );
        }
		itemText.text = "X" + items;
	}

    public void ItemUse() {
        if ( items > ITEM_MIN ) {
            items--;
            playerController.CapsulesUse();
        }
    }

    public void ItemsPlus() {
        if ( items < ITEM_MAX ) {
            items++;
        }
    }

    public void ItemMinus() {
        if ( items > ITEM_MIN ) {
            items--;
        }
    }
}
