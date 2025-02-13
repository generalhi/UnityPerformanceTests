using Components.UI.DevConsole;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class TestRunner : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

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
            //DevConsole.WriteLine("Run");

            var count = 1000000;
            DevConsole.WriteLine($"Run. Count - {count}");

            _arrayFill_JobSimple_Test.Start(count);
            _arrayFill_JobParallelFor_Test.Start(count);
        }
    }
}
