Sequence mySequence;

    void Start()
    {
        mySequence = DOTween.Sequence()
        .SetAutoKill(false) //추가
        .OnStart(() => {
            transform.localScale = Vector3.zero;
            GetComponent<CanvasGroup>().alpha = 0;
        })
        .Append(transform.DOScale(1, 1).SetEase(Ease.OutBounce))
        .Join(GetComponent<CanvasGroup>().DOFade(1, 1))
        .SetDelay(0.5f);
    }

    void OnEnable () {
        mySequence.Restart();
    }
