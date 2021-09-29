using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : BaseUIManager
{
    //所有的UI控件
    public List<BaseUIComponent> uiList = new List<BaseUIComponent>();
    //所有的dialog列表
    public List<DialogView> dialogList = new List<DialogView>();

    //所有的dialog模型
    public Dictionary<string, GameObject> listDialogModel = new Dictionary<string, GameObject>();
    //所有的Toast模型
    public Dictionary<string, GameObject> listToastModel = new Dictionary<string, GameObject>();

    /// <summary>
    /// 创建UI
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="uiName"></param>
    /// <returns></returns>
    public T CreateUI<T>(string uiName) where T : BaseUIComponent
    {
        //GameObject uiModel = LoadAssetUtil.SyncLoadAsset<GameObject>("ui/ui", uiName);
        BaseUIComponent uiModel = LoadResourcesUtil.SyncLoadData<BaseUIComponent>($"UI/{uiName}");
        if (uiModel)
        {
            Transform tfContainer = GetUITypeContainer(UITypeEnum.UIBase);
            GameObject objUIComponent = Instantiate(tfContainer.gameObject, uiModel.gameObject);
            objUIComponent.SetActive(false);
            objUIComponent.name = objUIComponent.name.Replace("(Clone)", "");
            T uiComponent = objUIComponent.GetComponent<T>();
            uiList.Add(uiComponent);
            return uiComponent;
        }
        else
        {
            LogUtil.LogError("没有找到指定UI：" + "UI/" + uiName);
            return null;
        }
    }

    /// <summary>
    /// 获取弹窗模型
    /// </summary>
    /// <param name="dialogName"></param>
    /// <returns></returns>
    public GameObject GetDialogModel(string dialogName)
    {
        return GetModel(listDialogModel, "ui/dialog", dialogName);
    }

    /// <summary>
    /// 创建弹窗
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dialogBean"></param>
    /// <param name="delayDelete"></param>
    /// <returns></returns>
    public T CreateDialog<T>(DialogBean dialogBean, float delayDelete = 0) where T : DialogView
    {
        string dialogName = dialogBean.dialogType.GetEnumName();
        GameObject objDialogModel = GetDialogModel(dialogName);
        if (objDialogModel == null)
        {
            LogUtil.LogError("没有找到指定Dialog：" + dialogName);
            return null;
        }

        GameObject objDialog = Instantiate(gameObject, objDialogModel);
        if (objDialog)
        {
            DialogView dialogView = objDialog.GetComponent<DialogView>();
            if (dialogView == null)
                Destroy(objDialog);
            dialogView.SetCallBack(dialogBean.callBack);
            dialogView.SetAction(dialogBean.actionSubmit, dialogBean.actionCancel);
            dialogView.SetData(dialogBean);
            if (delayDelete != 0)
                dialogView.SetDelayDelete(delayDelete);

            //改变焦点
            EventSystem.current.SetSelectedGameObject(objDialog);
            dialogList.Add(dialogView);
            return dialogView as T;
        }
        else
        {
            LogUtil.LogError("没有实例化Dialog成功：" + dialogName);
            return null;
        }
    }

    /// <summary>
    /// 移除弹窗
    /// </summary>
    /// <param name="dialogView"></param>
    public void RemoveDialog(DialogView dialogView)
    {
        if (dialogView != null && dialogList.Contains(dialogView))
            dialogList.Remove(dialogView);
    }

    /// <summary>
    /// 关闭所有弹窗
    /// </summary>
    public void CloseAllDialog()
    {
        for (int i = 0; i < dialogList.Count; i++)
        {
            DialogView dialogView = dialogList[i];
            if (dialogView != null)
                dialogView.DestroyDialog();
        }
        dialogList.Clear();
    }

    /// <summary>
    /// 获取toast模型
    /// </summary>
    /// <returns></returns>
    public GameObject GetToastModel(string toastName)
    {
        return GetModelForResources(listToastModel, $"UI/Toast/{toastName}");
    }

    /// <summary>
    /// 创建toast
    /// </summary>
    /// <param name="toastType"></param>
    /// <param name="toastIconSp"></param>
    /// <param name="toastContentStr"></param>
    /// <param name="destoryTime"></param>
    public void CreateToast<T>(ToastEnum toastType, Sprite toastIconSp, string toastContentStr, float destoryTime) where T : ToastView
    {
        string toastName = toastType.GetEnumName();
        GameObject objToastModel = GetToastModel(toastName);
        if (objToastModel == null)
        {
            LogUtil.LogError("没有找到指定Toast：" + toastName);
            return;
        }
        Transform objToastContainer = GetUITypeContainer(UITypeEnum.Toast);
        GameObject objToast = Instantiate(objToastContainer.gameObject, objToastModel);
        if (objToast)
        {
            ToastView toastView = objToast.GetComponent<ToastView>();
            toastView.SetData(toastIconSp, toastContentStr, destoryTime);
        }
        else
        {
            LogUtil.LogError("实例化Toast失败" + toastName);
        }
    }
}