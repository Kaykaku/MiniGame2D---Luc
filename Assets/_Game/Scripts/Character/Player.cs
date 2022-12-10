using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : Character
{
    protected int currentAnim;
    [SerializeField] public List<Weapon> weapons;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }


    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizonltalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        int newAnim;

        if (horizonltalInput == 0 && verticalInput == 0)
        {
            newAnim = 0;
        }
        else
        {
            newAnim = 3;
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(horizonltalInput, verticalInput, 0), Time.deltaTime * moveSpeed);
        }

        if (Mathf.Abs(verticalInput) >= Mathf.Abs(horizonltalInput))
        {
            if (verticalInput > 0)
            {
                newAnim += 0;
            }
            else
            {
                newAnim += 1;
            }
        }
        else
        {
            if (horizonltalInput >= 0)
            {
                newAnim += 2;
            }
            else
            {
                newAnim += 3;
            }
        }
        ChangeAnim(newAnim);
    }

    private void ChangeAnim(int animName)
    {
        if (animName == currentAnim) return;
        currentAnim = animName;
        animator.SetInteger("Action", currentAnim);
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
                fireRate += 0.5f;
                break;
            case BonusType.HitRate:
                fireRate += 0.5f;
                break;
        }
        MainUI.instance.LoadText();
    }

}
