using System.Diagnostics;
using Components.UI.DevConsole;
using Unity.Mathematics;
using UnityEngine;

namespace TestsRunner.Tests.Update
{
    public class Update_Array_For_Unmanaged
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

        public unsafe void Start(int count)
        {
            var type = "Struct[]";
            var body = "Calc ptr";

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

            fixed (Data* ptr = &data[0])
            {
                for (var i = 0; i < count; i++)
                {
                    if (ptr[i].health > 0 && ptr[i].health < ptr[i].maxHealth)
                    {
                        ptr[i].health += ptr[i].healthRegenRate * Time.deltaTime;
                        if (ptr[i].health > ptr[i].maxHealth)
                        {
                            ptr[i].health = ptr[i].maxHealth;
                        }
                    }

                    if (ptr[i].stamina > 0 && ptr[i].stamina < ptr[i].maxStamina)
                    {
                        ptr[i].stamina += ptr[i].staminaRegenRate * Time.deltaTime;
                        if (ptr[i].stamina > ptr[i].maxStamina)
                        {
                            ptr[i].stamina = ptr[i].maxStamina;
                        }
                    }
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
