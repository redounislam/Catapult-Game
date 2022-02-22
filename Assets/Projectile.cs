using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Projectile : MonoBehaviour
{
    public ParticleSystem HitRed ,Splash;
    public GameObject CamRotatorParent , DestroyedParts;
    public float power = 1000.0F , JumpPower;
    public int ImpactRadius = 200;
    public GameObject thisGuysRig;
    Collider[] ragCol;
    Rigidbody[] limbsRb;
    public float IncreaseAmmount;
    public GameObject Crack;
    Rigidbody rb;
    bool played;

    public bool Cannon;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void ExPlodeStart()
    {
        Collider[] objects = UnityEngine.Physics.OverlapSphere(transform.position, 20);
        foreach (Collider h in objects)
        {
            Rigidbody r = h.GetComponent<Rigidbody>();
            
            if (r != null && r.transform.root.transform.GetComponentInChildren<Enemy>() )
            {
                r.transform.root.transform.GetComponentInChildren<Enemy>().RagDollOnAdForce();
              //  r.transform.parent.transform.GetComponentInChildren<Enemy>().RagDollOn();
             //   r.AddExplosionForce(power,transform.position, ImpactRadius);
               // r.AddForce(new Vector3(Random.Range(-18f, 18f),0, Random.Range(-18f, 18f)).normalized * power);
            }
        }
    }


    private void Update()
    {
       
       
        

            if (Input.GetMouseButton(0) && Cannon)
        {
            if (!played)
            {
                Splash.transform.eulerAngles = new Vector3(-90, -270, 0);
                Splash.Play();
                GameObject g = Instantiate(DestroyedParts, transform.position, Quaternion.identity);
                
                Splash.transform.parent = null;
                GetComponent<Collider>().enabled = false;
                HitRed.Play();
                HitRed.transform.parent = null;
                Destroy(HitRed.transform.gameObject, .5f);
                ExPlodeStart();
                transform.GetChild(1).gameObject.SetActive(false);
                played = true;
                SharkAttack.CameraController.Instance.target = null;

            }
        }


           if( rb.velocity.magnitude <= .1f)
        {
            if (GM.Instance.KillCount < GM.Instance.TargetThisLevel)
            {
                UiManager.Instance.EndSceneUI.SetActive(true);
                UiManager.Instance.ScoreCard.SetActive(false);
            }
            

            StartCoroutine(SciptDisable());
        }

        
    }

    public IEnumerator SciptDisable()
    {
        yield return new WaitForSeconds(.2f);
        GetComponent<Projectile>().enabled = false;
    }


    void GetRagDollBits()
    {
        ragCol = thisGuysRig.GetComponentsInChildren<Collider>();
        limbsRb = thisGuysRig.GetComponentsInChildren<Rigidbody>();

    }
    void RagDollOn()
    {

        thisGuysRig.GetComponent<Rigidbody>().mass = 10f; 
        thisGuysRig.GetComponent<Collider>().enabled = false;
        
        foreach (Collider col in ragCol)
        {
            col.enabled = true;
        }
        foreach (Rigidbody rigid in limbsRb)
        {
            rigid.mass = 1f;
            rigid.angularDrag = 0; rigid.drag = 0;
            rigid.isKinematic = false;
            rigid.interpolation = RigidbodyInterpolation.Interpolate;
            
           rigid.velocity =FindObjectOfType<CannonController>(). ShotPoint.transform.up * FindObjectOfType<CannonController>().BlastPower;
        }
    //   GetComponent<Collider>().enabled = false;
        transform.GetComponent<Rigidbody>().isKinematic = false;
       
    }


    IEnumerator Ragdoll()
    {
        yield return new WaitForSeconds(0f);
        GetRagDollBits();
        RagDollOn();
    }
    private void OnEnable()
    {
        StartCoroutine(Ragdoll());
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Explode"))
        {
            
            
            other.transform.GetChild(0).gameObject.SetActive(true); other.transform.GetChild(0).gameObject.transform.parent = null;
            other.transform.gameObject.SetActive(false);
        }
            if (other.gameObject.CompareTag("Ground"))
        {
            Camera.main.GetComponent<SharkAttack.CameraController>().enabled = false;
            GetComponent<Rigidbody>().AddForce(Vector3.up * JumpPower * Time.deltaTime * 100, ForceMode.Impulse);
            Crack.SetActive(true);
            Crack.transform.DOMoveY(4.12f, 0);
            Crack.transform.eulerAngles = new Vector3(90, 0, 0);
            Crack.transform.parent = null;
            if(HitRed != null)
            HitRed.Play();

            
            Camera.main.transform.DOLocalMove(new Vector3(GM.Instance.offset.x , GM.Instance.offset.y , 22.8f),1);

            Camera.main.transform.DOLocalRotate(new Vector3(37.9f, -232.3f, 0), .5f).SetEase(Ease.InSine).OnComplete(() =>
            {
                
            });
           
            transform.GetComponentInChildren<MySDK.SimpleRotator>().enabled = false; 
            other.tag = "Untagged";
            Camera.main.transform.parent = GM.Instance.ParentCam.transform;
        }
        if (other.gameObject.CompareTag("Collectable"))
        {
            ImpactRadius += 50;
            this.transform.DOScale(new Vector3(this.transform.localScale.x + IncreaseAmmount, this.transform.localScale.y + IncreaseAmmount,
             this.transform.localScale.z + IncreaseAmmount), .3f).SetEase(Ease.InOutElastic);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().RagDollOn();
            other.GetComponent<Enemy>().Death = true;
        }
    }

}
