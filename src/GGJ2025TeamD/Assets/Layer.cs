using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Layer : MonoBehaviour
{
    private float MouseOriginalY;
    public Transform layer;
    public float RotateSpeed;
    private bool OnClick;
    public float offset;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            transform.position = new Vector3(transform.position.x,
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y + offset,
                transform.position.z);
            if (hits().collider != null)
            {
                OnClick = true;
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (hits().collider !=null)
            {
                OnDrag(false);
            }

            if (hits().collider == null && OnClick)
            {
                OnDrag(true);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnEndDrag();
            OnClick = false;
        }
    }

    public void OnEndDrag()
    {
        layer.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void OnDrag(bool isOutOfBox)
    {
        float Y = Input.GetAxis("Mouse Y");
        float rotateAngle = 0;
        if (Y < 0f)
        {
            rotateAngle = RotateSpeed * Time.deltaTime;
        }
        else if(Y > 0f)
        {
            rotateAngle = -1 * RotateSpeed * Time.deltaTime;
        }
        float currentRotationZ = layer.eulerAngles.z + rotateAngle;
        currentRotationZ = Mathf.Clamp(currentRotationZ, 0, 90);
        if (isOutOfBox)
        {
            layer.rotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            layer.rotation = Quaternion.Euler(0, 0, currentRotationZ);
        }
    }

    private RaycastHit2D hits()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return Physics2D.Raycast(mousePos, Vector2.zero);
    }
}
