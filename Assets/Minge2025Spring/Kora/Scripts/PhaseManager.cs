using UnityEngine;

public enum GamePhase
{
    Puzzle,
    Battle
}
public class PhaseManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //パズルフェーズ実装には、一度ゲームの進行を止めるメソッドが必要
    //on,off切り替え機能を実装する。　全部のインスタンスに...？ ->Activeで外からupdateを止める
    //必須：enemyの沸きを止める

    [Header("各フェーズのオブジェクト")]
    [SerializeField] private GameObject[] puzzleObj;
    [SerializeField] private GameObject[] battleObj;

    [Header("最初のフェーズ")]
    [SerializeField] private GamePhase startPhase = GamePhase.Puzzle;

    private GamePhase currentPhase;

    private void Start()
    {
        SetPhase(startPhase);
    }

    private void SetActiveGroup(GameObject[] group, bool isActive)
    {
        foreach (var obj in group)
        {
            if (obj != null) obj.SetActive(isActive);
        }
    }

    public void SetPhase(GamePhase phase)
    {
        currentPhase = phase;

        SetActiveGroup(puzzleObj, phase == GamePhase.Puzzle);
        SetActiveGroup(battleObj, phase == GamePhase.Battle);

        Debug.Log("SetPhase: [" + phase + "]");
    }

    public void PuzzleCompleted()
    {
        Debug.Log("PazzleCompleted, move to BattlePhase");
        SetPhase(GamePhase.Battle);
    }

    public GamePhase GetCurrentPhase()
    {
        return currentPhase;
    }
}
