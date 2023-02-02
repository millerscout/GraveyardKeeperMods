using HarmonyLib;

namespace ZombiePlus
{
	[HarmonyPatch(typeof(PorterStation))]
	[HarmonyPatch("OnCameToDestination")]
	internal class PatchOnCameToDestination
	{
		[HarmonyPostfix]
		public static void PatchOnCameToDestinationPostfix(PorterStation __instance, ref WorldGameObject ____wgo)
		{
			((MovementComponent)____wgo.linked_worker.components.character).SetSpeed(Entry.Config.Zombie_MovementSpeed);
		}
	}
}
