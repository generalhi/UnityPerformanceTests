using Components.UI.DevConsole;
using TestsRunner.Tests;
using UnityEngine;
using UnityEngine.UI;

namespace TestsRunner
{
    public class TestRunner : MonoBehaviour
    {
        public const int MethodNameSpace = 50;

        [SerializeField]
        private Button _button;

        private readonly Fill_Array_For_Managed _fillArrayForManaged = new();
        private readonly Fill_Array_For_Unmanaged _fillArrayForUnmanaged = new();
        private readonly Fill_Array_For_Unmanaged_Burst _fillArrayForUnmanagedBurst = new();

        private readonly Fill_NativeArray_For_Managed _fillNativeArrayForManaged = new();
        private readonly Fill_NativeArray_For_Unmanaged _fillNativeArrayForUnmanaged = new();
        private readonly Fill_NativeArray_For_Unmanaged_Burst _fillNativeArrayForUnmanagedBurst = new();

        private readonly Fill_NativeArray_JobSimple_Managed _fillNativeArrayJobSimpleManaged = new();
        private readonly Fill_NativeArray_JobSimple_Unmanaged _fillNativeArrayJobSimpleUnmanaged = new();
        private readonly Fill_NativeArray_JobSimple_Unmanaged_Burst _fillNativeArrayJobSimpleUnmanagedBurst = new();

        private readonly Fill_NativeArray_JobParallelFor_Managed _fillNativeArrayJobParallelForManaged = new();
        private readonly Fill_NativeArray_JobParallelFor_Unmanaged _fillNativeArrayJobParallelForUnmanaged = new();

        private readonly Fill_NativeArray_JobParallelFor_Unmanaged_Burst _fillNativeArrayJobParallelForUnmanagedBurst =
            new();

        private readonly Copy_Array_For_Managed _copyArrayForManaged = new();
        private readonly Copy_Array_For_Unmanaged _copyArrayForUnmanaged = new();
        private readonly Copy_Array_For_Unmanaged_Burst _copyArrayForUnmanagedBurst = new();

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
            DevConsole.WriteLine(string.Empty);

            _fillArrayForManaged.Start(count);
            _fillArrayForUnmanaged.Start(count);
            _fillArrayForUnmanagedBurst.Start(count);
            DevConsole.WriteLine(string.Empty);

            _fillNativeArrayForManaged.Start(count);
            _fillNativeArrayForUnmanaged.Start(count);
            _fillNativeArrayForUnmanagedBurst.Start(count);
            DevConsole.WriteLine(string.Empty);

            _fillNativeArrayJobSimpleManaged.Start(count);
            _fillNativeArrayJobSimpleUnmanaged.Start(count);
            _fillNativeArrayJobSimpleUnmanagedBurst.Start(count);
            DevConsole.WriteLine(string.Empty);

            _fillNativeArrayJobParallelForManaged.Start(count);
            _fillNativeArrayJobParallelForUnmanaged.Start(count);
            _fillNativeArrayJobParallelForUnmanagedBurst.Start(count);
            DevConsole.WriteLine(string.Empty);

            DevConsole.WriteLine($"--- Copy");
            DevConsole.WriteLine(string.Empty);

            _copyArrayForManaged.Start(count);
            _copyArrayForUnmanaged.Start(count);
            _copyArrayForUnmanagedBurst.Start(count);
            DevConsole.WriteLine(string.Empty);

            DevConsole.WriteLine(string.Empty);
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
