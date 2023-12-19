using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("���d�P���M��")]
    public List<Card> cardComparison;

    [Header("�d�P�����M��")]
    public List<CardPattern> cardsToBePutIn;//��8�i�P

    public Transform[] positions;

    [Header("�w�t�諸�d�P�ƶq")]
    public int matchedCardsCount = 0; 

    void Start()
    {
        //SetupCardsToBePutIn();
        //AddNewCard(CardPattern.���e��);
        GenerateRandomCards();
    }
    void SetupCardsToBePutIn()//Enum��List
    {
        Array array = Enum.GetValues(typeof(CardPattern));
        foreach (var item in array)
        {
            cardsToBePutIn.Add((CardPattern)item);//�j����
        }
        cardsToBePutIn.RemoveAt(0);//�R��Cardpattern.�L
    }
    void GenerateRandomCards()//�o�P
    {
        int positionIndex = 0;//�o�P��m

        for(int i = 0; i < 2; i++)//8�Ӥ@�աA����եX��
        {
            //�ǳ�8�i�P
            SetupCardsToBePutIn();

            //�̤j�üƤ��W�L8
            int maxRandomNumber = cardsToBePutIn.Count;

            for(int j = 0; j < maxRandomNumber; maxRandomNumber--)
            {
                //0��8�������Ͷü� �̤p�O0�̤j�O7
                int randomNumber = UnityEngine.Random.Range(0, maxRandomNumber);//Unity�̭����@��Random���O��Range��k

                //��P
                AddNewCard(cardsToBePutIn[randomNumber], positionIndex);
                cardsToBePutIn.RemoveAt(randomNumber);
                positionIndex++;
            }
        }

    }

    void AddNewCard(CardPattern cardPattern, int positionIndex)//positionIndex��m�s��
    {
        GameObject card = Instantiate(Resources.Load<GameObject>("Prefabs/Card"));//Ū����GameObject����������A���J�P������
        card.GetComponent<Card>().cardPattern = cardPattern;//�]�w�O���ؤ��G
        card.name = "Card_" + cardPattern.ToString();//��W��
        card.transform.position = positions[positionIndex].position;//�}�C�̭�����m�s���A�����󪺦�m

        GameObject graphic = Instantiate(Resources.Load<GameObject>("Prefabs/��"));//���J�Ϫ�����
        graphic.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Graphics/" + cardPattern.ToString());//�]�w�ϼ�
        graphic.transform.SetParent(card.transform);//�ܦ��P���l����
        graphic.transform.localPosition = new Vector3(0, 0, 0.1f);//�]�w�y��
        graphic.transform.eulerAngles = new Vector3(0, 180, 0);//����Y�b��180�� ½�P�ɤ��|���k�A��
    }

    public void AddCardInCardComparison(Card card)
    {
        cardComparison.Add(card);
    }
    public bool ReadyToCompareCards//�ݩ�
    {
        get
        {
            if (cardComparison.Count == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public void CompareCardsInList()
    {
        if (ReadyToCompareCards)
        {
            //Debug.Log("�i�H���d�P�F");
            if (cardComparison[0].cardPattern == cardComparison[1].cardPattern)
            {
                Debug.Log("��i�P�@��");
                foreach (var card in cardComparison)//�M��̪��C�@�Ӷ���
                {
                    card.cardState = CardState.�t�令�\;
                }
                ClearCardComparison();
                matchedCardsCount = matchedCardsCount + 2;
                if (matchedCardsCount >= positions.Length)//�w�t�諸�d�P�ƶq�A�W�L�ε���16�i
                {
                    //�N����������\�t��F
                    StartCoroutine(ReloadScene());
                }
            }
            else 
            {
                Debug.Log("��i�P���@��");
                StartCoroutine(MissMatchCards());
                //TurnBackCards();
                //ClearCardComparison();
            }
        }
    }
    void ClearCardComparison()
    {
        cardComparison.Clear();
    }
    void TurnBackCards()
    {
        foreach (var card in cardComparison)//���M��̭����C�@�i�P
        {
            card.gameObject.transform.eulerAngles = Vector3.zero;// = new Vector3(0, 0, 0) �����k�s
            card.cardState = CardState.��½�P;
        }
    }
    IEnumerator MissMatchCards()//IEnumerator��������k�A�ݭn�Ψ�{�Ӷi��
    {
        yield return new WaitForSeconds(1.5f);//��o�q����1.5��A�A���۰���H�U
        TurnBackCards();
        ClearCardComparison();
    }
    IEnumerator ReloadScene()//���s�~�P
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//���sŪ�������CŪ���ثe���椤������
    }
}
