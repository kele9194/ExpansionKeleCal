using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ExpansionKeleCal.Content.StaryArmor
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Legs value here will result in TML expecting a X_Legs.png file to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Legs)]
	public class StarLeggingsCalD : ModItem
	{
		public override string LocalizationCategory => "StaryArmor";
		public static int index = 3;
		public static  int LeggingsDefense = ArmorData.LeggingsDefense[index];
		public static  float MoveSpeedBonus = ArmorData.MoveSpeedBonus[index]/100f;
		public static float SummonDamage = ArmorData.SummonDamage[index]/100f;
		public static int MeleeCritChance = ArmorData.MeleeCritChance[index];
		public static int RangedCritChance = ArmorData.RangedCritChance[index];
		public static float MeleeSpeed = ArmorData.MeleeSpeed[index]/100f;
		public static int MaxMana= ArmorData.MaxMana[index];
		public static float ManaCostReduction = ArmorData.ManaCostReduction[index]/100f;
		public static float AmmoCostReduction = ArmorData.AmmoCostReduction[index];
		public static string setNameOverride="幻星元护胫D";
		



		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MoveSpeedBonus);

		public override void SetDefaults() {
			Item.SetNameOverride(setNameOverride);
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
			Item.rare = ItemRarityID.Green; // The rarity of the item
			Item.defense = LeggingsDefense; // The amount of defense the item will give when equipped
		}

		public override void UpdateEquip(Player player) {
			player.moveSpeed += MoveSpeedBonus; // Increase the movement speed of the player
			player.GetCritChance(DamageClass.Melee) += MeleeCritChance;
			player.GetCritChance(DamageClass.Ranged) += RangedCritChance;
			player.GetAttackSpeed(DamageClass.Melee) += MeleeSpeed;
			player.statManaMax2 += MaxMana;
			player.manaCost -= ManaCostReduction;
			player.ammoCost75 =true;
		}
		public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
			if (ModContent.GetInstance<ExpansionKeleCalConfig>().detailedTooltip)
            {
            tooltips.Add(new TooltipLine(Mod, "DetailedInfo", "[c/00FF00:详细信息:]"));
            var tooltipData = new Dictionary<string, string>
            {
                {"MoveSpeedBonus", $"移速 +{MoveSpeedBonus * 100}%"}, // 移速通常是百分比形式
        		{"MeleeCritChance", $"近战暴击 +{MeleeCritChance}%"}, // 近战暴击率
        		{"RangedCritChance", $"远程暴击 +{RangedCritChance}%"}, // 远程暴击率
        		{"MeleeSpeed", $"近战攻击速度 +{MeleeSpeed * 100}%"}, // 近战攻击速度通常是百分比形式
				{"MaxMana", $"法术上限 +{MaxMana}"},
        		{"ManaCostReduction", $"法术消耗减少 -{ManaCostReduction * 100}%"}, // 法术消耗减少通常是百分比形式
        		{"AmmoCost75", $"弹药消耗减少 -25%"},
				//{"Tooltip",$"星元套装的第一个系列的护胫"}
            };

            foreach (var kvp in tooltipData)
            {
                tooltips.Add(new TooltipLine(Mod, kvp.Key, kvp.Value));
            }
			}
        }


		

		public override void AddRecipes()  
	{  
    // 创建 GaSniperA 武器的合成配方  
    Recipe recipe = Recipe.Create(ModContent.ItemType<StarLeggingsCalD>()); // 替换为 GaSniperA 的类型  
    recipe.AddIngredient(ExpansionKeleCal.calamity.Find<ModItem>("AuricBar").Type, 2);
    recipe.AddIngredient(ModContent.ItemType<StarLeggingsCalC>(), 1);
    recipe.AddTile(ExpansionKeleCal.calamity.Find<ModTile>("CosmicAnvil"));//远古操纵机
    recipe.Register(); // 注册配方   
	}  
	}
}
