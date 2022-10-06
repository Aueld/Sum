using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace FSV.Scroll
{
    class Cell : FancyCell<ItemData>
    {
        [SerializeField] private Animator animator = default;
        [SerializeField] private TextMeshProUGUI message = default;
        [SerializeField] private Button button = default;

        private float currentPosition = 0;

        private void OnEnable() => UpdatePosition(currentPosition);

        private static class AnimatorHash
        {
            public static readonly int Scroll = Animator.StringToHash("scroll");
        }

        public override void UpdateContent(ItemData itemData)
        {
            message.text = itemData.Message;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => Loading(itemData.Message));
        }

        public override void UpdatePosition(float position)
        {
            currentPosition = position;

            if (animator.isActiveAndEnabled)
            {
                animator.Play(AnimatorHash.Scroll, -1, position);
            }

            animator.speed = 0;
        }

        private void Loading(string scene)
        {
            GameManager.instance.loadScene = scene;
            LoadSceneManager.ViewFrontOn();
        }
    }
}
