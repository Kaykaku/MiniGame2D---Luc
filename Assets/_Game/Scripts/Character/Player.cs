using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    [System.Serializable]
    public class Skill
    {
        public string name;
        public Sprite image;
        public bool isCoolDown;
        public KeyCode keyCode;
        public Cooldown cooldownUI;
    }

    protected PlayerState currentState;
    protected PlayerDirect currentDirect;
    [SerializeField] protected Image joyStickButton;
    [SerializeField] public List<Weapon> weapons;
    [SerializeField] public List<Skill> skills;
    
    private Vector3 moveDir;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }


    private void Update()
    {
        Control();
    }

    //Set control for character
    //Skill and movement
    private void Control()
    {
        if(isDeath) return;
        if (Input.GetKeyDown(skills[0].keyCode) && !skills[0].isCoolDown)
        {
            StartCoroutine(FireRateUp());
        }
        if (Input.GetKeyDown(skills[1].keyCode) && !skills[1].isCoolDown)
        {
            StartCoroutine(SkillBoom());
        }
        if (Input.GetKeyDown(skills[2].keyCode) && !skills[2].isCoolDown)
        {
            StartCoroutine(SkillSpeedUp());
        }
        if (Input.GetMouseButton(0))
        {
            moveDir = joyStickButton.rectTransform.anchoredPosition.normalized;
        }
        else
        {
            float horizonltalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            moveDir = new Vector3(horizonltalInput, verticalInput, 0);
            joyStickButton.rectTransform.anchoredPosition = moveDir*70;
        }

        PlayerDirect newDirect;
        PlayerState newState;
        if (moveDir == Vector3.zero)
        {
            ChangeAnim(currentDirect, PlayerState.Idle);
            return;
        }
        else
        {
            newState = PlayerState.Walk;
            if (moveSpeed >=4.5)
            {
                newState = PlayerState.Run;
            }
            transform.position = Vector3.Lerp(transform.position, transform.position + moveDir, Time.deltaTime * moveSpeed);
        }

        if(Mathf.Abs(moveDir.x) < Mathf.Abs(moveDir.y))
        {
            if (moveDir.y <= 0)
            {
                newDirect = PlayerDirect.Down;
            }
            else
            {
                newDirect = PlayerDirect.Top;
            }
        }
        else
        {
            if (moveDir.x <= 0)
            {
                newDirect = PlayerDirect.Left;
            }
            else
            {
                newDirect = PlayerDirect.Right;
            }
        }
        ChangeAnim(newDirect, newState);
    }

    private void ChangeAnim(PlayerDirect direct,PlayerState action)
    {
        if ((action == currentState && direct == currentDirect) || isDeath) return;
        currentState = action;
        currentDirect = direct;
        animator.SetInteger("Direct",Mathf.Clamp((int)direct,0,2));
        animator.SetTrigger(currentState.ToString());
        if (direct == PlayerDirect.Right)
        {
            model.transform.rotation = Quaternion.Euler(0,180,0);
        }
        else
        {
            model.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void AddBonus(BonusType bonus)
    {
        Time.timeScale = 1;
        switch (bonus)
        {
            case BonusType.ATK:
                damage += 1;
                break;
            case BonusType.HP:
                GetComponent<Health>().AddMaxHP(5);
                break;
            case BonusType.Speed:
                moveSpeed += 1;
                break;
            case BonusType.Range:
                attackRange += 1;
                break;
            case BonusType.Amor:
                GetComponent<Health>().AddMaxAmor(0.5f);
                break;
            case BonusType.Weapon:
                foreach (Weapon weapon in weapons)
                {
                    if(!weapon.gameObject.activeInHierarchy)
                    {
                        weapon.gameObject.SetActive(true);
                        break;
                    }
                }
                break;
            case BonusType.HitRate:
                fireRate -= 0.1f;
                break;
        }
        MainUI.instance.LoadText();
    }
    public override void Death()
    {
        base.Death();
        isDeath = true;
        Spawner.instance.DestroyAll();
        GameManager.ChangeState(GameState.End);
        StartCoroutine(OnDeath());
    }

    protected override IEnumerator OnDeath()
    {
        yield return new WaitForSeconds(1f);
        UIManager.Ins.OpenUI<Lose>();
    }

    public IEnumerator SkillBoom()
    {
        UIManager.Ins.OpenUI<SkillUI>().SetInfo(skills[1].image, skills[1].name);
        GameObject boom = Pool2.instance.SpawnFromPool("Boom");
        boom?.transform.SetPositionAndRotation(transform.position,Quaternion.identity);
        skills[1].cooldownUI.OnInit(22);
        skills[1].isCoolDown = true;
        yield return new WaitForSeconds(22f);
        skills[1].isCoolDown = false;
    }

    public IEnumerator SkillSpeedUp()
    {
        UIManager.Ins.OpenUI<SkillUI>().SetInfo(skills[2].image, skills[2].name);
        moveSpeed = moveSpeed * 1.5f;
        MainUI.instance.LoadText();
        skills[2].cooldownUI.OnInit(15);
        skills[2].isCoolDown = true;
        yield return new WaitForSeconds(3f);
        moveSpeed = moveSpeed * 2 / 3;
        MainUI.instance.LoadText();
        yield return new WaitForSeconds(12f);
        skills[2].isCoolDown = false;
    }

    public IEnumerator FireRateUp()
    {
        UIManager.Ins.OpenUI<SkillUI>().SetInfo(skills[0].image, skills[0].name);
        fireRate = fireRate * 2/3f;
        MainUI.instance.LoadText();
        skills[0].cooldownUI.OnInit(20);
        skills[0].isCoolDown = true;
        yield return new WaitForSeconds(5f);
        fireRate = fireRate * 3/2 ;
        MainUI.instance.LoadText();
        yield return new WaitForSeconds(15f);
        skills[0].isCoolDown = false;
    }
}
