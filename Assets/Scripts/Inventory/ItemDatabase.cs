using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;


public class ItemDatabase : MonoBehaviour {

	private List <Item> database = new List <Item> ();
	private JsonData itemData;

	void Start () {
		itemData = JsonMapper.ToObject (File.ReadAllText (Application.dataPath + "/StreamingAssets/Items.json"));
		ConstructItemDatabase ();
	}

	void ConstructItemDatabase () {
		for (int i = 0; i < itemData.Count; i++) {
			database.Add (new Item (
				(int)itemData[i]["id"], itemData[i]["itemName"].ToString(),
				itemData[i]["description"].ToString(), (bool)itemData[i]["stackable"],
				(int)itemData[i]["stats"]["attack"], (int)itemData[i]["stats"]["defence"], 
				(int)itemData[i]["stats"]["bonus coins"],itemData[i]["slug"].ToString())
			);
		}
	}

	public Item FetchItemByID (int id) {
		for (int i = 0; i < database.Count; i++) {
			if (database [i].ID == id) {
				return database [i];
			}
		}
		return null;
	}
}

public class Item {
	public int    ID 			{ get; set; }
	public string ItemName 		{ get; set; }
	public string Description 	{ get; set; }
	public bool   Stackable     { get; set; }
	public int    Attack	    { get; set; }
	public int    Defence		{ get; set; }
	public int    BonusCoins	{ get; set; }
    public string Slug 			{ get; set; }
	public Sprite Sprite 		{ get; set; }

	public Item (int id, string itemName, string description, bool stackable, int attack, int defence, int bonusCoins, string slug) {
		this.ID            = id;
		this.ItemName      = itemName;
		this.Description   = description;
		this.Stackable     = stackable;
		this.Attack        = attack;
		this.Defence       = defence;
		this.BonusCoins    = bonusCoins;
		this.Slug          = slug;
		this.Sprite        = Resources.Load<Sprite> ("Sprites/Items/" + slug); //lowercase_letters_this_is_a_slug
	}

	public Item () {
		this.ID = -1;
	}
}