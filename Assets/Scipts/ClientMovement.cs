using UnityEngine;

public class ClientMovement : MonoBehaviour
{
    public Transform targetPoint;
    public float moveSpeed = 2f;
    public string issue = "";

    // set a position to move.
    public void SetTarget(Transform targetTransform)
    {
        targetPoint = targetTransform;
    }

    // define what issue the client have.
    public void setIssue(int c) {
        switch(c) {
            case 0:
                issue = "Hardware";
                break;

            case 1:
                issue = "Software";
                break;
        }
    }

    // the client moves to the targetPoint
    void Update()
    {
        if (targetPoint == null) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPoint.position,
            moveSpeed * Time.deltaTime
        );

        Vector3 direction = (targetPoint.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 5f);
        }
    }
}
