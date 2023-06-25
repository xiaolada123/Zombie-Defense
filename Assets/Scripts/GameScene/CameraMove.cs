using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制摄像机移动脚本
/// </summary>
public class CameraMove : MonoBehaviour
{
    public Transform target;

    public Vector3 offsetPos;

    public float bodyHeight;

    public float moveSpeed;

    public float rotationSpeed;

    private Vector3 targetPos;

    private Quaternion targetRotation;
    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }
         targetPos= target.position + target.forward * offsetPos.z;
         targetPos += Vector3.up * offsetPos.y;
         targetPos += target.right * offsetPos.x;
        
        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, moveSpeed * Time.deltaTime);
        targetRotation =
            Quaternion.LookRotation(target.position + Vector3.up * bodyHeight - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        
    }

    public void SetTarget(Transform player)
    {
        target = player;
    }
}
