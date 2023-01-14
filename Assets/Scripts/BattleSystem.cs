using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST  }

public class BattleSystem : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerStats;
    public Transform enemyStats;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;

    public Button MysticButton;
    public Button SilentButton;
    public Button TransientButton;

    // Start is called before the first frame update
    void Start()
    {

        if (PlayerPrefs.GetString("SuperActive") == "mystic")
        {
            MysticButton.gameObject.SetActive(true);
        }
        else if (PlayerPrefs.GetString("SuperActive") == "silent")
        {
            SilentButton.gameObject.SetActive(true);
        }
        else if (PlayerPrefs.GetString("SuperActive") == "transient")
        {
            TransientButton.gameObject.SetActive(true);
        }

        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerStats);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyStats);
        enemyUnit = enemyGO.GetComponent<Unit>();

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack1() {

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage1);

        enemyHUD.SetHP(enemyUnit.currentHP);

        playerUnit.TakeDamage(playerUnit.recoil);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(0.1f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }


    IEnumerator PlayerAttack2()
    {

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage2);

        enemyHUD.SetHP(enemyUnit.currentHP);

        yield return new WaitForSeconds(0.1f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerDebuff1()
    {

        enemyUnit.ApplyDebuff(playerUnit.debuff1);

        enemyHUD.SetHP(enemyUnit.currentHP);

        yield return new WaitForSeconds(0.1f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator PlayerSuper()
    {

        enemyUnit.ApplyDebuff(playerUnit.debuff1);

        enemyHUD.SetHP(enemyUnit.currentHP);

        yield return new WaitForSeconds(0.1f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());

    }

    IEnumerator EnemyTurn()
    {
        bool isDead = playerUnit.TakeDamage(enemyUnit.enemyDamage);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(0.1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(5);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(0.1f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());

    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {

        } else if (state == BattleState.LOST)
        {

        }
    }

    void PlayerTurn()
    {

    }

    public void OnAttack1Button()
    {
        if (state != BattleState.PLAYERTURN)
                return;

        StartCoroutine(PlayerAttack1());
    }

    public void OnAttack2Button()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack2());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }

    public void OnDebuff1Button()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerDebuff1());
    }

    public void OnSuperButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerSuper());
    }

}
