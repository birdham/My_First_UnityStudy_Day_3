using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx = 0;

    public GameObject front;
    public GameObject back;

    public Animator amin;

    public SpriteRenderer frontImage;

    AudioSource audioSource;
    public AudioClip clip;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setting(int number)
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"rtan{idx}");
    }

    public void OpenCard()
    {
        audioSource.PlayOneShot(clip);

        amin.SetBool("isOpen", true);
        front.SetActive(true);
        back.SetActive(false);

        //firstCard가 비엇다면,
        if (GameManager.Instance.firstCard == null)
        {
            //firstCard가에 내 정보를 넘겨준다.
            GameManager.Instance.firstCard = this;
        }
        else
        {
            // secondCard에 내 정보를 넘겨준다.
            GameManager.Instance.secondCard = this;
            GameManager.Instance.Matched();
        }   // mached 함수를 호출해 준다.
        
        
    }
    public void DestroyCard() 
    {
        Invoke("DestroyCardInvoke", 1.0f);
    }

    void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void CloseCard()
    {
        Invoke("CloseCardinvoke", 1.0f);
    }

    void CloseCardinvoke()
    {
        amin.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);
    }
}
