using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;
    public GameObject endTxt;
    public GameObject endPanel;

    public Text NowScore;
    public Text BestScore;

    public int cardCount = 0;

    float time = 0.0f;

    string key = "BestScore";

    bool isPlay = true;

    AudioSource audioSource;
    public AudioClip clip;

    
    // Start is called before the first frame update

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {
            time += Time.deltaTime;
            timeTxt.text = time.ToString("N2");
        }
        if(time > 60.0f)
        {
            endTxt.SetActive(true);
            Time.timeScale = 0.0f;
            GameOver();
        }
        
    }
    public void Matched()
    {
        if(firstCard.idx == secondCard.idx)
        {

            audioSource.PlayOneShot(clip);
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;
            if(cardCount == 0)
            {
                Time.timeScale = 0.0f;
                endTxt.SetActive(true);
                GameOver();
            }
        }
        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        firstCard = null;
        secondCard = null;
    }
    public void GameOver()
    {
        isPlay = false;
        Invoke("TimeStop", 0.5f);
        NowScore.text = time.ToString("N2");
        if (PlayerPrefs.HasKey(key))
        {
            float best = PlayerPrefs.GetFloat(key);
            if (best < time) // 시간이 작아질수록 최고 점수로 갱신
            {
                PlayerPrefs.SetFloat(key, time);
                BestScore.text = best.ToString("N2");
            }
            else
            {
                BestScore.text = best.ToString("N2");
            }
        }
        else
        {
            PlayerPrefs.SetFloat(key, time);
            BestScore.text = time.ToString("N2");
        }
        endPanel.SetActive(true);

    }
    void TimeStop()
    {
        Time.timeScale = 0.0f;
    }
}

