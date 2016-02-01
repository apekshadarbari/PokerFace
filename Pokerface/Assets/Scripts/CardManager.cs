using UnityEngine;
using System.Collections;

public class CardManager : MonoBehaviour
{
    public GameObject[] cards = new GameObject[52];
    //cards[i] is fixed cards, like cards[0] always refers to Diamond A, the only thing changes all the time is its position
    private int[] cardNum = new int[52];
    private string[] cardPattern = new string[52];

    #region Player Variables
    public GameObject cube;
    public GameObject sphere;
    public GameObject cylinder;
    private GameObject[] cubeHands = new GameObject[2];
    private GameObject[] sphereHands = new GameObject[2];
    private GameObject[] communityCards = new GameObject[3];
    #endregion
    #region Shuffle Variables
    private GameObject[] shuffledCards = new GameObject[52];
    private Vector3[] cardOriginPos = new Vector3[52];
    private Vector3[] cardPos = new Vector3[52];
    private Vector3[] cardNewPos = new Vector3[52];
    private bool shuffled;
    #endregion
    #region Deal Variables
    private Vector3 cubePos;
    private Vector3 spherePos;
    private Vector3 cylinderPos;

    private Vector3 cardGap;
    public float time = 2.0f;
    private float rate = 0.0f;
    private float i = 0.0f;
    #endregion

    void Start()
    {
        for (int i = 0; i < 52; i++)
        {
            cardOriginPos[i] = cards[i].transform.position;
            cardPos[i] = cards[i].transform.position;
            cardNum[i] = cards[i].GetComponent<Cards>().cardNum;
            cardPattern[i] = cards[i].GetComponent<Cards>().cardPattern;
        }

        cubePos = cube.transform.position;
        spherePos = sphere.transform.position;
        cylinderPos = cylinder.transform.position;
        cardGap = new Vector3(0.5f, 0, 0);
    }

