using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    public Collider TerrainForClicking;
    public float WalkSpeed = 5f;
    public float RotationSpeed = 5f;
    public Transform TurnToTarget;

    private Vector3 target;
    private Vector3 targetRotation;
    private Quaternion toRotation;

    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        target = new Vector3(target.x, transform.position.y, target.z);
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * WalkSpeed);
        //TurnToTarget.forward = Vector3.Slerp(transform.forward, targetRotation,Time.deltaTime*50f);
        TurnToTarget.rotation = Quaternion.RotateTowards(TurnToTarget.rotation, toRotation, Time.deltaTime * 180f);
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (TerrainForClicking.Raycast(ray, out hit, Mathf.Infinity))
            {
                target = hit.point;
                targetRotation = (target - transform.position).normalized;
                targetRotation.y = 0f;
                //TurnToTarget.forward = targetRotation;
                toRotation.SetLookRotation(targetRotation);
            }
        }
    }
}
