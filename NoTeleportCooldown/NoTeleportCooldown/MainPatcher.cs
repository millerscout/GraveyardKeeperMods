using System.Reflection;
using HarmonyLib;

namespace NoTeleportCooldown
{
	public class MainPatcher
	{
		public static void Patch()
		{
			var harmony = new Harmony("com.glibfire.graveyardkeeper.teleport.mod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
	}
}
