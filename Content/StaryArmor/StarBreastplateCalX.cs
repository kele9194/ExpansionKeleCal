using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ExpansionKeleCal.Content.StaryArmor
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Body value here will result in TML expecting X_Arms.png, X_Body.png and X_FemaleBody.png sprite-sheet files to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Body)]
	public class StarBreastplateCalX : ModItem
	{
		public override string LocalizationCategory => "StaryArmor";
		public static int index = 5;
		//public static readonly string SetBonusText = "Set Bonus: 20% increased defense and 20% increased crit chance";
		public static  int plateDefense = ArmorData.PlateDefense[index];
		public static  int critChance = ArmorData.CritChance[index];
		public static  int MaxMinions = ArmorData.MaxMinions[index];

		public static string setNameOverride="幻星元甲X";

		//public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxManaIncrease, MaxMinionIncrease);

		public override void SetDefaults() {
            Item.SetNameOverride(setNameOverride);
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
			Item.rare = ItemRarityID.Green; // The rarity of the item
			Item.defense = plateDefense; // The amount of defense the item will give when equipped
		}

		public override void UpdateEquip(Player player) {
			player.buffImmune[BuffID.OnFire] = true; // Make the player immune to Fire
			//player.statManaMax2 += MaxManaIncrease; // Increase how many mana points the player can have by 20
			player.maxMinions += MaxMinions; // Increase how many minions the player can have by one
			player.noKnockback =true; // Increase knockback resistance
			player.GetCritChance(DamageClass.Generic) += critChance;


			
		}
		public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
			if (ModContent.GetInstance<ExpansionKeleCalConfig>().detailedTooltip)
            {
            tooltips.Add(new TooltipLine(Mod, "DetailedInfo", "[c/00FF00:详细信息:]"));
            var tooltipData = new Dictionary<string, string>
            {
                //{ "Defense", $"防御力 +{Item.defense}" },
				{"critChance", $"暴击率 +{critChance}%"},
                { "MaxMinions", $"最大召唤物数量 +{MaxMinions}" },
                { "FireImmunity", "免疫火焰伤害" },
				{"kbBuff","免疫击退"}
				//{"Tooltip",$"星元套装的第一个系列的胸甲"}
            };

            foreach (var kvp in tooltipData)
            {
                tooltips.Add(new TooltipLine(Mod, kvp.Key, kvp.Value));
            }
			}
        }

		

		//Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()  
	{  
    // 创建 GaSniperA 武器的合成配方  
    Recipe recipe = Recipe.Create(ModContent.ItemType<StarBreastplateCalX>()); // 替换为 GaSniperA 的类型  
    recipe.AddIngredient(ExpansionKeleCal.calamity.Find<ModItem>("ShadowspecBar").Type, 1);
    recipe.AddIngredient(ModContent.ItemType<StarBreastplateCalE>(), 1);
    recipe.AddTile(ExpansionKeleCal.calamity.Find<ModTile>("DraedonsForge"));
    recipe.Register(); // 注册配方  
	}  
	}
}