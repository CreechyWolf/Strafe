using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;

    public int damage1;
    public int damage2;
    public int recoil;

    //setting debuff numbers
    public int debuff1;

    public int enemyDamage;

    public int maxHP;
    public int currentHP;

    public bool debuff;
    public int debuffNumber;

    public bool TakeDamage(int dmg)
    {
        //if debuff active then extra dmg, using debuff numbers

        if (debuff == true)
        {
            dmg += debuffNumber;
            currentHP -= dmg; 

            debuffNumber = 0;
            debuff = false; 
        }
        else

        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public void ApplyDebuff(int buffSelect)
    {
        //turn debuff on
        debuff = true;
        //add the debuff numbers
        debuffNumber += buffSelect;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }
}
