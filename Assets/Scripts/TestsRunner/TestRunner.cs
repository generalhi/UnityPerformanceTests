using Components.UI.DevConsole;
using TestsRunner.Tests;
using UnityEngine;
using UnityEngine.UI;

namespace TestsRunner
{
    public class TestRunner : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        private readonly Fill_Array_For_Managed _fillArrayForManaged = new();
        private readonly Fill_Array_For_Unmanaged _fillArrayForUnmanaged = new();

        private readonly Fill_NativeArray_For_Managed _fillNativeArrayForManaged = new();
        private readonly Fill_NativeArray_For_Unmanaged _fillNativeArrayForUnmanaged = new();

        private readonly Fill_NativeArray_JobSimple_Managed _fillNativeArrayJobSimpleManaged = new();
        private readonly Fill_NativeArray_JobSimple_Unmanaged _fillNativeArrayJobSimpleUnmanaged = new();

        private readonly Fill_NativeArray_JobParallelFor_Managed _fillNativeArrayJobParallelForManaged = new();
        private readonly Fill_NativeArray_JobParallelFor_Unmanaged _fillNativeArrayJobParallelForUnmanaged = new();

        private readonly Copy_Array_For_Managed _copyArrayForManaged = new();
        private readonly Copy_Array_For_Unmanaged _copyArrayForUnmanaged = new();

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
            DevConsole.WriteLine($"---Run. Count - {count}");

            DevConsole.WriteLine($"--- Fill");

            _fillArrayForManaged.Start(count);
            _fillArrayForUnmanaged.Start(count);

            _fillNativeArrayForManaged.Start(count);
            _fillNativeArrayForUnmanaged.Start(count);

            _fillNativeArrayJobSimpleManaged.Start(count);
            _fillNativeArrayJobSimpleUnmanaged.Start(count);

            _fillNativeArrayJobParallelForManaged.Start(count);
            _fillNativeArrayJobParallelForUnmanaged.Start(count);

            DevConsole.WriteLine($"--- Copy");
            _copyArrayForManaged.Start(count);
            _copyArrayForUnmanaged.Start(count);
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
