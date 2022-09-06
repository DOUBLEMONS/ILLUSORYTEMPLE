using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class FadeEffect : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 10f)]
    private float fadeTime;
    private Image image;
    public float start;
    public float end;
    Player player;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Fade(0, 1));

            Invoke("NextSceneWithNum", 4f);
        }
        
    }

    public void NextSceneWithNum()
    {
        SceneManager.LoadScene(1);
    }


    private IEnumerator Fade(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = image.color;
            color.a     = Mathf.Lerp(start, end, percent);
            image.color = color;

            yield return null;
        }

    }
}
