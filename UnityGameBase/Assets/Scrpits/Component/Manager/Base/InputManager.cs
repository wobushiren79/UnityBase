using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : BaseManager
{
    public GameInputActions inputActions;

    public Dictionary<InputActionUIEnum, InputAction> dicInputUI = new Dictionary<InputActionUIEnum, InputAction>();
    public Dictionary<string, InputAction> dicInputPlayer = new Dictionary<string, InputAction>();

    public virtual void Awake()
    {
        inputActions = new GameInputActions();
    }

    /// <summary>
    /// 获取UI数据
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public InputAction GetInputUIData(InputActionUIEnum name)
    {
        if (dicInputUI.TryGetValue(name, out InputAction value))
        {
            return value;
        }
        return null;
    }

    /// <summary>
    /// 获取Player数据
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public InputAction GetInputPlayerData(string name)
    {
        if (dicInputPlayer.TryGetValue(name, out InputAction value))
        {
            return value;
        }
        return null;
    }
}