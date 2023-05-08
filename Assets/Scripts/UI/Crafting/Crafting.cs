using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace SR
{
    public class Crafting : MonoBehaviour
    {

        public static Crafting instance { get; private set; }


        [SerializeField]
        private AssetReference _invItemRef;

        private InventoryItem _inventoryItemOrigin;

        private AsyncOperationHandle<GameObject> _objectOperation;

        [SerializeField]
        private AssetReference[] _recipeRef;

        [SerializeField]
        private ItemSlot[] _ingredientSlot;

        [SerializeField]
        private ItemSlot[] _potionSlot;

        [SerializeField]
        private ItemSlot[] _craftSlot;

        [SerializeField]
        private ItemSlot[] _useSlot;

        private List<InventoryItem> _items = new List<InventoryItem>();

        private List<RecipeScr> _recipe = new List<RecipeScr>();

        private AsyncOperationHandle<RecipeScr> _recipeOperation;

        private int _numRecipeLoaded = 0;

        private void Awake() {
            instance = this;
        }


        private void OnEnable() {
            if (_inventoryItemOrigin == null) {
                _objectOperation = Addressables.LoadAssetAsync<GameObject>(_invItemRef);
                _objectOperation.Completed += objectLoadDone;
            } else {
                loadInventory();
            }
        }

        private void objectLoadDone(AsyncOperationHandle<GameObject> obj) {
            if (obj.Status == AsyncOperationStatus.Succeeded) {
                _inventoryItemOrigin = obj.Result.GetComponent<InventoryItem>();
                _objectOperation.Completed -= objectLoadDone;
                loadInventory();
            }
        }

        public void loadInventory() {
            clearInventory();
            int i;
            List<UserItem> ingredientItems = User.inventory.getByType((byte)ItemType.Ingredient);
            for (i = 0; i < ingredientItems.Count; i++) {
                if (i >= _ingredientSlot.Length) {
                    break;
                }
                addInventoryItem(ingredientItems[i], _ingredientSlot[i]);
            }
            List<UserItem> potionItems = User.inventory.getByType((byte)ItemType.Potion);
            for (i = 0; i < potionItems.Count; i++) {
                if (i >= _potionSlot.Length) {
                    break;
                }
                addInventoryItem(potionItems[i], _potionSlot[i]);
            }
            UserItem[] useItems = User.inventory.getUseItems();
            for (i = 0; i < useItems.Length; i++) {
                if (useItems[i] != null) {
                    addInventoryItem(useItems[i], _useSlot[i]);
                }
            }
            if (_numRecipeLoaded < _recipeRef.Length) {
                recipeLoad();
            }
        }

        public void updateUseItems() {
            int[] useSlot = new int[5];
            for (byte i = 0; i < _useSlot.Length; i++) {
                if (!_useSlot[i].isEmpty) {
                    useSlot[i] = _useSlot[i].item.itemData.id;
                    List<UserItem> potionItems = User.inventory.getByType((byte)ItemType.Potion);
                    for (int j = 0; j < potionItems.Count; j++) {
                        if (potionItems[j].id == _useSlot[i].item.itemData.id) {
                            _useSlot[i].item.count = potionItems[j].amount;
                            break;
                        }
                    }
                }
            }
            User.inventory.updateUseItems(useSlot);
        }

        private void recipeLoad() {
            _recipeOperation = Addressables.LoadAssetAsync<RecipeScr>(_recipeRef[_numRecipeLoaded]);
            _recipeOperation.Completed += recipeLoadCompleted;
        }

        private void recipeLoadCompleted(AsyncOperationHandle<RecipeScr> obj) {
            if (obj.Status == AsyncOperationStatus.Succeeded) {
                _recipeOperation.Completed -= recipeLoadCompleted;
                _recipe.Add(obj.Result);
                _numRecipeLoaded++;
                if (_numRecipeLoaded < _recipeRef.Length) {
                    recipeLoad();
                } else {
                    Debug.Log("all recipes loaded");
                }
            }
        }

        private void clearInventory() {
            int i;
            for (i = 0; i < _ingredientSlot.Length; i++) {
                _ingredientSlot[i].clear();
            }
            for (i = 0; i < _potionSlot.Length; i++) {
                _potionSlot[i].clear();
            }
            for (i = 0; i < _craftSlot.Length; i++) {
                _craftSlot[i].clear();
            }
            for (i = 0; i < _useSlot.Length; i++) {
                _useSlot[i].clear();
            }
        }

        private void addInventoryItem(UserItem itemModel, ItemSlot slot) {
            InventoryItem inventoryItem = Instantiate(_inventoryItemOrigin);
            inventoryItem.init(itemModel);
            slot.place(inventoryItem);

        }

        public void place(InventoryItem item) {
            int i;
            int firstEmptySlot = -1;
            bool duplFound = false;
            if (item.slot.type == ItemType.Ingredient) {
                for (i = 0; i < _craftSlot.Length; i++) {
                    if (_craftSlot[i].isEmpty && firstEmptySlot == -1) {
                        firstEmptySlot = i;
                    } else if (!_craftSlot[i].isEmpty && _craftSlot[i].item.itemData.id == item.itemData.id) {
                        duplFound = true;
                        break;
                    }
                }
                if (!duplFound && firstEmptySlot >= 0) {
                    _craftSlot[firstEmptySlot].place(item);
                }
            } else if (item.slot.type == ItemType.Potion) {
                for (i = 0; i < _useSlot.Length; i++) {
                    if (_useSlot[i].isEmpty && firstEmptySlot == -1) {
                        firstEmptySlot = i;
                    } else if (!_useSlot[i].isEmpty && _useSlot[i].item.itemData.id == item.itemData.id) {
                        duplFound = true;
                        break;
                    }
                }
                if (!duplFound && firstEmptySlot >= 0) {
                    _useSlot[firstEmptySlot].place(item);
                }
            }
        }

        public void craftBtnClick() {
            Debug.Log(_craftSlot[0].isEmpty + "_" + _craftSlot[1].isEmpty);
            if (_craftSlot[0].isEmpty || _craftSlot[1].isEmpty) {
                return;
            }
            RecipeScr recipe = _recipe.Where(x => x.weight == _craftSlot[0].item.itemData.data.weight + _craftSlot[1].item.itemData.data.weight).FirstOrDefault();
            if (recipe != null) {
                Debug.Log("recipe found");
                if (User.inventory.craft(_craftSlot[0].item.itemData, _craftSlot[1].item.itemData, recipe)) {
                    updateUseItems();
                }
            } else {
                Debug.Log("recipe not found");
            }
        }

    }
}
