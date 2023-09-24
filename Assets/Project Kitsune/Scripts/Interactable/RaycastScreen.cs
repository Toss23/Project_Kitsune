using UnityEngine;

public class RaycastScreen : Clickable2D
{
    [Header("Raycast")]
    [SerializeField] private Camera _raycastCamera;
    [SerializeField] private LayerMask _layer;

    protected override void OnSingleClick()
    {
        Interactable clickable = Cast();
        if (clickable != null)
            clickable.OnClick();
    }

    private Interactable Cast()
    {
        Vector2 position = _raycastCamera.ScreenToWorldPoint(TouchPositionUnfixed);
        RaycastHit2D hit = Physics2D.Raycast(position, -Vector2.up, 100f, _layer);

        Interactable interactable = hit.transform.GetComponent<Interactable>();

        if (interactable != null)
        {
            return interactable;
        }

        return null;
    }

    protected override void OnTouchDown() { }

    protected override void OnTouchUp() { }

    protected override void OnUpdate(float deltaTime) { }
}