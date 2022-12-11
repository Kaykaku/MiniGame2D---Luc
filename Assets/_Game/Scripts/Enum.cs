using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
     Idle,Walk,Run
}
public enum PlayerDirect
{
    Down, Top,  Left, Right
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