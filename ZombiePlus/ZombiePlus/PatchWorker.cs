using HarmonyLib;

namespace ZombiePlus
{
    [HarmonyPatch(typeof(Worker))]
    [HarmonyPatch("UpdateWorkerLevel")]
    internal class PatchWorker
    {
        public static void writeLog(Worker __instance, string title)
        {
            float param = __instance.worker_wgo.data.GetParam("working_k", 0f);
            float param2 = __instance.worker_wgo.data.GetParam("speed", 0f);
            int num = default(int);
            int num2 = default(int);
            int num3 = default(int);
            __instance.worker_wgo.data.GetBodySkulls(out num, out num2, out num3, true);
            Entry.Log("=========[]> " + title);
            Entry.Log("Z_ID:" + __instance.worker_unique_id);
            Entry.Log("Z_Backpack:" + __instance.GetBackpack().GetItemName());
            Entry.Log("Is_worker:" + __instance.worker_wgo.data.is_worker);
            Entry.Log("Efficiency:" + param + ",");
            Entry.Log("Negative:" + num + ",");
            Entry.Log("Positive:" + num2 + ",");
            Entry.Log("Available:" + num3 + ",");
            Entry.Log("Speed:" + param2 + ",");
        }

        [HarmonyPostfix]
        public static void PatchWorkerLevelPostfix(Worker __instance)
        {
            HelperWorker.SetWorkerSpeed(__instance);
            HelperWorker.SetWorkerEfficiency(__instance);
        }
    }
}
