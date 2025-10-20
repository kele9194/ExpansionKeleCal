using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using System.Threading;
using System.IO;



namespace ExpansionKeleCal.Content.StaryArmor
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Head)]
	public class StarHelmetCalE : ModItem
	{
		public override string LocalizationCategory => "StaryArmor";
		public static int index = 4;
		public static  int helmetDefense = ArmorData.HelmetDefense[index];
		public static  float GenericDamageBonus = ArmorData.GenericDamageBonus[index]/100f;

		public static  int maxTurrets = ArmorData.MaxTurrets[index];

		public static float rogueStealthMax = ArmorData.StealthMax[index]/100f;

		public static int RogueCritChance = ArmorData.RogueCritChance[index];



		
		public float a = ArmorData.CalculateA(0.34f+0.3f*ArmorData.GenericDamageBonus[index]/100f);
		public static string setNameOverride="幻星元盔E";

		public static LocalizedText SetBonusText { get; private set; }



		public override void SetStaticDefaults() {


			SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs(RogueCritChance, rogueStealthMax*100);
		}

		public override void SetDefaults() {
			Item.SetNameOverride(setNameOverride);
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
			Item.rare = ItemRarityID.Green; // The rarity of the item
			Item.defense = helmetDefense; // The amount of defense the item will give when equipped
		}

		// IsArmorSet determines what armor pieces are needed for the setbonus to take effect
		public override bool IsArmorSet(Item head, Item body, Item legs) {
    	return head.type == ModContent.ItemType<StarHelmetCalE>() &&
           body.type == ModContent.ItemType<StarBreastplateCalE>() &&
           legs.type == ModContent.ItemType<StarLeggingsCalE>();
}

		// UpdateArmorSet allows you to give set bonuses to the armor.
		 public override void UpdateArmorSet(Player player) {
            player.setBonus = SetBonusText.Value;
            if(ExpansionKeleCal.calamity!=null)
			{
				player.GetCritChance<ThrowingDamageClass>() +=RogueCritChance;
            	ReflectionHelper.ApplyRogueStealth(player, rogueStealthMax);
			}
			float lifePercentage = player.statLife / (float)player.statLifeMax2;
			if(lifePercentage > 1)
			{
				lifePercentage = 1;
			}
            float damageBoost = (1 / (lifePercentage + a)) - (1 / (1 + a));
            player.GetDamage<GenericDamageClass>() += damageBoost;
			
		
		}

		public override void UpdateEquip(Player player)
        {
			player.maxTurrets+= maxTurrets;
            player.GetDamage(DamageClass.Generic) += GenericDamageBonus;
            // 检查是否装备了整套星元盔甲
            
              
    }

	public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
			if (ModContent.GetInstance<ExpansionKeleCalConfig>().detailedTooltip)
            {
            tooltips.Add(new TooltipLine(Mod, "DetailedInfo", "[c/00FF00:详细信息:]"));
            var tooltipData = new Dictionary<string, string>
            {
                //{ "Defense", $"防御力 +{Item.defense}" },
				{"GenericDamageBonus", $"伤害 +{GenericDamageBonus*100}%"},
				{"maxTurrets", $"最大哨兵数量 +{maxTurrets}"},
				//{"Tooltip",$"星元套装的第一个系列的头盔"}
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
    Recipe recipe = Recipe.Create(ModContent.ItemType<StarHelmetCalE>()); // 替换为 GaSniperA 的类型  
    recipe.AddIngredient(ExpansionKeleCal.calamity.Find<ModItem>("MiracleMatter").Type, 1);
    recipe.AddIngredient(ModContent.ItemType<StarHelmetCalD>(), 1);
    recipe.AddTile(ExpansionKeleCal.calamity.Find<ModTile>("DraedonsForge"));
    recipe.Register(); // 注册配方 
	}  
            }
        }
		

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		// public override void AddRecipes() {
		// 	CreateRecipe()
		// 		.AddIngredient<ExampleItem>()
		// 		.AddTile<Tiles.Furniture.ExampleWorkbench>()
		// 		.Register();
		// }

