using System.Collections;
using System.Collections.Generic;
using SoulRunner.Configuration.Source.Inventory;
using UnityEngine;

namespace SR
{
    public class Collectable : MonoBehaviour
    {

        [SerializeField]
        private SpriteRenderer _renderer;

        [SerializeField]
        private int _id;

        [SerializeField]
        private byte _type;

        private InventoryItemScr _itemData;

        private void Start() {
            ItemDB.onItemLoaded += itemLoadedHandler;
            loadItemData();
        }

        public void init(InventoryItemScr itemData) {
            _renderer.sprite = itemData.icon;
        }

        public void collect() {
            User.inventory.add(_itemData);
            Destroy(gameObject);
        }

        private void loadItemData() {
            ItemDB.getItemData(_id, _type);

        }

        private void itemLoadedHandler(string key, InventoryItemScr itemData) {
            if (key == _type + "_" + _id) {
                _itemData = itemData;
                init(_itemData);
                ItemDB.onItemLoaded -= itemLoadedHandler;
            }
        }

    }
}
