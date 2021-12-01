using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _LivesImg1;
    [SerializeField]
    private Image _LivesImg2;
    [SerializeField]
    private Image _LivesImg3;
    [SerializeField]
    private Text _gameOver;
    [SerializeField]
    private Text _rKey;

    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score " +  0;
        _gameOver.gameObject.SetActive(false);
        _rKey.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives){
        if(currentLives == 2){
            _LivesImg3.enabled = false;
        }
        if(currentLives == 1){
            _LivesImg2.enabled = false;
        }
        if(currentLives == 0){
            _LivesImg1.enabled = false;
            _gameOver.gameObject.SetActive(true);
            _rKey.gameObject.SetActive(true);
            StartCoroutine(GameOverFlickerRoutine());
            _gameManager.GameOver();
        }

        
    }

    IEnumerator GameOverFlickerRoutine(){
        while(true){
            _gameOver.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOver.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

}
