using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClearFailText : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    [SerializeField] Text text;
    private RectTransform moveText;
    private float p;
    private float t;
    public float panelSpeed = 0.001f;
    public float textSpeed = 0.1f;
    [SerializeField] bool victory = false;
    [SerializeField] bool fadeout = false;
    // start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        p = Panel.GetComponent<Image>().color.a;
        moveText = text.GetComponent<RectTransform>();
        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(victory == true && Input.GetKeyDown(KeyCode.A))
        {
            text.text = "VICTORY";
            StartCoroutine(FadeinPanel());
            StartCoroutine(MoveText());
        }
        
        if(victory == false && Input.GetKeyDown(KeyCode.A))
        {
            text.text = "GAME  OVER";
            StartCoroutine(FadeinPanel());
            StartCoroutine(MoveText());
        }

        if (fadeout == true)
        {
            fadeout = false;
            StartCoroutine(FadeoutPanel());
        }
    }

    IEnumerator FadeinPanel()
    {
        while (p < 0.7)
        {
            Panel.GetComponent<Image>().color += new Color(0, 0, 0, panelSpeed);
            p += panelSpeed;
            yield return null;
        }
        
    }

    IEnumerator FadeoutPanel()
    {
        while (p > 0)
        {
            Panel.GetComponent<Image>().color -= new Color(0, 0, 0, panelSpeed);
            p -= panelSpeed;
            yield return null;
        }
        
    }

    IEnumerator MoveText()
    {
        moveText.anchoredPosition = Vector2.zero;
        text.enabled = true;
        t = 14;
        while(t > 0)
        {
            moveText.anchoredPosition += new Vector2(t, 0);
            t -= textSpeed;
            yield return null;
        }

        t = 0;
        while (t < 3000)
        {
            t += textSpeed;
        }
        
        t = 0;
        fadeout = true;
        while(t < 20)
        {
            moveText.anchoredPosition += new Vector2(t, 0);
            t += textSpeed*2.0f;
            yield return null;
        }
        text.enabled = false;
    }
    
}
