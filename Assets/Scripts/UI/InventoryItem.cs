using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SR
{
    public class InventoryItem : IDraggable, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, IPointerUpHandler
    {

        public static Action<InventoryItem> onBeginDrag;
        public static Action<InventoryItem> onEndDrag;

        [SerializeField]
        private Image _img;

        [SerializeField]
        private TextMeshProUGUI _count;

        [SerializeField]
        private bool _draggable = true;

        private ItemSlot _slot;

        public ItemSlot slot
        {
            get => _slot;
            set => _slot = value;
        }

        private UserItem _itemData;

        public UserItem itemData
        {
            get => _itemData;
        }

        private GameObject _draggableObj;

        private Vector3 _vel = Vector3.zero;

        public int count
        {
            get => int.Parse(_count.text);
            set => _count.text = value.ToString();
        }

        public void init(UserItem itemModel) {
            _itemData = itemModel;
            _img.sprite = itemModel.data.icon;
            _count.text = itemModel.amount.ToString();
        }

        public void OnBeginDrag(PointerEventData eventData) {
            return;
            if (!_draggable) {
                return;
            }
            _draggableObj = Instantiate(gameObject);
            _draggableObj.transform.SetParent(Crafting.instance.transform, true);
            _draggableObj.transform.position = transform.position;
            _draggableObj.transform.localScale = Vector3.one;
        }

        public void OnDrag(PointerEventData eventData) {
            return;
            if (!_draggable) {
                return;
            }
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_draggableObj.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out var mPos)) {
                _draggableObj.transform.position = Vector3.SmoothDamp(_draggableObj.transform.position, mPos, ref _vel, 0.02f);
            }
        }

        public void OnEndDrag(PointerEventData eventData) {
            return;
            if (!_draggable) {
                return;
            }
            Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);

            if (hit && hit.collider != null) {
                ItemSlot slot = hit.collider.GetComponent<ItemSlot>();

                if (slot != null && !slot.isWarehouse) {
                    slot.place(this);
                } else {
                    if (!_slot.isWarehouse) {
                        _slot.clear();
                        if (_slot.type == ItemType.Potion) {
                            Crafting.instance.updateUseItems();
                        }
                    }
                }
            } else {
                if (!_slot.isWarehouse) {
                    _slot.clear();
                    if (_slot.type == ItemType.Potion) {
                        Crafting.instance.updateUseItems();
                    }
                }
            }

            if (_draggableObj != null) {
                Destroy(_draggableObj);
                _draggableObj = null;
            }
        }

        public void OnPointerUp(PointerEventData eventData) {
            if (_slot.isWarehouse) {
                Crafting.instance.place(this);
            } else {
                _slot.clear();
                if (_slot.type == ItemType.Potion) {
                    Crafting.instance.updateUseItems();
                }
                _slot = null;
            }
        }

        public void OnPointerClick(PointerEventData eventData) {
            Debug.Log("pointer click");
        }
    }

    public enum ItemType
    {
        Ingredient = 1,
        Potion = 2
    }

}