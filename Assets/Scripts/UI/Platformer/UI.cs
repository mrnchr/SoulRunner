using SR.Character;
using UnityEngine;
using UnityEngine.UI;

namespace SR.Platformer
{
    public class UI : MonoBehaviour
    {

        [SerializeField]
        private RectTransform _hpMask;

        [SerializeField]
        private RectTransform _mpMask;

        [SerializeField]
        private RectTransform _hpMaskMin;

        [SerializeField]
        private RectTransform _mpMaskMin;

        [SerializeField]
        private GameObject _crafting;

        [SerializeField]
        private InventoryItem _item;

        [SerializeField]
        private Image _attackCD;

        [SerializeField]
        private Image _ability1CD;

        [SerializeField]
        private Image _ability2CD;

        private float _maxHpWidth;
        private float _maxMpWidth;
        private float _minHpWidth;
        private float _minMpWidth;

        private void Awake() {
            UserInventory.onCurrentItemChangeEvent += currentItemChangeHandler;
            User.onHPChangedEvent += hpChangedHandler;
            User.onMPChangedEvent += mpChangedHandler;
            CharacterControl.onAbility1CDChange += ability1CDChangedHandler;
            CharacterControl.onAbility2CDChange += ability2CDChangedHandler;
            CharacterControl.onShootCDChange += shootCDChangedHandler;

            _minHpWidth = _hpMaskMin.rect.width;
            _minMpWidth = _mpMaskMin.rect.width;

            _maxHpWidth = _hpMask.rect.width - _minHpWidth;
            _maxMpWidth = _mpMask.rect.width - _minMpWidth;
        }

        public void toggleCrafting() {
            Level.isPaused = !_crafting.activeSelf;
            _crafting.SetActive(!_crafting.activeSelf);
        
        }


        private void currentItemChangeHandler(UserItem item) {
            if (item != null) {
                _item.gameObject.SetActive(true);
                _item.init(item);
            } else {
                _item.gameObject.SetActive(false);
            }
            
        }

        private void hpChangedHandler(byte ind, float perc) {
            Debug.Log("hp change: " + perc);
            _hpMask.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, perc * (_maxHpWidth) + _minHpWidth); 
        }

        private void mpChangedHandler(byte ind, float perc) {
            _mpMask.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, perc * (_maxMpWidth) + _minMpWidth);
        }

        private void ability1CDChangedHandler(float perc) {
            _ability1CD.fillAmount = perc;
        }

        private void ability2CDChangedHandler(float perc) {
            _ability2CD.fillAmount = perc;
        }

        private void shootCDChangedHandler(float perc) {
            _attackCD.fillAmount = perc;
        }

    }

}