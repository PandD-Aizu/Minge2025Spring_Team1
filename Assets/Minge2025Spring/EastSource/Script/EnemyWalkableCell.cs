using System;
using UnityEngine;

public class EnemyWalkableCell : MonoBehaviour
{
    [Header("Property")] [SerializeField] private bool isSpawnPointCell = false;
    
    public bool isWalkable = true;
    public LayerMask observeTargetLayerMask;

    private float rayDistance = 0.8f;
    private Ray observeTargetRay;
    private EnemyWalkableCell upCell = null;
    private EnemyWalkableCell downCell = null;
    private EnemyWalkableCell leftCell = null;
    private EnemyWalkableCell rightCell = null;
    private EnemyWalkableCell nextCell = null;
    private EnemyWalkableCell previousCell = null;

    //変数のアクセサ
    public bool IsSpawnPointCell{get{return isSpawnPointCell;}}
    public EnemyWalkableCell UpCell {get{return upCell;}}
    public EnemyWalkableCell DownCell {get{return downCell;}}
    public EnemyWalkableCell LeftCell {get{return leftCell;}}
    public EnemyWalkableCell RightCell {get{return rightCell;}}
    public EnemyWalkableCell NextCell {get{return nextCell;} set{nextCell = value;}}
    public EnemyWalkableCell PreviousCell {get{return previousCell;} set{previousCell = value;}}
    
    private void Start()
    {
        observeTargetRay = new Ray(this.transform.position, this.transform.up);
        InitSurroundingCells();
    }

    private void InitSurroundingCells()
    {
        foreach (GameObject enemyWalkabaleCell in GameManager.Instance.EnemyWalkableCells)
        {
            if (Mathf.Approximately(this.gameObject.transform.position.z - enemyWalkabaleCell.transform.position.z, 1) 
                && Mathf.Approximately(this.gameObject.transform.position.x - enemyWalkabaleCell.transform.position.x, 0))
            {
                if (enemyWalkabaleCell.gameObject.TryGetComponent<EnemyWalkableCell>(out EnemyWalkableCell upEnemyWalkableCell))
                {
                    upCell = upEnemyWalkableCell;
                }
            }
            
            if (Mathf.Approximately(this.gameObject.transform.position.z - enemyWalkabaleCell.transform.position.z, -1) 
                && Mathf.Approximately(this.gameObject.transform.position.x - enemyWalkabaleCell.transform.position.x, 0))
            {
                if (enemyWalkabaleCell.gameObject.TryGetComponent<EnemyWalkableCell>(out EnemyWalkableCell downEnemyWalkableCell))
                {
                    downCell = downEnemyWalkableCell;
                }
            }
            
            if (Mathf.Approximately(this.gameObject.transform.position.x - enemyWalkabaleCell.transform.position.x, -1) 
                && Mathf.Approximately(this.gameObject.transform.position.z - enemyWalkabaleCell.transform.position.z, 0))
            {
                if (enemyWalkabaleCell.gameObject.TryGetComponent<EnemyWalkableCell>(out EnemyWalkableCell leftEnemyWalkableCell))
                {
                    leftCell = leftEnemyWalkableCell;
                }
            }
            
            if (Mathf.Approximately(this.gameObject.transform.position.x - enemyWalkabaleCell.transform.position.x, 1) 
                && Mathf.Approximately(this.gameObject.transform.position.z - enemyWalkabaleCell.transform.position.z, 0))
            {
                if (enemyWalkabaleCell.gameObject.TryGetComponent<EnemyWalkableCell>(out EnemyWalkableCell rightEnemyWalkableCell))
                {
                    rightCell = rightEnemyWalkableCell;
                }
            }
        }
        
        if (upCell != null)
        {
            Debug.Log(this.gameObject.name + ": upside is " + upCell.gameObject.name);
        }
        else
        {
            Debug.Log(this.gameObject.name + ": upside is null.");
        }

        if (downCell != null)
        {
            Debug.Log(this.gameObject.name + ": downside is " + downCell.gameObject.name);
        }
        else
        {
            Debug.Log(this.gameObject.name + ": downside is null.");
        }

        if (rightCell != null)
        {
            Debug.Log(this.gameObject.name + ": rightside is " + rightCell.gameObject.name);
        }
        else
        {
            Debug.Log(this.gameObject.name + ": rightside is null.");
        }

        if (leftCell)
        {
            Debug.Log(this.gameObject.name + ": leftside is " + leftCell.gameObject.name);
        }
        else
        {
            Debug.Log(this.gameObject.name + ": leftside is null.");
        }
    }
}
