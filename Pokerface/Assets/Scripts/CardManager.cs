using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardManager : Photon.MonoBehaviour
{

    private bool dealtRiver;
    private bool shuffled;
    //[SerializeField]
    //GameObject deck;


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
    public Stack<GameObject> cardStack;
    //private GameObject[] shuffledCards = new GameObject[52];
    //private Vector3[] cardOriginPos = new Vector3[52];
    //private Vector3[] cardPos = new Vector3[52];
    //private Vector3[] cardNewPos = new Vector3[52];

    #endregion
    #region Deal Variables
    //private Transform playerOneHandTrans;
    //private Transform playerTwoHandTrans;
    //private Transform communityTrans;

    //private Vector3 cubePos;
    //private Vector3 spherePos;
    //private Vector3 cylinderPos;

    //private Quaternion cubeRot;
    //private Quaternion sphereRot;
    //private Quaternion cylinderRot;

    private Vector3 cardGap;

    public float time = 2.0f;
    private float rate = 0.0f;
    private float i = 0.0f;
    #endregion

    void Start()
    {
        //if (PhotonNetwork.isMasterClient)
        //{
        //    PhotonNetwork.InstantiateSceneObject(deck.name, deck.transform.position, deck.transform.rotation, 0, null);

        //}
        //for (int i = 0; i < 52; i++)
        //{
        //    cardOriginPos[i] = cards[i].transform.position;
        //    cardPos[i] = cards[i].transform.position;
        //    cardNum[i] = cards[i].GetComponent<Cards>().cardNum;
        //    cardPattern[i] = cards[i].GetComponent<Cards>().cardPattern;
        //}

        //cubePos = cube.transform.position;
        //spherePos = sphere.transform.position;
        //cylinderPos = cylinder.transform.position;

        //cubeRot = cube.transform.rotation;
        //sphereRot = sphere.transform.rotation;
        //cylinderRot = cylinder.transform.rotation;

        //playerOneHandTrans = cube.transform;
        //playerTwoHandTrans = sphere.transform;
        //communityTrans = cylinder.transform;
        dealtRiver = false;
        cardGap = new Vector3(0.5f, 0, 0);


        cube = GameObject.Find("PlayerOneHand");
        sphere = GameObject.Find("PlayerTwoHand");
        cylinder = GameObject.Find("CommunityCards");

        List<GameObject> cardPrefabs = new List<GameObject>();

        string prefix = "Card_";
        string[] value = new string[]
        {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "J",
            "Q",
            "K",
            "A"
        };
        string[] suit = new string[]
        {
            "Club",
            "Diamond",
            "Heart",
            "Spade"
        };

        foreach (var v in value)
        {
            foreach (var s in suit)
            {
                cardPrefabs.Add(Resources.Load<GameObject>(prefix + v + s));
            }
        }

        cards = cardPrefabs.ToArray();

        Debug.Log("Hello from card manager(s)?");
    }

    public void Shuffle()
    {
        Debug.Log("Everyday I'm Shuffling");

        var shuffledCards = new GameObject[cards.Length];

        for (int i = 0; i < cards.Length; i++)
        {
            shuffledCards[i] = cards[i];
        }

        for (int iteration = 0; iteration < 5; iteration++)
        {
            for (int i = 0; i < shuffledCards.Length; i++)
            {
                var c = shuffledCards[i];
                int j = Random.Range(0, 51);
                shuffledCards[i] = shuffledCards[j];
                shuffledCards[j] = c;
            }
        }

        cardStack = new Stack<GameObject>(shuffledCards);
        shuffled = true;


        //for (int i = 0; i < 52; i++)
        //{
        //    Vector3 temp = cardPos[i];
        //    int j = Random.Range(0, 51);
        //    cardPos[i] = cardPos[j];
        //    cardPos[j] = temp;
        //    cards[i].transform.position = cardPos[i];
        //    cards[j].transform.position = cardPos[j];
        //}

        //for (int i = 0; i < 52; i++)
        //{
        //    cardNewPos[i] = cards[i].transform.position;
        //    for (int j = 0; j < 52; j++)
        //    {
        //        if (cardNewPos[i] == cardOriginPos[j])
        //        { //Find the card in the sequence, make sure who is the top, second, third...
        //            shuffledCards[j] = cards[i]; //shuffledCards[j] can be used as shuffled cards, with new positions
        //        }
        //    }
        //}


    }
    //void MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, Quaternion endRot, float time)
    //{
    //    rate = 1.0f / time;
    //    if (i < 1.0f)
    //    {
    //        i += Time.deltaTime * rate;
    //        thisTransform.position = Vector3.Lerp(startPos, endPos, i);
    //        thisTransform.rotation = endRot;
    //    }
    //}

    //private bool startDeal;

    //public void clickDeal()
    //{
    //    startDeal = true;
    //}
    //initial deal 
    public void Deal()
    {
        if (shuffled)
        {
            Debug.Log("Dealing");
            //var c = cardStack.Pop();

            for (int j = 0; j < 4; j++) //j is the number of cards going to be dealed
            {

                if (j % 2 == 0)//if j is even(number)
                {                                                                               //cubepos = player pos + gap 
                    DealCardTo(cube);
                    //PhotonNetwork.Instantiate(shuffledCards[j].name, cube.transform.position + 0.5f * j * cardGap, cube.transform.rotation, 0);
                    //var card = cardStack.Pop();
                    //PhotonNetwork.Instantiate(card.name, cube.transform.position, cube.transform.rotation, 0);
                    //Debug.Log("player one´s hand contains " + card);

                }
                else //if j is odd //if player.id == 2 
                {
                    //var card = cardStack.Pop();
                    DealCardTo(sphere);
                    //PhotonNetwork.Instantiate(card.name, sphere.transform.position, sphere.transform.rotation, 0);
                    //Debug.Log("player two´s hand contains " + card);
                    //PhotonNetwork.Instantiate(shuffledCards[j].name, sphere.transform.position + 0.5f * (j - 1) * cardGap, sphere.transform.rotation, 0);
                    //MoveObject(shuffledCards[j].transform, shuffledCards[j].transform.position, (spherePos + 0.5f * (j - 1) * cardGap), sphereRot, 5f);
                }
            }
        }
    }
    public void DealRiver()
    {
        if (shuffled)
        {
            if (!dealtRiver)
            {
                for (int i = 0; i < 3; i++)
                {
                    Debug.Log("river contains card");
                    DealCardTo(cylinder);
                    //var card = cardStack.Pop();
                    //PhotonNetwork.Instantiate(card.name, cylinder.transform.position, cylinder.transform.rotation, 0);
                    //PhotonNetwork.Instantiate(shuffledCards[j].name, cylinder.transform.position, cylinder.transform.rotation, 0);
                }
                dealtRiver = true;
                //dealtRiver = false;
                //Deal();
            }
        }
    }
    //MoveObject(
    //    shuffledCards[j].transform,
    //    shuffledCards[j].transform.position,
    //    (cylinderPos + (j - 1) * cardGap),
    //    Quaternion.Euler(-90, 180, 0),
    //    5f);
    ////}
    //if (!dealtRiver)
    //{
    //if (cardStack == null && !shuffled)
    //{
    //    Debug.Log("Simon er bøsse");
    //    return;
    //}
    //else
    //{
    //    Debug.Log("Jacob er bøsser");
    //}

    //Shuffle();
    //var c = cardStack.Pop();
    //for (int j = 4; j < 7; j++) //j is the number of cards going to be dealed

    private void DealCardTo(GameObject receiver)
    {
        var card = cardStack.Pop();
        card = PhotonNetwork.Instantiate(card.name, receiver.transform.position, receiver.transform.rotation, 0);
        //card.transform.parent = receiver.transform;
        //card.transform.SetParent(receiver.transform, true);
        //card.transform.position = Vector3.zero;

        //if set parent kommer til at virke -
        //compare
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
        //cubeFiveCards = new GameObject[5] { shuffledCards[0], shuffledCards[2], shuffledCards[4], shuffledCards[5], shuffledCards[6] };
        //sphereFiveCards = new GameObject[5] { shuffledCards[1], shuffledCards[3], shuffledCards[4], shuffledCards[5], shuffledCards[6] };

        //cube get children component
        //card holder klasse

        //#region Read the numbers and patterns of these five cards
        //for (int i = 0; i < 5; i++)
        //{
        //    cubeFiveNum[i] = cubeFiveCards[i].GetComponent<Cards>().cardNum;
        //    cubeFiveString[i] = cubeFiveCards[i].GetComponent<Cards>().cardPattern;
        //    sphereFiveNum[i] = sphereFiveCards[i].GetComponent<Cards>().cardNum;
        //    sphereFiveString[i] = sphereFiveCards[i].GetComponent<Cards>().cardPattern;
        //}
        //#endregion

        pairs();
    }

    int cubePairs = 0;
    int cubePairNum = 0;

    int spherePairs = 0;
    int spherePairNum = 0;

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

    //void Update()
    //{
    //    if (startDeal)
    //    {
    //        Deal();
    //    }
    //}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
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

}
