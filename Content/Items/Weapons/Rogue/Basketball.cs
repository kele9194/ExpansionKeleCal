using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using ExpansionKeleCal.Content.Projectiles;
using CalamityMod;
using CalamityMod.Items.Weapons.Rogue;

namespace ExpansionKeleCal.Content.Items.Weapons.Rogue
{
    public class Basketball : RogueWeapon
    {
        public new string LocalizationCategory => "Items.Weapons";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("篮球");
            // Tooltip.SetDefault("投掷篮球，潜伏攻击时更强力");
        }

        public override void SetDefaults()
        {
            //Item.SetNameOverride("篮球");
            Item.damage = 20;         // 基础伤害值
            Item.DamageType = ExpansionKeleCal.RogueDamageClassCal;              // 投掷伤害类型
            Item.width = 24;                                     // 物品宽高
            Item.height = 24;
            Item.useTime = 20;                                   // 使用时间（帧数）
            Item.useAnimation = 20;                              // 动画持续时间
            Item.useStyle = ItemUseStyleID.Swing;                // 使用样式为挥舞
            Item.noMelee = true;                                 // 关闭近战攻击判定
            Item.noUseGraphic = true;                            // 使用时隐藏物品图形
            Item.knockBack = 4;                                  // 击退值
            Item.value = Item.sellPrice(silver: 50);             // 卖出价格
            Item.rare = ItemRarityID.Green;                      // 稀有度：绿
            Item.UseSound = SoundID.Item1;                       // 使用音效
            Item.autoReuse = true;                               // 自动重用
            Item.shoot = ModContent.ProjectileType<BasketballProjectile>(); // 发射的弹幕类型
            Item.shootSpeed = 8f;                                // 弹幕初始速度
            Item.maxStack = 9999;                                // 最大堆叠数
            Item.consumable = true;                              // 可消耗
        }

        /// <summary>
        /// 自定义射击行为
        /// </summary>
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // 发射篮球弹幕
            int proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            Main.projectile[proj].originalDamage = damage;
            // 检查是否可以进行潜行攻击
            if (player.Calamity().StealthStrikeAvailable())
            {
                // 设置弹幕为潜行攻击
                Main.projectile[proj].Calamity().stealthStrike = true;
                // 设置潜行攻击标记，以便在弹幕中处理特殊效果
                Main.projectile[proj].ai[0] = 1f; // 使用ai[0]标记为潜行攻击
                Main.projectile[proj].originalDamage = damage;
                // 潜伏攻击速度大幅提升且不受重力影响
            }

            return false; // 阻止原版弹幕发射
        }
        public override float StealthDamageMultiplier => 1.4f;
        public override float StealthVelocityMultiplier => 1.2f;
        public override float StealthKnockbackMultiplier => 1f;
        public override bool AdditionalStealthCheck() => false;

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {

        }

        public override bool ConsumeItem(Player player) => true;

        /// <summary>
        /// 注册合成配方：需要1个皮革，在工作台合成150个。
        /// </summary>
        public override void AddRecipes()
        {
            CreateRecipe(150)
                .AddIngredient(ItemID.Leather, 1)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}