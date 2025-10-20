using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace ExpansionKeleCal
{
    public static class ReflectionHelper
    {
        public static void ApplyRogueStealthAlter(Player player, float rogueStealthMax)
        {
            if (ExpansionKeleCal.calamity != null)
            {
                var calamityPlayerType = player.GetModPlayer(ExpansionKeleCal.calamity.Find<ModPlayer>("CalamityPlayer"));
                if (calamityPlayerType != null)
                {
                    // 获取 rogueStealthMax 字段
                    FieldInfo rogueStealthMaxField = calamityPlayerType.GetType().GetField("rogueStealthMax", BindingFlags.Public | BindingFlags.Instance);
                    if (rogueStealthMaxField != null)
                    {
                        // 获取当前值并增加 rogueStealthMax
                        float currentValue = (float)rogueStealthMaxField.GetValue(calamityPlayerType);
                        rogueStealthMaxField.SetValue(calamityPlayerType, currentValue + rogueStealthMax);
                    }

                    // 获取 wearingRogueArmor 字段
                    FieldInfo wearingRogueArmorField = calamityPlayerType.GetType().GetField("wearingRogueArmor", BindingFlags.Public | BindingFlags.Instance);
                    if (wearingRogueArmorField != null)
                    {
                        // 设置为 true
                        wearingRogueArmorField.SetValue(calamityPlayerType, true);
                    }
                }
            }
        }

        public static void ApplyRogueStealth(Player player, float rogueStealthMax)
        {
            if (ExpansionKeleCal.expansionkele == null) 
            {
                // 如果ExpansionKele模组不存在，使用本地实现作为替补
                ApplyRogueStealthAlter(player, rogueStealthMax);
                return;
            }

            try
            {
                // 获取ReflectionHelper类型
                Type reflectionHelperType = GetExpansionKeleType("ExpansionKele.ReflectionHelper");
                if (reflectionHelperType == null)
                {
                    // 如果找不到ExpansionKele的ReflectionHelper类型，使用本地实现作为替补
                    ApplyRogueStealthAlter(player, rogueStealthMax);
                    return;
                }

                // 获取ApplyRogueStealth方法
                MethodInfo method = reflectionHelperType.GetMethod("ApplyRogueStealth", 
                    BindingFlags.Public | BindingFlags.Static);
                
                if (method != null)
                {
                    // 调用ExpansionKele中的方法
                    method.Invoke(null, new object[] { player, rogueStealthMax });
                }
                else
                {
                    // 如果找不到方法，使用本地实现作为替补
                    ApplyRogueStealthAlter(player, rogueStealthMax);
                }
            }
            catch (Exception ex)
            {
                // 如果反射调用失败，使用本地实现作为替补
                ModContent.GetInstance<ExpansionKeleCal>().Logger.Error($"CallExpansionKeleApplyRogueStealth failed: {ex}");
                ApplyRogueStealthAlter(player, rogueStealthMax);
            }
        }
    

        /// <summary>
        /// 从ExpansionKele模组中获取指定的类类型
        /// </summary>
        /// <param name="typeName">完整类型名称（包括命名空间）</param>
        /// <returns>找到的类型，如果未找到则返回null</returns>
        public static Type GetExpansionKeleType(string typeName)
        {
            // 确保模组已加载
            if (ExpansionKeleCal.expansionkele == null)
                return null;

            // 通过反射获取类型
            Type type = ExpansionKeleCal.expansionkele.Code.GetType(typeName);
            
            return type;
        }

        /// <summary>
        /// 从ExpansionKele模组中获取指定的抽象类类型
        /// </summary>
        /// <param name="abstractClassName">抽象类完整名称（包括命名空间）</param>
        /// <returns>找到的抽象类类型，如果未找到则返回null</returns>
        public static Type GetExpansionKeleAbstractClass(string abstractClassName)
        {
            // 确保模组已加载
            if (ExpansionKeleCal.expansionkele == null)
                return null;

            // 通过反射获取类型
            Type type = ExpansionKeleCal.expansionkele.Code.GetType(abstractClassName);
            
            // 确保这是一个抽象类
            if (type != null && type.IsAbstract && type.IsClass)
            {
                return type;
            }
            
            return null;
        }

        /// <summary>
        /// 检查指定类型是否继承自ExpansionKele的抽象类
        /// </summary>
        /// <param name="className">要检查的类名称</param>
        /// <param name="abstractClassName">抽象类名称</param>
        /// <returns>是否继承自指定抽象类</returns>
        public static bool IsSubclassOfExpansionKeleAbstractClass(string className, string abstractClassName)
        {
            // 获取抽象类类型
            Type abstractType = GetExpansionKeleAbstractClass(abstractClassName);
            if (abstractType == null)
                return false;

            // 确保模组已加载
            if (ExpansionKeleCal.expansionkele == null)
                return false;

            // 获取要检查的类类型
            Type classType = ExpansionKeleCal.expansionkele.Code.GetType(className);
            if (classType == null)
                return false;

            // 检查是否继承自抽象类
            return abstractType.IsAssignableFrom(classType);
        }

        /// <summary>
        /// 创建继承自ExpansionKele抽象类的具体实例
        /// </summary>
        /// <param name="concreteClassName">具体实现类的完整名称</param>
        /// <param name="abstractClassName">抽象类完整名称</param>
        /// <param name="args">构造函数参数</param>
        /// <returns>创建的实例，如果失败则返回null</returns>
        public static object CreateInstanceFromAbstract(string concreteClassName, string abstractClassName, params object[] args)
        {
            // 检查抽象类是否存在
            Type abstractType = GetExpansionKeleAbstractClass(abstractClassName);
            if (abstractType == null)
            {
                ModContent.GetInstance<ExpansionKeleCal>().Logger.Warn($"Abstract class {abstractClassName} not found");
                return null;
            }

            // 确保模组已加载
            if (ExpansionKeleCal.expansionkele == null)
            {
                ModContent.GetInstance<ExpansionKeleCal>().Logger.Warn("ExpansionKele mod not loaded");
                return null;
            }

            try
            {
                // 获取具体实现类
                Type concreteType = ExpansionKeleCal.expansionkele.Code.GetType(concreteClassName);
                if (concreteType == null)
                {
                    ModContent.GetInstance<ExpansionKeleCal>().Logger.Warn($"Concrete class {concreteClassName} not found");
                    return null;
                }

                // 检查是否继承自抽象类
                if (!abstractType.IsAssignableFrom(concreteType))
                {
                    ModContent.GetInstance<ExpansionKeleCal>().Logger.Warn($"{concreteClassName} does not inherit from {abstractClassName}");
                    return null;
                }

                // 创建实例
                object instance = Activator.CreateInstance(concreteType, args);
                return instance;
            }
            catch (Exception ex)
            {
                ModContent.GetInstance<ExpansionKeleCal>().Logger.Error($"Failed to create instance of {concreteClassName}: {ex}");
                return null;
            }
        }

        /// <summary>
        /// 创建指定类型的实例
        /// </summary>
        /// <param name="typeName">类型名称</param>
        /// <returns>创建的实例，如果失败则返回null</returns>
        public static object CreateInstance(string typeName)
        {
            Type type = GetExpansionKeleType(typeName);
            if (type == null || type.IsAbstract)
                return null;

            try
            {
                object instance = Activator.CreateInstance(type);
                return instance;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 清除所有缓存
        /// </summary>
        public static void ClearCache()
        {
        }

        /// <summary>
        /// 调用指定类型的静态方法
        /// </summary>
        /// <param name="typeName">类型名称</param>
        /// <param name="methodName">方法名称</param>
        /// <param name="parameters">方法参数</param>
        /// <returns>方法返回值</returns>
        public static object InvokeStaticMethod(string typeName, string methodName, params object[] parameters)
        {
            Type type = GetExpansionKeleType(typeName);
            if (type == null)
                return null;

            MethodInfo method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
            if (method == null)
                return null;

            return method.Invoke(null, parameters);
        }

        /// <summary>
        /// 获取指定类型实例的字段值
        /// </summary>
        /// <param name="instance">实例对象</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="bindingFlags">绑定标志</param>
        /// <returns>字段值</returns>
        public static object GetFieldValue(object instance, string fieldName, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance)
        {
            if (instance == null)
                return null;

            FieldInfo field = instance.GetType().GetField(fieldName, bindingFlags);
            if (field == null)
                return null;

            return field.GetValue(instance);
        }

        /// <summary>
        /// 设置指定类型实例的字段值
        /// </summary>
        /// <param name="instance">实例对象</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="value">要设置的值</param>
        /// <param name="bindingFlags">绑定标志</param>
        public static void SetFieldValue(object instance, string fieldName, object value, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance)
        {
            if (instance == null)
                return;

            FieldInfo field = instance.GetType().GetField(fieldName, bindingFlags);
            if (field != null)
            {
                field.SetValue(instance, value);
            }
        }
    }
}