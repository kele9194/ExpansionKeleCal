using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System;
using System.Collections.Generic;

namespace ExpansionKeleCal.Content.StaryMelee
{
    public class StarySwordCalB : StarySwordCalAbs
    {
        public override string LocalizationCategory => "StaryMelee";
        public override int BaseDamage => 166;
        public override int UseTime => 20;
        public override int Rarity => ItemRarityID.Blue;
        public override int Crit => 19;
        
        // 特定属性
        // private new const string setNameOverride = "幻星元剑B";
        // private const string introduction ="幻星元剑A的升级版";

        public override void SetStaticDefaults()
        {
            // 可以根据需要添加静态默认值
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // 添加自定义的 tooltip  
            // TooltipLine line = new TooltipLine(Mod, "StarySwordCalB", introduction);
            // line.OverrideColor = Color.Lerp(Color.Blue, Color.White, 0.5f);
            // tooltips.Add(line);
        }
        
        public override void AddRecipes()
        {
            // 创建合成配方  
            Recipe recipe = Recipe.Create(Type);
            recipe.AddIngredient(ExpansionKeleCal.calamity.Find<ModItem>("ProfanedCore").Type, 1);
            recipe.AddIngredient(ModContent.ItemType<StarySwordCalA>(), 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}