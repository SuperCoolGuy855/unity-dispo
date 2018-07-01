using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject player;
    private Player playerScript;
    private Animator playerAnimator;
    private Rigidbody2D playerRD2D;

    private float xposSpawn = 14f;
    private float xposDel = -9.2f;
    private IEnumerator cactusContainer;
    private IEnumerator cloudContainer;
    public GameObject gameoverText;
    public Transform ground1;
	public Transform ground2;
    public Button restart;
    public GameObject cactus1;
    public GameObject cactus2;
    public GameObject cactus3;
    public GameObject cactus4;
    public GameObject cactus5;
    public GameObject cloud;
    public float startWait = 5;
    [Range(0, 50)]
	public float speed;

	void Start ()
    {
        playerScript = player.GetComponent<Player>();
        playerAnimator = player.GetComponent<Animator>();
        playerRD2D = player.GetComponent<Rigidbody2D>();
        cactusContainer = SpawnCactus();
        cloudContainer = SpawnCloud();
        StartCoroutine(cactusContainer);
        StartCoroutine(cloudContainer);
        restart.gameObject.SetActive(false);
        restart.onClick.AddListener(startover);
        gameoverText.SetActive(false);
	}

    void FixedUpdate()
    {
        ground1.Translate(Vector3.left * Time.deltaTime * speed);
        ground2.Translate(Vector3.left * Time.deltaTime * speed);
        if (ground1.position.x <= xposDel)
        {
            ground1.position = new Vector3(xposSpawn, -1f, 0f);
        }
        if (ground2.position.x <= xposDel)
        {
            ground2.position = new Vector3(xposSpawn, -1f, 0f);
        }
        foreach (GameObject r in GameObject.FindGameObjectsWithTag("Cactus"))
        {
            r.transform.Translate(Vector3.left * Time.deltaTime * speed);
            if (r.transform.position.x <= -9.2f)
            {
                Destroy(r);
            }
        }
        foreach (GameObject r in GameObject.FindGameObjectsWithTag("Cloud"))
        {
            r.transform.Translate(Vector3.left * Time.deltaTime * 0.5f);
            if (r.transform.position.x <= -9.2f)
            {
                Destroy(r);
            }
        }
        if (playerScript.died)
        {
            playerAnimator.SetBool("die", true);
            speed = 0f;
            playerRD2D.constraints = RigidbodyConstraints2D.FreezeAll;
            StopCoroutine(cactusContainer);
            StopCoroutine(cloudContainer);
            restart.gameObject.SetActive(true);
            gameoverText.SetActive(true);
            if (Input.anyKeyDown)
            {
                startover();
            }
        }
    }

    IEnumerator SpawnCactus()
    {
        Vector3 spawnLocation = new Vector3(4, -0.8f, 0);
        yield return new WaitForSeconds(startWait);
        float endtimeSpawn = 3f;
        while (true)
        {
            switch (Random.Range(1, 6))
            {
                case 1:
                    Instantiate(cactus1, spawnLocation, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(cactus2, spawnLocation, Quaternion.identity);
                    break;
                case 3:
                    Instantiate(cactus3, spawnLocation, Quaternion.identity);
                    break;
                case 4:
                    Instantiate(cactus4, spawnLocation, Quaternion.identity);
                    break;
                case 5:
                    Instantiate(cactus5, spawnLocation, Quaternion.identity);
                    break;
            }
            yield return new WaitForSeconds(Random.Range(1f, endtimeSpawn));
            speed += 0.2f;
            endtimeSpawn -= 0.05f;
        }
    }

    IEnumerator SpawnCloud()
    {
        while (true)
        {
            Vector3 spawnLocation = new Vector3(4, Random.Range(0f, 1.5f), 0);
            Instantiate(cloud, spawnLocation, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5, 11));
        }

    }

    void startover()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
