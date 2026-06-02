using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "ToolData", menuName = "Scriptable Objects/Tools/Tool Brain/Probe")]
public class ProbeObject : _ToolObject
{
    public override bool UseAim => false;
    public override float AimRadius => 0f;

    [SerializeField] private GameObject probePrefab;
    [SerializeField] private float maxProbeRange;

    private GameObject currentProbe;

    public override void OnUse(Vector2 targetPosition, Player player)
    {
        if (currentProbe != null) return;

        GameObject probe = Instantiate(probePrefab, player.transform.position, Quaternion.identity);
        ProbePulse probePulse = probe.GetComponent<ProbePulse>();
        probePulse.maxRadius = maxProbeRange;
        currentProbe = probe;
    }

}
