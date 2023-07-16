using CodeStage.AntiCheat.Storage;
using UnityEditor;

namespace SoulRunner.Editor
{
  public static class CacheUtils
  {
    [MenuItem("Cache/ClearPlayerPrefs")]
    private static void ClearPlayerPrefs() => ObscuredPrefs.DeleteAll();
  }
}