using HarmonyLib;
using UnityEngine;

namespace ZombiePlus
{
    [HarmonyPatch(typeof(Worker))]
    [HarmonyPatch("GetWorkerEfficiencyText")]
    internal class PatchWorkerEfficiencyText
    {
        [HarmonyPrefix]
        private static bool PatchPrefix(Worker __instance, ref string __result)
        {
            __instance.UpdateWorkerLevel();
            int num = default(int);
            int num2 = default(int);
            int num3 = default(int);
            __instance.worker_wgo.data.GetBodySkulls(out num, out num2, out num3, true);
            float param = __instance.worker_wgo.data.GetParam("speed", 0f);
            string text = Mathf.RoundToInt(__instance.worker_wgo.data.GetParam("working_k", 0f) * 100f) + "%";
            long worker_unique_id = __instance.worker_wgo.worker_unique_id;
            string text2 = (__result = (((BaseGUI)GUIElements.me.autopsy).is_just_opened ? $"ID: {worker_unique_id} ({text}) [(rskull){num}|(skull){num2}|a{num3}] [s{param}]" : $"ID: {worker_unique_id} ({text})\nQuality:[(rskull){num}|(skull){num2}|a{num3}]\nSpeed: {param}\n"));
            return false;
        }
    }
}
