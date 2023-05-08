using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SR.Character
{
    public class GroundChecker : MonoBehaviour
    {

        public Action onTriggerEnter;
        public Action onTriggerExit;

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.CompareTag("Ground")) {
                onTriggerEnter?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (collision.CompareTag("Ground")) {
                onTriggerExit?.Invoke();
            }
        }

    }
}
