using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Reflection;
using CalamityMod.CalPlayer;
using CalamityMod;

namespace ExpansionKeleCal.Content.Items.Accessories
{
    /// <summary>
    /// 潜伏电容 - 一个增强盗贼能力的饰品
    /// 增加玩家所有潜伏值的15%，同时增加5%的盗贼暴击率
    /// </summary>
    public class StealthCapacitor : ModItem
    {
        public override string LocalizationCategory => "Items.Accessories";
        
        // 常量定义
        private const float StealthBonus = 0.15f; // 15%潜伏值加成
        private const int RogueCritBonus = 5; // 5%盗贼暴击率加成

        /// <summary>
        /// 设置饰品的基本属性
        /// </summary>
        public override void SetDefaults()
        {
            Item.SetNameOverride("潜伏电容"); // 设置饰品名称
            Item.width = 24; // 设置饰品宽度
            Item.height = 28; // 设置饰品高度
            Item.value = Item.buyPrice(0, 5, 0, 0); // 设置饰品价值（5金币）
            Item.rare = ItemRarityID.Pink; // 设置饰品稀有度为粉色
            Item.accessory = true; // 设置饰品为可装备的饰品
        }

        /// <summary>
        /// 更新饰品效果
        /// </summary>
        /// <param name="player">装备饰品的玩家对象</param>
        /// <param name="hideVisual">是否隐藏饰品的视觉效果</param>
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // 增加盗贼暴击率
            if (ExpansionKeleCal.calamity != null)
            {
                // 通过反射获取盗贼职业
                var rogueDamageClass = ExpansionKeleCal.calamity.Find<DamageClass>("RogueDamageClass");
                if (rogueDamageClass != null)
                {
                    player.GetCritChance(rogueDamageClass) += RogueCritBonus;
                }
            }

            // 增加潜伏值
            // ReflectionHelper.CallExpansionKeleApplyRogueStealth(player, StealthBonus);
            // 替换为直接调用Calamity的API
            player.Calamity().rogueStealthMax += StealthBonus;
            player.Calamity().wearingRogueArmor = true;
        }

        /// <summary>
        /// 修改饰品的工具提示
        /// </summary>
        /// <param name="tooltips">工具提示列表</param>
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
        }

        /// <summary>
        /// 定义饰品的制作配方
        /// </summary>
        public override void AddRecipes()
        {
            // 使用电线和铁锭制作
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wire, 10);
            recipe.AddIngredient(ItemID.IronBar, 5);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

            // 使用电线和铅锭制作（替代配方）
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wire, 10);
            recipe.AddIngredient(ItemID.LeadBar, 5);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}