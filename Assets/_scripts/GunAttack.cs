using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets._scripts
{
    public class GunAttack: NetworkBehaviour 
    {
        public Transform gun;
        public float bulletSpeed = 50.0f;
        public float attackSpeed = 0.1f;
        private bool shot;
        public Object Bullet;

        public void Fire()
        {
            if (!isLocalPlayer) return;

            if(Input.GetAxis("Fire1") == 1.0f){
                if(!shot){
                    FireCommad();
                    shot = true;
                    if(attackSpeed <= 0 )
                    {
                        attackSpeed = 0;
                    }
                    StartCoroutine(Delay());
                }            
            }        
        }

        public void SetGun()
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.tag == Constants.CharacterSnapPoints.Gun.ToString())
                {
                    gun = child;
                }
            }
        }

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(attackSpeed);
            shot = false;
        }

        [Command]
        public void FireCommad()
        {
            FireRPC();
        }

        [ClientRpc]
        public void FireRPC()
        {
            var bullet = Instantiate(Bullet, gun.position, transform.rotation);
            var rb = ((GameObject)bullet).GetComponent<Rigidbody>();
            rb.AddForce( transform.forward * bulletSpeed);
        }
    }
}
