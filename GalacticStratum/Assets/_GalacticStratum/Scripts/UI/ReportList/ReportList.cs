using TMPro;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

public class ReportList : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject reportButtonPrefab;
    [SerializeField] private TextMeshProUGUI reportTitlePrefab;
    [SerializeField] private TextMeshProUGUI reportSubTitlePrefab;
    [SerializeField] private TextMeshProUGUI reportResourceAmountPrefab;
    [SerializeField] private TextMeshProUGUI reportResourcePercentagePrefab;

    [Header("List Properties")]
    [SerializeField] private Button reportListButton;
    [SerializeField] private GameObject reportListContent;
    [SerializeField] private float animationSpeed;

    private int numberOfReports;
    bool isReportListOpen = false;
    private Vector2 newPosition = new(280f, 0);

    private RectTransform reportListPanelTransform;

    //private void OnEnable()
    //{
    //    GlobalEvents.CreateReport += CreateNewReport;
    //}

    private void OnDisable()
    {
        GlobalEvents.CreateReport -= CreateNewReport;
    }

    private void Start()
    {
        reportListPanelTransform = GetComponent<RectTransform>();
        GlobalEvents.CreateReport += CreateNewReport;

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

        GameObject newReport = Instantiate(reportButtonPrefab, reportListContent.transform);
        GameObject reportContent = newReport.GetComponent<ReportButton>().reportContent;

        TextMeshProUGUI newReportTitle = Instantiate(reportTitlePrefab, reportContent.transform);
        newReportTitle.text = $"#{numberOfReports:000} Report";

        TextMeshProUGUI newReportSubtitle = Instantiate(reportSubTitlePrefab, reportContent.transform);
        newReportSubtitle.text = $"{asteroid.Size.Type} Asteroid";

        TextMeshProUGUI newReportResourceAmount = Instantiate(reportResourceAmountPrefab, reportContent.transform);
        newReportResourceAmount.text = $"Resources Amount: {asteroid.ResourcesQuantity}";

        for (int i = 1; i < (int)AsteroidData.ResourceType.Count; i++)
        {
            AsteroidData.ResourceType type = (AsteroidData.ResourceType)i;

            if (!asteroid.CheckIfResourceDepleted(type))
            {
                float percentage = ((float)asteroid.GetResourceAmount(type)/ (float)asteroid.ResourcesQuantity)*100f;

                TextMeshProUGUI newResourcePercentage = Instantiate(reportResourcePercentagePrefab, reportContent.transform);
                newResourcePercentage.text = $"{type}: {percentage:F1}%";
            }
        }
    }
}
