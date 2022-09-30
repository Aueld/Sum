using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FSV.Scroll
{
    class ScrollBasic : MonoBehaviour
    {
        [SerializeField] ScrollView scrollView = default;

        private void Start()
        {
            var items = Enumerable.Range(0, 20)
                .Select(i => new ItemData($"Gyro"))
                .ToArray();

            List<ItemData> itemArray = new List<ItemData>();

            itemArray.Add(new ItemData($"Gyro"));
            itemArray.Add(new ItemData($"DropBall"));
            itemArray.Add(new ItemData($"LeftRight"));
            itemArray.Add(new ItemData($"LineDraw"));

            scrollView.UpdateData(itemArray);
        }
    }
}
