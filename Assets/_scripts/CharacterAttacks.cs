using UnityEngine;
using System.Collections;

public class CharacterAttacks : MonoBehaviour 
{
    public Transform gun;
    public float bulletSpeed = 50.0f;
    public float attackSpeed = 0.5f;
    private bool shot;
    public GameObject characterModel;

    void Update ()
    {
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
        GameObject bullet = (GameObject)PhotonNetwork.Instantiate("Bullet", gun.position, characterModel.transform.rotation, 0);
        var rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(transform.localPosition * bulletSpeed);
    }
}
