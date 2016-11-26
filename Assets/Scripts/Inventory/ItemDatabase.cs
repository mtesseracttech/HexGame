using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    public class ItemDatabase : MonoBehaviour {

        private readonly List <Item> _database = new List <Item> ();
        private JsonData _itemData;

        void Start () {
            _itemData = JsonMapper.ToObject (File.ReadAllText (Application.dataPath + "/StreamingAssets/Items.json"));
            ConstructItemDatabase ();
        }

        private void ConstructItemDatabase () {
            for (int i = 0; i < _itemData.Count; i++) {
                _database.Add (new Item (
                        (int)_itemData[i]["id"], _itemData[i]["itemName"].ToString(),
                        _itemData[i]["description"].ToString(), (bool)_itemData[i]["stackable"],
                        (int)_itemData[i]["stats"]["attack"], (int)_itemData[i]["stats"]["defence"], 
                        (int)_itemData[i]["stats"]["bonus coins"],_itemData[i]["slug"].ToString())
                );
            }
        }

        public Item FetchItemById (int id) {
            for (int i = 0; i < _database.Count; i++) {
                if (_database [i].ID == id) {
                    return _database [i];
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
}