using UnityEngine;

namespace CharacterInfo
{
    [CreateAssetMenu(fileName = "AllyInfo", menuName = "CharacterInfo/AllyInfo")]
    public class AllyInfo : ScriptableObject
    {
        [SerializeField] private AttackType          attackType;
        [SerializeField] private CharacterDeployInfo characterDeployInfo;
        [SerializeField] private CharacterState      characterState;
        [SerializeField] private CharacterDirection  characterDirection;
        [SerializeField] private LayerMask           characterLayer;
        [SerializeField] private Collider            characterAttackRange;
        [SerializeField] private Sprite              characterSprite;
        [SerializeField] private string              characterName;
        [SerializeField] private int                 cost;
        [SerializeField] private int                 hp;
        [SerializeField] private int                 attack;
        [SerializeField] private float               attackCoolDown;
        [SerializeField] private int                 defence;
        
        public AttackType AttackType                   { get => attackType; set => attackType = value; }
        public CharacterDeployInfo CharacterDeployInfo { get => characterDeployInfo; set => characterDeployInfo = value; }
        public CharacterState CharacterState           { get => characterState; set => characterState = value; }
        public CharacterDirection CharacterDirection   { get => characterDirection; set => characterDirection = value; }
        public LayerMask CharacterLayer                { get => characterLayer; }
        public Collider CharacterAttackRange           { get => characterAttackRange; set => characterAttackRange = value; }
        public Sprite CharacterSprite                  { get => characterSprite; set => characterSprite = value; }
        public string CharacterName                    { get => characterName; set => characterName = value; }
        public int Cost                                { get => cost; set => cost = value; }
        public int Hp                                  { get => hp; set => hp = value; }
        public int Attack                              { get => attack; set => attack = value; }
        public float AttackCoolDown                    { get => attackCoolDown; set => attackCoolDown = value; }
        public int Defence                             { get => defence; set => defence = value; }
    }
}