using System;
using UnityEngine;
using Zenject;

namespace SoulRunner.Player.Movement
{
    public class LedgeChecker : MonoBehaviour
    {

        public Action<Transform> OnLedgeEnter;
        public Action OnLedgeExit;
        [HideInInspector] public View View;

        private LedgeCheckerService _ledgeCheckerSvc;

        [Inject]
        public void Construct(LedgeCheckerService ledgeCheckerSvc)
        {
            _ledgeCheckerSvc = ledgeCheckerSvc;
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.CompareTag("Ledge")) {
                OnLedgeEnter?.Invoke(collision.transform);
                _ledgeCheckerSvc.OnLedgeEnter(View.Entity, collision.transform.position.x);
            }
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (collision.CompareTag("Ledge")) {
                OnLedgeExit?.Invoke();
                _ledgeCheckerSvc.OnLedgeExit(View.Entity);
            }
        }

    }

}