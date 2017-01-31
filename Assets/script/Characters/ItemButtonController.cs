using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonController : MonoBehaviour {

	[SerializeField]
	public Text itemText;

	private const int ITEM_MAX = 3;
	private const int ITEM_MIN = 0;
	private int items = 0;

	void Awake() {
		itemText.text = "X" + items;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
