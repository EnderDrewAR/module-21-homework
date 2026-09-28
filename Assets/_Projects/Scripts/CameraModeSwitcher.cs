using System.Collections.Generic;
using Cinemachine;

public class CameraModeSwitcher
{
    private readonly Queue<CinemachineVirtualCamera> _camerasQueue;

    public CameraModeSwitcher(IEnumerable<CinemachineVirtualCamera> cameras)
    {
        _camerasQueue = new Queue<CinemachineVirtualCamera>(cameras);
    }

    public void SwitchNextMode()
    {
        if (_camerasQueue.Count == 0)
            return;

        foreach (CinemachineVirtualCamera camera in _camerasQueue)
            camera.gameObject.SetActive(false);

        CinemachineVirtualCamera nextMode = _camerasQueue.Dequeue();
        nextMode.gameObject.SetActive(true);
        _camerasQueue.Enqueue(nextMode);
    }
}