using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BaseDataController : BaseMVCController<BaseDataModel, IBaseDataView>
{
    private Dictionary<BaseDataEnum, BaseDataBean> mMapData;
    public BaseDataController(BaseMonoBehaviour content, IBaseDataView view) : base(content, view)
    {
        InitAllBaseData();
    }
    public override void InitData()
    {

    }

    public void InitAllBaseData()
    {
        mMapData = new Dictionary<BaseDataEnum, BaseDataBean>();
        List<BaseDataBean> listData = GetModel().GetAllBaseData();
        for (int i = 0; i < listData.Count; i++)
        {
            BaseDataBean itemData = listData[i];
            mMapData.Add(itemData.name.GetEnum<BaseDataEnum>(), itemData);
        }
    }

    public BaseDataBean GetBaseData(BaseDataEnum baseDataType)
    {
        if (mMapData == null)
            return null;
        if (mMapData.TryGetValue(baseDataType,out BaseDataBean baseData))
        {
            return baseData;
        }
        return null;
    } 
}