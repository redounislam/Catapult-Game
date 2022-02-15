using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
namespace SharkAttack
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController Instance;
        public Transform target;
        public Vector3 offset;
        public float smoothFactor;
        public float OffsetY, OffsetZ , Xoffset;
        private bool isTargetFound = false;
        private Vector3 defaultPos;
        private Vector3 step;

        private CanvasScaler playerCanvasScaler;
        private Camera mainCam;
     


        public static CameraController SharedManager()
        {
            return Instance;
        }


        private void Awake()
        {
            if (!Instance)
                Instance = this;
          
        }

        private void Start()
        {
            mainCam = GetComponent<Camera>();
            defaultPos = transform.localPosition;
       
   
           
        }


        private void LateUpdate()
        {
           

                if (target == null)
                {
                    return;
                }

                if (target && isTargetFound == false)
                {

                    isTargetFound = true;
                }

                Vector3 smoothedPosition = Vector3.Lerp(transform.position, new Vector3(target.transform.position.x + Xoffset, target.transform.position.y + OffsetY,
                    target.transform.position.z + OffsetZ), smoothFactor * Time.deltaTime);

                transform.position = smoothedPosition;

                transform.LookAt(smoothedPosition);


        }



   
  

        public void BlastShake()
        {
            transform.DOShakePosition(1f, new Vector3(100f, 50f, 0), 10, 8f, false, true);
        }
    }
}