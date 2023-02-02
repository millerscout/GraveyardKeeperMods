using System;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using Random = System.Random;

namespace ZombiePlus
{
	public static class Craft
	{
		[HarmonyPatch(typeof(CraftComponent))]
		[HarmonyPatch("FillCraftsList")]
		internal class FillCraftsList_Patcher
		{
			[HarmonyPrefix]
			public static void FillCraftsList_Prefix(CraftComponent __instance)
			{
				//IL_0060: Unknown result type (might be due to invalid IL or missing references)
				//IL_0065: Unknown result type (might be due to invalid IL or missing references)
				//IL_0082: Unknown result type (might be due to invalid IL or missing references)
				//IL_009f: Unknown result type (might be due to invalid IL or missing references)
				//IL_00b5: Expected O, but got Unknown
				if (Entry.Config.Craft_ZombieFarm_SeedsNeed_Enabled)
				{
					SetNeedsToCraft(ref __instance, "zombie_garden_desk", "grow_desk_planting", Entry.Config.Craft_ZombieFarm_Garden_Needs_Value);
					SetNeedsToCraft(ref __instance, "zombie_vineyard_desk", "grow_vineyard_planting", Entry.Config.Craft_ZombieFarm_Vineyard_Needs_Value);
				}
				if (Entry.Config.Craft_ZombieFarm_ProduceWaste_Enabled)
				{
					Item item = new Item("crop_waste")
					{
						min_value = SmartExpression.ParseExpression(Entry.Config.Craft_ZombieFarm_ProduceWaste_Min.ToString()),
						max_value = SmartExpression.ParseExpression(Entry.Config.Craft_ZombieFarm_ProduceWaste_Max.ToString()),
						self_chance = 
						{
							default_value = Entry.Config.Craft_ZombieFarm_ProduceWaste_Chance
						}
					};
					AddItemToCraft(ref __instance, "zombie_vineyard_desk", "vineyard_planting", item);
					AddItemToCraft(ref __instance, "zombie_garden_desk", "grow_desk_planting", item);
				}
			}
		}

		public static int RandomBetween(int a, int b)
		{
			return new Random().Next(a, b);
		}

		public static void ModifyCraftObject(ref CraftComponent __instance, string objIdContains, string craftIdContains, Action<CraftDefinition> act)
		{
			if (!((WorldGameObjectComponentBase)__instance).wgo.obj_id.Contains(objIdContains))
			{
				return;
			}
			GameBalance.me.GetCraftsForObject(((WorldGameObjectComponentBase)__instance).wgo.obj_id).ForEach(delegate(CraftDefinition craft)
			{
				if (((BalanceBaseObject)craft).id.Contains(craftIdContains))
				{
					act(craft);
				}
			});
		}

		public static void ReduceSeedsNeedToZombieCraft(ref CraftComponent __instance)
		{
			ModifyCraftObject(ref __instance, "zombie_garden_desk", "grow_desk_planting", delegate(CraftDefinition craft)
			{
				craft.needs.ForEach(delegate(Item n)
				{
					n.value = Entry.Config.Craft_ZombieFarm_Garden_Needs_Value;
				});
			});
		}

		public static void AddCropWasteToZombieCraft(ref CraftComponent __instance)
		{
			ModifyCraftObject(ref __instance, "zombie_garden_desk", "grow_desk_planting", delegate(CraftDefinition craft)
			{
				//IL_0038: Unknown result type (might be due to invalid IL or missing references)
				//IL_003d: Unknown result type (might be due to invalid IL or missing references)
				//IL_005a: Unknown result type (might be due to invalid IL or missing references)
				//IL_0077: Unknown result type (might be due to invalid IL or missing references)
				//IL_008d: Expected O, but got Unknown
				if (!craft.output.Exists((Item o) => o.id == "crop_waste"))
				{
					Item item = new Item("crop_waste")
					{
						min_value = SmartExpression.ParseExpression(Entry.Config.Craft_ZombieFarm_ProduceWaste_Min.ToString()),
						max_value = SmartExpression.ParseExpression(Entry.Config.Craft_ZombieFarm_ProduceWaste_Max.ToString()),
						self_chance = 
						{
							default_value = Entry.Config.Craft_ZombieFarm_ProduceWaste_Chance
						}
					};
					craft.output.Add(item);
				}
			});
		}

		public static void AddItemToCraft(ref CraftComponent instance, string objIdContain, string craftIdContain, Item item)
		{
			Predicate<Item> v9__1;
			ModifyCraftObject(ref instance, objIdContain, craftIdContain, delegate(CraftDefinition craft)
			{
				List<Item> output = craft.output;
				if (!output.Exists(v9__1 = (Item o) => o.id == item.id))
				{
					craft.output.Add(item);
				}
			});
		}

		public static void SetNeedsToCraft(ref CraftComponent instance, string objIdContain, string craftIdContain, int value)
		{
			Action<Item> v9__1;
			ModifyCraftObject(ref instance, objIdContain, craftIdContain, delegate(CraftDefinition craft)
			{
				List<Item> needs = craft.needs;
				needs.ForEach(v9__1 = delegate(Item n)
				{
					n.value = value;
				});
			});
		}

		public static void CraftModifyNeeds(string craftPlaceContains, int value, bool allNeeds = false)
		{
			try
			{
				Action<Item> v9__1;
				GameBalance.me.craft_data.ForEach(delegate(CraftDefinition craft)
				{
					if (craft != null && craft.needs != null && craft.needs.Count != 0 && allNeeds && ((BalanceBaseObject)craft).id.Contains(craftPlaceContains))
					{
						List<Item> needs2 = craft.needs;
						needs2.ForEach(v9__1 = delegate(Item needs)
						{
							if (needs.id.Contains("seeds"))
							{
								needs.value = value;
							}
						});
					}
				});
			}
			catch (Exception ex)
			{
				Debug.Log((object)$"[ZombieEnhanced] Couldn't modify output: {craftPlaceContains} vl:{value} an:{allNeeds} e={ex}");
			}
		}

		public static void CraftAddOutput(string craftPlaceContains, Item item)
		{
			try
			{
				Predicate<Item> v9__1;
				GameBalance.me.craft_data.ForEach(delegate(CraftDefinition craft)
				{
					if (craft != null && craft.output != null && craft.output.Count != 0 && ((BalanceBaseObject)craft).id.Contains(craftPlaceContains))
					{
						List<Item> output = craft.output;
						if (!output.Exists(v9__1 = (Item o) => o.id == item.id))
						{
							craft.output.Add(item);
						}
					}
				});
			}
			catch (Exception arg)
			{
				Entry.Log($"[ZombiePlus] Couldn't modify output: {craftPlaceContains} {arg}");
			}
		}
	}
}
