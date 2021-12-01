using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f;
    private PlayerController _player;
    [SerializeField]
    private GameObject _laserPrefab;
    private float _fireRate = 2.0f;
    private float _canFire = -1f;
    private bool _makeLaser = true;
    private Animator _anim;
    private bool _shoot = true;
    private AudioSource _audioSource;
 
   
    // Start is called before the first frame update
    void Start()
    {
         _player = GameObject.Find("Player").GetComponent<PlayerController>();
         _anim = GetComponent<Animator>();
         _audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        if(Time.time > _canFire && _player._lives > 0 && _shoot){
            _canFire = Time.time + _fireRate;
            Instantiate(_laserPrefab, transform.position, transform.rotation);
        }

        if(transform.position.x < -11.3f){
            float randomX = Random.Range(-11.3f, 11.3f);
            transform.position = new Vector3(randomX, 8.2f, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {

            PlayerController player = other.transform.GetComponent<PlayerController>();

            if(player != null){
                player.Damage();
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _shoot = false;
             _audioSource.Play();
            Destroy(this.gameObject, 2.0f);
           


        }

        if (other.tag == "Missile")
        {
             _makeLaser = false;
            Destroy(other.gameObject);
             if(_player != null){
                _player.AddScore(10);

            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _shoot = false;
            _audioSource.Play();
            Destroy(this.gameObject, 2.0f);
            
        }

    }

}
