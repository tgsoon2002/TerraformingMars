using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{

    #region Data Member
    int ranking, generation;

    // Production related
    [Space]
    int currentProductionSelected;
    int[] currentProduction;
    public Text[] currentProductionLabel;
    public Text rankingLabel;
    [Space]
    // Resource related
    int[] currentResource;
    public Text[] currentResourceLabel;
    [Space]
    // Temporary Resource related
    int[] temporaryResourceUsed;
    public Text[] temporaryResourceUsedLabel;
    public Text resourceUsedSummaryLabel;



    public enum ResourceType
    {
        MEGACREDIT,
        STEEL,
        TITANIUM,
        PLANT,
        ENERGY,
        HEAT
    }
    #endregion

    #region Getter Setter
    public int Ranking { get { return ranking; } set { ranking = value; rankingLabel.text = "Rank : " + ranking.ToString(); } }
    #endregion

    #region Built in
    // Use this for initialization
    void Start()
    {
        currentProduction = new int[6];

        temporaryResourceUsed = new int[6];
        currentResource = new int[6];

        for (int i = 0; i < 6; i++)
        {
            UpdateResource(i, 0);
            UpdateProduction(i, 1);
            temporaryResourceUsed[i] = 0;
        }
        UpdateProduction(0, -1);
        UpdateResource(0, 42);
        Ranking = 20;
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region Public Method
    /// <summary>
    /// EndGeneration 
    /// </summary
    public void EndGeneration()
    {
        // transfer the rest of enegy to heat before get new enegy
        currentResource[5] += currentResource[4];
        currentResource[4] = 0;
        // get resource from ptoduction
        UpdateResource(0, (Ranking + currentProduction[0]));
        UpdateResource(1, currentProduction[1]);
        UpdateResource(2, currentProduction[2]);
        UpdateResource(3, currentProduction[3]);
        UpdateResource(4, currentProduction[4]);
        UpdateResource(5, currentProduction[5]);

    }


    #region ------------------- Resource Related -------------------

    public void _IncreaseResourceCounting(int res)
    {
        temporaryResourceUsed[res]++;
        UpdateTemporaryResouceSummary(res);
    }

    public void _DecreaseResourceCounting(int res)
    {
        temporaryResourceUsed[res]--;
        UpdateTemporaryResouceSummary(res);
    }

    /// <summary>
    /// Update Temporary Resource Summary
    /// Show the resource will be consumed.
    /// </summary>
    /// <param name="res"></param>
    private void UpdateTemporaryResouceSummary(int res)
    {
        temporaryResourceUsedLabel[res].text = temporaryResourceUsed[res].ToString();
        string result = "";
        int tempCreditValue = 0;
        if (temporaryResourceUsed[0] != 0)
        {
            tempCreditValue += temporaryResourceUsed[0];
        }
        if (temporaryResourceUsed[1] != 0)
        {
            tempCreditValue += 2 * temporaryResourceUsed[1];
        }
        if (temporaryResourceUsed[2] != 0)
        {
            tempCreditValue += 3 * temporaryResourceUsed[2];
        }
        if (temporaryResourceUsed[0] != 0 || temporaryResourceUsed[1] != 0 || temporaryResourceUsed[2] != 0)
        {
            result = "You will spend " + tempCreditValue.ToString() + " credit (" + temporaryResourceUsed[0].ToString() + " MC / " + temporaryResourceUsed[1].ToString() + " Steel / " + temporaryResourceUsed[2].ToString() + " Titanium) \n";
        }
        result += " " + temporaryResourceUsed[3].ToString() + " Plant, " + temporaryResourceUsed[4].ToString() + " Energy, and " + temporaryResourceUsed[5].ToString() + " Heat Cube";

        resourceUsedSummaryLabel.text = result;
    }

    /// <summary>
    /// _OpenSpendResourceCanvas 
    /// </summary
    public void _OpenSpendResourceCanvas()
    {
        transform.Find("SpendResource_Canvas").gameObject.SetActive(true);

    }

    /// <summary>
    /// _ConfirmUseResource
    /// Check if there is enough resource. If there is enough, reduce the resource. And close the panel.
    /// Else open the warning.
    /// </summary
    public void _ConfirmUseResource()
    {
        bool overSpend = false;
        for (int i = 0; i < 6; i++)
        {
            if (-temporaryResourceUsed[i] > currentResource[i])
            {
                overSpend = true;
            }
        }

        if (!overSpend)
        {
            for (int i = 0; i < 6; i++)
            {
                UpdateResource(i, temporaryResourceUsed[i]);
                temporaryResourceUsed[i] = 0;
                temporaryResourceUsedLabel[i].text = "0";
            }
        }
        else
        {
            transform.Find("Warning").gameObject.SetActive(true);
        }
        transform.Find("SpendResource_Canvas").gameObject.SetActive(false);
    }

    /// <summary>
    /// _CancelUseResource
    /// </summary
    public void _CancelUseResource()
    {
        for (int i = 0; i < 6; i++)
        {
            temporaryResourceUsed[i] = 0;
            temporaryResourceUsedLabel[i].text = "0";
        }
        transform.Find("SpendResource_Canvas").gameObject.SetActive(false);
    }


    /// <summary>
    /// UpdateResource 
    /// </summary
    void UpdateResource(int res, int quantity)
    {
        currentResource[res] += quantity;
        switch (res)
        {
            case 0:
                currentResourceLabel[res].text = "Mega Credit : " + currentResource[res].ToString();
                break;
            case 1:
                currentResourceLabel[res].text = "Steel Cube : " + currentResource[res].ToString();
                break;
            case 2:
                currentResourceLabel[res].text = "Titanium : " + currentResource[res].ToString();
                break;
            case 3:
                currentResourceLabel[res].text = "Plant Cube : " + currentResource[res].ToString();
                break;
            case 4:
                currentResourceLabel[res].text = "Energy  : " + currentResource[res].ToString();
                break;
            case 5:
                currentResourceLabel[res].text = "Heat Cube : " + currentResource[res].ToString();
                break;
            default:
                break;
        }

    }


    #endregion

    #region ------------------- Production Related -------------------
    /// <summary>
    /// _OpenProductionChange 
    /// </summary
    public void _OpenProductionChange(int type)
    {
        transform.GetChild(3).gameObject.SetActive(true);
        currentProductionSelected = type;
        ResourceType selectedType = (ResourceType)type;
        transform.GetChild(3).GetChild(0).GetChild(2).GetComponent<Text>().text = selectedType.ToString();
        transform.GetChild(3).GetChild(0).GetChild(3).GetComponent<Text>().text = currentProduction[type].ToString();
    }

    /// <summary>
    /// _ChangeValue 
    /// </summary
    public void _ChangeProductionValue(bool isIncreasing)
    {
        if (isIncreasing) { currentProduction[currentProductionSelected]++; }
        else
        {
            if (currentProduction[currentProductionSelected] == 0)
            {
                transform.Find("Warning").gameObject.SetActive(true);
            }
            else
            {
                currentProduction[currentProductionSelected]--;
            }
        }
        transform.GetChild(3).GetChild(0).GetChild(3).GetComponent<Text>().text = currentProduction[currentProductionSelected].ToString();
    }

    /// <summary>
    /// _CloseProductionChangeCanvas 
    /// </summary
    public void _CloseProductionChangeCanvas()
    {
        UpdateProduction(currentProductionSelected, 0);

        transform.GetChild(3).gameObject.SetActive(false);
    }

    /// <summary>
    /// UpdateProduction 
    /// </summary
    public void UpdateProduction(int res, int ammount)
    {
        currentProduction[res] += ammount;
        currentProductionLabel[res].text = "Production : " + currentProduction[res].ToString();
    }


    #endregion

    #region ------------------- Ranking Related -------------------

    /// <summary>
    /// _UpdateRanking 
    /// </summary
    public void _UpdateRanking()
    {
        Ranking++;
    }

    /// <summary>
    /// _UpdateRanking 
    /// </summary
    public void _UpdateRanking(bool isIncreasing)
    {
        if (isIncreasing) { Ranking++; }
        else { Ranking--; }

    }




    #endregion


    /// <summary>
    /// _CloseWarning 
    /// </summary
    public void _CloseWarning()
    {
        transform.Find("Warning").gameObject.SetActive(false);
    }








    #endregion

    #region Private method

    #endregion

    #region Protected method

    #endregion





}
