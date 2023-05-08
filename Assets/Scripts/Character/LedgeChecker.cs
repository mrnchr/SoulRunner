using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SR.Character
{
    public class LedgeChecker : MonoBehaviour
    {

        public Action<Transform> onTriggerEnter;
        public Action onTriggerExit;

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.CompareTag("Ledge")) {
                onTriggerEnter?.Invoke(collision.transform);
            }
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (collision.CompareTag("Ledge")) {
                onTriggerExit?.Invoke();
            }
        }

    }

}