using System;
using System.IO;
using System.Reflection;
using HarmonyLib;
using HarmonyLib;

namespace ZombiePlus
{
	public static class Entry
	{
		public static IniConfig IniInstance = IniConfig.Instance;

		public static Options Config;

		public static void Patch()
		{
			LoadIniSettings();
			try
			{
                var harmony = new Harmony("com.amaurym.graveyardkeeper.zombieplus.mod");
                harmony.PatchAll(Assembly.GetExecutingAssembly());
			}
			catch (Exception ex)
			{
				Log(ex.ToString());
			}
		}

		public static void Log(string v)
		{
			string path = ".\\QMods\\ZombiePlus\\log.txt";
			if (!File.Exists(path))
			{
				path = "log.txt";
			}
			using StreamWriter streamWriter = File.AppendText(path);
			streamWriter.WriteLine(v);
		}

		public static void LoadIniSettings()
		{
			try
			{
				if (!File.Exists(IniInstance.Path))
				{
					Config = new Options();
					IniInstance.WriteJsonConfig(Config);
				}
				else
				{
					Config = IniInstance.ReadJsonConfig();
				}
			}
			catch (Exception ex)
			{
				Log(ex.ToString());
			}
		}

		public static void reloadSettings()
		{
			try
			{
				if (!File.Exists(IniInstance.Path))
				{
					Config = new Options();
					IniInstance.WriteJsonConfig(Config);
				}
				else
				{
					Config = IniInstance.DeserializeConfig<Options>(IniInstance.Path);
				}
			}
			catch (Exception ex)
			{
				Log(ex.ToString());
			}
		}
	}
}
