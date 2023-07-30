using UnityEngine;

namespace SoulRunner.Configuration.Source.Inventory
{
  public class InventoryItemScr : ScriptableObject
  {
    public int id;
    public string title;
    public Sprite icon;
    public byte weight;
    public byte type;
  }
}