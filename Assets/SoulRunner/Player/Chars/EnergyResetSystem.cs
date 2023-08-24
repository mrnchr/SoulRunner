using UnityEngine;

namespace SoulRunner.Player
{
  public class EnergyResetSystem : MonoBehaviour
  {
    [SerializeField] private PlayerChars _chars;

    private void Update()
    {
      _chars.Energy.Current += _chars.EnergyResetRatio * _chars.EnergyResetSpeed * Time.deltaTime;
    }
  }
}