using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    IdleDown,
    IdleTop,
    IdleLeft,
    IdleRight,
    WalkDown,
    WalkTop,
    WalkLeft,
    WalkRight,
}

public enum EnemyState { 
    Run,Hit,Death,Attack
}

public enum GameState
{
    OnInit, Playing, End , Pause
}

public enum EnemyType
{
    Rat=1,Bat , Crab,Pebble,Skull,Slime
}

public enum BonusType
{
    ATK,HP,Speed,Range,Amor,Weapon,HitRate
}