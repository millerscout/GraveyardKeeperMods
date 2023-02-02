using HarmonyLib;

[HarmonyPatch(typeof(Item))]
[HarmonyPatch]
internal class Item_Inventory_Size_Patch
{
	[HarmonyPrefix]
	public static bool Prefix(ref int __result)
	{
		__result = MainPatcher.newSize;
		return false;
	}
}
