using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ToolData", menuName = "Scriptable Objects/Tools/Tool Brain/Drill")]
public class DrillObject : _ToolObject
{
    public override bool UseAim => true;
    public override float AimRadius => 15f;
    [SerializeField] private GameObject drillPrefab;
    private Drill currentDrill;

    public override void OnUse(Vector2 targetPosition, Player player)
    {
        if (currentDrill != null) return;

        GameObject drillInstance = Instantiate(drillPrefab, player.transform.position, Quaternion.identity);
        Drill drill = drillInstance.GetComponent<Drill>();
        currentDrill = drill;
        drill.startRopePoint = player;
        drill.targetPosition = targetPosition;

        drillInstance.transform.position = player.transform.position;

        drill.canMove = true;
    }
}
