using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

#nullable disable

public class GamepadUISelectionHelper : MonoBehaviour {
    public GameObject defaultSelectedGameObject;

    void Update() {
        if (Gamepad.current == null || !Gamepad.current.wasUpdatedThisFrame) return;
        if (EventSystem.current.currentSelectedGameObject != null
            && EventSystem.current.currentSelectedGameObject.activeSelf
            && EventSystem.current.currentSelectedGameObject.activeInHierarchy) return;
        EventSystem.current.SetSelectedGameObject(defaultSelectedGameObject);
    }
}
