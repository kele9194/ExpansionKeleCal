using ExpansionKeleCal.Commons;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ExpansionKeleCal
{  
    public class ExpansionKeleGlobalItem : GlobalItem
    {


        public override void UseItemFrame(Item item, Player player)
        {
            // 应用改进的物品定位逻辑到所有近战挥舞类武器
            if (item.useStyle == ItemUseStyleID.Swing)
            {
                ExpansionKeleCalUtils.ConductBetterItemLocation(player);
            }
        }

    }
}
