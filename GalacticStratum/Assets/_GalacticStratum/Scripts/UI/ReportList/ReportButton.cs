using UnityEngine;
using UnityEngine.UI;

public class ReportButton : MonoBehaviour
{
    public GameObject reportContent;
    [SerializeField] private float animationSpeed;

    private readonly float closedContentHeight = 45f;
    private readonly float openContentHeight = 165f;

    private bool isContentOpen = false;
    private float newHeight = 45f;
    private Button interactButton;
    private LayoutElement layoutElement;

    private void Start()
    {
        interactButton = GetComponent<Button>();
        layoutElement = GetComponent<LayoutElement>();

        interactButton.onClick.AddListener(ToggleContent);

        layoutElement.preferredHeight = closedContentHeight;
    }

    private void Update()
    {
        if (layoutElement.preferredHeight != newHeight)
        {
            layoutElement.preferredHeight = Mathf.Lerp(layoutElement.preferredHeight, newHeight, animationSpeed * Time.deltaTime);

            if (Mathf.Abs(layoutElement.preferredHeight - newHeight) <= 0.1f)
            {
                layoutElement.preferredHeight = newHeight;
            }

            if (transform.parent != null && transform.parent.TryGetComponent<RectTransform>(out var parentRect))
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(parentRect);
            }
        }
    }

    private void ToggleContent()
    {
        isContentOpen = !isContentOpen;
        float targetHeight = isContentOpen ? openContentHeight : closedContentHeight;
        newHeight = targetHeight;
    }


}
