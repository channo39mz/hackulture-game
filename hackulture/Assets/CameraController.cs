// CameraController script
using UnityEngine;
using Cinemachine;
using Photon.Pun;

public class CameraController : MonoBehaviourPun
{
    private CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        if (photonView.IsMine)
        {
            InitializeCamera();

            // Ensure that the CameraController object is not destroyed on scene changes
            DontDestroyOnLoad(gameObject);
        }
    }

    private void InitializeCamera()
    {
        // Implement your Cinemachine camera initialization logic here
        // For example, you might want to follow a target GameObject or set up other properties.
    }

    // RPC method to synchronize camera properties
    [PunRPC]
    public void SetCameraTarget(int targetPhotonViewID)
    {
        PhotonView targetPhotonView = PhotonView.Find(targetPhotonViewID);

        if (targetPhotonView != null)
        {
            GameObject playerGameObject = targetPhotonView.gameObject;

            if (playerGameObject != null && virtualCamera != null)
            {
                // Set the Cinemachine camera's follow target
                virtualCamera.Follow = playerGameObject.transform;
            }
            else
            {
                Debug.LogError("Invalid playerGameObject or Cinemachine camera.");
            }
        }
        else
        {
            Debug.LogError("PhotonView with ID " + targetPhotonViewID + " not found.");
        }
    }
}
