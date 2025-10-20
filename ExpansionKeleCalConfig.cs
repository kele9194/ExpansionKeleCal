using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Microsoft.Xna.Framework.Input;
using Terraria;
using System.ComponentModel;

namespace ExpansionKeleCal
{
    public class ExpansionKeleCalConfig : ModConfig
    {
         public override ConfigScope Mode => ConfigScope.ServerSide;

        
        // [Label("激光常开")]
        // [Tooltip("如果启用，激光将始终显示。")]
        [DefaultValue(true)] // 设置默认值
        //用这的，别用加载的
        public bool LaserAlwaysOn;

        [DefaultValue(true)]
        public bool detailedTooltip;
        
        
        
        
    }
}