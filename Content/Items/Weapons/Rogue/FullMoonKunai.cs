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
    /// <summary>
    /// 望月苦无 - 一种消耗型投掷武器
    /// 普通攻击发射穿透2个敌人的苦无
    /// 潜伏攻击发射穿透4个敌人的苦无，伤害为1.4倍，并在被穿透敌人身上生成6个向外均匀分布的追踪苦无分身
    /// </summary>
    public class FullMoonKunai : RogueWeapon, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("望月苦无");
            // Tooltip.SetDefault("投掷穿透性苦无，潜伏攻击时更强力");
        }

        public override void SetDefaults()
        {
            //Item.SetNameOverride("望月苦无");
            Item.damage = 92;         // 基础伤害值
            Item.DamageType = ExpansionKeleCal.RogueDamageClassCal;              // 投掷伤害类型
            Item.width = 18;                                     // 物品宽高
            Item.height = 32;
            Item.useTime = 18;                                   // 使用时间（帧数）
            Item.useAnimation = 18;                              // 动画持续时间
            Item.useStyle = ItemUseStyleID.Swing;                // 使用样式为挥舞
            Item.noMelee = true;                                 // 关闭近战攻击判定
            Item.noUseGraphic = true;                            // 使用时隐藏物品图形
            Item.knockBack = 3;                                  // 击退值
            Item.value = Item.sellPrice(gold: 5);                // 卖出价格
            Item.rare = ItemRarityID.Pink;                       // 稀有度：粉红
            Item.UseSound = SoundID.Item1;                       // 使用音效
            Item.autoReuse = true;                               // 自动重用
            Item.shoot = ModContent.ProjectileType<FullMoonKunaiProjectile>(); // 发射的弹幕类型
            Item.shootSpeed = 25f;                               // 弹幕初始速度
            Item.maxStack = 9999;                                 // 最大堆叠数
            Item.consumable = true;                              // 可消耗
        }
        public override float StealthDamageMultiplier => 1.8f;
        public override float StealthVelocityMultiplier => 2f;
        public override float StealthKnockbackMultiplier => 1f;
        public override bool AdditionalStealthCheck() => false;

        /// <summary>
        /// 自定义射击行为
        /// </summary>
        // ... existing code ...
        /// <summary>
        /// 自定义射击行为
        /// </summary>
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // 发射苦无弹幕
            int proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            
            // 检查是否可以进行潜行攻击
            if (player.Calamity().StealthStrikeAvailable())
            {
                // 设置弹幕为潜行攻击
                Main.projectile[proj].Calamity().stealthStrike = true;
                // 提升穿透力到4次
                Main.projectile[proj].penetrate = 4;
                // 增加潜行攻击标记，以便在弹幕中处理特殊效果
                Main.projectile[proj].ai[0] = 1f; // 使用ai[0]标记为潜行攻击
            }
            else
            {
                // 普通攻击穿透2个敌人
                Main.projectile[proj].penetrate = 2;
            }

            return false; // 阻止原版弹幕发射
        }
// ... existing code ...

        /// <summary>
        /// 添加自定义提示信息
        /// </summary>
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // tooltips.Add(new TooltipLine(Mod, "FullMoonKunaiTooltip1",
            //     "普通攻击：发射穿透2个敌人的苦无"));
            // tooltips.Add(new TooltipLine(Mod, "FullMoonKunaiTooltip2",
            //     "潜伏攻击：穿透4个敌人，伤害提高至1.4倍，并在敌人身上生成追踪分身"));
        }

        public override void ModifyStatsExtra(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
        }

        public override bool ConsumeItem(Player player) => true;

        /// <summary>
        /// 注册合成配方：需要8个【满月锭】，在铁砧上合成。
        /// </summary>
        public override void AddRecipes()
        {
            CreateRecipe(250) // 一次合成50个
                .AddIngredient(ExpansionKeleCal.expansionkele.Find<ModItem>("FullMoonBar"), 1) // 材料修正
                .AddTile(TileID.Anvils) // 合成台使用铁砧
                .Register();
        }
    }
}