using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float rate;
    public float health;
    public float previousHealth;
    public float mana;
    public float previousMana;

    public BarController healthController;
    public BarController manaController;

    // Start is called before the first frame update
    void Start()
    {
        health = rate;
        mana = rate;

        previousHealth = health;
        previousMana = mana;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            health += 0.5f;
            mana += 0.5f;
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            health -= 0.5f;
            mana -= 0.5f;
        }

        if (health >= rate)
        {
            health = rate;
        }
        if (health <= 0)
        {
            health = 0;
        }

        if (mana >= rate)
        {
            mana = rate;
        }
        if (mana <= 0)
        {
            mana = 0;
        }
        DetectHealthChange();
        DetectManaChange();
    }

    public void DetectHealthChange()
    {
        if (health < previousHealth)
        {
            if (health == 0.5)
            {
                healthController.barChosen = 4;
            }
            else if (health != 0)
            {
                healthController.barChosen += 0.5f;
            }
            healthController.Decrease();
            previousHealth = health;
        }
        else if (health > previousHealth)
        {
            if (health == 4 || healthController.barChosen <= -0.5f)
            {
                healthController.barChosen = -0.5f;
            }
            else if(health != 4)
            {
                healthController.barChosen -= 0.5f;
            }
            
            healthController.Increase();
            previousHealth = health;
        }
        else
        {
            previousHealth = health;
        }

    }

    public void DetectManaChange()
    {
        if (mana < previousMana)
        {
            if (mana == 0.5)
            {
                manaController.barChosen = 4;
            }
            else if (mana != 0)
            {
                manaController.barChosen += 0.5f;
            }
            manaController.Decrease();
            previousMana = mana;
        }
        else if (mana > previousMana)
        {
            if (mana == 4 || manaController.barChosen <= -0.5f)
            {
                manaController.barChosen = -0.5f;
            }
            else if (mana != 4)
            {
                manaController.barChosen -= 0.5f;
            }

            manaController.Increase();
            previousMana = mana;
        }
        else
        {
            previousMana = mana;
        }

    }
}
