using UnityEngine;
using UnityEngine.InputSystem;

public class Lever : MonoBehaviour {
    public InputActionReference InteractActionReference;
    public InputActionReference DeltaActionReference;
    public AnimationCurve DeltaYToRotationZCurve;
    public AnimationCurve DeltaYToFlowRateCurve;

    public Vector2 Delta;

    void OnEnable() {
        InteractActionReference.action.Enable();
        DeltaActionReference.action.Enable();
    }

    void OnDisable() {
        InteractActionReference.action.Disable();
        DeltaActionReference.action.Disable();
    }

    void Update() {
        if (InteractActionReference.action.WasPressedThisFrame()) Delta = Vector2.zero;
        if (InteractActionReference.action.WasReleasedThisFrame()) transform.eulerAngles = Vector3.zero;
        if (InteractActionReference.action.IsPressed()) Interact();
    }

    void Interact() {
        Delta += DeltaActionReference.action.ReadValue<Vector2>();
        transform.eulerAngles = new Vector3(0, 0, DeltaYToRotationZCurve.Evaluate(Delta.y));
        var flowRate = DeltaYToFlowRateCurve.Evaluate(Delta.y);
    }
}
