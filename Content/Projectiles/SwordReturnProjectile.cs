using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace ExpansionKeleCal.Content.Projectiles
{
    /// <summary>
    /// 剑归宗弹幕
    /// 投掷出去后会逐渐减速直至停止，然后可以通过右键召回
    /// </summary>
    public class SwordReturnProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // 设置尾迹长度和模式
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        // ... existing code ...
        // ... existing code ...
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.penetrate = -1; // 无限穿透
            Projectile.timeLeft = 1200;
            Projectile.light = 0.2f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.DamageType = ExpansionKeleCal.RogueDamageClassCal;
            Projectile.extraUpdates = 0;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20; // 局部无敌帧20帧
        }
// ... existing code ...

        // ... existing code ...
        // ... existing code ...
        // ... existing code ...
// ... existing code ...
        public override void AI()
        {
            // 添加发光效果
            Lighting.AddLight(Projectile.Center, Color.White.ToVector3() * 0.2f);

            // 设置旋转角度始终跟随速度方向+Pi/4偏移
            if (Projectile.velocity.Length() > 0.1f)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
            }

            // 检查是否处于召回状态 (ai[0] == 1f)
            if (Projectile.ai[0] == 1f)
            {
                // 新召回逻辑：在小范围内直接销毁
                Player player = Main.player[Projectile.owner];
                Vector2 direction = player.Center - Projectile.Center;
                float distance = direction.Length();
                
                // 如果距离玩家很近（比如50像素内），直接销毁
                if (distance < 50f)
                {
                    Projectile.Kill();
                }
                else
                {
                    // 否则仍然缓慢移动到玩家位置
                    direction.Normalize();
                    Projectile.velocity = direction * Math.Min(distance / 15f, 10f); // 更慢的速度
                }
                
                // 在召回过程中不干扰潜行条增长
                // 不执行任何会影响潜行状态的操作
            }
            else
            {
                // 正常飞行状态：逐渐减速直到停止
                if (Projectile.velocity.Length() > 0.1f)
                {
                    Projectile.velocity *= 0.98f; // 每帧减少2%的速度
                }
                else
                {
                    Projectile.velocity = Vector2.Zero; // 完全停止
                }

                // 移除重力效果
                // Projectile.velocity.Y += 0.2f;
            }
            
            // 添加粒子效果
            if (Main.rand.NextBool(8))
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
                    DustID.Silver, -Projectile.velocity.X * 0.1f, -Projectile.velocity.Y * 0.1f,
                    100, default, 1f).noGravity = true;
            }
            
            // 处理伤害递减
            if (Projectile.ai[0] == 0f) // 未召回状态
            {
                if (Projectile.ai[1] == 1f) // 潜行攻击
                {
                    // 潜行攻击每10帧检查一次伤害衰减
                    if (Projectile.timeLeft % 10 == 0)
                    {
                        // 每次穿透后伤害衰减至80%，最低40%
                        float damageMultiplier = Math.Max(0.4f, (float)Math.Pow(0.8f, Projectile.MaxUpdates * (1200 - Projectile.timeLeft) / 10));
                        Projectile.damage = Math.Max((int)(Projectile.originalDamage * 0.9f * damageMultiplier), (int)(Projectile.originalDamage * 0.4f));
                    }
                }
                else // 非潜行攻击
                {
                    // 每10帧检查一次伤害衰减（约0.16秒）
                    if (Projectile.timeLeft % 10 == 0)
                    {
                        // 计算伤害衰减，每次减少到70%，最低到10%
                        float damageMultiplier = Math.Max(0.1f, (float)Math.Pow(0.7f, (1200 - Projectile.timeLeft) / 10));
                        Projectile.damage = (int)(Projectile.originalDamage * damageMultiplier);
                    }
                }
            }
        }
// ... existing code ...
// ... existing code ...

        // ... existing code ...
        // ... existing code ...
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // 因为现在是无限穿透，所以不需要减少穿透次数
            // 只有在召回状态下才考虑销毁，但应该允许穿透多个敌人
            /*if (Projectile.ai[0] == 1f)
            {
                Projectile.Kill();
            }*/
            // 删除原有的逻辑，现在召回状态也应保持穿透
        }
// ... existing code ...
// ... existing code ...

        // ... existing code ...
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // 如果是潜行攻击，则不反弹
            if (Projectile.ai[1] == 1f)
            {
                return false; // 潜行攻击无视地形
            }
            
            // 如果不是处于召回状态，则反弹
            if (Projectile.ai[0] != 1f)
            {
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X * 0.5f;
                }
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y * 0.5f;
                }

                // 播放撞击声音
                Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
                
                return false;
            }
            
            return base.OnTileCollide(oldVelocity);
        }
// ... existing code ...

        public override bool PreDraw(ref Color lightColor)
        {
            // 可以在这里添加自定义绘制效果
            return base.PreDraw(ref lightColor);
        }
        
        public override void OnKill(int timeLeft)
        {
            // 播放销毁声音
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            
            // 产生粒子效果
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
                    DustID.Silver, -Projectile.velocity.X * 0.1f, -Projectile.velocity.Y * 0.1f,
                    100, default, 1f).noGravity = true;
            }
        }
    }
}