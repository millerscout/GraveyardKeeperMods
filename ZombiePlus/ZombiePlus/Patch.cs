using System;
using System.Linq.Expressions;
using HarmonyLib;

namespace ZombiePlus
{
    [HarmonyPatch(typeof(WorldGameObject))]
    [HarmonyPatch("CustomUpdate")]
    internal class Patch
    {
        public static float executedTime = 1f;

        private static float timestamp = 0f;

        private static int justOnce = 0;

        private static bool haveRun = false;

        private static bool reload = false;

        private static string GetMethodName<T>(Expression<Action<T>> action)
        {
            MethodCallExpression methodCallExpression = action.Body as MethodCallExpression;
            if (methodCallExpression == null)
            {
                throw new ArgumentException("Only method calls are supported");
            }
            return methodCallExpression.Method.Name;
        }

        private static void RunJustOnce()
        {
            if (justOnce == 0)
            {
                HelperWorker.UpdateWorkersWithReloadFlag(haveRun);
                justOnce = 1;
                haveRun = true;
            }
        }

        [HarmonyPostfix]
        public static void PatchPostfix(WorldGameObject __instance, ref Item ____data)
        {
            InGameReload();
            RunJustOnce();
            HelperWorker.UpdateWorkersWhitReloadFlag(ref reload);
        }

        private static void InGameReload()
        {
            Utilities.DelayerDelegateWithKey(Entry.Config.InGame_ReloadConfig_Key, delegate
            {
                Entry.LoadIniSettings();
                MainGame.me.player.Say("Z+: Config Reloaded");
                if (Entry.Config.InGame_ReloadConfig_Rerun)
                {
                    justOnce = 0;
                }
                reload = true;
            });
        }
    }
}
