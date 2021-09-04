using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
public class NodeBaseEditorWindow : EditorWindow
{
    private NodeBaseView nodeView;

    [MenuItem("Custom/Node/NodeBaseEditor")]
    public static void OpenWindow()
    {
        var window = GetWindow<NodeBaseEditorWindow>();
        window.titleContent = new GUIContent("NodeBaseEditor");
    }

    public void OnEnable()
    {
        ConstructGraphView();
    }

    public void OnDestroy()
    {
        rootVisualElement.Remove(nodeView);
    }

    public void ConstructGraphView()
    {
        nodeView = new NodeBaseView
        {
            name = "NodeView"
        };

        nodeView.StretchToParentSize();
        rootVisualElement.Add(nodeView);
    }


}