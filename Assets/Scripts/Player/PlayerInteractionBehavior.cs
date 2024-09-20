using UnityEngine;

public class PlayerInteractionBehavior : MonoBehaviour
{
    [Header("Settings")]
    public LayerMask layer;
    public float range;

    [Header("References")]
    public PlayerBehavior player;
    public PlayerCameraBehavior playerCamera;

    [Header("Debug")]
    public GameObject itemHovered;

    Ray ray;
    RaycastHit hitInfo;

    void Update()
    {
        ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (!Physics.Raycast(ray, out hitInfo, range, layer))
        {
            PassHoveredInteractable();
            return;
        }

        HoverInteractable();

        if (player.interactInput.action.WasPressedThisFrame())
        {
            OnPressInteract();
        }
    }

    void HoverInteractable()
    {
        if (itemHovered == hitInfo.collider.gameObject)
            return;

        itemHovered = hitInfo.collider.gameObject;
    }

    void PassHoveredInteractable()
    {
        if (itemHovered == null)
            return;

        itemHovered = null;
        // On unhover item
    }

    void OnPressInteract()
    {
        if (itemHovered == null)
            return;

        if (itemHovered.TryGetComponent<IInteractable>(out var interactable))
        {
            interactable.Interact();
            return;
        }

    }
}
