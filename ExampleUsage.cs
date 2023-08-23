using System.Collections;
using UnityEngine;

public class ExampleUsage : MonoBehaviour
{
    public Transform target;
    public Transform startPositionTransform;
    public float speed = 0.05f;
    public float rotationSpeed = 200f;
    public float curveFactor = 2f;
    public float heightFactor = 2f;
    public float RandomFactor = 0.5f;
    public float RandomFactor2 = 0.5f;
    public float outwardCurvePredisposition = 1f; // Curve predisposition when moving towards the target.
    public float returnCurvePredisposition = -1f; // Curve predisposition when returning.

    private bool isInMotion = false;
    private Vector3 startPosition;
    private Vector3 outwardControlPoint;
    private Vector3 returnControlPoint;
    private Vector3 targetPosition;

    private void Update()
    {
        // Rotate the boomerang
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && !isInMotion)
        {
            isInMotion = true;
            startPosition = startPositionTransform.position;
            targetPosition = target.position;

            // Randomize curve factor a bit
            float randomizedCurveFactor = curveFactor + Random.Range(-RandomFactor2, RandomFactor);

            // Calculate the curve control points
            outwardControlPoint = startPosition + (targetPosition - startPosition) / 2 + transform.right * outwardCurvePredisposition * randomizedCurveFactor + Vector3.up * heightFactor;

            // Start moving along the bezier curve to the target
            StartCoroutine(MoveToTarget());
        }
    }

    private IEnumerator MoveToTarget()
    {
        for (float t = 0; t <= 1; t += Time.deltaTime * speed)
        {
            targetPosition = target.position;
            transform.position = CalculateBezierPoint(t, startPositionTransform.position, outwardControlPoint, targetPosition);
            yield return null;
        }

        // Calculate the return control point with predisposition
        returnControlPoint = targetPosition + (startPosition - targetPosition) / 2 + transform.right * returnCurvePredisposition * curveFactor + Vector3.up * heightFactor;

        StartCoroutine(MoveBackToStart());
    }

    private IEnumerator MoveBackToStart()
    {
        for (float t = 0; t <= 1; t += Time.deltaTime * speed)
        {
            transform.position = CalculateBezierPoint(t, targetPosition, returnControlPoint, startPositionTransform.position);
            yield return null;
        }

        isInMotion = false;
    }

    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0; // (1 - t) ^ 2 * p0
        p += 2 * u * t * p1; // 2(1 - t) * t * p1
        p += tt * p2; // t ^ 2 * p2

        return p;
    }
}
