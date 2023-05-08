using Rewired;
using System;
using System.Threading;
using UnityEngine;

namespace SR {

    public class Level : MonoBehaviour
    {

        public static Action<bool> onPauseStateChangedEvent;

        private static bool _isPaused;

        public static bool isPaused
        {
            get { return _isPaused; }
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
                _player = _player == null ? (ReInput.isReady ? ReInput.players.GetPlayer(0) : null) : _player;
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
