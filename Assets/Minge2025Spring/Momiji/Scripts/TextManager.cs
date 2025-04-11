using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    [SerializeField] private Text text1;
    [SerializeField] private Text text2;
    [SerializeField] private Text text3;
    [SerializeField] private Text text4;
    [SerializeField] private Text text5;
    private int textCount;
    [SerializeField] private GameObject screen;
    [SerializeField] private Button nextButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        screen.SetActive(true);
        nextButton.gameObject.SetActive(false);
        textCount = 0;
        GameManager.Instance.PauseGame();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            textCount++;
        }

        if (textCount > 2) textCount = 0;

        switch (textCount)
        {
            case 0:
                text1.text = "コストを貯めてキャラクターを出そう！！";
                text2.text = "画面上部中央の白い部分をクリックすると";
                text3.text = "コストを貯めるためのパズルに挑戦できるよ。";
                text4.text = "パズルを解いてコストを貯めよう！";
                text5.text = " ";
                nextButton.gameObject.SetActive(false);
                break;
            case 1:
                text1.text = "右下のキャラクターをドラッグ&ドロップで好きな位置に配置しよう！！";
                text2.text = "キャラクターごとに必要なコストが決められているよ。";
                text3.text = "必要な分のコストを消費することでキャラクターを配置できるよ！";
                text4.text = " ";
                text5.text = " ";
                break;
            case 2:
                text1.text = "敵の情報は画面左中央に表示されているよ。上から順に";
                text2.text = "①　今まで出現した敵の数";
                text3.text = "②　このステージで出現する敵の数";
                text4.text = "③　ゴールポイントに到達した敵の数";
                text5.text = "④　③がこの数に達したらゲームオーバーになる";
                nextButton.gameObject.SetActive(true);
                break;
        }

    }

    public void NextClick()
    {
        text1.gameObject.SetActive(false);
        text2.gameObject.SetActive(false);
        text3.gameObject.SetActive(false);
        text4.gameObject.SetActive(false);
        text5.gameObject.SetActive(false);
        screen.SetActive(false);
        nextButton.gameObject.SetActive(false);
        GameManager.Instance.ResumeGame();
        TutorialManager.Instance.ShowPuzzleOnOffButtonArrow();
    }

}
