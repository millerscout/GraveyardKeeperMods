using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using static NGUIText;

namespace ItemDescriptionElement
{
    [HarmonyPatch(typeof(ItemDefinition), "GetTooltipData", null)]
    public class ItemDefinition_GetTooltipData
    {
        [HarmonyPostfix]
        public static void Patch(ItemDefinition __instance, List<BubbleWidgetData> __result, bool full_detail = true)
        {
            //IL_005f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0069: Expected O, but got Unknown
            //IL_0088: Unknown result type (might be due to invalid IL or missing references)
            //IL_0092: Expected O, but got Unknown
            MainPatcher.Elements elements = MainPatcher.Elements.None;
            string empty = string.Empty;
            string empty2 = string.Empty;
            if (!SurveyCompleted(__instance, full_detail))
            {
                return;
            }
            elements = ((!MainPatcher.itemIDLookup.ContainsKey(__instance.GetNameWithoutQualitySuffix())) ? LookupCraftingRecipe(__instance.GetNameWithoutQualitySuffix()) : MainPatcher.itemIDLookup[__instance.GetNameWithoutQualitySuffix()]);
            if (elements != 0)
            {
                empty = GetTextColorForElement(elements);
                empty2 = GetElementDescriptionLocalized(elements);
                if (full_detail)
                {
                    __result.Add((BubbleWidgetData)new BubbleWidgetSeparatorData());
                }
                __result.Add((BubbleWidgetData)new BubbleWidgetTextData(string.Concat(new string[3] { empty, empty2, "[-][/c]" }), (UITextStyles.TextStyle)4, (Alignment)2, -1));
            }
        }

        private static bool SurveyCompleted(ItemDefinition ItemDefinition, bool full_detail)
        {
            CraftDefinition surveyCraft = ItemDefinition.GetSurveyCraft();
            if (full_detail && surveyCraft != null && MainGame.me.save.completed_one_time_crafts.Contains(((BalanceBaseObject)surveyCraft).id))
            {
                return true;
            }
            return false;
        }

        private static string GetTextColorForElement(MainPatcher.Elements Element)
        {
            return Element switch
            {
                MainPatcher.Elements.Slowing => "[c][7A3C0B]",
                MainPatcher.Elements.Acceleration => "[c][092862]",
                MainPatcher.Elements.Health => "[c][255201]",
                MainPatcher.Elements.Death => "[c][380A4F]",
                MainPatcher.Elements.Order => "[c][D9FB74]",
                MainPatcher.Elements.Toxic => "[c][C62513]",
                MainPatcher.Elements.Chaos => "[c][8909D7]",
                MainPatcher.Elements.Life => "[c][A56F01]",
                MainPatcher.Elements.Electric => "[c][24FFFF]",
                _ => string.Empty,
            };
        }

        private static MainPatcher.Elements LookupCraftingRecipe(string ItemID)
        {
            //IL_001b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0021: Invalid comparison between Unknown and I4
            MainPatcher.Elements elements = MainPatcher.Elements.None;
            for (int i = 0; i < GameBalance.me.craft_data.Count; i++)
            {
                CraftDefinition val = GameBalance.me.craft_data[i];
                if ((int)val.craft_type == 5 && val.needs.Count == 1 && val.output.Count == 1 && val.needs[0] != null && val.output[0] != null && val.needs[0].id.Equals(ItemID))
                {
                    elements = val.output[0].id switch
                    {
                        "alchemy_1_brown" => MainPatcher.Elements.Slowing,
                        "alchemy_2_brown" => MainPatcher.Elements.Slowing,
                        "alchemy_3_brown" => MainPatcher.Elements.Slowing,
                        "alchemy_1_d_blue" => MainPatcher.Elements.Acceleration,
                        "alchemy_2_d_blue" => MainPatcher.Elements.Acceleration,
                        "alchemy_3_d_blue" => MainPatcher.Elements.Acceleration,
                        "alchemy_1_d_green" => MainPatcher.Elements.Health,
                        "alchemy_2_d_green" => MainPatcher.Elements.Health,
                        "alchemy_3_d_green" => MainPatcher.Elements.Health,
                        "alchemy_1_d_violet" => MainPatcher.Elements.Death,
                        "alchemy_2_d_violet" => MainPatcher.Elements.Death,
                        "alchemy_3_d_violet" => MainPatcher.Elements.Death,
                        "alchemy_1_green" => MainPatcher.Elements.Order,
                        "alchemy_2_green" => MainPatcher.Elements.Order,
                        "alchemy_3_green" => MainPatcher.Elements.Order,
                        "alchemy_1_red" => MainPatcher.Elements.Toxic,
                        "alchemy_2_red" => MainPatcher.Elements.Toxic,
                        "alchemy_3_red" => MainPatcher.Elements.Toxic,
                        "alchemy_1_violet" => MainPatcher.Elements.Chaos,
                        "alchemy_2_violet" => MainPatcher.Elements.Chaos,
                        "alchemy_3_violet" => MainPatcher.Elements.Chaos,
                        "alchemy_1_yellow" => MainPatcher.Elements.Life,
                        "alchemy_2_yellow" => MainPatcher.Elements.Life,
                        "alchemy_3_yellow" => MainPatcher.Elements.Life,
                        "alchemy_1_yellow_electro" => MainPatcher.Elements.Electric,
                        _ => MainPatcher.Elements.None,
                    };
                    break;
                }
            }
            MainPatcher.itemIDLookup.Add(ItemID, elements);
            return elements;
        }

