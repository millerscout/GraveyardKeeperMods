using HarmonyLib;

namespace NoTeleportCooldown
{
	[HarmonyPatch(typeof(Item))]
	[HarmonyPatch("GetGrayedCooldownPercent")]
	internal class Item_GetGrayedCooldownPercent_Patch
	{
		[HarmonyPrefix]
		public static bool Prefix(Item __instance, ref float __result)
		{
			if (__instance.id == "hearthstone")
			{
				__result = 0f;
				return false;
			}
			return true;
		}
	}
}
