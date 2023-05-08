using SR.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnChecker : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            collision.gameObject.GetComponent<CharacterControl>().respawn();
        }
    }

}
