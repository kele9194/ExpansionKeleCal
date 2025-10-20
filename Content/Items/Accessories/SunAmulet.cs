using Terraria;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using ExpansionKeleCal;
using System;

namespace ExpansionKeleCal.Content.Items.Accessories
{
    /// <summary>
    /// 护符类，继承自 ModItem。
    /// 这个饰品增加玩家的防御和最大生命值，并根据最近的敌对生物的距离动态调整这些属性。
    /// </summary>
    [AutoloadEquip(EquipType.Waist)]
    public class SunAmulet : ModItem
    {
        public override string LocalizationCategory => "Items.Accessories";
        // 常量定义
        private const int DefenseBonus = 4; // 基础防御加成
        private const int LifeMaxBonus = 30; // 基础最大生命值加成
        private const int SearchRadius = 480; // 搜索敌对生物的半径（像素）
        private const int MinSearchRadius = 96; // 最小搜索敌对生物的半径（像素）
        private const int DefenseMultiBonus = 24; // 根据距离增加的额外防御
        private const float EnduranceMultiBonus = 0.12f; // 根据距离增加的额外耐力
        private const float AttackMultiBonus = 0.25f; // 根据距离增加的额外近战伤害
        private const float TrueMeleeMultiBonus = 0.25f; // 根据距离增加的额外真近战伤害
        private const float AttackSpeedMultiBonus = 0.25f; // 根据距离增加的额外攻击速度

        /// <summary>
        /// 设置饰品的基本属性。
        /// </summary>
        public override void SetDefaults()
        {
            Item.SetNameOverride("阳炎护符"); // 设置饰品名称
            Item.width = 24; // 设置饰品宽度
            Item.height = 28; // 设置饰品高度
            Item.value = Item.buyPrice(0, 2, 0, 0); // 设置饰品价值（1银币）
            Item.rare = ItemRarityID.Blue; // 设置饰品稀有度为蓝色
            Item.accessory = true; // 设置饰品为可装备的饰品
            Item.defense += DefenseBonus*2;
        }

        /// <summary>
        /// 更新饰品效果。
        /// </summary>
        /// <param name="player">装备饰品的玩家对象。</param>
        /// <param name="hideVisual">是否隐藏饰品的视觉效果。</param>
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // 增加基础防御和最大生命值
            player.statLifeMax2 += LifeMaxBonus;

            // 如果灾厄模组加载，则增加额外的防御和最大生命值
            if (ExpansionKeleCal.calamity != null)
            {
                player.statLifeMax2 += LifeMaxBonus;
            }

            // 寻找最近的敌对生物
            NPC closestNPC = null;
            float closestDistance = float.MaxValue;

            foreach (NPC npc in Main.npc)
            {
                if (npc.active && !npc.friendly && npc.Distance(player.Center) < SearchRadius)
                {
                    float distance = player.Distance(npc.Center);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestNPC = npc;
                    }
                }
            }

            // 如果找到了最近的敌对生物，则根据距离动态调整属性
            if (closestNPC != null)
            {
                float t = Math.Min(1 - (closestDistance - MinSearchRadius) / SearchRadius, 1f); // 计算 t 值

                // 增加额外的防御
                player.statDefense += (int)(DefenseMultiBonus * t + 0.5f);

                // 增加额外的耐力
                player.endurance += EnduranceMultiBonus * t;

                // 增加近战伤害
                player.GetDamage<MeleeDamageClass>() += AttackMultiBonus * t;

                // 增加攻击速度
                player.GetAttackSpeed<MeleeDamageClass>() += AttackSpeedMultiBonus * t;

                // 如果灾厄模组加载，则增加真近战伤害
                if (ExpansionKeleCal.calamity != null)
                {
                    var trueMeleeDamageClass = ExpansionKeleCal.calamity.Find<DamageClass>("TrueMeleeDamageClass");

                    if (trueMeleeDamageClass != null)
                    {
                        player.GetDamage(trueMeleeDamageClass) += TrueMeleeMultiBonus * t;
                    }
                }

                // 根据 t 值的概率增加生命再生时间
                if (Main.rand.NextFloat() < t)
                {
                    player.lifeRegenTime += 1;
                }
            }
        }

        /// <summary>
        /// 修改饰品的工具提示。
        /// </summary>
        /// <param name="tooltips">工具提示列表。</param>
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (ModContent.GetInstance<ExpansionKeleCalConfig>().detailedTooltip)
            {
            tooltips.Add(new TooltipLine(Mod, "DetailedInfo", "[c/00FF00:详细信息:]"));
            tooltips.Add(new TooltipLine(Mod, "LifeMaxBonus", $"增加 {2*LifeMaxBonus} 点最大生命值"));

            // 添加动态效果的提示
            tooltips.Add(new TooltipLine(Mod, "DynamicEffect", $"在一定范围(半径{SearchRadius / 16}格)内搜索敌对生物，根据距离动态最多增加{DefenseMultiBonus}防御、{EnduranceMultiBonus * 100}%耐力和额外100%生命回复"));
            tooltips.Add(new TooltipLine(Mod, "DynamicEffect2", $"在一定范围(半径{SearchRadius / 16}格)内搜索敌对生物，根据距离动态最多增加{AttackMultiBonus * 100}%近战伤害、{TrueMeleeMultiBonus * 100}%真近战伤害和{AttackSpeedMultiBonus * 100}%攻击速度"));

            // 添加额外的提示
            tooltips.Add(new TooltipLine(Mod, "AdditionalInfo", $"光明对勇敢者的赏赐,同时还有昔日光辉的战旗的影子"));
        }
        }

        /// <summary>
        /// 定义饰品的制作食谱。
        /// </summary>
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<SunAmulet>()); // 创建饰品的食谱
            recipe.AddIngredient(ExpansionKeleCal.expansionkele.Find<ModItem>("LightAmulet").Type, 1); 
            recipe.AddIngredient(ExpansionKeleCal.calamity.Find<ModItem>("WarbanneroftheSun").Type, 1); 
            recipe.AddTile(TileID.TinkerersWorkbench); // 设置制作台为工匠台
            recipe.Register(); // 注册食谱
        }
    }
}