using System;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle
{
    // サンプルのパズル
    // データを管理するクラス
    [Serializable]
    public class SamplePuzzleModel
    {
        [Header("ボタン")]
        [SerializeField] private Button cookieButton; // クッキーボタン
        
        /* getter と setter */
        public Button CookieButton { get => cookieButton; set => cookieButton = value; }
    }
}