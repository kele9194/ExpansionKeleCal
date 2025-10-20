using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System;
using System.Collections.Generic;

namespace ExpansionKeleCal.Content.StaryMelee
{
    public abstract class StarySwordCalAbs : ModItem
    {
        // 基础属性
        public virtual string setNameOverride { get; }
        public virtual int BaseDamage { get; }
        public virtual int UseTime { get; }
        public virtual new int UseAnimation => UseTime;
        public virtual int Rarity { get; }
        public virtual int Crit { get; }
        public virtual int Width => 80;
        public virtual int Height => 80;
        
        public virtual float KnockBack => 8f;
        public virtual bool AutoReuse => true;
        public virtual int ShootSpeed => 10;
        public virtual bool UseTurn => true;
        
        // 增益效果属性
        protected virtual int BoostTime => 200;
        protected virtual int DefenseBoost => 10;
        protected virtual int LifeRegenBoost => 4 * 20;
        protected virtual float SpeedBoost => 0.1f;
        protected virtual float EnduranceBoost => 0.07f;
        public virtual int WingTimeBoost => 2;
        
        // 冲刺属性
        protected virtual float DashSpeed => 30f;
        protected virtual int DashDuration => 5;
        protected virtual int DashIFrames => 20;
        protected virtual float ManaCostFactor => 0.33f;
        protected virtual float DefaultGravity => 0.4f;
        
        // 冲刺功能相关字段
        private bool _isDashing = false;
        private Vector2 _afterDashVelocity;
        private int _totalDashingTime = 10;
        private int _boostDuration = 0;

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Item.type] = true;
        }
        
        public override void SetDefaults()
        {
            Item.SetNameOverride(setNameOverride);
            Item.damage = BaseDamage;
            Item.useTime = UseTime;
            Item.useAnimation = UseAnimation;
            Item.width = Width;
            Item.height = Height;
            Item.rare = Rarity;
            Item.crit = Crit;

            
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = UseTurn;
            Item.knockBack = KnockBack;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = AutoReuse;
            Item.value = Item.sellPrice(silver:(int)(Item.damage*0.3f));
            Item.shoot = ExpansionKeleCal.expansionkele.Find<ModProjectile>("ColaProjectile").Type;
            Item.shootSpeed = ShootSpeed;
        }

        public override Vector2? HoldoutOffset() 
        {  
            return new Vector2(0, 0);
        }
        
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            return false;
        }
        
        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers) 
        {
            // 临时移除敌人的免疫状态
            target.immune[player.whoAmI] = 0;
        }
        
        // 增益效果处理
        public virtual void ApplyHitEffects(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            // 应用增益效果
            _boostDuration = BoostTime;
            if(target.lifeMax <= 8000)
            {
                _boostDuration = (int)(BoostTime / 5);
            }
            
            player.statDefense += DefenseBoost;
            player.moveSpeed += SpeedBoost;
            player.endurance += EnduranceBoost;
            player.lifeRegen += LifeRegenBoost;
            player.wingTime += WingTimeBoost;
            
            // 应用减益效果
            target.AddBuff(ExpansionKeleCal.expansionkele.Find<ModBuff>("ArmorPodwered").Type, 82);
            target.AddBuff(BuffID.OnFire, 82);
            
            // 基于目标生命值的额外伤害
            int lifePercentageDamage = (int)(target.lifeMax * 0.006);
            int modifiedDamage = (int)(damageDone * Math.Pow(damageDone / (float)Item.damage, 1) + 1);
            modifiedDamage += lifePercentageDamage;
            
            // 创建生命百分比伤害射弹
            Vector2 position = player.Center;
            Vector2 velocity = player.DirectionTo(target.Center).SafeNormalize(Vector2.UnitX) * 10f;
            Projectile.NewProjectile(
                player.GetSource_ItemUse(Item), 
                position, 
                velocity, 
                ExpansionKeleCal.expansionkele.Find<ModProjectile>("LifePercentageProjectile").Type, 
                modifiedDamage, 
                0f, 
                player.whoAmI, 
                target.whoAmI
            );
        }
        
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone) 
        {
            ApplyHitEffects(player, target, hit, damageDone);
        }
        
        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
            
            // 更新增益效果
            if (_boostDuration > 0)
            {
                _boostDuration--;
                player.statDefense += DefenseBoost;
                player.moveSpeed += SpeedBoost;
                player.endurance += EnduranceBoost;
                player.lifeRegen += LifeRegenBoost;
                
                // 增益效果结束时移除效果
                if (_boostDuration <= 0)
                {
                    player.statDefense -= DefenseBoost;
                    player.moveSpeed -= SpeedBoost;
                    player.endurance -= EnduranceBoost;
                    player.lifeRegen -= LifeRegenBoost;
                }
            }
            
            // 更新冲刺状态
            if (_isDashing)
            {
                _totalDashingTime--;
                
                // 维持冲锋速度和无重力状态
                player.velocity = _afterDashVelocity;
                player.gravity = 0f;
                
                if (_totalDashingTime >= 0) 
                {
                    for (int i = 0; i < 5; i++) 
                    {
                        Dust dust = Dust.NewDustDirect(
                            player.position, 
                            player.width, 
                            player.height, 
                            DustID.BlueFlare, 
                            -player.velocity.X * 0.1f, 
                            -player.velocity.Y * 0.1f, 
                            100, 
                            default(Color), 
                            1.5f
                        );
                        dust.noGravity = true;
                        dust.scale = 2.5f;
                    }
                }
                
                // 冲锋结束
                if (_totalDashingTime <= 0) 
                {
                    _isDashing = false;
                    player.gravity = DefaultGravity;
                    player.velocity = _afterDashVelocity * 0.35f;
                }
            }
        }
        
        public override bool AltFunctionUse(Player player)
        {
            return true; // 允许右键使用
        }
        
        public override bool CanUseItem(Player player) 
        {
            // 右键使用逻辑（冲刺）
            if (player.altFunctionUse == 2) 
            {
                Item.noUseGraphic = true;
                Item.shoot = ProjectileID.None;
                
                // 检查魔力是否足够
                int manaCost = (int)(player.statManaMax * ManaCostFactor);
                if (player.statMana < manaCost) 
                {
                    return false;
                }
            }
            else
            {
                Item.noUseGraphic = false;
                Item.shoot = ExpansionKeleCal.expansionkele.Find<ModProjectile>("ColaProjectile").Type;
            }
            return base.CanUseItem(player);
        }

        public override bool? UseItem(Player player) 
        {
            if (player.altFunctionUse == 2) 
            {
                // 消耗魔力
                int manaCost = (int)(player.statManaMax * ManaCostFactor);
                player.statMana -= manaCost;
                
                // 设置无敌帧
                player.immune = true;
                player.immuneTime = DashIFrames;
                
                // 开始冲锋
                _isDashing = true;
                Vector2 direction = player.DirectionTo(Main.MouseWorld).SafeNormalize(Vector2.UnitX);
                _afterDashVelocity = direction * DashSpeed;
                player.velocity = _afterDashVelocity;
                
                // 设置重力为0
                player.gravity = 0f;
                
                // 设置计时器
                _totalDashingTime = DashDuration;
                
                return true;
            }
            return base.UseItem(player);
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // 子类需实现具体内容
        }
        
        public override void AddRecipes()
        {
            // 子类需实现具体配方
        }
    }
}