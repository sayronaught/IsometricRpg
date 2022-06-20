using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vRisingCharacterControler : MonoBehaviour
{
    public Collider TerrainForClicking;
    public float WalkSpeed = 5f;
    public float RotationSpeed = 5f;
    public float xMouseSpeed = 20.0f;
    public float yMouseSpeed = 20.0f;
    public Transform TurnToTarget;

    private Vector3 target;
    private Vector3 targetRotation;
    private Quaternion toRotation;
    private Vector3 dir;
    private float velocityX = 0.0f;
    private float velocityY = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (TerrainForClicking.Raycast(ray, out hit, Mathf.Infinity))
        {
            target = hit.point;
            targetRotation = (target - transform.position).normalized;
            targetRotation.y = 0f;
            toRotation.SetLookRotation(targetRotation);
        }
        target = new Vector3(target.x, transform.position.y, target.z);
        //transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * WalkSpeed);
        TurnToTarget.rotation = Quaternion.RotateTowards(TurnToTarget.rotation, toRotation, Time.deltaTime * 800f);
        if (Input.GetMouseButton(0))
        { // left click, some attacks at some point
        }
        if (Input.GetMouseButton(1))
        { // right click, orbit camera
            var around = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z);
                    Camera.main.transform.RotateAround(around,
                                                    transform.up,
                                                    -Input.GetAxis("Mouse X") * xMouseSpeed);

                    Camera.main.transform.RotateAround(around,
                                                    transform.right * -1,
                                                    -Input.GetAxis("Mouse Y") * yMouseSpeed);
            //Camera.main.transform.rotation = Quaternion.
            Camera.main.transform.LookAt(transform.position, Vector3.up);
        }
        if (Input.GetAxis("Vertical") != 0f)
        { // forward/back
            dir = new Vector3( Camera.main.transform.forward.x,0f, Camera.main.transform.forward.z);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + (dir* Input.GetAxis("Vertical")), Time.deltaTime * WalkSpeed);
        }
        if (Input.GetAxis("Horizontal") != 0f)
        { // left/right
            dir = new Vector3(Camera.main.transform.right.x, 0f, Camera.main.transform.right.z);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + (dir * Input.GetAxis("Horizontal")), Time.deltaTime * WalkSpeed);
        }
    }
}
