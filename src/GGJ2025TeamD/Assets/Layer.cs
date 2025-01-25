using UnityEngine;
using UnityEngine.InputSystem;

public class Layer : MonoBehaviour
{
    public Transform layer;
    public float RotateSpeed;
    private bool OnClick;
    private bool cupReady = false;

    [SerializeField] private InputActionReference pourAction;

    private void Awake()
    {
        GlobalEvent.OnRoundStart += OnRoundReady;
        pourAction.action.performed += OnPourAction;
        pourAction.action.Enable();
    }

    private float clickY = 0f;

    private void OnPourAction(InputAction.CallbackContext context)
    {
        OnClick = true;
        clickY = Mouse.current.position.ReadValue().y;
    }

    private void OnRoundReady()
    {
        cupReady = true;
        OnClick = false;
        layer.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void OnDestroy()
    {
        GlobalEvent.OnRoundStart -= OnRoundReady;
        pourAction.action.performed -= OnPourAction;
    }

    private void Update()
    {
        if (!cupReady || TimeManager.IsTimeStopped)
            return;
        
        if (OnClick)
        {
            OnDrag();

            if(pourAction.action.ReadValue<float>() <= 0)
            {
                OnEndDrag();
                OnClick = false;
            }
        }
    }

    private void OnEndDrag()
    {
        layer.rotation = Quaternion.Euler(0, 0, 0);
        cupReady = false;
        
        Invoke("DealayRoundEnd", 1f);
    }

    private void DealayRoundEnd()
    {
        GlobalEvent.RaiseMouseUp();
    }

    [SerializeField]
    private float dragDistanceRate = 0.1f;

    public void OnDrag()
    {
        float Y = Mouse.current.position.ReadValue().y;
        float diff = Mathf.Max(clickY - Y, 0f);
        float rate = diff / (Screen.height * dragDistanceRate);
        rate = Mathf.Clamp(rate, 0f, 1f);
        float targetAngle = 90 * rate;
        float currentRotationZ = layer.eulerAngles.z;
        float newRotationZ = Mathf.MoveTowardsAngle(currentRotationZ, targetAngle, RotateSpeed * Time.deltaTime);
        layer.rotation = Quaternion.Euler(0, 0, newRotationZ);
    }
}
