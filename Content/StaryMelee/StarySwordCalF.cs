using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System;
using System.Collections.Generic;





namespace ExpansionKeleCal.Content.StaryMelee
{
    public class StarySwordCalF : StarySwordCalAbs
    {
        public override string LocalizationCategory => "StaryMelee";
        public override int BaseDamage => 754;
        public override int UseTime => 18;
        public override int Rarity => ExpansionKeleCal.calamity.Find<ModRarity>("Violet").Type;
        public override int Crit => 31;
        
        // 特定属性
        private new const string setNameOverride="幻星元剑F";

        private const string introduction ="幻星元剑D的升级版";

        public override void SetStaticDefaults()
        {
            // 可以根据需要添加静态默认值
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // 添加自定义的 tooltip  
            // TooltipLine line = new TooltipLine(Mod, "SetNameOverride", introduction);
            // tooltips.Add(line);
        }
        
        public override void AddRecipes()
        {
            // 创建合成配方
            Recipe recipe = Recipe.Create(Type);
            recipe.AddIngredient(ExpansionKeleCal.calamity.Find<ModItem>("MiracleMatter").Type, 1);
            recipe.AddIngredient(ModContent.ItemType<StarySwordCalE>(), 1);
            recipe.AddTile(ExpansionKeleCal.calamity.Find<ModTile>("DraedonsForge"));
            recipe.Register();
        }
    }
}
//     public class StarySwordCalF : ModItem
//     {
//          private const int LeftClickDamage = 754;
//         private const float LeftClickKnockBack = 8f;
//         private const float LeftClickShootSpeed = 10f;
//         private const int LeftClickUseTime = 18;
//         private const int LeftClickUseAnimation = LeftClickUseTime;

//         private const int constcrit = 31;
//         private const int constrare = 4;
//         private const string setNameOverride="幻星元剑F";

//         private const string introduction ="幻星元剑E的升级版";


//          private const int RightClickDamage = (int)(LeftClickDamage*rightClickCoefficient); // 示例伤害值
//         private const float RightClickKnockBack = 10f; // 示例击退值
//         private const float RightClickShootSpeed = 40f; // 示例射击速度
//         private const int RightClickUseTime = (int)(LeftClickUseTime*2); // 示例使用时间
//         private const int RightClickUseAnimation = RightClickUseTime; // 示例使用动画时间
//         const double rightClickCoefficient = 2.7;
//         //int rightClickBulletType=ExpansionKeleCal.expansionkele.Find<ModProjectile>("SharkyBullet").Type;

//         double critOverflowCoefficient = 1.5;

//         public double damageGenericUp  { get; set; }
//         public double damageMeleeUp  { get; set; }
        
//          private bool isDashing = false;
//         private Vector2 AfterDashVelocity; // 记录冲刺前的速度
//         private const float dashingUp = 40f;
//         private const float dashingUp2=dashingUp / 8 ;

//         private int totalDashingTime=10;

//         private float dashingGravity = 0.0f;

//         private float defaultGravity =0.4f;

//         private int immuningtime=80+10;

//         //private int cooldownTicks = 7;
//         private int BoostDuration;

//          private const int BoostTime=90;

//         private int defenseBoost=130;

//         private int lifeRegenBoost=(int)(20*20);

//         private float speedBoost=0.55f;

//         private float enduranceBoost=0.5f;

//         private int wingTimeBoost=4;

         

//         // public override Color? GetAlpha(Color lightColor) {
// 		// 	return Color.Red;
// 		// }

//         public override void SetStaticDefaults()
// 	{
// 		//ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Item.type] = true;
// 	}

