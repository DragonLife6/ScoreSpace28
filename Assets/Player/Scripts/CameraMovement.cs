using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target; // ��������� �� ������, �� ���� �������
    public float smoothSpeed = 0.125f; // ���������� ������������
    public Vector3 offset; // ³����� �� ������
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target != null)
        {
            // ���������� �������, �� ��� �� ������ ������� ������
            Vector3 desiredPosition = target.position + offset;
            desiredPosition.z = -10;

            // ����������� ������������, ��� ������� ��� ������ �������
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

            // ������ ������� ������
            transform.position = smoothedPosition;
        }
    }
}