    public void Shuffle()
    {
        for (int i = 0; i < 52; i++)
        {
            Vector3 temp = cardPos[i];
            int j = Random.Range(0, 51);
            cardPos[i] = cardPos[j];
            cardPos[j] = temp;
            cards[i].transform.position = cardPos[i];
            cards[j].transform.position = cardPos[j];
        }

        for (int i = 0; i < 52; i++)
        {
            cardNewPos[i] = cards[i].transform.position;
            for (int j = 0; j < 52; j++)
            {
                if (cardNewPos[i] == cardOriginPos[j])
                { //Find the card in the sequence, make sure who is the top, second, third...
                    shuffledCards[j] = cards[i]; //shuffledCards[j] can be used as shuffled cards, with new positions
                }
            }
        }
        shuffled = true;
    }
    void MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        rate = 1.0f / time;
        if (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
        }
    }

    private bool startDeal;

    public void clickDeal()
    {
        startDeal = true;
    }
    //initial deal 
    public void Deal()
    {
        if (shuffled)
        {
            for (int j = 0; j < 4; j++) //j is the number of cards going to be dealed
            {
                if (j < 4)
                {
                    if (j % 2 == 0)//if j is even(number)
                    {                                                                               //cubepos = player pos + gap 
                        MoveObject(shuffledCards[j].transform, shuffledCards[j].transform.position, (cubePos + 0.5f * j * cardGap), 5f);
                    }
                    else //if j is odd //if player.id == 2 
                    {
                        MoveObject(shuffledCards[j].transform, shuffledCards[j].transform.position, (spherePos + 0.5f * (j - 1) * cardGap), 5f);
                    }
                }
            }
        }
    }

    public void DealRiver()
    {
        for (int j = 0; j < 7; j++) //j is the number of cards going to be dealed
        {
            //else 
            //{        
            Debug.Log("jeg burde få en river");//
            MoveObject(shuffledCards[j].transform, shuffledCards[j].transform.position, (cylinderPos + (j - 4) * cardGap), 5f);
            //}

        }
    }
    //cube
    private GameObject[] cubeFiveCards;
    int[] cubeFiveNum = new int[5];
    string[] cubeFiveString = new string[5];

    private GameObject[] sphereFiveCards;
    int[] sphereFiveNum = new int[5];
    string[] sphereFiveString = new string[5];

    public void compareCards()
    {
        //this will have to be run after the River - or whenever the game ends - or when 
        cubeFiveCards = new GameObject[5] { shuffledCards[0], shuffledCards[2], shuffledCards[4], shuffledCards[5], shuffledCards[6] };
        sphereFiveCards = new GameObject[5] { shuffledCards[1], shuffledCards[3], shuffledCards[4], shuffledCards[5], shuffledCards[6] };

        #region Read the numbers and patterns of these five cards
        for (int i = 0; i < 5; i++)
        {
            cubeFiveNum[i] = cubeFiveCards[i].GetComponent<Cards>().cardNum;
            cubeFiveString[i] = cubeFiveCards[i].GetComponent<Cards>().cardPattern;
            sphereFiveNum[i] = sphereFiveCards[i].GetComponent<Cards>().cardNum;
            sphereFiveString[i] = sphereFiveCards[i].GetComponent<Cards>().cardPattern;
        }
        #endregion

        pairs();
    }

    int cubePairs = 0;
    int cubePairNum = 0;

    int spherePairs = 0;
    int spherePairNum = 0;

    private void pairs()
    {
        for (int i = 0; i <= 3; i++)
        {
            for (int j = i + 1; j <= 4; j++)
            {
                if (cubeFiveNum[i] == cubeFiveNum[j])
                {
                    cubePairs++;
                    cubePairNum = cubeFiveNum[i];
                }
                if (sphereFiveNum[i] == sphereFiveNum[j])
                {
                    spherePairs++;
                    spherePairNum = sphereFiveNum[i];
                }
            }
        }

        if (cubePairs > spherePairs)
        {
            Debug.Log("Cube has one more pairs. So cube wins!");
        }
        else if (cubePairs < spherePairs)
        {
            Debug.Log("Sphere has one more pairs. So Sphere wins!");
        }
        else
        {
            if (cubePairs == 0 && spherePairs == 0)
            {
                int cubeTop = largerCards(cubeFiveNum)[0];
                int sphereTop = largerCards(sphereFiveNum)[0];
                for (int i = 0; i < 5; i++)
                {
                    Debug.Log(cubeTop);
                    Debug.Log(sphereTop);
                }

                if (cubeTop > sphereTop)
                {
                    Debug.Log("No one has any pairs. Cube has the largest card: " + cubeTop + ". So cube wins!");
                }
                else if (cubeTop < sphereTop)
                {
                    Debug.Log("No one has any pairs. Sphere has the largest card: " + sphereTop + ". So sphere wins!");
                }
                else
                {
                    Debug.Log("No one has any pairs. They both have largest cards. So it's draw game!");
                }
            }
            else
            {
                if (cubePairNum > spherePairNum)
                {
                    Debug.Log("Cube has a pair of " + cubePairNum + ". Sphere has a pair of " + spherePairNum + " . So cube wins!");
                }
                else if (cubePairNum < spherePairNum)
                {
                    Debug.Log("Cube has a pair of " + cubePairNum + ". Sphere has a pair of " + spherePairNum + " . So sphere wins!");
                }
                else
                {
                    Debug.Log("Cube has a pair of " + cubePairNum + ". Sphere has a pair of " + spherePairNum + " . So draw game!");
                }
            }
        }
    }

    private int[] largerCards(int[] cardList)
    {
        //if (listI == null) throw new ArgumentNullException("listI");
        int temp = 0;
        for (int i = 0; i < cardList.Length - 1; i++)
        {
            for (int j = i + 1; j < cardList.Length; j++)
            {
                if (cardList[i] < cardList[j])
                {
                    temp = cardList[i];
                    cardList[i] = cardList[j];
                    cardList[j] = temp;
                }
            }
        }
        return cardList;
    }

    void Update()
    {
        if (startDeal)
        {
            Deal();
        }
    }
}