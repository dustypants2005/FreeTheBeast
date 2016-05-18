using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CharacterAttacks : NetworkBehaviour 
{
    public Transform gun;
    public float bulletSpeed = 50.0f;
    public float attackSpeed = 0.1f;
    private bool shot;
    public GameObject characterModel;
    public Object Bullet;

    void Update ()
    {
        if (!isLocalPlayer) return;

        if(Input.GetAxis("Fire1") == 1.0f){
            if(!shot){
                Fire();
                shot = true;
                if(attackSpeed <= 0 )
                {
                    attackSpeed = 0;
                }
                StartCoroutine(Delay());
            }            
        }        
	}

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(attackSpeed);
        shot = false;
    }

    void Fire()
    {
        var bullet = Instantiate(Bullet, gun.position, transform.rotation);
        var rb = ((GameObject)bullet).GetComponent<Rigidbody>();
        rb.AddForce( transform.forward * bulletSpeed);
    }
}
