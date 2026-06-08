using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ToolData", menuName = "Scriptable Objects/Tools/Tool Brain/Drill")]
public class DrillObject : _ToolObject
{
    public override bool UseAim => true;
    public override float AimRadius => 15f;
    [SerializeField] private GameObject drillPrefab;
    private List<Drill> currentDrills = new();

    private void OnEnable()
    {
        currentDrills.Clear();
    }

    public override bool OnUse(Vector2 targetPosition, Player player)
    {
        currentDrills.RemoveAll(drill => drill == null);

        if (currentDrills.Count >= 3) return false;

        GameObject drillInstance = Instantiate(drillPrefab, player.transform.position, Quaternion.identity);
        Drill drill = drillInstance.GetComponent<Drill>();
        currentDrills.Add(drill);
        drill.startRopePoint = player;
        drill.targetPosition = targetPosition;

        drillInstance.transform.position = player.transform.position;

        drill.canMove = true;
        AudioManager.Instance.PlaySFX("DrillLaunch");
        return true;
    }
}
