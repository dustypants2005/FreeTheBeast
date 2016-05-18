using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Damage : NetworkBehaviour {

    public float damage = 10.0f;

	void OnTriggerEnter(Collider other)
    {
        if(other.tag == "player")
        {
            if ((other.gameObject.GetComponent<NetworkBehaviour>()).isLocalPlayer) return;

            CharacterConfig config = other.GetComponent("CharacterConfig") as CharacterConfig;
            config.health -= damage;
        }
    }
}