        private static void GetAlchemyCraftingResults()
        {
            //IL_0019: Unknown result type (might be due to invalid IL or missing references)
            //IL_001f: Invalid comparison between Unknown and I4
            for (int i = 0; i < GameBalance.me.craft_data.Count; i++)
            {
                CraftDefinition val = GameBalance.me.craft_data[i];
                if ((int)val.craft_type == 5 && val.needs.Count == 1 && val.needs[0] != null && val.output[0] != null)
                {
                    Debug.Log((object)(i + ": needs: " + val.needs.Count + "| outputs: " + val.output.Count + " |  craftDefinition.needs[0]: " + val.needs[0].id + " | craftDefinition.output[0]: " + (object)val.output[0]));
                    switch (val.output[0].id)
                    {
                    }
                }
            }
        }

        private static void InitializeElementAssignment()
        {
            MainPatcher.itemIDLookup.Add("alchemy_1_brown", MainPatcher.Elements.Slowing);
            MainPatcher.itemIDLookup.Add("alchemy_2_brown", MainPatcher.Elements.Slowing);
            MainPatcher.itemIDLookup.Add("alchemy_3_brown", MainPatcher.Elements.Slowing);
            MainPatcher.itemIDLookup.Add("alchemy_1_d_blue", MainPatcher.Elements.Acceleration);
            MainPatcher.itemIDLookup.Add("alchemy_2_d_blue", MainPatcher.Elements.Acceleration);
            MainPatcher.itemIDLookup.Add("alchemy_3_d_blue", MainPatcher.Elements.Acceleration);
            MainPatcher.itemIDLookup.Add("alchemy_1_d_green", MainPatcher.Elements.Health);
            MainPatcher.itemIDLookup.Add("alchemy_2_d_green", MainPatcher.Elements.Health);
            MainPatcher.itemIDLookup.Add("alchemy_3_d_green", MainPatcher.Elements.Health);
            MainPatcher.itemIDLookup.Add("alchemy_1_d_violet", MainPatcher.Elements.Death);
            MainPatcher.itemIDLookup.Add("alchemy_2_d_violet", MainPatcher.Elements.Death);
            MainPatcher.itemIDLookup.Add("alchemy_3_d_violet", MainPatcher.Elements.Death);
            MainPatcher.itemIDLookup.Add("alchemy_1_green", MainPatcher.Elements.Order);
            MainPatcher.itemIDLookup.Add("alchemy_2_green", MainPatcher.Elements.Order);
            MainPatcher.itemIDLookup.Add("alchemy_3_green", MainPatcher.Elements.Order);
            MainPatcher.itemIDLookup.Add("alchemy_1_red", MainPatcher.Elements.Toxic);
            MainPatcher.itemIDLookup.Add("alchemy_2_red", MainPatcher.Elements.Toxic);
            MainPatcher.itemIDLookup.Add("alchemy_3_red", MainPatcher.Elements.Toxic);
            MainPatcher.itemIDLookup.Add("alchemy_1_violet", MainPatcher.Elements.Chaos);
            MainPatcher.itemIDLookup.Add("alchemy_2_violet", MainPatcher.Elements.Chaos);
            MainPatcher.itemIDLookup.Add("alchemy_3_violet", MainPatcher.Elements.Chaos);
            MainPatcher.itemIDLookup.Add("alchemy_1_yellow", MainPatcher.Elements.Life);
            MainPatcher.itemIDLookup.Add("alchemy_2_yellow", MainPatcher.Elements.Life);
            MainPatcher.itemIDLookup.Add("alchemy_3_yellow", MainPatcher.Elements.Life);
            MainPatcher.itemIDLookup.Add("alchemy_1_yellow_electro", MainPatcher.Elements.Electric);
        }

        private static string GetElementDescriptionLocalized(MainPatcher.Elements Element)
        {
            string value = string.Empty;
            switch (GJL.GetCurLng())
            {
                case "en":
                    MainPatcher.translationEN.TryGetValue(Element, out value);
                    break;
                case "de":
                    MainPatcher.translationDE.TryGetValue(Element, out value);
                    break;
                case "fr":
                    MainPatcher.translationFR.TryGetValue(Element, out value);
                    break;
                case "pt-br":
                    MainPatcher.translationPT.TryGetValue(Element, out value);
                    break;
                case "es":
                    MainPatcher.translationES.TryGetValue(Element, out value);
                    break;
                case "ru":
                    MainPatcher.translationRU.TryGetValue(Element, out value);
                    break;
                case "it":
                    MainPatcher.translationIT.TryGetValue(Element, out value);
                    break;
                case "pl":
                    MainPatcher.translationPL.TryGetValue(Element, out value);
                    break;
                case "ja":
                    MainPatcher.translationJP.TryGetValue(Element, out value);
                    break;
                case "zh_cn":
                    MainPatcher.translationCN.TryGetValue(Element, out value);
                    break;
                case "ko":
                    MainPatcher.translationKO.TryGetValue(Element, out value);
                    break;
                default:
                    MainPatcher.translationEN.TryGetValue(Element, out value);
                    break;
            }
            return value;
        }
    }
}
