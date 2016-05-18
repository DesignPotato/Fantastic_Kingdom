using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Stats
{
    public abstract class BaseStats : MonoBehaviour
    {
        public float health = 0.0f;
        public float armour = 0.0f;
        public float attackPower = 0.0f;
        public float heavyAttackPower = 0.0f;

        public virtual void ApplyDamage(float unitsOfDamage)
        {
            health -= unitsOfDamage;//Math.Max(unitsOfDamage + armour, 0);
            Debug.Log(health);
        }

        public virtual void Recover(float unitsOfHealth)
        {
            health += unitsOfHealth;
        }
    }
}
