using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using CalamityMod;

namespace ExpansionKeleCal.Content.Projectiles
{
    /// <summary>
    /// 望月苦无弹幕
    /// 普通攻击：穿透2个敌人
    /// 潜行攻击：穿透4个敌人，并在被击中的敌人身上生成追踪苦无分身
    /// </summary>
    public class FullMoonKunaiProjectile : ModProjectile
    {
        // 弹幕穿透次数
        private const int NormalPenetrate = 2;
        private const int StealthPenetrate = 4;
        
        // 已击中的NPC列表        
        public override void SetStaticDefaults()
        {
            // 设置尾迹长度和模式
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        // ... existing code ...
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 26;
            Projectile.friendly = true;
            Projectile.penetrate = NormalPenetrate; // 穿透2个敌人
            Projectile.timeLeft = 600;
            Projectile.light = 0.6f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.DamageType = ExpansionKeleCal.RogueDamageClassCal;
            Projectile.extraUpdates = 0;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15; // 修改为15帧无敌时间
        }
// ... existing code ...

        public override void AI()
        {
            // 添加发光效果
            Lighting.AddLight(Projectile.Center, Color.Cyan.ToVector3() * 0.5f);
            
            // 旋转动画
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            
            // 重力效果
            //Projectile.velocity.Y += 0.15f;
            
            // 添加粒子效果
            if (Main.rand.NextBool(4))
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
                    DustID.BlueTorch, -Projectile.velocity.X * 0.1f, -Projectile.velocity.Y * 0.1f,
                    100, default, 1f).noGravity = true;
            }
            
            // 检查是否为潜行攻击，并在适当时机生成追踪分身
            if (Projectile.ai[0] == 1f && Projectile.numHits > 0)
            {
                // 当击中敌人后，生成追踪苦无分身
                SpawnTrackingKnives();
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // 碰撞时播放音效
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            
            // 简单的反弹效果
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X * 0.8f;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y * 0.8f;
            }
            
            // 减少时间以免无限弹跳
            Projectile.timeLeft -= 30;
            
            return false;
        }
        
        public override bool PreDraw(ref Color lightColor)
        {
            // 可以在这里添加自定义绘制效果
            return base.PreDraw(ref lightColor);
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {      
                // 如果是潜行攻击，则生成追踪苦无分身
                if (Projectile.ai[0] == 1f)
                {
                    SpawnTrackingKnivesOnNPC(target);
                }
            }
        
        
        // 在被击中的NPC身上生成追踪苦无分身
        private void SpawnTrackingKnivesOnNPC(NPC target)
        {
            // 生成6个均匀分布的追踪苦无分身
            for (int i = 0; i < 6; i++)
            {
                float angle = MathHelper.TwoPi / 6f * i;
                Vector2 velocity = angle.ToRotationVector2() * 4f;
                Vector2 position = target.Center;
                
                // 创建追踪苦无分身
                int proj = Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    position,
                    velocity,
                    ModContent.ProjectileType<FullMoonKunaiTrackingProjectile>(),
                    (int)(Projectile.damage * 0.4f),
                    Projectile.knockBack * 0.3f,
                    Projectile.owner
                );
                
                // 设置追踪目标
                if (proj.WithinBounds(Main.maxProjectiles))
                {
                    Main.projectile[proj].ai[0] = target.whoAmI;
                    Main.projectile[proj].ai[1] = 1f; // 标记为已设置目标
                }
            }
        }
        
        // 在弹幕位置生成追踪苦无分身（备用方法）
        private void SpawnTrackingKnives()
        {
            // 这个方法可以用于在弹幕击中敌人时生成追踪刀
        }
        
    }

    /// <summary>
    /// 望月苦无追踪分身弹幕
    /// </summary>
    // ... existing code ...
    /// <summary>
    /// 望月苦无追踪分身弹幕
    /// </summary>
    public class FullMoonKunaiTrackingProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // 设置尾迹长度和模式
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.penetrate = 1; // 只穿透一个敌人
            Projectile.timeLeft = 180; // 存在时间较短
            Projectile.light = 0.4f;
            Projectile.alpha=127;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = ExpansionKeleCal.RogueDamageClassCal;
            Projectile.extraUpdates = 1; // 增加更新频率以获得更平滑的追踪
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15;
            
            // 初始无伤害
            Projectile.friendly = false;
        }

        public override void AI()
        {
            // 添加发光效果
            Lighting.AddLight(Projectile.Center, Color.Cyan.ToVector3() * 0.3f);
            
            // 旋转动画
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            
            // 添加粒子效果
            if (Main.rand.NextBool(3))
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
                    DustID.BlueTorch, -Projectile.velocity.X * 0.1f, -Projectile.velocity.Y * 0.1f,
                    100, default, 0.7f).noGravity = true;
            }
            
            // 前10帧不造成伤害
            if (Projectile.timeLeft <= 160) // 180-10=170
            {
                Projectile.friendly = true; // 10帧后开始造成伤害
                // 使用ExpansionKele的追踪算法
                // 速度6f，最大追踪距离400f，转向阻力5f
                ExpansionKele.Content.Customs.ProjectileHelper.FindAndMoveTowardsTarget(Projectile, 15f, 400f, 5f);
            }
        }
// ... existing code ...
// ... existing code ...

        
        public override bool PreDraw(ref Color lightColor)
        {
            // 可以在这里添加自定义绘制效果
            return base.PreDraw(ref lightColor);
        }
    }
}