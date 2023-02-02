using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace ItemDescriptionElement
{
	public class MainPatcher
	{
		public enum Elements
		{
			None,
			Slowing,
			Acceleration,
			Health,
			Death,
			Order,
			Toxic,
			Chaos,
			Life,
			Electric
		}

		private static readonly string configFilePathAndName = Application.dataPath + "/../QMods/ItemDescriptionElement/Translations.txt";

		private static readonly char parameterSeparator = '|';

		private static readonly string parameterComment = "#";

		private static readonly string parameterSectionBegin = "[";

		private static readonly string parameterSectionEnd = "]";

		public static Dictionary<string, Elements> itemIDLookup = new Dictionary<string, Elements>(300);

		public static Dictionary<Elements, string> translationEN = new Dictionary<Elements, string>(9);

		public static Dictionary<Elements, string> translationDE = new Dictionary<Elements, string>(9);

		public static Dictionary<Elements, string> translationFR = new Dictionary<Elements, string>(9);

		public static Dictionary<Elements, string> translationPT = new Dictionary<Elements, string>(9);

		public static Dictionary<Elements, string> translationES = new Dictionary<Elements, string>(9);

		public static Dictionary<Elements, string> translationRU = new Dictionary<Elements, string>(9);

		public static Dictionary<Elements, string> translationIT = new Dictionary<Elements, string>(9);

		public static Dictionary<Elements, string> translationPL = new Dictionary<Elements, string>(9);

		public static Dictionary<Elements, string> translationJP = new Dictionary<Elements, string>(9);

		public static Dictionary<Elements, string> translationCN = new Dictionary<Elements, string>(9);

		public static Dictionary<Elements, string> translationKO = new Dictionary<Elements, string>(9);

		public static void Patch()
		{
            var harmony = new Harmony("com.graveyardkeeper.urbanvibes.itemdescriptionelement");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

			ReadParametersFromFile();
		}

		public static void ReadParametersFromFile()
		{
			string[] array = null;
			string empty = string.Empty;
			string text = string.Empty;
			try
			{
				array = File.ReadAllLines(configFilePathAndName);
			}
			catch (Exception)
			{
				Debug.Log((object)("ItemDescriptionElement: Translations File not found: " + configFilePathAndName));
				return;
			}
			if (array == null || array.Length == 0)
			{
				return;
			}
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				empty = array2[i].Trim();
				if (empty.StartsWith(parameterComment))
				{
					continue;
				}
				if (empty.StartsWith(parameterSectionBegin) && empty.EndsWith(parameterSectionEnd))
				{
					text = empty.Trim().Replace(parameterSectionBegin, string.Empty).Replace(parameterSectionEnd, string.Empty);
					continue;
				}
				string[] array3 = empty.Split(parameterSeparator);
				if (array3.Length != 2)
				{
					continue;
				}
				array3[0].Trim();
				array3[1].Trim();
				Elements result = Elements.None;
				Enum.TryParse<Elements>(array3[0], out result);
				if (result != 0)
				{
					switch (text)
					{
					case "en":
						translationEN.Add(result, array3[1]);
						break;
					case "de":
						translationDE.Add(result, array3[1]);
						break;
					case "fr":
						translationFR.Add(result, array3[1]);
						break;
					case "pt-br":
						translationPT.Add(result, array3[1]);
						break;
					case "es":
						translationES.Add(result, array3[1]);
						break;
					case "ru":
						translationRU.Add(result, array3[1]);
						break;
					case "it":
						translationIT.Add(result, array3[1]);
						break;
					case "pl":
						translationPL.Add(result, array3[1]);
						break;
					case "ja":
						translationJP.Add(result, array3[1]);
						break;
					case "zh_cn":
						translationCN.Add(result, array3[1]);
						break;
					case "ko":
						translationKO.Add(result, array3[1]);
						break;
					}
				}
			}
		}
	}
}
