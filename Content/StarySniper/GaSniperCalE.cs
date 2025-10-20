using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ExpansionKeleCal.Content.StarySniper
{
    public class GaSniperCalE : GaSniperAbs
    {
        public override string LocalizationCategory => "StarySniper";
        // private const string SetNameOverride = "SG幻星元狙击步枪-B";
        const float RightClickCoefficient = 3f;
        // 重写基础属性
        public override int BaseDamage => 3249;
        public override float KnockBack => 8f;
        public override float ShootSpeed => 32f;
        public override int UseTime => 29;
        public override int Crit => 46;
        public override int Rarity => ExpansionKeleCal.calamity.Find<ModRarity>("Violet").Type;
        // public override string ItemName => SetNameOverride;
        // public override string introduction => "该狙击步枪型号A升级版,使用火枪子弹会转化为高速子弹。";

        // 重写右键增强属性
        public override float RightClickDamageMultiplier => RightClickCoefficient;

        public override void AddRecipes()
        {
            // 创建 GaSniperA 武器的合成配方  
            Recipe recipe = Recipe.Create(Type);
            recipe.AddIngredient(ExpansionKeleCal.calamity.Find<ModItem>("AuricBar").Type, 5);
            recipe.AddIngredient(ModContent.ItemType<GaSniperCalD>(), 1);
            recipe.AddTile(ExpansionKeleCal.calamity.Find<ModTile>("CosmicAnvil"));//远古操纵机
            recipe.Register(); // 注册配方  
	}  
        }
}
	
// 	public class GaSniperCalE : ModItem
// 	{// 右键属性
//          private const int LeftClickDamage = 3249;
//         private const float LeftClickKnockBack = 8f;
//         private const float LeftClickShootSpeed = 32f;
//         private const int LeftClickUseTime = 29;
//         private const int LeftClickUseAnimation = LeftClickUseTime;

//         private const int constcrit = 46;
//         private const int constrare = 10;
//         private const string setNameOverride="SG幻星元狙击步枪E";


//          private const int RightClickDamage = (int)(LeftClickDamage*rightClickCoefficient); // 示例伤害值
//         private const float RightClickKnockBack = 10f; // 示例击退值
//         private const float RightClickShootSpeed = 40f; // 示例射击速度
//         private const int RightClickUseTime = (int)(LeftClickUseTime*2); // 示例使用时间
//         private const int RightClickUseAnimation = RightClickUseTime; // 示例使用动画时间
//         const double rightClickCoefficient = 3;
//         //int rightClickBulletType=ExpansionKeleCal.expansionkele.Find<ModProjectile>("SharkyBullet").Type;

//         double critOverflowCoefficient = 3.5;
       
		

//         public override void SetDefaults()
//         {
//             Item.SetNameOverride(setNameOverride);
//             Item.width = 80; // The width of item hitbox
//             Item.height = 31; // The height of item hitbox
//             Item.damage = LeftClickDamage;
//             Item.autoReuse = true;  
//             Item.DamageType = DamageClass.Ranged; 
//             Item.knockBack = LeftClickKnockBack; 
//             Item.noMelee = true; 
            
//             Item.shootSpeed = LeftClickShootSpeed; 
//             Item.useAnimation = LeftClickUseTime; 
//             Item.useTime = LeftClickUseAnimation; 
//              Item.UseSound = ExpansionKeleCal.SniperSound;
//             Item.useStyle = ItemUseStyleID.Shoot; 
//             Item.value = Item.buyPrice(gold: 270); 
//             Item.crit = constcrit;
//             Item.rare = ItemRarityID.LightRed; 

//             Item.shoot = ProjectileID.Bullet;
//             Item.useAmmo = AmmoID.Bullet;

//             // Custom ammo and shooting homing projectiles
//             // Item.shoot = ModContent.ProjectileType<Projectiles.ExampleHomingProjectile>();
//             // Item.useAmmo = ModContent.ItemType<ExampleCustomAmmo>(); // Restrict the type of ammo the weapon can use, so that the weapon cannot use other ammos
//         }

        

//         public override void SetStaticDefaults()
// 	{
// 		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Item.type] = true;
// 	}

//         public override bool AltFunctionUse(Player player)
//         {
//             return true; // 允许右键使用
//         }

