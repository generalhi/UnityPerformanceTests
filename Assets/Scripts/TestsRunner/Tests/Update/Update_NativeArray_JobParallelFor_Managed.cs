using System.Diagnostics;
using Components.UI.DevConsole;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace TestsRunner.Tests.Copy
{
    public class Update_NativeArray_JobParallelFor_Managed
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

        private struct JobParallelFor : IJobParallelFor
        {
            public NativeArray<Data> Data;
            public float DeltaTime;

            public void Execute(int i)
            {
                var d = Data[i];

                if (d.health > 0 && d.health < d.maxHealth)
                {
                    d.health += d.healthRegenRate * DeltaTime;
                    if (d.health > d.maxHealth)
                    {
                        d.health = d.maxHealth;
                    }

                    Data[i] = d;
                }

                if (d.stamina > 0 && d.stamina < d.maxStamina)
                {
                    d.stamina += d.staminaRegenRate * DeltaTime;
                    if (d.stamina > d.maxStamina)
                    {
                        d.stamina = d.maxStamina;
                    }

                    Data[i] = d;
                }
            }
        }

        public void Start(int count)
        {
            var type = "NativeArray<Struct>()";
            var body = "Calc";

            var data = new NativeArray<Data>(count, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
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

            var job = new JobParallelFor {Data = data, DeltaTime = Time.deltaTime};

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var handle = job.Schedule(count, 64);
            handle.Complete();

            stopwatch.Stop();
            DevConsole.WriteLine(
                $"{GetType().Name,TestRunner.MethodNameLength} | " +
                $"{type,TestRunner.TypeLength} | " +
                $"{body,TestRunner.BodyLength} | " +
                $"{stopwatch.ElapsedTicks} ticks");

            data.Dispose();
        }
    }
}
