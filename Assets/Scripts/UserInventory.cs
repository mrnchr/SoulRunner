using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CodeStage.AntiCheat.ObscuredTypes;
using CodeStage.AntiCheat.Storage;
using Newtonsoft.Json;
using SoulRunner.Configuration.Source.Inventory;

namespace SR
{
    public class UserInventory
    {

        public static Action<int[]> onUseItemsChangeEvent;
        public static Action<UserItem> onCurrentItemChangeEvent;

        private static byte MAX_INGREDIENT_SLOTS = 14;
        private static byte MAX_POTION_SLOTS = 12;

        private List<UserItem> _items;

        private int[] _useItems = new int[5];

        private int _itemsToLoad = 0;

        public byte _selectedSlot = 0;

        public int currentItem
        {
            get => _useItems[currentItem];
        }

        public void load() {

            ObscuredString userInventoryStr = ObscuredPrefs.Get("user_inventory", "{}");
            InventorySave invSave = JsonConvert.DeserializeObject<InventorySave>(userInventoryStr);
            if (invSave.items == null) {
                _items = new List<UserItem>();
            } else {
                ItemDB.onItemLoaded += itemLoadedHandler;
                _items = invSave.items.ToList();
                for (int i = 0; i < _items.Count; i++) {
                    _itemsToLoad++;
                    ItemDB.getItemData(_items[i].id, _items[i].type);
                }
            }

            ObscuredString useItemsStr = ObscuredPrefs.Get("use_items", "{}");

            UseItemsSave useItemsSave = JsonConvert.DeserializeObject<UseItemsSave>(useItemsStr);
            if (useItemsSave.id != null) {
                _useItems = useItemsSave.id;
            }
        }

        public void save() {
            InventorySave invSave = new InventorySave();
            invSave.items = _items.ToArray();
            ObscuredPrefs.Set("user_inventory", JsonConvert.SerializeObject(invSave));
        }

        public void saveUseItems() {
            UseItemsSave useItemsSave = new UseItemsSave();
            useItemsSave.id = _useItems;
            ObscuredPrefs.Set("use_items", JsonConvert.SerializeObject(useItemsSave));
        }

        public void useSelectedItem() {
            if (_useItems[_selectedSlot] != 0) {
                UserItem userItem = getUseItems()[_selectedSlot];
                if (userItem.id == 1) {
                    User.heal(30);
                } else if (userItem.id == 2) {
                    User.restoreMana(30);
                }
                remove(userItem);
                save();
            }
        }

        public void nextUseSlot() {
            findUseSlot(1);
            onCurrentItemChangeEvent?.Invoke(getUseItems()[_selectedSlot]);
        }

        public void prevUseSlot() {
            findUseSlot(-1);
            onCurrentItemChangeEvent?.Invoke(getUseItems()[_selectedSlot]);
        }

        private void findUseSlot(int dir) {
            byte checkSlot = nextCheckUseInd(_selectedSlot, dir);
            for (byte i = 0; i < _useItems.Length; i++) {
                if (_useItems[checkSlot] != 0) {
                    _selectedSlot = checkSlot;
                    return;
                }
                checkSlot = nextCheckUseInd(checkSlot, dir);
            }
            _selectedSlot = 0;
        }

        private byte nextCheckUseInd(byte curCheckInd, int dir) {
            sbyte nextCheckInd = (sbyte)(curCheckInd + dir);
            if (nextCheckInd >= _useItems.Length) {
                return 0;
            } else if (nextCheckInd < 0) {
                return (byte)(_useItems.Length - 1);
            }
            return (byte)nextCheckInd;
        }

        public bool add(InventoryItemScr itemData) {
            UserItem[] freeSpaceTest = _items.Where(x => x.type == itemData.type).ToArray();
            if (itemData is IngredientItemScr && freeSpaceTest.Length >= MAX_INGREDIENT_SLOTS) {
                return false;
            } else if (itemData is PotionItemScr && freeSpaceTest.Length >= MAX_POTION_SLOTS) {
                return false;
            }
            UserItem item = _items.Where(x => (x.id == itemData.id) && (x.type == itemData.type)).FirstOrDefault();
            if (item == null) {
                item = new UserItem();
                item.load(itemData);
                _items.Add(item);
            } else {
                item.amount++;
            }
            if (_useItems[_selectedSlot] == item.id && item.type == 2) {
                onCurrentItemChangeEvent?.Invoke(item);
            }
            save();
            return true;
        }

        public void remove(UserItem item) {
            for (int i = 0; i < _items.Count; i++) {
                if (_items[i].id == item.id && _items[i].type == item.type) {
                    if (_items[i].amount > 1) {
                        _items[i].amount--;
                        if (_useItems[_selectedSlot] == item.id && item.type == 2) {
                            onCurrentItemChangeEvent?.Invoke(item);
                        }
                    } else {
                        _items.RemoveAt(i);
                        if (_useItems[_selectedSlot] == item.id && item.type == 2) {
                            _useItems[_selectedSlot] = 0;
                            nextUseSlot();
                        }
                    }

                    break;
                }
            }
        }

        public void updateUseItems(int[] slot) {
            byte i;
            for (i = 0; i < slot.Length; i++) {
                _useItems[i] = slot[i];
            }
            if (_useItems[_selectedSlot] == 0) {
                nextUseSlot();
            }
            saveUseItems();
        }

        public bool craft(UserItem item1, UserItem item2, RecipeScr recipe) {

            if (add(recipe.resultItem)) {
                remove(item1);
                remove(item2);
                save();
                Crafting.instance.loadInventory();
                
                return true;
            } else {
                
            }
            return false;
        }

        public List<UserItem> getByType(byte type) {
            return _items.Where(x => x.type == type).ToList();
        }

        public UserItem[] getUseItems() {
            UserItem[] useItems = new UserItem[5];
            for (byte i = 0; i < _useItems.Length; i++) {
                if (_useItems[i] != 0) {
                    UserItem userItem = getByType(2).Where(x => x.id == _useItems[i]).SingleOrDefault();
                    useItems[i] = userItem;
                }
            }
            return useItems;
        }

        private void itemLoadedHandler(string key, InventoryItemScr itemData) {
            for (int i = 0; i < _items.Count; i++) {
                if (_items[i].type + "_" + _items[i].id.ToString() == key) {
                    _itemsToLoad--;
                    _items[i].load(itemData);
                    break;
                }
            }
            if (_itemsToLoad == 0) {
                ItemDB.onItemLoaded -= itemLoadedHandler;
                _itemsToLoad--;
                if (_useItems[_selectedSlot] != 0) {
                    onCurrentItemChangeEvent?.Invoke(getUseItems()[_selectedSlot]);
                } else {
                    nextUseSlot();
                }
            }
        }

    }
    [System.Serializable]
    public struct InventorySave
    {
        public UserItem[] items;


    }
    [Serializable]
    public struct UseItemsSave
    {
        public int[] id;
    }


    public class UserItem
    {
        public int id;
        public int amount = 1;
        public byte type;

        [JsonIgnore]
        public InventoryItemScr data;

        public void load(InventoryItemScr itemData) {
            data = itemData;
            id = itemData.id;
            if (itemData is PotionItemScr) {
                type = (byte)ItemType.Potion;
            } else if (itemData is IngredientItemScr) {
                type = (byte)ItemType.Ingredient;
            }
        }
    }
}
