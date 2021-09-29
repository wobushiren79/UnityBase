using UnityEngine;

public class Test : BaseMonoBehaviour
{

    private void OnGUI()
    {
        if (GUILayout.Button("Create"))
        {
            UIHandler.Instance.OpenUIAndCloseOther<UITest>(UIEnum.Test);
        }
        if (GUILayout.Button("Add"))
        {
            GameConfigBean gameConfig = GameDataHandler.Instance.manager.GetGameConfig();
            gameConfig.uiSize += 0.1f;
            if (gameConfig.uiSize >= 2)
            {
                gameConfig.uiSize = 0.5f;
            }
            UIHandler.Instance.ChangeUISize(gameConfig.uiSize);
        }
    }

    private void Start()
    {

    }
}
