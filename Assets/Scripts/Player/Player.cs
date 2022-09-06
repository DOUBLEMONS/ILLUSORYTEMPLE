using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player :  GameManager
{
    [SerializeField]
    [Range(0.01f, 10f)]
    private float fadeTime;

    public GameManager gameManager;
    [SerializeField] public float maxSpeed;
    [SerializeField] public float jumpPower;
    [SerializeField] Transform pos;
    CapsuleCollider2D capsuleCollider;
    Rigidbody2D rigid; // �����̵��� ���� ���� ����
    SpriteRenderer spriteRenderer;
    Animator anim;
    AudioSource audioSource;
    public AudioClip audioJump;
    Image image;

    void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        rigid = GetComponent<Rigidbody2D>(); // ���� �ʱ�ȭ
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        image = GetComponent<Image>();
        this.audioSource = GetComponent<AudioSource>();
    }

    void PlaySound(String action)
    {
        switch (action)
        {
            case "Jump":
                audioSource.clip = audioJump;
                break;
        }
        audioSource.Play();
    }

    void Update()
    {
        //Jump
        if (Input.GetButton("Jump") && !anim.GetBool("isJumping")) // �� �� �������� �� ���� ����
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
            audioSource.PlayOneShot(audioJump);
        }

        //Stop Speed
        if (Input.GetButton("Horizontal")) // normalized : ���� ũ�⸦ 1�� ���� ���� (��������)
        {
            rigid.velocity = new Vector3(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y, 0);
        }

        //Animation
        if (Mathf.Abs(rigid.velocity.x) < 0.1) // Mathf.Abs() : ���밪
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);

        /* rigid.Velocity */
        float pos = Input.GetAxis("Horizontal");
        rigid.velocity = new Vector2(pos * maxSpeed, rigid.velocity.y);

        // �¿����
        if (pos < 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else if (pos > 0)
            transform.eulerAngles = new Vector3(0, 0, 0);

        //Move Speed
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h * 2, ForceMode2D.Impulse); // ���� �����Ͽ� ���θ� �ö��� ���� 2�� ����

        //Max Speed
        if (rigid.velocity.x > maxSpeed) // Right Max Speed // velocity : ������ٵ��� ���� �ӵ�
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }

        else if (rigid.velocity.x < maxSpeed * (-1)) // Left Max Speed 
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }

        //Landing Platform
        if (rigid.velocity.y < 0) // ������Ʈ�� �����ߴٰ� �ٽ� ������ ��
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Tilemap"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 2)
                    //Debug.Log(rayHit.collider.name); // ������Ʈ�� ��ġ�� �ǽð����� �˷���
                    anim.SetBool("isJumping", false);
            }
        }

        if (rigid.velocity.y < 0) // ������Ʈ�� �����ߴٰ� �ٽ� ������ ��
        {
            RaycastHit2D rayHit1 = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Shild"));
            if (rayHit1.collider != null)
            {
                if (rayHit1.distance < 2)
                    //Debug.Log(rayHit.collider.name); // ������Ʈ�� ��ġ�� �ǽð����� �˷���
                    anim.SetBool("isJumping", false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            HealthDown();
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Clear")
        {
            StartCoroutine(Fade(1, 0));

            Invoke("OnClear", 2);
        }
    }

    void OnDamaged()
    {
        // Health Down
        gameManager.HealthDown();

        // Change Layer (Immortal Active)
        gameObject.layer = 13;

        // View Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        Invoke("OffDamaged", 2);
    }

    void OffDamaged()
    {
        gameObject.layer = 6;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void OnDie()
    {
        gameObject.layer = 13;
        StartCoroutine(Fade(1, 0));
        StartCoroutine(Fade1(0, 1));

        Invoke("NextSceneWithNum", 4);
    }

    public void OnClear()
    {
        Application.Quit();
    }

    public void NextSceneWithNum()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator Fade(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = spriteRenderer.color;
            color.a = Mathf.Lerp(start, end, percent);
            spriteRenderer.color = color;

            yield return null;
        }

    }

    private IEnumerator Fade1(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = image.color;
            color.a = Mathf.Lerp(start, end, percent);
            image.color = color;

            yield return null;
        }

    }
}
