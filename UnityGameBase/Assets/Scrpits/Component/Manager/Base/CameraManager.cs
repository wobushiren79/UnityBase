using UnityEngine;

public class CameraManager : BaseManager
{
    //主摄像头
    protected Camera _mainCamera;
    //ui摄像头
    protected Camera _uiCamera;

    public Camera mainCamera
    {
        get
        {
            if (_mainCamera == null)
            {
                _mainCamera = Camera.main;
            }
            return _mainCamera;
        }
    }

    public Camera uiCamera
    {
        get
        {
            if (_uiCamera == null)
            {
                _uiCamera = FindWithTag<Camera>(TagInfo.Tag_UICamera);
            }
            return _uiCamera;
        }
    }
}
