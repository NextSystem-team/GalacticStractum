using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

public class ReportList : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject reportButtonPrefab;
    [SerializeField] private Text reportTitlePrefab;
    [SerializeField] private Text reportSubTitlePrefab;
    [SerializeField] private Text reportResourceAmountPrefab;
    [SerializeField] private Text reportResourcePercentagePrefab;

    [Header("List Properties")]
    [SerializeField] private Button reportListButton;
    [SerializeField] private GameObject reportContent;
    [SerializeField] private float animationSpeed;

    private int numberOfReports;
    bool isReportListOpen = false;
    private Vector2 newPosition = new(280f, 0);

    private RectTransform reportListPanelTransform;

    private void OnEnable()
    {
        GlobalEvents.CreateReport += CreateNewReport;
    }

    private void OnDisable()
    {
        GlobalEvents.CreateReport -= CreateNewReport;
    }

    private void Start()
    {
        reportListPanelTransform = GetComponent<RectTransform>();

        reportListButton.onClick.AddListener(ToggleReportList);
    }

    private void Update()
    {
        if (reportListPanelTransform.anchoredPosition != newPosition)
        {
            reportListPanelTransform.anchoredPosition = Vector2.Lerp(reportListPanelTransform.anchoredPosition, newPosition, Time.deltaTime * animationSpeed);

            if (Vector2.Distance(reportListPanelTransform.anchoredPosition, newPosition) <= 0.1f)
            {
                reportListPanelTransform.anchoredPosition = newPosition;
            }
        }
    }

    private void ToggleReportList()
    {
        isReportListOpen = !isReportListOpen;

        if (isReportListOpen)
        {
            newPosition = new(0f, 0);
        }
        else
        {
            newPosition = new(280f, 0);
        }
    }

    private void CreateNewReport(AsteroidData asteroid)
    {
        numberOfReports++;

        GameObject newReport = Instantiate(reportButtonPrefab, reportContent.transform);

        Text newReportTitle = Instantiate(reportTitlePrefab, newReport.transform);
        newReportTitle.text = $"#{numberOfReports.ToString("000")} Report";

        Text newReportSubtitle = Instantiate(reportSubTitlePrefab, newReport.transform);
        newReportSubtitle.text = $"{asteroid.Size.Type} Asteroid";

        Text newReportResourceAmount = Instantiate(reportResourceAmountPrefab, newReport.transform);
        newReportResourceAmount.text = $"Resources Amount: {asteroid.ResourcesQuantity}";

        for (int i = 1; i < (int)AsteroidData.ResourceType.Count; i++)
        {
            AsteroidData.ResourceType type = (AsteroidData.ResourceType)i;

            if (!asteroid.CheckIfResourceDepleted(type))
            {
                float percentage = (asteroid.GetResourceQuantity(type)/asteroid.ResourcesQuantity)*100f;

                Text newResourcePercentage = Instantiate(reportResourcePercentagePrefab, newReport.transform);
                newResourcePercentage.text = $"{type}: {percentage:F1}%";
            }
        }
    }
}
