using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public CardState cardState;
    public CardPattern cardPattern;
    public GameManager gameManager;
    void Start()
    {
        cardState = CardState.��½�P;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();//�䦳�o�Ӽ��Ҫ��C������A�̭����o�Ӳե�
    }
    private void OnMouseUp()
    {
        if (cardState.Equals(CardState.�w½�P))//�d�P���A
        {
            return;//�᭱�H�U�����|����
        }

        if (cardState.Equals(CardState.�t�令�\))//�d�P���A�A�ۦ�s�W
        {
            return;//�᭱�H�U�����|����
        }

        if (gameManager.ReadyToCompareCards)//�w�g�i�H���d�P�����p�U�AList����i�P
        {
            return;
        }

        OpenCard();
        gameManager.AddCardInCardComparison(this);
        gameManager.CompareCardsInList();
    }
    void OpenCard()
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
        cardState = CardState.�w½�P;
    }
}

public enum CardState//�d�P���A
{
    ��½�P, �w½�P, �t�令�\
}

public enum CardPattern
{
     �L,�_���G,�h��,��l,���e��,�ݼ�,����,ī�G,���
}
