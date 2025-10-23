//using ExpansionKele.Content.Weapon;
//using CalamityMod.Items.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using System.Collections;
using System.Reflection;
using Microsoft.Xna.Framework.Input;
using Terraria.Audio;

namespace ExpansionKeleCal
{
    // Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
    public class ExpansionKeleCal : Mod
    {
        public static Mod calamity;

        public static Mod expansionkele;
        
        public static ModKeybind StarKeyBindCal;
        public static DamageClass RogueDamageClassCal ;   
        

        public override void Load()
        {
            if (ModLoader.HasMod("CalamityMod"))
            {
                calamity = ModLoader.GetMod("CalamityMod");
                RogueDamageClassCal=calamity.Find<DamageClass>("RogueDamageClass");  
            }
            else
            {
                calamity = null;
            }

            if (ModLoader.HasMod("ExpansionKele"))
            {
                expansionkele = ModLoader.GetMod("ExpansionKele");
            }
            else
            {
                expansionkele = null;
            }

            StarKeyBindCal = KeybindLoader.RegisterKeybind(this, "StarArmorSetBonus(Cal)", Keys.F);
        }
            public static SoundStyle SniperSound = new SoundStyle("ExpansionKeleCal/Content/Audio/SniperSound")
        {
            Volume = 0.3f,
            PitchVariance = 0.2f,
            MaxInstances = 3,
        };

        /*private void ModifyExpansionKeleItems()
{
    if (expansionkele != null)
    {
        // 获取 ExpansionKele 模组中的某个物品
        ModItem moditem = ExpansionKeleCal.expansionkele.Find<ModItem>("GaSniperA"); // 假设物品名称为 GasniperA

         if (moditem != null)
        {
            // 获取物品的 Item 实例
            Terraria.Item item = moditem.Item;

            if (item != null)
            {
                // 修改物品的伤害属性
                item.damage = 100; // 将物品的伤害设置为 100
                item.crit=50;
                this.Logger.Info("GaSniperA 确认已经过此次修改");
            }
           else
            {
                this.Logger.Info("GaSniperA 物品的 Item 实例未找到。");
            }
        }
        else
        {
            this.Logger.Info("GaSniperA 物品未找到。");
        }
    }
}*/

        
    }
}

