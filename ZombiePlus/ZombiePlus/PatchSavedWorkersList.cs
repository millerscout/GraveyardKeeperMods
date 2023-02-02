using System.Collections.Generic;
using HarmonyLib;

namespace ZombiePlus
{
	[HarmonyPatch(typeof(SavedWorkersList))]
	[HarmonyPatch("GetWorker")]
	internal class PatchSavedWorkersList
	{
		[HarmonyPostfix]
		private static void PatchPostfix(SavedWorkersList __instance, ref long worker_unique_id, ref List<Worker> ____workers)
		{
			HelperWorker.SavedWorkers = ____workers;
		}
	}
}
