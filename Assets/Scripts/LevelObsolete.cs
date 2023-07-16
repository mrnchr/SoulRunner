using Rewired;
using System;
using UnityEngine;

namespace SR {

    public class LevelObsolete : MonoBehaviour
    {

        public static Action<bool> onPauseStateChangedEvent;

        private static bool _isPaused;

        public static bool isPaused
        {
            get => _isPaused;
            set {
                if (_isPaused != value) {
                    onPauseStateChangedEvent?.Invoke(value);
                }
                _isPaused = value;

            }
        }

        private Player _player;

        private Player player
        {
            get {
                _player ??= (ReInput.isReady ? ReInput.players.GetPlayer(0) : null);
                return _player;
            }
        }


        private void Start() {
            Application.targetFrameRate = 60;
            User.load();

        }

        private void Update() {
            if (_isPaused) {
                return;
            }
            float dt = Time.deltaTime;
            User.step(dt);
        }


    }
 }
