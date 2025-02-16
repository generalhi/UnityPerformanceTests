using System.Diagnostics;
using Components.UI.DevConsole;
using UnityEngine;

namespace TestsRunner.Tests.Update
{
    public class Update_Array_For_Managed
    {
        private struct Data
        {
            public float health;
            public float maxHealth;
            public float healthRegenRate;
            public float stamina;
            public float maxStamina;
            public float staminaRegenRate;
        }

        public void Start(int count)
        {
            var type = "Struct[]";
            var body = "Calc";

            var data = new Data[count];

            for (var i = 0; i < count; i++)
            {
                data[i] = new Data
                {
                    health = 100f,
                    maxHealth = 100f,
                    healthRegenRate = 1f,
                    stamina = 100f,
                    maxStamina = 100f,
                    staminaRegenRate = 1f,
                };
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (var i = 0; i < count; i++)
            {
                var d = data[i];

                if (d.health > 0 && d.health < d.maxHealth)
                {
                    d.health += d.healthRegenRate * Time.deltaTime;
                    if (d.health > d.maxHealth)
                    {
                        d.health = d.maxHealth;
                    }

                    data[i] = d;
                }

                if (d.stamina > 0 && d.stamina < d.maxStamina)
                {
                    d.stamina += d.staminaRegenRate * Time.deltaTime;
                    if (d.stamina > d.maxStamina)
                    {
                        d.stamina = d.maxStamina;
                    }

                    data[i] = d;
                }
            }

            stopwatch.Stop();
            DevConsole.WriteLine(
                $"{GetType().Name,TestRunner.MethodNameLength} | " +
                $"{type,TestRunner.TypeLength} | " +
                $"{body,TestRunner.BodyLength} | " +
                $"{stopwatch.ElapsedTicks} ticks");
        }
    }
}