//         public override void SetDefaults()
//         {
//         Item.SetNameOverride(setNameOverride);
//         Item.width = 80;
// 		Item.height = 80;
// 		Item.damage = LeftClickDamage;
//         if(ExpansionKeleCal.calamity!=null){
//             Item.damage=(int)(Item.damage*1);
//         }
// 		Item.DamageType = DamageClass.Melee;
// 		Item.useAnimation = LeftClickUseAnimation ;
// 		Item.useStyle = ItemUseStyleID.Swing;
// 		Item.useTime = LeftClickUseTime;
// 		Item.useTurn = false;//自动转向
// 		Item.knockBack = LeftClickKnockBack;
// 		Item.UseSound = SoundID.Item1;
// 		Item.autoReuse = true;
//         Item.value = Item.sellPrice(gold: 1); // 价值
//         Item.rare = ItemRarityID.Blue; // 稀有度
//         Item.shoot=ExpansionKeleCal.expansionkele.Find<ModProjectile>("ColaProjectile").Type;
//         //Item.shoot = ModContent.ProjectileType<ColaProjectile>(); // 射弹类型
//         Item.shootSpeed =  LeftClickShootSpeed; // 射弹速度
//         Item.crit = constcrit;
//         Item.rare = constrare;
//         }

//          public override Vector2? HoldoutOffset() {  
//              return new Vector2(0, 0); // 持有偏移量。  
//          }  

        
//     public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
// {
//     // 发射原有的射弹
//     Terraria.Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);

//     // 发射 LifePercentageProjectile
//     // int lifePercentageProjectileType = ExpansionKeleCal.expansionkele.Find<ModProjectile>("LifePercentageProjectile").Type;
//     // Terraria.Projectile.NewProjectile(source, position, velocity, lifePercentageProjectileType, (int)(0.7*damage*Math.Sqrt(damage/Item.damage)), knockback, player.whoAmI);

//     return false; // 返回 false 以防止默认行为
// }

//     public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers) {
//     // 临时移除敌人的免疫状态
//     target.immune[player.whoAmI] = 0;
// }
//     public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> tooltips)
//         {
//             // 添加自定义的 tooltip  
//             TooltipLine line = new TooltipLine(Mod, setNameOverride, introduction);
//             tooltips.Add(line);
//         }
//         public override void AddRecipes()
//         {
//             // 创建 GaSniperA 武器的合成配方  
//             Recipe recipe = Recipe.Create(ModContent.ItemType<StarySwordCalF>()); // 替换为 GaSniperD 的类型  
// 	recipe.AddIngredient(ExpansionKeleCal.calamity.Find<ModItem>("MiracleMatter").Type, 1);
// 	recipe.AddIngredient(ModContent.ItemType<StarySwordCalE>(), 1);
//     recipe.AddTile(ExpansionKeleCal.calamity.Find<ModTile>("DraedonsForge"));//远古操纵机
//     recipe.Register();

    
//         }

//     public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone) {
//             // Additional effects on hit can be added here if needed
//             //近战增益

//             BoostDuration = BoostTime;
//             if(target.lifeMax<=8000)
//             {
//                 BoostDuration=(int)(BoostTime/5);
//             }
//             player.statDefense += defenseBoost;

//             // 增加玩家的移动速度15%
//             player.moveSpeed += speedBoost;

//             player.endurance += enduranceBoost;

//             // 增加玩家的回血速率 +10Hp/s
//             player.lifeRegen += lifeRegenBoost;

//             //恢复玩家的飞行时间20ticks
//             player.wingTime += wingTimeBoost;
//             player.immune = true;
//             player.immuneTime = (int)(immuningtime/4);
            
//             target.AddBuff(ExpansionKeleCal.expansionkele.Find<ModBuff>("ArmorPodwered").Type, 82);
//             target.AddBuff(BuffID.OnFire, 82);
//             int lifePercentageDamage = (int)(target.lifeMax * 0.006);  // 例如，10%生命值
//             damageDone=(int)(damageDone*Math.Pow(damageDone/Item.damage,1)+1);
//             damageDone+=lifePercentageDamage;
//             Vector2 position = player.Center;
//     Vector2 velocity = player.DirectionTo(target.Center).SafeNormalize(Vector2.UnitX) * 10f; // 调整速度和方向
//     Terraria.Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity, ExpansionKeleCal.expansionkele.Find<ModProjectile>("LifePercentageProjectile").Type, damageDone, 0f, player.whoAmI, target.whoAmI);
            
