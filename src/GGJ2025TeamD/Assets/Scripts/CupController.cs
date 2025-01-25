using UnityEngine;

public class CupController : MonoBehaviour
{
    [SerializeField]
    private Transform zeroTransform;
    [SerializeField]
    private Transform overHeightTransform;
    [SerializeField]
    private Transform minTransform;
    [SerializeField]
    private Transform maxTransform;

    public void SetMaxAndMin(float max, float min)
    {
        float distance = overHeightTransform.position.y - zeroTransform.position.y;
        minTransform.localPosition = new Vector3(minTransform.localPosition.x, zeroTransform.localPosition.y + distance * min, minTransform.localPosition.z);
        maxTransform.localPosition = new Vector3(maxTransform.localPosition.x, zeroTransform.localPosition.y + distance * max, maxTransform.localPosition.z);
    }
}
