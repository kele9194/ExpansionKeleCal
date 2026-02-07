using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace ExpansionKeleCal.Content.Tools { 

public static class ItemUtils
    {
        public static int CalculateValueFromRecipes(ModItem item, float profitMargin = 1.0f,int defaultPrice =1000)
        {
            var recipes = new List<Recipe>();
            
            // 遍历所有配方，找出结果为此物品的配方
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe currentRecipe = Main.recipe[i]; // 修改变量名为 currentRecipe
                if (currentRecipe.createItem.type == item.Type)
                {
                    recipes.Add(currentRecipe);
                }
            }

            // 如果没有配方，返回默认值0
            if (recipes.Count == 0)
                return defaultPrice;

            // 使用第一个配方进行计算（通常是最主要的配方）
            var recipe = recipes[0];
            int totalValue = 0;

            // 计算所有材料的价值
            foreach ((Item ingredient, int stack) in recipe.requiredItem.ToArray().WithStack())
            {
                totalValue += ingredient.value * stack;
            }

            // 应用利润率并确保结果至少为1铜币
            int calculatedValue = (int)(totalValue * profitMargin);
            return calculatedValue > 0 ? calculatedValue : 1;
        }

        public static int CalculateRarityFromRecipes(ModItem item, int defaultRarity = Terraria.ID.ItemRarityID.Green)
        {
            var recipes = new List<Recipe>();
            
            // 遍历所有配方，找出结果为此物品的配方
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe currentRecipe = Main.recipe[i];
                if (currentRecipe.createItem.type == item.Type)
                {
                    recipes.Add(currentRecipe);
                }
            }

            // 如果没有配方，返回默认稀有度（绿色）
            if (recipes.Count == 0)
                return defaultRarity;

            // 使用第一个配方进行计算
            var recipe = recipes[0];
            int highestRarity = int.MinValue;

            // 找到所有材料中最高的稀有度
            foreach ((Item ingredient, int stack) in recipe.requiredItem.ToArray().WithStack())
            {
                if (ingredient.rare > highestRarity)
                {
                    highestRarity = ingredient.rare;
                }
            }

            // 如果找到材料，则返回最高稀有度，否则返回默认稀有度
            return highestRarity != int.MinValue ? highestRarity : defaultRarity;
        } 
    }
    public static class EnumerableExtensions
{
    public static IEnumerable<(Item item, int stack)> WithStack(this Item[] items)
    {
        foreach (var item in items)
        {
            if (item != null && item.type > 0)
                yield return (item, item.stack);
        }
    }
}
}