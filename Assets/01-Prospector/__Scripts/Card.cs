﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Card : MonoBehaviour {


	public string    suit;
	public int       rank;
	public Color     color = Color.black;
	public string    colS = "Black";  // or "Red"
	
	public List<GameObject> decoGOs = new List<GameObject>();
	public List<GameObject> pipGOs = new List<GameObject>();
	
	public GameObject back;  // back of card;
	public CardDefinition def;  // from DeckXML.xml		
	public SpriteRenderer[] spriteRenderers;


	// Use this for initialization
	void Start () {
		SetSortOrder(0); // Ensures that the card starts properly depth sorted
	}

	// If spriteRenderers is not yet defined, this function defines it
	public void PopulateSpriteRenderers() {
	// If spriteRenderers is null or empty
	if (spriteRenderers == null || spriteRenderers.Length == 0) {
	// Get SpriteRenderer Components of this GameObject and its children
	spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
	}
	}

	// Sets the sortingLayerName on all SpriteRenderer Components
	public void SetSortingLayerName(string tSLN) {
	PopulateSpriteRenderers();
	foreach (SpriteRenderer tSR in spriteRenderers) {
	tSR.sortingLayerName = tSLN;
	}
	}

	// Sets the sortingOrder of all SpriteRenderer Components
	public void SetSortOrder(int sOrd) { // a
	PopulateSpriteRenderers();
	// Iterate through all the spriteRenderers as tSR
	foreach (SpriteRenderer tSR in spriteRenderers) {
	if (tSR.gameObject == this.gameObject) {
	// If the gameObject is this.gameObject, it's the background
	tSR.sortingOrder = sOrd; // Set it's order to sOrd
	continue; // And continue to the next iteration of the loop
	}
	// Each of the children of this GameObject are named
	// switch based on the names
	switch (tSR.gameObject.name) {
	case "back" : // if the name is "back"
	// Set it to the highest layer to cover the other sprites
	tSR.sortingOrder = sOrd+2;
	break;
	case "face": // if the name is "face"
	default: // or if it's anything else
	// Set it to the middle layer to be above the background
	tSR.sortingOrder = sOrd+1;
	break;
	}
	}
	}


	public bool faceUp {
		get {
			return (!back.activeSelf);
		}

		set {
			back.SetActive(!value);
		}
	}


	
	// Update is called once per frame
	void Update () {
	
	}
} // class Card

[System.Serializable]
public class Decorator{
	public string	type;			// For card pips, tyhpe = "pip"
	public Vector3	loc;			// location of sprite on the card
	public bool		flip = false;	//whether to flip vertically
	public float 	scale = 1.0f;
}

[System.Serializable]
public class CardDefinition{
	public string	face;	//sprite to use for face cart
	public int		rank;	// value from 1-13 (Ace-King)
	public List<Decorator>	
					pips = new List<Decorator>();  // Pips Used
}
