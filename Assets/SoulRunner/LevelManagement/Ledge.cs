using UnityEngine;

namespace SoulRunner.LevelManagement
{
  public class Ledge : MonoBehaviour
  {
    public float PosX;

    private void Awake()
    {
      PosX = transform.position.x;
    }
  }
}