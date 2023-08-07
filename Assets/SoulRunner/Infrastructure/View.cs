using SoulRunner.Characteristics;
using UnityEngine;

namespace SoulRunner.Infrastructure
{
  public abstract class View : MonoBehaviour
  {
    public ObjectType Id;
    public ICharacteristicList Chars;
  }
}