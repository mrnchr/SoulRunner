using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

using System.Threading.Tasks;
using System;

public class ItemDB
{

    public static Action<string, InventoryItemScr> onItemLoaded;

    private static Dictionary<string, InventoryItemScr> _loadedItemsData = new Dictionary<string, InventoryItemScr>();

    private static AsyncOperationHandle<InventoryItemScr> _objectOperation;

    private static Queue<QueueData> _queue = new Queue<QueueData>();

    private static bool _busy = false;

    private struct QueueData {
        public int id;
        public byte type;
    }



    public static void init() {
        
    }

    public static void getItemData(int id, byte type) {
        if (_busy) {
            _queue.Enqueue(new QueueData() { id = id, type = type });
            return;
        }
        prepareLoading(id, type);
    }

    private static void prepareLoading(int id, byte type) {
        string key = type.ToString() + "_" + id.ToString();
        if (_loadedItemsData.ContainsKey(key)) {
            onItemLoaded?.Invoke(key, _loadedItemsData[key]);
            if (_queue.Count > 0) {
                QueueData nextItem = _queue.Dequeue();
                prepareLoading(nextItem.id, nextItem.type);
            }
            return;
        }
        loadItemData(key);
    }

    private static void loadItemData(string key) {
        _busy = true;
        _objectOperation = Addressables.LoadAssetAsync<InventoryItemScr>(string.Concat("Inventory/Item", key));
        _objectOperation.Completed += objectLoadDone;

    }

    private static void objectLoadDone(AsyncOperationHandle<InventoryItemScr> obj) {
        _objectOperation.Completed -= objectLoadDone;
        if (_objectOperation.Status == AsyncOperationStatus.Succeeded) {
            InventoryItemScr itemData = _objectOperation.Result;
            Type typeClass = itemData.GetType();
            byte type = 0;
            if (typeClass == typeof(IngredientItemScr)) {
                type = 1;
            } else if (typeClass == typeof(PotionItemScr)) {
                type = 2;
            }
            string key = type.ToString() + "_" + itemData.id.ToString();
            if (!_loadedItemsData.ContainsKey(key)) {
                _loadedItemsData.Add(key, itemData);
            }
            onItemLoaded?.Invoke(key, itemData);
            _busy = false;
            if (_queue.Count > 0) {
               
                QueueData nextItem = _queue.Dequeue();
                prepareLoading(nextItem.id, nextItem.type);
            }
        }

    }

}
