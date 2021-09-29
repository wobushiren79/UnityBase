using RotaryHeart.Lib;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIHandler : BaseUIHandler<UIHandler, UIManager>
{
    /// <summary>
    /// 获取打开的UI
    /// </summary>
    /// <returns></returns>
    public BaseUIComponent GetOpenUI()
    {
        for (int i = 0; i < manager.uiList.Count; i++)
        {
            BaseUIComponent itemUI = manager.uiList[i];
            if (itemUI.gameObject.activeSelf)
            {
                return itemUI;
            }
        }
        return null;
    }

    /// <summary>
    /// 获取打开UI的名字
    /// </summary>
    /// <returns></returns>
    public string GetOpenUIName()
    {
        for (int i = 0; i < manager.uiList.Count; i++)
        {
            BaseUIComponent itemUI = manager.uiList[i];
            if (itemUI.gameObject.activeSelf)
            {
                return itemUI.name;
            }
        }
        return null;
    }

    /// <summary>
    /// 根据UI的名字获取UI
    /// </summary>
    /// <param name="uiName"></param>
    /// <returns></returns>
    public T GetUI<T>(string uiName) where T : BaseUIComponent
    {
        if (manager.uiList == null || uiName.IsNull())
            return null;
        for (int i = 0; i < manager.uiList.Count; i++)
        {
            BaseUIComponent itemUI = manager.uiList[i];
            if (itemUI.name.Equals(uiName))
            {
                return itemUI as T;
            }
        }
        T uiComponent = manager.CreateUI<T>(uiName);
        if (uiComponent)
        {
            return uiComponent as T;
        }
        return null;
    }

    /// <summary>
    /// 获取UI
    /// </summary>
    /// <param name="uiEnum"></param>
    /// <returns></returns>
    public T GetUI<T>(UIEnum uiEnum) where T : BaseUIComponent
    {
        return GetUI<T>(uiEnum.GetEnumName());
    }

    /// <summary>
    /// 根据UI的名字获取UI列表
    /// </summary>
    /// <param name="uiName"></param>
    /// <returns></returns>
    public List<BaseUIComponent> GetUIList(string uiName)
    {
        if (manager.uiList == null || uiName.IsNull())
            return null;
        List<BaseUIComponent> tempuiList = new List<BaseUIComponent>();
        for (int i = 0; i < manager.uiList.Count; i++)
        {
            BaseUIComponent itemUI = manager.uiList[i];
            if (itemUI.name.Equals(uiName))
            {
                tempuiList.Add(itemUI);
            }
        }
        return tempuiList;
    }

    /// <summary>
    /// 根据UI的枚举获取UI列表
    /// </summary>
    /// <param name="uiEnum"></param>
    /// <returns></returns>
    public List<BaseUIComponent> GetUIList(UIEnum uiEnum)
    {
        return GetUIList(uiEnum.GetEnumName());
    }

    /// <summary>
    /// 通过UI的名字开启UI
    /// </summary>
    /// <param name="uiName"></param>
    public T OpenUI<T>(string uiName) where T : BaseUIComponent
    {
        if (uiName.IsNull())
            return null;
        for (int i = 0; i < manager.uiList.Count; i++)
        {
            BaseUIComponent itemUI = manager.uiList[i];
            if (itemUI.name.Equals(uiName))
            {
                itemUI.OpenUI();
                return itemUI as T;
            }
        }
        T uiComponent = manager.CreateUI<T>(uiName);
        if (uiComponent)
        {
            uiComponent.OpenUI();
            return uiComponent;
        }
        return null;
    }

    /// <summary>
    /// 开启UI
    /// </summary>
    /// <param name="uiEnum"></param>
    public T OpenUI<T>(UIEnum uiEnum) where T : BaseUIComponent
    {
        string uiName = uiEnum.GetEnumName();
        return OpenUI<T>(uiName);
    }


    /// <summary>
    /// 通过UI的名字关闭UI
    /// </summary>
    /// <param name="uiName"></param>
    public void CloseUI(string uiName)
    {
        if (manager.uiList == null || uiName.IsNull())
            return;
        for (int i = 0; i < manager.uiList.Count; i++)
        {
            BaseUIComponent itemUI = manager.uiList[i];
            if (itemUI.name.Equals(uiName))
            {
                itemUI.CloseUI();
            }
        }
    }

    /// <summary>
    /// 通过UI的枚举关闭UI
    /// </summary>
    /// <param name="uiEnum"></param>
    public void CloseUI(UIEnum uiEnum)
    {
        CloseUI(uiEnum.GetEnumName());
    }

    /// <summary>
    /// 关闭所有UI
    /// </summary>
    public void CloseAllUI()
    {
        for (int i = 0; i < manager.uiList.Count; i++)
        {
            BaseUIComponent itemUI = manager.uiList[i];
            if (itemUI.gameObject.activeSelf)
                itemUI.CloseUI();
        }
    }

    /// <summary>
    /// 通过UI的名字开启UI并关闭其他UI
    /// </summary>
    /// <param name="uiName"></param>
    public T OpenUIAndCloseOther<T>(string uiName) where T : BaseUIComponent
    {
        if (manager.uiList == null || uiName.IsNull())
            return null;
        //首先关闭其他UI
        for (int i = 0; i < manager.uiList.Count; i++)
        {
            BaseUIComponent itemUI = manager.uiList[i];
            if (!itemUI.name.Equals(uiName))
            {
                if (itemUI.gameObject.activeSelf)
                    itemUI.CloseUI();
            }
        }
        return OpenUI<T>(uiName);
    }

    /// <summary>
    /// 通过UI的枚举开启UI并关闭其他UI
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ui"></param>
    /// <returns></returns>
    public T OpenUIAndCloseOther<T>(UIEnum uiEnum) where T : BaseUIComponent
    {
        return OpenUIAndCloseOther<T>(uiEnum.GetEnumName());
    }

    /// <summary>
    /// 通过UI开启UI并关闭其他UI
    /// </summary>
    /// <param name="uiName"></param>
    public void OpenUIAndCloseOther(BaseUIComponent uiComponent)
    {
        if (manager.uiList == null || uiComponent == null)
            return;
        for (int i = 0; i < manager.uiList.Count; i++)
        {
            BaseUIComponent itemUI = manager.uiList[i];
            if (!itemUI == uiComponent)
            {
                itemUI.CloseUI();
            }
        }
        for (int i = 0; i < manager.uiList.Count; i++)
        {
            BaseUIComponent itemUI = manager.uiList[i];
            if (itemUI == uiComponent)
            {
                itemUI.OpenUI();
            }
        }
    }

    /// <summary>
    /// 刷新UI
    /// </summary>
    public void RefreshAllUI()
    {
        if (manager.uiList == null)
            return;
        for (int i = 0; i < manager.uiList.Count; i++)
        {
            BaseUIComponent itemUI = manager.uiList[i];
            itemUI.RefreshUI();
        }
    }

    /// <summary>
    /// 根据名字刷新UI
    /// </summary>
    /// <param name="uiName"></param>
    public void RefreshUI(string uiName)
    {
        if (manager.uiList == null || uiName.IsNull())
            return;
        for (int i = 0; i < manager.uiList.Count; i++)
        {
            BaseUIComponent itemUI = manager.uiList[i];
            if (itemUI.name.Equals(uiName))
            {
                itemUI.RefreshUI();
            }
        }
    }

    /// <summary>
    /// 根据枚举刷新UI
    /// </summary>
    /// <param name="uiEnum"></param>
    public void RefreshUI(UIEnum uiEnum)
    {
        RefreshUI(uiEnum.GetEnumName());
    }

    /// <summary>
    /// 打开弹窗
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dialogBean"></param>
    /// <param name="delayDelete"></param>
    /// <returns></returns>
    public T ShowDialog<T>(DialogBean dialogBean, float delayDelete = 0) where T : DialogView
    {
        return manager.CreateDialog<T>(dialogBean, delayDelete);
    }

    /// <summary>
    /// Toast提示
    /// </summary>
    /// <param name="hintContent"></param>
    public void ToastHint(string hintContent)
    {
        manager.CreateToast<ToastView>(ToastEnum.Normal, null, hintContent, 5);
    }

    public void ToastHint(string hintContent, float destoryTime)
    {
        manager.CreateToast<ToastView>(ToastEnum.Normal, null, hintContent, destoryTime);
    }

    public void ToastHint(Sprite toastIconSp, string hintContent)
    {
        manager.CreateToast<ToastView>(ToastEnum.Normal, toastIconSp, hintContent, 5);
    }

    public void ToastHint(Sprite toastIconSp, string hintContent, float destoryTime)
    {
        manager.CreateToast<ToastView>(ToastEnum.Normal, toastIconSp, hintContent, destoryTime);
    }

}