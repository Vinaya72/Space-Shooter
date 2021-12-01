using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private float _speedMultiplier = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    private SpawnManager _spawnManager;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject _shieldVisualiser;
     [SerializeField]
    private int _score;
    private UI_Manager _uiManager;
    [SerializeField]
    public int _lives = 3;
     private AudioSource _audioSource;
     [SerializeField]
     private AudioClip _laserSoundClip;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
         _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
         _audioSource = GetComponent<AudioSource>();
        if(_spawnManager == null){
              Debug.LogError("The Spawn MAnager is NULL");
         }
         if(_uiManager == null){
             Debug.LogError("The UI Manager is NULL");
         }
      
        if(_audioSource == null){
            Debug.LogError("The audio sourse is null");
        }

        else{
            _audioSource.clip = _laserSoundClip;
        }

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void MovePlayer()
    {
        float HorizontalInput = Input.GetAxisRaw("Horizontal");
        float VerticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(-VerticalInput, HorizontalInput, 0);

        transform.Translate(direction * speed * Time.deltaTime);
        if (transform.position.x >= 8.6f)
        {
            transform.position = new Vector3(8.6f, transform.position.y, 0);
        }
        else if (transform.position.x <= -9.3f)
        {
            transform.position = new Vector3(-9.3f, transform.position.y, 0);
        }
        if (transform.position.y > 7.4f)
        {
            transform.position = new Vector3(transform.position.x, -5.4f, 0);
        }
        else if (transform.position.y < -5.4f)
        {
            transform.position = new Vector3(transform.position.x, 7.4f, 0);
        }
    }

    void FireLaser()
    {

        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + new Vector3(1.05f, 0, 0), transform.rotation);
        _audioSource.Play();
    }

    public void Damage(){

        if(_isShieldActive == true){
            _isShieldActive = false;
            _shieldVisualiser.SetActive(false);
            return;
        }
         _lives --;
        _uiManager.UpdateLives(_lives);
        if(_lives < 1){
            _spawnManager.onPalyerDeath();
            Destroy(this.gameObject);
        }
    }


    public void SpeedBoostActive(){
        _isSpeedBoostActive = true;
        speed = speed * _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine(){
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        speed = speed / _speedMultiplier;
    }

    public void ShieldsActive(){
        _isShieldActive = true;
        _shieldVisualiser.SetActive(true);
    }

     public void AddScore(int points){
        _score += points;
        _uiManager.UpdateScore(_score);
    }

}
