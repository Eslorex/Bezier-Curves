using UnityEngine;

public class LeftRightCalculator : MonoBehaviour
{
    public Transform objectA; 

    void Update()
    {
        Vector3 directionToObjectA = (objectA.position - transform.position);
        directionToObjectA.y = 0;
        directionToObjectA.Normalize();

        Vector3 objectBForward = transform.forward;
        objectBForward.y = 0;
        objectBForward.Normalize();

        float angle = Vector3.SignedAngle(objectBForward, directionToObjectA, Vector3.up);

        if (angle < -50f || angle > 50f)
        {
            Debug.Log("No object in visible 100 degree range");
        }
        else
        {
            float normalizedAngle = Mathf.Clamp(angle / 50f, -1f, 1f);

            normalizedAngle = Mathf.Round(normalizedAngle * 8f + 0.5f) / 8f;

            normalizedAngle = Mathf.Clamp(normalizedAngle, -1f, 1f);

            Debug.Log(normalizedAngle);
        }

        Debug.DrawLine(transform.position, objectA.position, Color.green);

        Debug.DrawRay(transform.position, objectBForward * 3, Color.red);

        Debug.DrawRay(transform.position, Quaternion.Euler(0, 50, 0) * objectBForward * 3, Color.blue);
        Debug.DrawRay(transform.position, Quaternion.Euler(0, -50, 0) * objectBForward * 3, Color.blue);
    }
}
