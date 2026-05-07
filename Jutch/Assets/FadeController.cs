using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    SpriteRenderer sr;
    Color c ;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        c = sr.color;
        FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }
    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeInCoroutine()
    {
        c.a = 1f;
        sr.color = c;

        float duration = 0.5f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / duration);
            sr.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        sr.color = new Color(c.r, c.g, c.b, 0f);
    }

    IEnumerator FadeOutCoroutine()
    {
        c.a = 0f;
        sr.color = c;

        float duration = 0.5f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, t / duration);
            sr.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        sr.color = new Color(c.r, c.g, c.b, 1f);
    }

}
