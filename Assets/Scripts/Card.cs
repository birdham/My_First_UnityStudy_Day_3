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

        //firstCard�� ����ٸ�,
        if (GameManager.Instance.firstCard == null)
        {
            //firstCard���� �� ������ �Ѱ��ش�.
            GameManager.Instance.firstCard = this;
        }
        else
        {
            // secondCard�� �� ������ �Ѱ��ش�.
            GameManager.Instance.secondCard = this;
            GameManager.Instance.Matched();
        }   // mached �Լ��� ȣ���� �ش�.
        
        
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
