using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SR
{
    public class ItemSlot : MonoBehaviour
    {

        public bool isTemp;

        public bool isWarehouse;

        public ItemType type;

        public bool isUseSlot;

        private InventoryItem _item = null;

        public InventoryItem item
        {
            get => _item;
            set => _item = value;
        }

        public bool isEmpty
        {
            get => _item == null;
        }

        public bool place(InventoryItem item) {
            if ((byte)type != item.itemData.type) {
                return false;
            }
            clear();
            if (item.slot != null && item.slot.isWarehouse) {
                GameObject newItemObj = Instantiate(item.gameObject);
                newItemObj.transform.SetParent(transform);
                newItemObj.transform.localPosition = Vector3.zero;
                newItemObj.transform.localScale = Vector3.one;
                _item = newItemObj.GetComponent<InventoryItem>();
                _item.init(item.itemData);
                _item.slot = this;

            } else {
                if (item.slot != null) {
                    item.slot.item = null;
                }
                _item = item;
                _item.transform.SetParent(transform);
                _item.transform.localPosition = Vector3.zero;
                _item.transform.localScale = Vector3.one;
                _item.slot = this;
            }
            if (isUseSlot) {
                Crafting.instance.updateUseItems();
            }
            return true;
        }

        public void clear() {
            if (_item != null) {
                Destroy(_item.gameObject);
                _item = null;
            }
        }

    }

    public enum SlotType
    {
        Ingredient = 0,
        PotionWarehouse = 1,
        Craft = 2,
        PotionActive = 3
    }

}