using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace GKSleepMod
{
	[HarmonyPatch(typeof(SleepGUI))]
	[HarmonyPatch("<Open>m__0")]
	internal class SleepGUI_delegate_Patcher
	{
		
		[HarmonyTranspiler]
		private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			Debug.Log((object)"Starting transpiler");
			float num = 50f;
			float num2 = 5f / 12f;
			int index = 0;
			int index2 = 0;
			List<CodeInstruction> list = new List<CodeInstruction>(instructions);
			for (int i = 1; i < list.Count; i++)
			{
				if (list[i].operand != null)
				{
					if (list[i].opcode == OpCodes.Call && list[i].operand.ToString() == "Void set_fixedDeltaTime(Single)" && list[i - 1].opcode == OpCodes.Ldc_R4)
					{
						index2 = i - 1;
						Debug.Log((object)"Found fixedDeltaTime");
					}
					else if (list[i].opcode == OpCodes.Call && list[i].operand.ToString() == "Void set_timeScale(Single)" && list[i - 1].opcode == OpCodes.Ldc_R4)
					{
						index = i - 1;
						Debug.Log((object)"Found timeScale");
					}
				}
			}
			Debug.Log((object)"Finished searching");
			list[index2].operand = num2;
			list[index].operand = num;
			Debug.Log((object)"Done with transpiler");
			return list.AsEnumerable();
		}
	}
}
