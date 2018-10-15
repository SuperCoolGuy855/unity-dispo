using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject Hangman;
    public GameObject Text;
    public GameObject GameStatus;
    public GameObject ResetButton;
    public GameObject CheatIndicator;
    public Sprite one;
    public Sprite two;
    public Sprite three;
    public Sprite four;
    public Sprite five;
    public Sprite six;
    public int charlength;
    private VariableStorage variable;
    private string url = "http://api.wordnik.com/v4/words.json/randomWord?api_key=71391fbd91b52827607060cd730004d358fc58c1d7e262d33";
    private string reponse = "";
    private SpriteRenderer HangmanSprite;
    private TextMeshProUGUI textMeshText;
    private TextMeshProUGUI gameStatusText;
    private char[] characters = new char[50];
    private int failed = 0;
    private Handler jsondecode;
    private int step = 0;
    private bool CheatsIndicatorStatus = false;
    private KeyCode[] list = { KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.B, KeyCode.A };

    // Use this for initialization
    void Start()
    {
        HangmanSprite = Hangman.GetComponent<SpriteRenderer>();
        textMeshText = Text.GetComponent<TextMeshProUGUI>();
        gameStatusText = GameStatus.GetComponent<TextMeshProUGUI>();
        variable = (VariableStorage)FindObjectOfType(typeof(VariableStorage));
        ResetButton.SetActive(false);
        textMeshText.text = "";
        gameStatusText.text = "";
        StartCoroutine(GetText());
    }

    // Update is called once per frame
    void Update()
    {
        if (reponse != "")
        {
        retry:
            jsondecode = JsonUtility.FromJson<Handler>(reponse);
            switch (variable.number)
            {
                case 0:
                    if (jsondecode.word.Length >= charlength)
                    {
                        StartCoroutine(GetText());
                        goto retry;
                    }
                    break;
                case 1:
                    if (jsondecode.word.Length < charlength)
                    {
                        StartCoroutine(GetText());
                        goto retry;
                    }
                    break;
                default:
                    break;
            }
            reponse = "";
            jsondecode.word = jsondecode.word.ToLower();
            Debug.Log(jsondecode.word);
            characters = jsondecode.word.ToCharArray();
            foreach (char chara in characters)
            {
                if (Char.IsLetter(chara))
                {
                    textMeshText.text += "_";
                }
                else
                {
                    textMeshText.text += chara;
                }
            }
        }
        if (failed == 1)
        {
            HangmanSprite.sprite = one;
        }
        else if (failed == 2)
        {
            HangmanSprite.sprite = two;
        }
        else if (failed == 3)
        {
            HangmanSprite.sprite = three;
        }
        else if (failed == 4)
        {
            HangmanSprite.sprite = four;
        }
        else if (failed == 5)
        {
            HangmanSprite.sprite = five;
        }
        else if (failed == 6)
        {
            HangmanSprite.sprite = six;
        }
        if (Input.GetKeyDown(list[step]))
        {
            CheatsIndicatorStatus = !CheatsIndicatorStatus;
            step++;
        }
        else
        {
            step = 0;
            CheatsIndicatorStatus = false;
        }
    }

    void FixedUpdate()
    {
        CheatIndicator.SetActive(CheatsIndicatorStatus);
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            string text = "";
            char[] textMesh = textMeshText.text.ToCharArray();
            bool status = false;
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null)
            {
                for (int i = 0; i < characters.Length; i++)
                {
                    if (hit.collider.gameObject.name == characters[i].ToString())
                    {
                        text += hit.collider.gameObject.name;
                        status = true;
                    }
                    else
                    {
                        text += textMesh[i];
                    }
                }
                textMeshText.text = text;
                Destroy(hit.collider.gameObject);
                if (!status)
                {
                    failed++;
                }
            }
        }
        if (failed == 6)
        {
            DestroyGameObject("button");
            textMeshText.text = jsondecode.word;
            gameStatusText.text = "You lose";
        }
        if (textMeshText.text == jsondecode.word && failed < 6)
        {
            DestroyGameObject("button");
            gameStatusText.text = "You win";
        }
        if (step == 10)
        {
            step = 0;
            string text = "";
            char[] textMesh = textMeshText.text.ToCharArray();
            char temp = (char)UnityEngine.Random.Range(97, 123);
            for (int i = 0; i < characters.Length; i++)
            {
                if (temp == characters[i])
                {
                    text += temp;
                }
                else
                {
                    text += textMesh[i];
                }
            }
            if (text != textMeshText.text)
            {
                textMeshText.text = text;
                Destroy(GameObject.Find(temp.ToString()));
            }
            else
            {
                step = 10;
            }
        }
    }

    void DestroyGameObject(string name)
    {
        GameObject[] GO = GameObject.FindGameObjectsWithTag(name);
        foreach (GameObject needToDestroy in GO)
        {
            Destroy(needToDestroy);
        }
        ResetButton.SetActive(true);
    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            reponse = www.downloadHandler.text;
        }
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    [System.Serializable]
    public class Handler
    {
        public int id;
        public string word;
    }
}
