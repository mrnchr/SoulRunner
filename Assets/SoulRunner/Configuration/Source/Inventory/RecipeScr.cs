using UnityEngine;

namespace SoulRunner.Configuration.Source.Inventory
{
  [CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Recipe", order = 1)]
  public class RecipeScr : ScriptableObject
  {
    public byte weight;
    public InventoryItemScr resultItem;
  }
}