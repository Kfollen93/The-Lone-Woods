using UnityEngine;

public class MovingTargets : MonoBehaviour
{
    [SerializeField] private Transform leftTarget;
    [SerializeField] private Transform rightTarget;
    private Vector3 pointA = new Vector3(-60, 8, 6);
    private Vector3 pointB = new Vector3(-55.5f, 8, 6);
    private Vector3 pointC = new Vector3(-50, 8, 6);
    private Vector3 pointD = new Vector3(-54f, 8, 6);

    private void Update()
    {
        leftTarget.transform.localPosition = Vector3.Lerp(pointA, pointB, Mathf.PingPong(Time.time, 1));
        rightTarget.transform.localPosition = Vector3.Lerp(pointC, pointD, Mathf.PingPong(Time.time, 1));
    }
}
