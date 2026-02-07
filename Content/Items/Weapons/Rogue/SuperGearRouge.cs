using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod;

namespace ExpansionKeleCal.Content.Items.Weapons.Rogue
{
    public class SuperGearRouge : RogueWeapon, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("超级齿轮");
            // Tooltip.SetDefault("投掷高科技齿轮，潜伏攻击时更强力");
        }

        public override void SetDefaults()
        {
            //Item.SetNameOverride("超级齿轮");
            Item.damage = 60;         // 基础伤害值
            Item.DamageType = ExpansionKeleCal.RogueDamageClassCal;              // 投掷伤害类型
            Item.width = 40;                                     // 物品宽高
            Item.height = 40;
            Item.useTime = 30;                                   // 使用时间（帧数）
            Item.useAnimation = 30;                              // 动画持续时间
            Item.useStyle = ItemUseStyleID.Swing;                // 使用样式为挥舞
            Item.noMelee = true;                                 // 关闭近战攻击判定
            Item.noUseGraphic = true;                            // 使用时隐藏物品图形
            Item.knockBack = 6f;                                 // 击退值
            Item.value = Item.sellPrice(gold: 2);                // 卖出价格
            Item.rare = ItemRarityID.Green;                      // 稀有度：绿
            Item.UseSound = SoundID.Item1;                       // 使用音效
            Item.autoReuse = true;                               // 自动重用
            Item.shoot = ModContent.ProjectileType<Projectiles.SuperGearRougeProjectile>(); // 发射的弹幕类型
            Item.shootSpeed = 14f;                               // 弹幕初始速度
            // 修改为非消耗性武器，允许拥有修饰语
            Item.maxStack = 1;                                   // 最大堆叠数设为1
            Item.consumable = false;                             // 设为不可消耗
        }

        // ... existing code ...
        public override float StealthDamageMultiplier => 2f;
        public override float StealthVelocityMultiplier => 2f;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // 发射齿轮弹幕
            int proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);

            // 检查是否可以进行潜行攻击
            if (player.Calamity().StealthStrikeAvailable())
            {
                // 设置弹幕为潜行攻击
                Main.projectile[proj].Calamity().stealthStrike = true;
                // 使用ai[0]标记为潜行攻击
                Main.projectile[proj].ai[0] = 1f;
            }

            return false; // 阻止原版弹幕发射
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Cog, 50)
                .AddIngredient(ItemID.SoulofFlight, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
// ... existing code ...
    }
}