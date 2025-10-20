using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
namespace ExpansionKeleCal.Content
{
    public static class ArmorData
    {
        public static  int[] HelmetDefense={22,24,27,  30,32,40}; 
        public static  int[] PlateDefense={26,29,33,  37,40,50}; 
        
        public static  int[] LeggingsDefense = {23,26,29,  32,34,40}; // 防御值
        public static float[] GenericDamageBonus = {40,44,50,  56,62,75}; // 伤害加成
        public static  int[] CritChance = {20,22,23,  24,25,30}; // 暴击加成
        public static  float[] MoveSpeedBonus = {35,38,41,  44,47,60}; // 移速加成
        public static  int[] MeleeCritChance = {13,14,15,  16,17,22}; // 近战暴击加成
        public static  int[] RangedCritChance = {7,7,8,  8,9,12}; // 远程暴击加成
        public static  int[] SummonDamage = RangedCritChance; // 召唤伤害加成
        public static  int[] RogueCritChance = MeleeCritChance; // 盗贼暴击加成
        public static  float[] MeleeSpeed = {23,25,29,  32,35,41}; // 近战攻速加成
        public static  int[] MaxMinions = {5,6,7,  8,9,12}; // 最大仆从数加成
        public static  int[] MaxTurrets = {2,2,3,  3,4,6}; // 最大哨兵数加成
        public static  int[] MaxMana = {100,120,140,  160,180,200}; // 最大魔力值加成
        public static  float[] ManaCostReduction = {15,16,18,  20,22,33}; // 魔力减耗
        public static  float[] AmmoCostReduction = {25,25,25,  25,25,25}; // 弹药减耗
        public static  int[] StealthMax = {113,120,127,  134,141,180}; // 潜伏值
        public static  int[] WhipRange={26,28,32,  36,40,60};//鞭子范围

        public static float CalculateA(float t)
        {
            float discriminant = (float)Math.Sqrt(1 + 4f / t);
            float a1 = (-1f + discriminant) / 2f;
            
            // 根据实际情况选择合适的解
            return a1 ;
        }
        
    }
    }