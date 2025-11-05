using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Input;
using System;

using Microsoft.Xna.Framework;
using ExpansionKeleCal.Content.StaryArmor;
using Terraria.ID;

namespace ExpansionKeleCal
{
    public class ExpansionKeleCalPlayer : ModPlayer
    {
        // private Keys? setBonusKey = null; // 缓存键绑定
        private int buffDuration = 504; // 增益持续时间，默认5秒
        public override void PostUpdateEquips()
        {
            // 应用改进的物品定位逻辑到所有近战武器
            if (Player.HeldItem.type != ItemID.None && Player.itemAnimation > 0)
            {
                // 检查是否是近战武器
                if (Player.HeldItem.useStyle == ItemUseStyleID.Swing)
                {
                    ConductBetterItemLocation(Player);
                }
            }
        }

        public static void ConductBetterItemLocation(Player player)
        {
            float xoffset = 6f;
            float yoffset = -10f;
            
            if (player.itemAnimation < player.itemAnimationMax * 0.333)
                yoffset = 4f;
            else if (player.itemAnimation >= player.itemAnimationMax * 0.666)
                xoffset = -4f;
                
            player.itemLocation.X = player.Center.X + xoffset * player.direction;
            player.itemLocation.Y = player.MountedCenter.Y + yoffset;
            
            if (player.gravDir < 0)
                player.itemLocation.Y = player.Center.Y + (player.position.Y - player.itemLocation.Y);
        }

        


        

        // public override void Initialize()
        // {
        //     // 初始化时获取并解析键绑定配置
        //     var config = ModContent.GetInstance<ExpansionKeleConfig>();
        //     if (Enum.TryParse(config.SetBonusKey, out Keys key))
        //     {
        //         setBonusKey = key;
        //     }
        //     else
        //     {
        //         setBonusKey = Keys.F;
        //         Main.NewText("无效的键绑定配置，请检查配置文件。", Color.Red);
        //     }
        // }

        public override void PostUpdate()
        {
            // 使用 KeybindSystem 来检测按键是否刚刚按下
            if (ExpansionKeleCal.StarKeyBindCal.JustPressed)
            {
                
                // 使用 Player 属性访问当前玩家实例
                Player playerInstance = Player;

                // 检查玩家是否装备了完整的套装
                if (playerInstance.armor[0].type == ModContent.ItemType<StarHelmetCalA>() &&
                    playerInstance.armor[1].type == ModContent.ItemType<StarBreastplateCalA>() &&
                    playerInstance.armor[2].type == ModContent.ItemType<StarLeggingsCalA>())
                {
                    // 应用增益
                    playerInstance.AddBuff(ExpansionKeleCal.expansionkele.Find<ModBuff>("StarSetBonusBuff").Type, buffDuration);
                    
                }
                if (playerInstance.armor[0].type == ModContent.ItemType<StarHelmetCalB>() &&
                    playerInstance.armor[1].type == ModContent.ItemType<StarBreastplateCalB>() &&
                    playerInstance.armor[2].type == ModContent.ItemType<StarLeggingsCalB>())
                {
                    // 应用增益
                    playerInstance.AddBuff(ExpansionKeleCal.expansionkele.Find<ModBuff>("StarSetBonusBuff").Type, buffDuration);
                    //Main.NewText("检测通过", Color.Red);
                }
                if (playerInstance.armor[0].type == ModContent.ItemType<StarHelmetCalC>() &&
                    playerInstance.armor[1].type == ModContent.ItemType<StarBreastplateCalC>() &&
                    playerInstance.armor[2].type == ModContent.ItemType<StarLeggingsCalC>())
                {
                    // 应用增益
                    playerInstance.AddBuff(ExpansionKeleCal.expansionkele.Find<ModBuff>("StarSetBonusBuff").Type, buffDuration);
                    //Main.NewText("检测通过", Color.Red);
                }
                if (playerInstance.armor[0].type == ModContent.ItemType<StarHelmetCalD>() &&
                    playerInstance.armor[1].type == ModContent.ItemType<StarBreastplateCalD>() &&
                    playerInstance.armor[2].type == ModContent.ItemType<StarLeggingsCalD>())
                {
                    // 应用增益
                    playerInstance.AddBuff(ExpansionKeleCal.expansionkele.Find<ModBuff>("StarSetBonusBuff").Type, buffDuration);
                    //Main.NewText("检测通过", Color.Red);
                }
                if (playerInstance.armor[0].type == ModContent.ItemType<StarHelmetCalE>() &&
                    playerInstance.armor[1].type == ModContent.ItemType<StarBreastplateCalE>() &&
                    playerInstance.armor[2].type == ModContent.ItemType<StarLeggingsCalE>())
                {
                    // 应用增益
                    playerInstance.AddBuff(ExpansionKeleCal.expansionkele.Find<ModBuff>("StarSetBonusBuff").Type, buffDuration);
                    //Main.NewText("检测通过", Color.Red);
                }
                if (playerInstance.armor[0].type == ModContent.ItemType<StarHelmetCalX>() &&
                    playerInstance.armor[1].type == ModContent.ItemType<StarBreastplateCalX>() &&
                    playerInstance.armor[2].type == ModContent.ItemType<StarLeggingsCalX>())
                {
                    // 应用增益
                    playerInstance.AddBuff(ExpansionKeleCal.expansionkele.Find<ModBuff>("StarSetBonusBuff").Type, buffDuration);
                    //Main.NewText("检测通过", Color.Red);
                }
                
            }
        }
    }
}