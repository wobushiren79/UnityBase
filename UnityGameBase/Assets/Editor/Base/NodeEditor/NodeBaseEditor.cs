using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NodeBaseEditor : EditorWindow
{
    protected List<NodeBaseView> nodes;
    protected GUIStyle nodeStyle;

    [MenuItem("Custom/NodeEditor")]
    private static void OpenWindow()
    {
        NodeBaseEditor window = GetWindow<NodeBaseEditor>();
        window.titleContent = new GUIContent("NodeEditor");
    }

    private void OnEnable()
    {
        nodeStyle = new GUIStyle();
        nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
        nodeStyle.border = new RectOffset(12, 12, 12, 12);
    }

    private void OnGUI()
    {
        DrawNodes();

        ProcessNodeEvents(Event.current);
        ProcessEvents(Event.current);

        if (GUI.changed) Repaint();
    }

    /// <summary>
    /// 回执节点
    /// </summary>
    private void DrawNodes()
    {
        if (nodes == null)
            return;
        for (int i = 0; i < nodes.Count; i++)
        {
            nodes[i].Draw();
        }
    }

    /// <summary>
    /// 操作事件
    /// </summary>
    /// <param name="e"></param>
    private void ProcessEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.MouseDown:
                //右键点击打开创建节点蓝
                if (e.button == 1)
                {
                    ProcessContextMenu(e.mousePosition);
                }
                break;
        }
    }

    /// <summary>
    /// 节点操作事件
    /// </summary>
    /// <param name="e"></param>
    private void ProcessNodeEvents(Event e)
    {
        if (nodes != null)
        {
            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                bool guiChanged = nodes[i].ProcessEvents(e);

                if (guiChanged)
                {
                    GUI.changed = true;
                }
            }
        }
    }

    /// <summary>
    /// 菜单
    /// </summary>
    /// <param name="mousePosition"></param>
    private void ProcessContextMenu(Vector2 mousePosition)
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("创建节点"), false, () => OnClickForAddNode(mousePosition));
        genericMenu.ShowAsContext();
    }


    /// <summary>
    /// 菜单点击创建节点
    /// </summary>
    /// <param name="mousePosition"></param>
    private void OnClickForAddNode(Vector2 mousePosition)
    {
        if (nodes == null)
        {
            nodes = new List<NodeBaseView>();
        }
        NodeBaseView node = new NodeBaseView(mousePosition, 200, 50, nodeStyle);
        node.title = "sfa";
        nodes.Add(node);
    }
}