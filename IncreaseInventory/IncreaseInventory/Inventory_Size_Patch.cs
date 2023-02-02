// IncreaseInventory.Inventory_Size_Patch
using HarmonyLib;

[HarmonyPatch(typeof(Inventory))]
[HarmonyPatch]
internal class Inventory_Size_Patch
{
	[HarmonyPrefix]
	public static bool Prefix(ref int __result, ref Inventory __instance)
	{
		if (__instance.name.Contains("offer"))
		{
			__result = 6;
		}
		else
		{
			__result = MainPatcher.newSize;
		}
		return false;
	}
}
