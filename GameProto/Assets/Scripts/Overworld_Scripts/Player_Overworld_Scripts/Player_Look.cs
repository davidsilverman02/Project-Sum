using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Look : MonoBehaviour
{

    public Transform cam_pivot;
    public GameObject cam_obj;

    public bool invert_vert_look;

    public float rot_x_min;
    public float rot_x_max;

    public float cam_dist_max;
    public float cam_dist_min;
    public float cam_fix_speed;
    public float cam_sph_rad;

    public float cam_sensitivity = 0.4f;

    private float y_rot;
    private float x_rot;
    private float desired_cam_loc;

    private float camDist;


    // Start is called before the first frame update
    void Start()
    {
        y_rot = transform.rotation.eulerAngles.y;
        x_rot = cam_pivot.rotation.eulerAngles.x;
        desired_cam_loc = cam_dist_max;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCharacter();
        RotatePivot();
        SetCameraDist();
    }

    void RotateCharacter()
    {
        //take in the player's input
        float rot = Input.GetAxisRaw("Mouse X");
        //apply camera sensitivity
        rot *= cam_sensitivity;
        //add the new rotation delta to the current rotation
        y_rot += rot;
        //apply rotation
        transform.rotation = Quaternion.Euler(0, y_rot, 0);
    }

    void RotatePivot()
    {
        //take in the player's input
        float rot = Input.GetAxisRaw("Mouse Y");
        //invert look if desired
        rot *= invert_vert_look ? 1 : -1;
        //apply camera sensitivity
        rot *= cam_sensitivity;
        //add the new rotation delta to the current rotation
        x_rot += rot;

        //clamp the cam_pivot's rotation, so we can't go into the ground, or loop around vertically 
        x_rot = Mathf.Clamp(x_rot, rot_x_min, rot_x_max);

        cam_pivot.localRotation = Quaternion.Euler(x_rot, 0, 0);
    }

    void SetCameraDist()
    {
        camDist = -cam_obj.transform.localPosition.z;
        
        FindDistance();

        //ResolveCameraOverlaps();

        float t = cam_fix_speed * Time.deltaTime;

        float newDist = Mathf.Lerp(camDist, desired_cam_loc, t);

        newDist = Mathf.Clamp(newDist, cam_dist_min, cam_dist_max);

        cam_obj.transform.localPosition = new Vector3(0, 0, -newDist);
        
    }

    //make sure there is LOS from camera to player, and zoom in if there is something blocking the camera
    void FindDistance()
    {

        //from camera to player, used if there is a conflict, to determine how far forward the camera must go
        Vector3 vecToCam = cam_obj.transform.position - transform.position;
        Ray toCam = new Ray(transform.position, vecToCam);
        RaycastHit hit1;
        //does the camera see the player? if true there is something in the way, and so we must move closer to see the player
        Physics.Raycast(toCam, out hit1, cam_dist_max);
        if (hit1.collider != null)
        {

            desired_cam_loc = Vector3.Distance(transform.position, hit1.point) - cam_sph_rad;

        }
        else
        {
            //Check behind the cam, to see how far the camera can back up if it can see the player
            Ray camBackup = new Ray(cam_obj.transform.position - cam_obj.transform.forward * cam_sph_rad, -cam_obj.transform.forward);
            RaycastHit hit2;
            //we hit the player, now see how far we can back up
            if (Physics.Raycast(camBackup, out hit2, cam_dist_max - camDist))
            {
                desired_cam_loc = camDist + hit2.distance;
            }
            else
            {
                //nothing was behind the camera, zoom out to max
                desired_cam_loc = cam_dist_max;
            }
        }
    }
}
