using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseSelectController : MonoBehaviour
{
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject titleButton;
	[SerializeField] private GameObject stageSelectButton;
	[SerializeField] private GameObject pauseSelectOverlay;

	private bool isPaused = false;
	
	public void Onclick()
	{
		// 選択中のボタンを取得
		GameObject selectedButton = EventSystem.current.currentSelectedGameObject;

		if (selectedButton == titleButton)
		{
			Debug.Log("titleButtonが押されました。");
			SceneManager.LoadScene("Title");
			
		}
		else if (selectedButton == stageSelectButton)
		{
			Debug.Log("stageSelectButtonが押されました。");
			SceneManager.LoadScene("StageSelect");
		}
		else if (selectedButton == pauseMenu)
		{
			Debug.Log("pauseMenuが押されました。");
			isPaused = !isPaused; // isPausedの状態を反転　(isPausedの値をトグル（切り替え）にする)
			if (isPaused)
			{
				Time.timeScale = 0; // ゲームを一時停止
				// Pauseメニューを表示する処理をここに追加
				// 例えば、PauseメニューのUIを表示するなど
				pauseSelectOverlay.SetActive(true);
				// Titleボタンを選択状態にする
				EventSystem.current.SetSelectedGameObject(titleButton);
			}
			else
			{
				Time.timeScale = 1; // ゲームを再開
				// Pauseメニューを非表示にする処理をここに追加
				pauseSelectOverlay.SetActive(false);
			}
		}
	}
}
