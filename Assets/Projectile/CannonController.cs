using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CannonController : MonoBehaviour
{
    public GameObject MainStick;
    public float rotationSpeed = 1;
    public float BlastPower = 5;

    public GameObject Cannonball;
    public Transform ShotPoint;
    public MegaFiers.MegaBend Bend;
    float i , j , k;

    public Collider mainCollider;
    public GameObject thisGuysRig;
    public Animator anim;
    public LineRenderer l;
    private void Start()
    {
       
        l = GetComponent<LineRenderer>();
        l.enabled = false;
    }
    Collider[] ragCol;
    Rigidbody[] limbsRb;
    Collider[] ragCol1;
    Rigidbody[] limbsRb1;
    void GetRagDollBits()
    {
        ragCol = thisGuysRig.GetComponentsInChildren<Collider>();
        limbsRb = thisGuysRig.GetComponentsInChildren<Rigidbody>();

    }
    void GetRagDollBitss()
    {
        ragCol1 = Cannonball.GetComponentsInChildren<Collider>();
        limbsRb1 = Cannonball.GetComponentsInChildren<Rigidbody>();

    }
    void RagDollOff()
    {
        foreach(Collider col in ragCol)
        {
            col.enabled = false;
        }
        foreach (Rigidbody rigid in limbsRb)
        {
            rigid.isKinematic = true;
        }
        mainCollider.enabled = true;
        mainCollider.transform.GetComponent<Rigidbody>().isKinematic =false;
    }
    void RagDollOn()
    {


        foreach (Collider col in ragCol)
        {
            col.enabled = true;
        }
        foreach (Rigidbody rigid in limbsRb)
        {
            rigid.isKinematic =false;
        }
        Cannonball.GetComponent<Collider>().enabled = false;
        Cannonball.transform.GetComponent<Rigidbody>().isKinematic = false;
    }
    bool Played;
    private void Update()
    {

        if (GM.Instance.StartGame)
        {

            if (!Played)
            {
                i = -Input.GetAxis("Mouse Y");

                if (Input.GetMouseButton(0))
                {
                    j = (-Input.GetAxis("Mouse Y"));

                    l.enabled = true;
                    Bend.angle = Bend.angle + -(j * 2);


                    BlastPower = BlastPower + i;
                    if (Input.GetAxis("Mouse X") < 0)
                    {
                        transform.Rotate(0, (Input.GetAxis("Mouse X")), 0 * Time.deltaTime * rotationSpeed);
                        Bend.transform.Rotate(0, 0, (Input.GetAxis("Mouse X")) * Time.deltaTime * rotationSpeed * 2f);

                    }
                    else if (Input.GetAxis("Mouse X") > 0)
                    {
                        transform.Rotate(0, (Input.GetAxis("Mouse X")), 0 * Time.deltaTime * rotationSpeed);

                        Bend.transform.Rotate(0, 0, (Input.GetAxis("Mouse X")) * Time.deltaTime * rotationSpeed * 2f);
                    }

                }
                if (Input.GetMouseButtonUp(0))
                {

                    l.enabled = false;
                    Bend.angle = 0;
                    GameObject CreatedCannonball = Instantiate(Cannonball, ShotPoint.position, ShotPoint.rotation);
                    CreatedCannonball.GetComponent<Rigidbody>().velocity = ShotPoint.transform.up * BlastPower;

                    SharkAttack.CameraController.Instance.gameObject.transform.DORotate(new Vector3(22, -101.977f, 0), 2f);
                    SharkAttack.CameraController.Instance.target = CreatedCannonball.transform.GetChild(1);
                    Played = true;

                }

            }




            //float HorizontalRotation = Input.GetAxis("Horizontal");
            //float VericalRotation = Input.GetAxis("Vertical");

            //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + 
            //    new Vector3(0, HorizontalRotation * rotationSpeed, VericalRotation * rotationSpeed));

            if (Input.GetKeyDown(KeyCode.Space))
            {


            }
        }
    }

}
