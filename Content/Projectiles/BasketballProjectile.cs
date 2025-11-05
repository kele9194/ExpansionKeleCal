using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace ExpansionKeleCal.Content.Projectiles
{
    /// <summary>
    /// 篮球弹幕
    /// 普通攻击：受重力影响，穿透数10并反弹，每次反弹伤害增加10%
    /// 潜伏攻击：伤害为1.5倍，无限穿透，反弹后伤害增加20%，但仍受重力影响
    /// </summary>
    public class BasketballProjectile : ModProjectile
    {
        // 记录反弹次数，用于计算伤害增幅
        private int bounceCount = 0;
        // 每次反弹伤害增加比例（普通攻击）
        private const float NormalDamageIncreasePerBounce = 0.1f;
        // 每次反弹伤害增加比例（潜伏攻击）
        private const float StealthDamageIncreasePerBounce = 0.2f;
        private const int maxTimeLeft = 1200;
        private const int normalPenetrate = 10;

        public override void SetStaticDefaults()
        {
            // 设置尾迹长度和模式
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.penetrate = -1; // 默认无限穿透，普通攻击会在AI中修改
            Projectile.timeLeft = maxTimeLeft; // 存活时间长一些，普通攻击会在AI中修改
            Projectile.light = 0.3f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.DamageType = ExpansionKeleCal.RogueDamageClassCal;
            Projectile.extraUpdates = 0;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15;
        }

        public override void AI()
        {
            // 添加发光效果
            Lighting.AddLight(Projectile.Center, Color.White.ToVector3() * 0.3f);

            // 旋转动画
            Projectile.rotation += Projectile.velocity.Length() * 0.01f * Projectile.direction;

            // 检查是否为普通攻击（非潜伏攻击）
            if (Projectile.ai[0] != 1f)
            {
                // 普通攻击设置：减半存活时间
                if (Projectile.timeLeft <= maxTimeLeft / 2)
                {
                    Projectile.Kill();
                }

                // 普通攻击设置：限制穿透数为10
                if (Projectile.penetrate == -1)
                {
                    Projectile.penetrate = normalPenetrate;
                }
            }

            // 所有攻击都受重力影响
            // 应用重力
            Projectile.velocity.Y += 0.2f;

            // 添加粒子效果
            if (Main.rand.NextBool(5))
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
                    DustID.WoodFurniture, -Projectile.velocity.X * 0.1f, -Projectile.velocity.Y * 0.1f,
                    100, default, 1f).noGravity = true;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // 实现无限反弹
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X * 0.95f; // 小小的能量损失
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y * 0.95f; // 小小的能量损失
            }

            // 增加反弹计数
            bounceCount++;

            // 播放撞击声音
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

            // 根据是否是潜伏攻击确定伤害增幅比例
            float damageIncreasePerBounce = (Projectile.ai[0] == 1f) ? 
                StealthDamageIncreasePerBounce : NormalDamageIncreasePerBounce;

            // 更新伤害（每次反弹按对应比例增加伤害）
            Projectile.damage = (int)(Projectile.originalDamage * (1 + bounceCount * damageIncreasePerBounce));

            // 返回false表示不销毁弹幕
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            // 可以在这里添加自定义绘制效果
            return base.PreDraw(ref lightColor);
        }
    }
}