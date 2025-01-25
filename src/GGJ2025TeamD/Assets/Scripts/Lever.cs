using UnityEngine;
using UnityEngine.InputSystem;

public class Lever : MonoBehaviour {
    public InputActionReference InteractActionReference;
    public InputActionReference DeltaActionReference;
    public AnimationCurve DeltaYToRotationZCurve;
    public Rigidbody2D BeerPrefab;
    public float BeerSpawnRate;
    public Vector3 SpawnOffset;
    public Vector3 MinRandomSpawnOffset;
    public Vector3 MaxRandomSpawnOffset;
    public Vector2 SpawnInitialVelocity;

    public Vector2 Delta;
    public float LastSpawnElapsed;

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
        if (!InteractActionReference.action.IsPressed()) return;
        Delta += DeltaActionReference.action.ReadValue<Vector2>();
        transform.eulerAngles = new Vector3(0, 0, DeltaYToRotationZCurve.Evaluate(Delta.y));
        if (BeerSpawnRate < float.Epsilon) return;
        LastSpawnElapsed += Time.deltaTime;
        if (LastSpawnElapsed > BeerSpawnRate) Spawn();
    }

    void Spawn() {
        var offset = SpawnOffset + new Vector3(Random.Range(MinRandomSpawnOffset.x, MaxRandomSpawnOffset.x),
            Random.Range(MinRandomSpawnOffset.y, MaxRandomSpawnOffset.y),
            Random.Range(MinRandomSpawnOffset.z, MaxRandomSpawnOffset.z));
        var beer = Instantiate(BeerPrefab, transform.position + offset, Quaternion.identity);
        beer.linearVelocity = SpawnInitialVelocity;
        LastSpawnElapsed -= BeerSpawnRate;
    }
}
