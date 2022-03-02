using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using UnityEngine.SceneManagement;
public class CannonController : MonoBehaviour
{
   
    public GameObject Dummy;
    public GameObject MainStick;
    public float rotationSpeed = 1;
    public float BlastPower = 5;
    public float UpMultiplier = 2 , Delay;
    public GameObject Cannonball;
    public Transform ShotPoint;
    public MegaFiers.MegaBend Bend;
    float i , j , k;

    public Collider mainCollider;
    public GameObject thisGuysRig;
    public Animator anim;
    public LineRenderer l;


    Scene m_Scene;
    string sceneName;
    private void Start()
    {
        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;

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
   public bool Played , launched;
    float m;


    public IEnumerator LaunchDelay()
    {
        yield return new WaitForSeconds(Delay);
        Invoke("DummyDisable", 0);
        GameObject CreatedCannonball = Instantiate(Cannonball, ShotPoint.position, ShotPoint.rotation);
        CreatedCannonball.GetComponent<Rigidbody>().velocity = ShotPoint.transform.up * BlastPower;
        SharkAttack.CameraController.Instance.target = CreatedCannonball.transform.GetChild(1);
        if (sceneName != "5 1")
        {
            
            SharkAttack.CameraController.Instance.gameObject.transform.DORotate(new Vector3(22, -101.977f, 0), 2f);
          
        }

    }
    bool SoundPlayed;
    private void Update()
    {

        if (GM.Instance.StartGame)
        {
       
            if (launched )
            {
                if (Bend.angle <= 0)
                {
                    m += Time.deltaTime * UpMultiplier;
                
                    Bend.angle = Bend.angle + m;
                }
            
                
            }

            if (!Played)
            {
                i = -Input.GetAxis("Mouse Y");

                if (Input.GetMouseButton(0))
                {
                    UiManager.Instance.Tut.SetActive(false);
                    if (!SoundPlayed)
                    {
                        SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().StickStrechSFX);
                        SoundPlayed = true;
                    }
                  

                    GM.Instance.EnemyMove = true;
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
                    MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                    SoundManager.SharedManager().StopSFX();
                    l.enabled = false;
                    SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().PlayerShootSFX);

                    launched = true ;
                    StartCoroutine(LaunchDelay());
                    GetComponent<DrawProjection>().Cube.SetActive(false);
                    
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
    public void DummyDisable()
    {
        Dummy.gameObject.SetActive(false);
    }

}
