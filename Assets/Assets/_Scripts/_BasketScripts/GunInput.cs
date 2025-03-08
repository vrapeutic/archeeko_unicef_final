using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.Interaction.Toolkit;
#if UNITY_ANDROID
using UnityEngine.InputSystem;
#endif
namespace PaintIn3D
{
    public class GunInput : MonoBehaviour
    {
        public GameObject prefab;
        public GameObject[] bullets;
        [SerializeField]
        GameEvent bulletFired;
        public int currentBulletNo = 0;
        public float speed;
        bool gunEnabled = true;
        P3dPaintDecal decal;
        public bool paintGun = false;
        public delegate void ReleaseAction();
        public static event ReleaseAction OnRelease;
        public static GunInput instance;
#if UNITY_ANDROID

#endif
        void Start()
        {
            if (paintGun) decal = prefab.GetComponent<P3dPaintDecal>();

            GameManager.OnEndSuccess += DisableGun;
            GameManager.OnEndUnsuccess += DisableGun;

        }

        private void OnDisable()
        {
            GameManager.OnEndSuccess -= DisableGun;
            GameManager.OnEndUnsuccess -= DisableGun;
        }



#if UNITY_ANDROID

#endif
        private void DisableGun()
        {
            gunEnabled = false;
        }

        public void ShootNewBullet()
        {
            if (Statistics.instance.tries == 1)
            {
                bullets[currentBulletNo].AddComponent<LastProjectile>();
            }
            //PlayShootingExplosion(currentBulletNo);
            bullets[currentBulletNo].SetActive(true);
            var bulletRigidbody = bullets[currentBulletNo].GetComponent<Rigidbody>();
            bulletRigidbody.velocity = bullets[currentBulletNo].transform.forward * speed;
            bullets[currentBulletNo].transform.parent = null;
            Vector3 pos = bullets[currentBulletNo].transform.position;
            Vector3 vel = bullets[currentBulletNo].transform.forward * speed;
            Statistics.instance.StopResponseTimeCounter();
            Statistics.instance.tries--;
            bulletFired.Raise();
            if (Statistics.instance.tries > 0 && Player.instance.canPlay)
            {
                gunEnabled = true;
                currentBulletNo++;
            }
            else if (Statistics.instance.tries == 0)
            {
                bullets[currentBulletNo].GetComponent<LastProjectile>().ReleaseLastProjectile();
            }
            OnRelease();
        }

        //private void PlayShootingExplosion(int bulletNo)
        //{
        //    var color = bullets[bulletNo].GetComponent<Renderer>().material.color;
        //    if (explosion != null)
        //    {
        //        var main = explosion.main;
        //        main.startColor = color;
        //        decal.Color = color;
        //    }
        //    explosion.Play();
        //}
    }

}