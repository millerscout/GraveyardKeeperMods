// IncreaseInventory.MainPatcher
using System;
using System.IO;
using System.Reflection;
using HarmonyLib;

public class MainPatcher
{
	public static int newSize;

	public static void Patch()
	{
		string[] array;
		using (StreamReader streamReader = new StreamReader("./QMods/IncreaseInventory/config.txt"))
		{
			array = streamReader.ReadLine().Split('=');
		}
		newSize = Convert.ToInt32(array[1]);
		new Harmony("com.kaupcakes.graveyardkeeper.increaseinventory.mod").PatchAll(Assembly.GetExecutingAssembly());
	}
}
