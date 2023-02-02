using HarmonyLib;

namespace ZombiePlus
{
	[HarmonyPatch(typeof(PorterStation))]
	[HarmonyPatch("TrySendPorter")]
	internal class PatchTrySendPorter
	{
		[HarmonyPostfix]
		public static void PatchTrySendPorterPostfix(PorterStation __instance, ref WorldGameObject ____wgo)
		{
			((MovementComponent)____wgo.linked_worker.components.character).SetSpeed(Entry.Config.Zombie_MovementSpeed);
		}
	}
}
