using CodeStage.AntiCheat.Storage;
using UnityEditor;

public class CacheUtils
{

	[MenuItem("Cache/ClearPlayerPrefs")]
	static void ClearPlayerPrefs() {
		ObscuredPrefs.DeleteAll();
	}
}
