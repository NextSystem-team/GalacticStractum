using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolShopCategoriesDropdown : MonoBehaviour
{
    [SerializeField] private BuyToolList buyToolList;

    private TMP_Dropdown dropdown;

    private void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();    

        SetDropdown();

        dropdown.onValueChanged.AddListener(OnOptionChange);
    }

    private void SetDropdown()
    {
        dropdown.ClearOptions();

        string[] options = Enum.GetNames(typeof(ToolData.ToolType));
        List<string> optionsList = new(options);

        dropdown.AddOptions(optionsList);
    }

    private void OnOptionChange(int index)
    {
        ToolData.ToolType toolCategory = (ToolData.ToolType)index;

        buyToolList.DisplayToolsByCategory(toolCategory);
    }
}
