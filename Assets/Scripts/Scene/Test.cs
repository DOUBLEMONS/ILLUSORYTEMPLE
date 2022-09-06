using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
 
public class Test : MonoBehaviour 
{
    [Header("속도, 길이")]
 
    [SerializeField][Range(0f, 10f)] private float speed = 1f;
    [SerializeField][Range(0f, 10f)] private float length = 1f;
    [SerializeField][Range(0f, 10f)] private float wait = 1f;
    [SerializeField][Range(-10f, 10f)] private float yPos = -2f;

    private float runningTime = 0f;
    GameObject GameObject;
    Vector2 originPos;

    void Awake()
    {
        GameObject = GetComponent<GameObject>();
        originPos = transform.position;
    }

    void Start()
    {
        Invoke("WaitSec", wait);
    }

    void WaitSec()
    {
        this.UpdateAsObservable()
           .Subscribe(_ =>
           {
               runningTime += Time.deltaTime * speed;
               yPos = Mathf.Sin(runningTime) * length;
               this.transform.position = new Vector2(0, yPos) + originPos;
           });
    }
}

