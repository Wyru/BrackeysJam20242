using UnityEngine;

public class StampMinigame : MonoBehaviour, IInteractable
{
    public Transform _gameLocation;
    public void Interact()
    {
        // Debug.Log(_gameLocation.position);
        PlayerController.instance.transform.position = _gameLocation.position;
        PlayerController.instance.transform.rotation = _gameLocation.rotation;
        Debug.Log(PlayerController.instance.gameObject.transform.GetChild(0).rotation);
        // = Quaternion.Euler(30, 0, 0);
        PlayerController.instance.DisableController();
    }
}