//         public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
//         {
//             focustime=0;
//             double damageGenericUp=player.GetDamage(DamageClass.Ranged).ApplyTo(1)-1;
//             double damageRangedUp=player.GetDamage(DamageClass.Ranged).ApplyTo(1)-1;
//             int actualCrit=constcrit+(int)(1*player.GetCritChance(DamageClass.Ranged)+player.GetCritChance(DamageClass.Generic));
//             int actualRightDamage=(int)(RightClickDamage * (1+damageGenericUp+damageRangedUp));
//             // 根据鼠标按钮设置不同的弹药类型和属性
//             if (player.altFunctionUse == 2) // 右键
//             {
// 				//Main.NewText($"Using SharkyBullet with type {shotType}");
//                 type = ExpansionKeleCal.expansionkele.Find<ModProjectile>("SharkyBullet").Type;
//                 damage = (int)(damage*rightClickCoefficient/2);
//                 knockback = RightClickKnockBack;
//                 Item.shootSpeed = RightClickShootSpeed;
//                 player.itemAnimation = RightClickUseAnimation;
//                 player.itemTime = RightClickUseTime;
//                 Item.crit = constcrit;
//                 if(actualCrit>=100)
//                 {
//                     damage=(int)(damage*(1+critOverflowCoefficient*(actualCrit*0.01-1))); 
//                 }
//             }
//             else // 左键
//             {
// 				if(actualCrit>=100)
//                 {
//                     damage=(int)(damage*(1+critOverflowCoefficient*(actualCrit*0.01-1)));
//                 }
//             }
            

//             // 使用 Projectile.NewProjectile 方法创建新的弹丸
//             Terraria.Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);

//             // 注意可能与官方冲突，可能建议在官方上面
//             return false; // 返回 false 以防止默认发射行为
//         }


// 		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
// 		public override void AddRecipes()  
// 	{  
//     // 创建 GaSniperA 武器的合成配方  
//     Recipe recipe = Recipe.Create(ModContent.ItemType<GaSniperCalE>()); 
// 	recipe.AddIngredient(ExpansionKeleCal.calamity.Find<ModItem>("AuricBar").Type, 5);
// 	recipe.AddIngredient(ModContent.ItemType<GaSniperCalD>(), 1);
//     recipe.AddTile(ExpansionKeleCal.calamity.Find<ModTile>("CosmicAnvil"));//远古操纵机
//     recipe.Register(); // 注册配方  
// 	}  
	
// 	  // 此方法可以调整武器在玩家手中的位置。调整这些值直到与图形效果匹配。  
//         public override Vector2? HoldoutOffset() {  
//             return new Vector2(-17f, -2f); // 持有偏移量。  
//         }  
// 		public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> tooltips) {  
//             // 添加自定义的 tooltip  
//             TooltipLine line = new TooltipLine(Mod, "GaSniperCalETooltip", "该狙击步枪CalD型号升级版,使用火枪子弹,钨/银子弹时,会转化为高速子弹。");  
//             tooltips.Add(line);  
//         }  

//         // 修改发射统计数据的方法。  
//         //public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {  
//             // 每个从这把枪发射的弹丸都有1/3的概率成为自定义弹丸。  
//        //     if (Main.rand.NextBool?(3)) {  
//          //       type = ModContent.ProjectileType<ExampleInstancedProjectile>(); // 替换为你的自定义弹丸类型。  
//         //    }  
//        // } 
// 	   private float focustime;
//         private float focusbonus;
// 	   public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {  
//             // 检查当前使用的弹药类型  
//             // 如果弹药是火枪子弹、钨子弹或银子弹，转换为高速子弹  
//             if (type == ProjectileID.Bullet ) {  
//                 type = ProjectileID.BulletHighVelocity; // 转换为高速子弹  
//             }  
//             if (player.velocity == Vector2.Zero)
//             {
//                 damage=(int)(damage*1.2*(1+focusbonus));
//             }
//             else 
//             {
//                 damage=(int)(damage*(1+focusbonus));
//             }
// 	   }
//        public override void UpdateInventory(Player player)
//         {
            
//             if(focustime<300&&player.HeldItem.type==Item.type){
//                 focustime++;
//             }
//             focusbonus=Math.Min(focustime/Item.useAnimation-1,2);
            
//             base.UpdateInventory(player);
//             // 检查玩家是否站在不动
            
//         }

		


// 	}
// }