//         }
    
//     public override bool AltFunctionUse(Player player)
// {
//     return true; // 允许右键使用
    
// }

// public override bool? UseItem(Player player)
// {
//     //Main.NewText("进入 UseItem", 255, 255, 255);
//     //Main.NewText($"player.altFunctionUse: {player.altFunctionUse}", 255, 255, 255);
//     if (player.altFunctionUse == 2)
//     {
        
//         //Main.NewText("右键使用2", 255, 255, 255);
//         // 设置冲刺方向
//         // float lifeLoss = (float)(player.statLifeMax2 * 0.015 + player.statLife * 0.02 + 3);
//         //         player.statLife -= (int)lifeLoss;
//         //         player.HealEffect((int)lifeLoss, true);

//         //         // 检查生命值是否小于0
//         //         if (player.statLife <= 0)
//         //         {
//         //             player.immune = false;
//         //             player.immuneTime = 0;
//         //             player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " 受到了复仇的反噬"), 9999, 0);
//         //             return true;
//         //         }
//        float manaCost = (float)(player.statManaMax2 * 0.33);

//         if (player.statMana >= manaCost)
//         {
//             player.statMana -= (int)manaCost; // 扣除魔力值
//             player.ManaEffect((int)manaCost); // 魔力消耗效果

//             Vector2 direction = player.DirectionTo(Main.MouseWorld).SafeNormalize(Vector2.UnitX);
//             direction *= dashingUp; // 冲刺速度

//             // 设置无敌帧
//             player.immune = true;
//             player.immuneTime = immuningtime;

//             // 设置玩家速度以实现冲刺效果
//             player.velocity = direction;
//             AfterDashVelocity = player.velocity;

//             player.gravity = dashingGravity;

//             // 记录冲刺开始时间
//             player.itemAnimation = totalDashingTime; // 假设冲刺持续10帧
//             isDashing = true;

//             return true;
//         }
//         else
//         {
//             return false; // 魔力不足时返回 false
//         }
//     }
//     else
//     {
//         return base.UseItem(player);
//     }
// }

// public override void UpdateInventory(Player player)
//         {
//             base.UpdateInventory(player);
            

//             if (isDashing)
//             {
//                 player.gravity = dashingGravity;

//                 player.endurance += enduranceBoost;

//                 player.statDefense += defenseBoost*10;

//                 int remainingTime = player.itemAnimation;
//                 int thresholdTime = (int)(totalDashingTime*2 / 5); // 计算冲刺时间的1/5点

//                 if (remainingTime <= thresholdTime)
//                 {
//                     // 开始逐渐降低速度和恢复重力
//                     float t = (float)(thresholdTime - remainingTime) / thresholdTime; // 计算从1/5到结束的时间比例
//                     player.velocity = Vector2.Lerp(player.velocity, AfterDashVelocity / dashingUp2, t);
//                     player.gravity = MathHelper.Lerp(dashingGravity, defaultGravity, t);
//                 }

//                 if (remainingTime <= 0)
//                 {
//                     // 重置速度
//                     player.velocity = AfterDashVelocity / dashingUp2;

//                     // 恢复重力
//                     player.gravity = defaultGravity;

//                     // 重置冲刺状态标志
//                     isDashing = false;
//                 }
//             }

            

//     if (BoostDuration > 0)
//     {
//         // 如果防御增益仍在持续，减少计时器
//         BoostDuration--;
//         player.statDefense += defenseBoost;

//             // 增加玩家的移动速度15%
//         player.moveSpeed += speedBoost;
//         player.endurance += enduranceBoost;

//             // 增加玩家的回血速率 +10Hp/s
//         player.lifeRegen += lifeRegenBoost;



//     }



//         }
//     }
// }



