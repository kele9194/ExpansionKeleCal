using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Input;
using System;

using Microsoft.Xna.Framework;
using ExpansionKeleCal.Content.StaryArmor;

namespace ExpansionKeleCal
{
    public class ExpansionKeleCalPlayer : ModPlayer
    {
        // private Keys? setBonusKey = null; // 缓存键绑定
        private int buffDuration = 504; // 增益持续时间，默认5秒

        


        

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