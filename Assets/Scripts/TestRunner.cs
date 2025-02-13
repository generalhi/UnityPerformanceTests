using System;
using Components.UI.DevConsole;
using TestsRunner.Tests;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class TestRunner : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        private readonly ArrayFill_Managed_Test _arrayFill_Managed_Test = new();
        private readonly ArrayFill_Unmanaged_Test _arrayFill_Unmanaged_Test = new();
        private readonly ArrayFill_JobSimple_Test _arrayFill_JobSimple_Test = new();
        private readonly ArrayFill_JobParallelFor_Test _arrayFill_JobParallelFor_Test = new();

        private void Start()
        {
            if (_button != null)
            {
                _button.onClick.AddListener(Run);
            }
            else
            {
                Debug.Log("Button is null");
            }
        }

        private void Run()
        {
            var count = 1000000;
            DevConsole.WriteLine($"Run. Count - {count}");

            _arrayFill_Managed_Test.Start(count);
            _arrayFill_Unmanaged_Test.Start(count);
            _arrayFill_JobSimple_Test.Start(count);
            _arrayFill_JobParallelFor_Test.Start(count);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
