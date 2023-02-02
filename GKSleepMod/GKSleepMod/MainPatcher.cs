using HarmonyLib;
using System.Reflection;

namespace GKSleepMod
{
	internal class MainPatcher
	{
		public static void Patch()
		{
            var harmony = new Harmony("com.fluffiest.graveyardkeeper.fastsleep.mod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
	}
}
