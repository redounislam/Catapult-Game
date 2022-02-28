using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.NiceVibrations;

public enum ProjectileType
{
    Main , 
    Copy
}
public class Projectile : MonoBehaviour
{
    public float CloneForce;
    public GameObject GiantHitFX;
    [SerializeField] private GameObject Clone;
    [SerializeField] private int numToInstantiate;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;
    public ProjectileType m_ProjectileType;
    public ParticleSystem HitRed ,Splash;
    public GameObject DestroyedParts;
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


        StartCoroutine(Ragdoll());

    }

    private void OnEnable()
    {
        if (m_ProjectileType == ProjectileType.Copy)
        {
            transform.GetComponent<Rigidbody>().AddForce(Vector3.forward * Random.Range(CloneForce - 80, CloneForce + 500) * Time.deltaTime);
            transform.GetComponent<Rigidbody>().mass = Random.Range(1, 15);
        }
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
            if(r != null && r.transform.gameObject.CompareTag("Explode"))
            {
                r.transform.GetChild(0).gameObject.SetActive(true);
                r.transform.GetChild(0).gameObject.SetActive(true); r.transform.GetChild(0).gameObject.transform.parent = null;
                r.transform.gameObject.SetActive(false);
            }
        }
    }


    private void Update()
    {


       

        if (m_ProjectileType == ProjectileType.Main)
        {
            if (Input.GetMouseButton(0) && Cannon)
            {
                if (GM.Instance.TapTut)
                {
                    UiManager.Instance.HandTap.SetActive(false);
                    Time.timeScale = 1f;

                }
                if (!played)
                {
                    SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().TapToBurstSFX);
                    Splash.transform.eulerAngles = new Vector3(-90, -270, 0);
                    Splash.Play();
                    DestroyedParts.gameObject.SetActive(true);
                    DestroyedParts.gameObject.transform.parent = null;
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


            if (rb.velocity.magnitude <= .2f)
            {
                if (GM.Instance.KillCount < GM.Instance.TargetThisLevel)
                {
                    StartCoroutine(DelayEnd());
                }


                StartCoroutine(SciptDisable());
            }

        }

        else
        {
            if (Input.GetMouseButton(0) && Cannon)
            {
                if (!played)
                {
                    Splash.transform.eulerAngles = new Vector3(-90, -270, 0);
                    Splash.Play();
                    DestroyedParts.gameObject.SetActive(true);
                    DestroyedParts.gameObject.transform.parent = null;
                    Splash.transform.parent = null;
                    GetComponent<Collider>().enabled = false;
                    HitRed.Play();
                    HitRed.transform.parent = null;
                    Destroy(HitRed.transform.gameObject, .5f);
                    ExPlodeStart();
                    transform.GetChild(1).gameObject.SetActive(false);
                    played = true;
                 

                }
            }

            if (rb.velocity.magnitude <= .2f)
            {

                
            }
        }
    }
    public IEnumerator DelayEnd()
    {
        yield return new WaitForSeconds(4f);
        GM.Instance.LevelFail();
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

    void RagDollJump()
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
            rigid.AddForce(Vector3.up * JumpPower * Time.deltaTime *100, ForceMode.Impulse);
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

    public IEnumerator SlowMoRoutine()
    {
        yield return new WaitForSeconds(.01f);
        Time.timeScale = 0.3f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        yield return new WaitForSeconds(.21f);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Explode"))
        {
            SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().BarrelExplosionSfx);
            other.transform.GetChild(0).gameObject.SetActive(true); other.transform.GetChild(0).gameObject.transform.parent = null;
                other.transform.gameObject.SetActive(false);
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        }
        if (other.gameObject.CompareTag("Ground1"))
        {
            if (HitRed != null)
                HitRed.Play();
            if (m_ProjectileType == ProjectileType.Main)
            {
                SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().PlayerFallHit2SFX);
                MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                if (GM.Instance.TapTut)
                {
                    Time.timeScale = 0.05f;
                    UiManager.Instance.HandTap.SetActive(true);
                }
            }
           
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            if (m_ProjectileType == ProjectileType.Main)
            {
                SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().PlayerFallHit1fx);

                MMVibrationManager.Haptic(HapticTypes.MediumImpact);
            }
            Camera.main.GetComponent<SharkAttack.CameraController>().enabled = false;
            RagDollJump();
            Crack.SetActive(true);
            Crack.transform.DOMoveY(4.12f, 0);
            Crack.transform.eulerAngles = new Vector3(90, 0, 0);
            Crack.transform.parent = null;
            if(HitRed != null)
            HitRed.Play();


            Camera.main.transform.DOLookAt(GM.Instance.ParentCam.transform.position, 1).OnComplete(() =>
            {
                GM.Instance.ParentCam.GetComponent<CamRotator>().enabled = true;
            });
            //Camera.main.transform.DOLocalMove(new Vector3(GM.Instance.offset.x, GM.Instance.offset.y, 22.8f), 2f).SetEase(Ease.InSine);
            //Camera.main.transform.DOLocalRotate(new Vector3(37.9f, -232.3f, 0), 2f).SetEase(Ease.InSine).OnComplete(() =>
            //{

            //});
            GM.Instance.ParentCam.GetComponent<CamRotator>();
            Camera.main.transform.parent = GM.Instance.ParentCam.transform;
            transform.GetComponentInChildren<MySDK.SimpleRotator>().enabled = false; 
            other.tag = "Untagged";
          
        }
        if (other.gameObject.CompareTag("Collectable"))
        {
            SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().PizzaSfx);
            ImpactRadius += 250;
            this.transform.GetChild(0).DOScale(new Vector3(this.transform.localScale.x + IncreaseAmmount, this.transform.localScale.y + IncreaseAmmount,
             this.transform.localScale.z + IncreaseAmmount), .3f).SetEase(Ease.InOutSine);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().RagDollOn();
            other.GetComponent<Enemy>().Death = true;
            if (other.GetComponent<Enemy>().m_EnemyType == EnemyType.Giant)
            {
                SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().GiantHitSfx);
                other.GetComponent<Enemy>().mainCollider.isTrigger = true;
                if(GiantHitFX != null)
                Instantiate(GiantHitFX, other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position), Quaternion.identity);
                if (m_ProjectileType == ProjectileType.Main)
                {
                    StartCoroutine(SlowMoRoutine());
                    MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                }
            }
        }

        if (other.gameObject.CompareTag("Hoop5"))
        {
            numToInstantiate = 5;
            if (m_ProjectileType == ProjectileType.Main)
            {
                SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().MultiplierHoopSfx);
                for (int i = 0; i < numToInstantiate; i++)
                {
                    GameObject g = Instantiate(Clone, other.transform.GetChild(i).transform.position, Quaternion.Euler(new Vector3(other.transform.GetChild(i).eulerAngles.x, other.transform.GetChild(i).eulerAngles.y, other.transform.GetChild(i).eulerAngles.z)));
                }

            }
        }

        if (other.gameObject.CompareTag("Hoop10"))
        {
            numToInstantiate = 10;
            if (m_ProjectileType == ProjectileType.Main)
            {
                SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().MultiplierHoopSfx);
                for (int i = 0; i < numToInstantiate; i++)
                {
                    GameObject g = Instantiate(Clone, other.transform.GetChild(i).transform.position, Quaternion.Euler(new Vector3(other.transform.GetChild(i).eulerAngles.x, other.transform.GetChild(i).eulerAngles.y, other.transform.GetChild(i).eulerAngles.z)));
                 






                }

            }
        }

        if (other.gameObject.CompareTag("Hoop15"))
        {
            numToInstantiate = 15;
            if (m_ProjectileType == ProjectileType.Main)
            {
                SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().MultiplierHoopSfx);
                for (int i = 0; i < numToInstantiate; i++)
                {
                    GameObject g = Instantiate(Clone, other.transform.GetChild(i).transform.position, Quaternion.Euler(new Vector3(other.transform.GetChild(i).eulerAngles.x, other.transform.GetChild(i).eulerAngles.y, other.transform.GetChild(i).eulerAngles.z)));
                   






                }

            }
        }
        if (other.gameObject.CompareTag("Death"))
        {
            if (m_ProjectileType == ProjectileType.Main)
            {
                if (GM.Instance.KillCount == 0)
                {
                    GM.Instance.LevelFail();
                }

                if (GM.Instance.KillCount < GM.Instance.TargetThisLevel)
                {
                    GM.Instance.LevelFail();
                }
            }
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.CompareTag("Enemy"))
    //    {
    //        if (collision.gameObject.GetComponent<Enemy>().m_EnemyType == EnemyType.Giant)
    //        {
    //            ContactPoint contact = collision.contacts[0];
    //            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
    //            Vector3 pos = contact.point;
    //            Instantiate(GiantHitFX, pos, rot);
    //        }
    //    }
    //}
}
