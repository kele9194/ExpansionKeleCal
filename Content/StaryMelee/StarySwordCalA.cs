using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System;
using System.Collections.Generic;

namespace ExpansionKeleCal.Content.StaryMelee
{
    public class StarySwordCalA : StarySwordCalAbs
    {
        public override string LocalizationCategory => "StaryMelee";
        
        
        // 基础属性
        public override int BaseDamage => 160;
        public override int UseTime => 21;
        public override int Rarity => ItemRarityID.Red;
        public override int Crit => 17;
        
        // 特定属性
        // private new const string setNameOverride = "幻星元剑A";
        // private const string introduction = "该系列剑在灾厄的第一把剑，更名幻元星剑，左键攻击提供无敌帧";
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // // 添加自定义的 tooltip  
            // TooltipLine line = new TooltipLine(Mod, "SetNameOverride", introduction);
            // tooltips.Add(line);
        }
        
        public override void AddRecipes()
        {
            // 创建合成配方
            Recipe recipe = Recipe.Create(Type);
            recipe.AddIngredient(ExpansionKeleCal.calamity.Find<ModItem>("EffulgentFeather").Type, 8);
            recipe.AddIngredient(ExpansionKeleCal.expansionkele.Find<ModItem>("StarySwordJ").Type, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();

            Recipe recipeI = Recipe.Create(Type);
            recipeI.AddIngredient(ExpansionKeleCal.calamity.Find<ModItem>("LifeAlloy").Type, 8);
            recipeI.AddIngredient(ExpansionKeleCal.calamity.Find<ModItem>("GalacticaSingularity").Type, 8);
            recipeI.AddIngredient(ItemID.LunarBar, 8);
            recipeI.AddTile(TileID.LunarCraftingStation);
            recipeI.Register();

            
        }
    }
}