using System.Diagnostics;
using Components.UI.DevConsole;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;

namespace TestsRunner.Tests.Copy
{
    public class Update_NativeArray_JobParallelFor_Unmanaged_Burst
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

        [BurstCompile]
        private unsafe struct JobParallelFor : IJobParallelFor
        {
            [NativeDisableUnsafePtrRestriction]
            public Data* Ptr;
            public float DeltaTime;

            public void Execute(int i)
            {
                if (Ptr[i].health > 0 && Ptr[i].health < Ptr[i].maxHealth)
                {
                    Ptr[i].health += Ptr[i].healthRegenRate * DeltaTime;
                    if (Ptr[i].health > Ptr[i].maxHealth)
                    {
                        Ptr[i].health = Ptr[i].maxHealth;
                    }
                }

                if (Ptr[i].stamina > 0 && Ptr[i].stamina < Ptr[i].maxStamina)
                {
                    Ptr[i].stamina += Ptr[i].staminaRegenRate * DeltaTime;
                    if (Ptr[i].stamina > Ptr[i].maxStamina)
                    {
                        Ptr[i].stamina = Ptr[i].maxStamina;
                    }
                }
            }
        }

        public unsafe void Start(int count)
        {
            var type = "NativeArray<Struct>()";
            var body = "Calc ptr";

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

            var job = new JobParallelFor
            {
                Ptr = (Data*) data.GetUnsafePtr(),
                DeltaTime = Time.deltaTime
            };

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
