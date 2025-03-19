using UnityEngine;

namespace CharacterInfo
{
    [CreateAssetMenu(fileName = "AllyInfo", menuName = "CharacterInfo/AllyInfo")]
    public class AllyInfo : ScriptableObject
    {
        [SerializeField] private AttackType attackType;
        [SerializeField] private LayerMask characterLayer;
        [SerializeField] private Sprite characterSprite;
        [SerializeField] private string characterName;
        [SerializeField] private int cost;
        [SerializeField] private int hp;
        [SerializeField] private int attack;
        [SerializeField] private int defence;
        
        public AttackType AttackType    { get => attackType; set => attackType = value; }
        public LayerMask CharacterLayer { get => characterLayer; }
        public Sprite CharacterSprite   { get => characterSprite; set => characterSprite = value; }
        public string CharacterName     { get => characterName; set => characterName = value; }
        public int Cost                 { get => cost; set => cost = value; }
        public int Hp                   { get => hp; set => hp = value; }
        public int Attack               { get => attack; set => attack = value; }
        public int Defence              { get => defence; set => defence = value; }
    }
}