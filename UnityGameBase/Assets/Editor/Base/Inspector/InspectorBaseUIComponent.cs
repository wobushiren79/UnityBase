using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BaseUIComponent),true)]
public class InspectorBaseUIComponent : Editor
{
    protected readonly static string scrpitsTemplatesPath = "/Editor/ScrpitsTemplates/UI_BaseUIComponent.txt";

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (EditorUI.GUIButton("生成UICompont脚本", 120))
        {
            HandleForCreateUIComponent();
        }

    }

    //Hierarchy视图
    [MenuItem("GameObject/创建/UIComponent")]
    //Projects视图
    [MenuItem("Assets/创建/UIComponent")]
    public static void HandleForCreateUIComponent()
    {
        GameObject objSelect = Selection.activeGameObject;
        string fileName = "UI" + objSelect.name + "Component";

        string templatesPath = Application.dataPath + scrpitsTemplatesPath;
        string[] path = EditorUtil.GetScriptPath(fileName);

        //获取最后一个/的索引
        int lastIndex = path[0].LastIndexOf('/');
        string createPath = path[0].Substring(0,lastIndex);
        //规则替换
        Dictionary<string, string> dicReplace = ReplaceRole("UI" + objSelect.name);
        //创建文件
        EditorUtil.CreateClass(dicReplace, templatesPath, fileName, createPath);
    }

    /// <summary>
    /// 替换规则
    /// </summary>
    /// <param name="scripteContent"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    protected static Dictionary<string, string> ReplaceRole(string className)
    {
        //这里实现自定义的一些规则  
        Dictionary<string, string> dicReplaceData = new Dictionary<string, string>();
        dicReplaceData.Add("#ClassName#", className);
        return dicReplaceData;
    }

}