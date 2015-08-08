using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {

    public float damage = 10.0f;

	void OnTriggerEnter(Collider other)
    {
        if(other.tag == "player")
        {
            CharacterConfig config = other.GetComponent("CharacterConfig") as CharacterConfig;
            config.health -= damage;
        }
    }
}
