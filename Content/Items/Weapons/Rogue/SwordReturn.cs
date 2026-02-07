using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.Items.Weapons.Rogue;

namespace ExpansionKeleCal.Content.Items.Weapons.Rogue
{
    /// <summary>
    /// 剑归宗 - 一把特殊的盗贼武器，投掷银短剑并在适当时候召回
    /// </summary>
    public class SwordReturn : RogueWeapon
    {
        public new string LocalizationCategory => "Items.Weapons";
        public static int UseTime=25;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("剑归宗");
            // Tooltip.SetDefault("投掷银短剑，右键召回\n潜伏攻击时伤害更高");
        }

        public override void SetDefaults()
        {
            Item.SetNameOverride("剑归宗");
            Item.damage = 25;
            Item.DamageType = ExpansionKeleCal.RogueDamageClassCal;
            Item.width = 24;
            Item.height = 24;
            Item.useTime = UseTime;
            Item.useAnimation = UseTime;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(silver: 30);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.shoot = ModContent.ProjectileType<Projectiles.SwordReturnProjectile>();
            Item.shootSpeed = 10f;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        // ... existing code ...
        // ... existing code ...
        public override bool CanUseItem(Player player)
        {
            // 检查是否已经投出了剑
            if (player.altFunctionUse == 2) // 右键
            {
                // 检查是否已经有投出的剑需要召回
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];
                    if (proj.active && proj.owner == player.whoAmI && proj.type == Item.shoot && proj.ai[0] == 0f)
                    {
                        Item.useTime = UseTime/2;
                        Item.useAnimation = UseTime/2;
                        return true; // 允许召回
                    }
                }
                return false; // 没有可召回的剑
            }
            else // 左键
            {
                Item.useTime = UseTime;
                Item.useAnimation = UseTime;
                // 检查是否已经有未召回的剑
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];
                    if (proj.active && proj.owner == player.whoAmI && proj.type == Item.shoot && proj.ai[0] == 0f)
                    {
                        return false; // 已经有一把未召回的剑
                    }
                }
                return true; // 可以投掷新剑
            }
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            // 如果是召回操作，则不应用潜行攻击效果
            if (player.altFunctionUse == 2)
            {
                // 清除潜行攻击标记，确保不消耗潜行值
                // player.Calamity().stealthStrikeThisFrame = false;
            }
        }
// ... existing code ...
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2) // 右键召回
            {
                // // 防止召回操作消耗潜行值
                // player.Calamity().stealthStrikeThisFrame = false;
                
                // 寻找已投出的剑并标记为召回状态
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];
                    if (proj.active && proj.owner == player.whoAmI && proj.type == type && proj.ai[0] == 0f)
                    {
                        proj.ai[0] = 1f; // 标记为召回状态
                        proj.netUpdate = true;
                        
                        // 召回时伤害为原始伤害的40%
                        proj.damage = (int)(proj.originalDamage * 0.4f);
                        // 确保召回时也有无限穿透
                        proj.penetrate = -1;
                        // 重置局部无敌帧计时器，以便能再次命中敌人
                        proj.localNPCImmunity = new int[200];
                    }
                }
            }
            else // 左键投掷
            {
                // 检查是否可以进行潜行攻击
                bool stealthStrike = player.Calamity().StealthStrikeAvailable();

                if (stealthStrike)
                {
                    // 潜行攻击：生成5个弹幕，分别向不同角度发射
                    float speed = velocity.Length();
                    float baseRotation = velocity.ToRotation();
                    
                    // 生成5个弹幕，角度分别为：0, ±20°, ±40°
                    for (int i = 0; i < 5; i++)
                    {
                        float rotationOffset = MathHelper.ToRadians(i switch
                        {
                            0 => 0f,      // 中间
                            1 => 20f,     // 右20度
                            2 => -20f,    // 左20度
                            3 => 40f,     // 右40度
                            4 => -40f,    // 左40度
                            _ => 0f
                        });
                        
                        Vector2 newVelocity = (baseRotation + rotationOffset).ToRotationVector2() * speed;
                        int proj = Projectile.NewProjectile(source, position, newVelocity, type, damage, knockback, player.whoAmI);
                        
                        // 设置弹幕的初始旋转角度为速度方向+Pi/4
                        Main.projectile[proj].rotation = Main.projectile[proj].velocity.ToRotation() + MathHelper.PiOver4;
                        
                        // 设置原始伤害用于后续计算
                        Main.projectile[proj].originalDamage = damage;
                        
                        // 潜行攻击标记和特殊属性
                        Main.projectile[proj].Calamity().stealthStrike = true;
                        Main.projectile[proj].ai[1] = 1f; // 标记为潜行攻击
                        
                        // 设置潜行攻击特有的属性
                        Main.projectile[proj].penetrate = -1; // 无限穿透
                        Main.projectile[proj].tileCollide = false; // 无碰撞
                        Main.projectile[proj].damage = (int)(damage * 0.9f); // 90%伤害
                    }
                }
                else
                {
                    // 投掷新剑
                    int proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
                    
                    // 设置弹幕的初始旋转角度为速度方向+Pi/4
                    Main.projectile[proj].rotation = Main.projectile[proj].velocity.ToRotation() + MathHelper.PiOver4;
                    
                    // 设置原始伤害用于后续计算
                    Main.projectile[proj].originalDamage = damage;
                    
                    // 确保初始状态具有无限穿透
                    Main.projectile[proj].penetrate = -1;
                }
            }

            return false;
        }
// ... existing code ...        
        public override float StealthDamageMultiplier => 1.5f;
        public override float StealthVelocityMultiplier => 1f;
        public override float StealthKnockbackMultiplier => 1f;
        public override bool AdditionalStealthCheck() => false;

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
        }

        public override bool ConsumeItem(Player player) => false;

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SilverShortsword, 1)
                .AddIngredient(ItemID.TungstenShortsword, 1)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}