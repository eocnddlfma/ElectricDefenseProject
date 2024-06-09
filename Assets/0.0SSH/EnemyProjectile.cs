using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public Transform target;  // 목표 위치
    public float duration = 2.0f;  // 이동 시간
    public Vector3 controlPointOffset = new Vector3(0, 5, 0);  // 제어점의 오프셋

    [SerializeField]private Vector3 startPoint;
    [SerializeField]private Vector3 endPoint;
    [SerializeField]private Vector3 controlPoint;

    public void Setting(Vector3 start, Vector3 end)
    {
        startPoint = start;
        endPoint = end;
        controlPoint = (startPoint + endPoint) / 2 + controlPointOffset;
        
        print("debugarrow");
        StartCoroutine(MoveAlongBezierCurve());
    }
    
    IEnumerator MoveAlongBezierCurve()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            transform.position = CalculateQuadraticBezierPoint(t, startPoint, controlPoint, endPoint);
            yield return null;
        }

        transform.position = endPoint;
        Destroy(gameObject);
    }

    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        // 2차 베지어 곡선 공식
        return Mathf.Pow(1 - t, 2) * p0 +
               2 * (1 - t) * t * p1 +
               Mathf.Pow(t, 2) * p2;
    }
}
