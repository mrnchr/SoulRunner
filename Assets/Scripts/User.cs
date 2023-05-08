using CodeStage.AntiCheat.ObscuredTypes;
using CodeStage.AntiCheat.Storage;
using System;
using UnityEngine;

namespace SR
{
    public class User
    {

        private static float AUTORESTORE_DELAY = 0.15f;

        public static Action<byte, float> onHPChangedEvent;
        public static Action<byte, float> onMPChangedEvent;
        public static Action onDeathEvent;

        private static ObscuredShort[] _hp;

        private static ObscuredShort[] _mp;

        public static ObscuredShort mp(byte id) {
            return _mp[id];
        }

        private static ObscuredShort[] _maxHp;

        private static ObscuredShort[] _maxMp;

        public static UserInventory inventory;

        private static float _autoRestoreDelay;

        public static void load() {
            _autoRestoreDelay = AUTORESTORE_DELAY;
            ItemDB.init();
            if (inventory == null) {
                inventory = new UserInventory();
            }
            inventory.load();
            ObscuredShort kelliHp = (ObscuredShort)ObscuredPrefs.Get("kelli_hp", 100);
            ObscuredShort shonHp = (ObscuredShort)ObscuredPrefs.Get("shon_hp", 100);

            ObscuredShort kelliMp = (ObscuredShort)ObscuredPrefs.Get("kelli_mp", 100);
            ObscuredShort shonMp = (ObscuredShort)ObscuredPrefs.Get("shon_mp", 100);

            _maxHp = new ObscuredShort[2] { kelliHp, shonHp };
            _maxMp = new ObscuredShort[2] { kelliMp, shonMp };

            _hp = new ObscuredShort[2] { 0, 0 };
            _mp = new ObscuredShort[2] { 0, 0 };

            hpUpdate(0, kelliHp);
            hpUpdate(1, shonHp);

            mpUpdate(0, kelliMp);
            mpUpdate(1, kelliMp);
        }



        public static void damage(ObscuredShort value, byte charId = 0) {
            hpUpdate(charId, (ObscuredShort)(_hp[charId] - value));
        }

        public static void heal(ObscuredShort value, byte charId = 0) {
            hpUpdate(charId, (ObscuredShort)(_hp[charId] + value));
        }

        public static void spendMana(ObscuredShort value, byte charId = 0) {
            mpUpdate(charId, (ObscuredShort)(_mp[charId] - value));
        }

        public static void restoreMana(ObscuredShort value, byte charId = 0) {
            mpUpdate(charId, (ObscuredShort)(_mp[charId] + value));
        }

        private static void hpUpdate(byte charInd, ObscuredShort value) {
            _hp[charInd] = (ObscuredShort)Mathf.Clamp(value, 0, _maxHp[charInd]);
            onHPChangedEvent?.Invoke(charInd, (float)value / (float)_maxHp[charInd]);
            if (_hp[charInd] <= 0) {
                onDeathEvent?.Invoke();
            }
        }

        private static void mpUpdate(byte charInd, ObscuredShort value) {
            _mp[charInd] = value;
            onMPChangedEvent?.Invoke(charInd, (float)value / (float)_maxMp[charInd]);
        }

        public static void step(float dt) {
            if (_autoRestoreDelay <= 0) {
                for (byte i = 0; i < 1; i++) {
                    if (_mp[i] < _maxMp[i] * .25f) {
                        restoreMana(1, i);
                    }
                }
                _autoRestoreDelay = AUTORESTORE_DELAY;
            } else {
                _autoRestoreDelay -= dt;
            }
            
        }

    }
}
