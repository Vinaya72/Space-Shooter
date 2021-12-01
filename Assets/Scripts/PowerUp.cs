using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
     [SerializeField]
    private float _speed = 3.0f;
     [SerializeField]
    private int powerupID;
    [SerializeField]
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
         transform.Translate(Vector3.left * _speed * Time.deltaTime);
        if(transform.position.y < -11.1f)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){

            PlayerController player = other.transform.GetComponent<PlayerController>();

            if(player != null){

               switch(powerupID){
                   case 0:
                       player.SpeedBoostActive();
                       break;
                   case 1: 
                       player.ShieldsActive();
                       break;
                    default:
                        Debug.Log("Default Value");
                        break;
               }

                   
                }

                Destroy(this.gameObject);
                
            }
            
        }
}
