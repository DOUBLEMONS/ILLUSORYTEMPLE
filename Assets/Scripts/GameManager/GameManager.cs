using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    public int life;
    public Player player;
    public Image[] UIlife;

    [SerializeField]
    public GameObject Dumble;
    public GameObject Stone;
    public GameObject Wood;

    public void HealthDown()
    {
        if (life > 1)
        {
            life--;
            UIlife[life].color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
        else 
        {
            UIlife[0].color = new Color(0.5f, 0.5f, 0.5f, 1);
            player.maxSpeed = 0;
            player.jumpPower = 0;
            player.OnDie();
        }
    }

    void Start()
    {
        StartCoroutine(CreateDumbleRoutine());
        StartCoroutine(CreateStoneRoutine());
        StartCoroutine(CreateWoodRoutine());
    }

    void Update()
    {
        
    }

    IEnumerator CreateDumbleRoutine()
    {
        while (true)
        {
            CreateDumble();
            yield return new WaitForSeconds(UnityEngine.Random.Range(2f, 3f));
        }
    }

    IEnumerator CreateStoneRoutine()
    {
        while (true)
        {
            CreateStone();
            yield return new WaitForSeconds(UnityEngine.Random.Range(2f, 3f));
        }
    }

    IEnumerator CreateWoodRoutine()
    {
        while (true)
        {
            CreateWood();
            yield return new WaitForSeconds(UnityEngine.Random.Range(2f, 3f));
        }
    }

    private void CreateDumble()
    {
        Vector3 pos1 = new Vector3(UnityEngine.Random.Range(-40.0f, 40.0f), 184, 0);
        pos1.z = 0.0f;
        Instantiate(Dumble, pos1, Quaternion.identity);
    }

    private void CreateStone()
    {
        Vector3 pos2 = new Vector3(UnityEngine.Random.Range(-40.0f, 40.0f), 184, 0);
        pos2.z = 0.0f;
        Instantiate(Stone, pos2, Quaternion.identity);
    }

    private void CreateWood()
    {
        Vector3 pos3 = new Vector3(UnityEngine.Random.Range(-40.0f, 40.0f), 184, 0);
        pos3.z = 0.0f;
        Instantiate(Wood, pos3, Quaternion.identity);
    }
}
