using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    private float _speed = 8.0f;
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        if (transform.position.x < -10.6f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }


    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            PlayerController player = other.transform.GetComponent<PlayerController>();
            if(player != null){
                player.Damage();
            }

            Destroy(this.gameObject);
        }
    }
}

