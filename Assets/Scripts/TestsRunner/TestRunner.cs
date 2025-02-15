using Components.UI.DevConsole;
using TestsRunner.Tests.Copy;
using TestsRunner.Tests.Fill;
using TestsRunner.Tests.Update;
using UnityEngine;
using UnityEngine.UI;

namespace TestsRunner
{
    public class TestRunner : MonoBehaviour
    {
        public const int MethodNameLength = 50;
        public const int TypeLength = 21;
        public const int BodyLength = 22;

        [SerializeField]
        private Button _buttonClear;

        [SerializeField]
        private Button _buttonRun;

        private readonly Fill_Array_For_Managed _fillArrayForManaged = new();
        private readonly Fill_Array_For_Unmanaged _fillArrayForUnmanaged = new();
        private readonly Fill_Array_For_Unmanaged_Burst _fillArrayForUnmanagedBurst = new();
        private readonly Fill_Array_While_Unmanaged_Burst _fillArrayWhileUnmanagedBurst = new();

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

        private readonly Copy_NativeArray_For_Managed _copyNativeArrayForManaged = new();
        private readonly Copy_NativeArray_For_Unmanaged _copyNativeArrayForUnmanaged = new();
        private readonly Copy_NativeArray_For_Unmanaged_Burst _copyNativeArrayForUnmanagedBurst = new();

        private readonly Copy_NativeArray_JobParallelFor_Managed _copyNativeArrayJobParallelForManaged = new();
        private readonly Copy_NativeArray_JobParallelFor_Unmanaged _copyNativeArrayJobParallelForUnmanaged = new();

        private readonly Copy_NativeArray_JobParallelFor_Unmanaged_Burst _copyNativeArrayJobParallelForUnmanagedBurst =
            new();

        private readonly Copy_Array_Managed _copyArrayManaged = new();
        private readonly Copy_Array_Managed_Burst _copyArrayManagedBurst = new();
        private readonly Copy_NativeArray_Unmanaged _copyNativeArrayUnmanaged = new();
        private readonly Copy_NativeArray_Unmanaged_Burst _copyNativeArrayUnmanagedBurst = new();

        private readonly Update_Array_For_Managed _updateArrayForManaged = new();
        private readonly Update_Array_For_Unmanaged _updateArrayForUnmanaged = new();
        private readonly Update_Array_For_Unmanaged_Burst _updateArrayForUnmanagedBurst = new();

        private readonly Update_NativeArray_JobParallelFor_Managed _updateNativeArrayJobParallelForManaged = new();
        private readonly Update_NativeArray_JobParallelFor_Unmanaged _updateNativeArrayJobParallelForUnmanaged = new();

        private readonly Update_NativeArray_JobParallelFor_Unmanaged_Burst
            _updateNativeArrayJobParallelForUnmanagedBurst = new();

        private void Start()
        {
            if (_buttonClear != null)
            {
                _buttonClear.onClick.AddListener(Clear);
            }
            else
            {
                Debug.Log("Button Clear is null");
            }

            if (_buttonRun != null)
            {
                _buttonRun.onClick.AddListener(Run);
            }
            else
            {
                Debug.Log("Button Run is null");
            }
        }

        private void Clear()
        {
            DevConsole.Clear();
        }

        private void Run()
        {
            var count = 10_000_000;
            DevConsole.WriteLine($"---Run. Count - {count}");

            var methodName = "Method Name";
            var type = "Type";
            var body = "Body";
            var time = "Time";
            DevConsole.WriteLine(
                $"{methodName,MethodNameLength} | " +
                $"{type,TypeLength} | " +
                $"{body,BodyLength} | " +
                $"{time}");

            DevConsole.WriteLine($"--- Fill");
            DevConsole.WriteLine(string.Empty);

            _fillArrayForManaged.Start(count);
            _fillArrayForUnmanaged.Start(count);
            _fillArrayForUnmanagedBurst.Start(count);
            DevConsole.WriteLine(string.Empty);

            _fillArrayWhileUnmanagedBurst.Start(count);
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

            _copyNativeArrayForManaged.Start(count);
            _copyNativeArrayForUnmanaged.Start(count);
            _copyNativeArrayForUnmanagedBurst.Start(count);
            DevConsole.WriteLine(string.Empty);

            _copyNativeArrayJobParallelForManaged.Start(count);
            _copyNativeArrayJobParallelForUnmanaged.Start(count);
            _copyNativeArrayJobParallelForUnmanagedBurst.Start(count);
            DevConsole.WriteLine(string.Empty);

            DevConsole.WriteLine($"--- Copy Array");
            _copyArrayManaged.Start(count);
            _copyArrayManagedBurst.Start(count);
            _copyNativeArrayUnmanaged.Start(count);
            _copyNativeArrayUnmanagedBurst.Start(count);
            DevConsole.WriteLine(string.Empty);

            DevConsole.WriteLine($"--- Update");
            _updateArrayForManaged.Start(count);
            _updateArrayForUnmanaged.Start(count);
            _updateArrayForUnmanagedBurst.Start(count);
            DevConsole.WriteLine(string.Empty);
            _updateNativeArrayJobParallelForManaged.Start(count);
            _updateNativeArrayJobParallelForUnmanaged.Start(count);
            _updateNativeArrayJobParallelForUnmanagedBurst.Start(count);
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
