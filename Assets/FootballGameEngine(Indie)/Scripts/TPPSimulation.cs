using Assets.FootballGameEngine_Indie.Scripts.Entities;
using Patterns.Singleton;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TPPSimulation : Singleton<TPPSimulation>
{

    public Slider playerSpeed; 
    public TMP_Dropdown teamDropdownButton;
    
    public GameObject Team1Parent, Team2Parent;
    [SerializeField] GameObject[] Team1Control,Team2Control;
    public Button saveButton;

    public GameObject TPP_Panel;
    
    void Start()
    {
        playerSpeed.minValue = 1f;
        playerSpeed.maxValue = 10f;

        saveButton.onClick.AddListener(ApplySettings);
        
        int childCount1 =  Team1Parent.transform.childCount;
        Team1Control = new GameObject[childCount1];
        for (int i = 0; i < childCount1; i++)
        {
            Team1Control[i] = Team1Parent.transform.GetChild(i).gameObject;
        }
        
        int childCount =  Team1Parent.transform.childCount;
        Team2Control = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            Team2Control[i] = Team2Parent.transform.GetChild(i).gameObject;
        }

    }


    void ApplySettings()
    {
        float speedValue = playerSpeed.value;
        int teamIndex = teamDropdownButton.value;        
       

        if (teamIndex == 0)
        {
            for (int i = 0; i < Team1Control.Length - 1; i++)
            {
                Team1Control[i].GetComponent<Player>().IsUserControlled = true;
                Team1Control[i].GetComponent<Player>().IsTeamInControl = true;
            }

        }
        else
        {
            for (int i = 0; i < Team1Control.Length - 1; i++)
            {
                Team2Control[i].GetComponent<Player>().IsUserControlled = true;
                Team2Control[i].GetComponent<Player>().IsTeamInControl = true;
            }

            
        }

        Debug.Log($"Applied speed {speedValue} to Team {(teamIndex + 1)}");
    }

    public void TurnOnTPP_Panel()
    {
       
        TPP_Panel.SetActive(true);
    }

    public void TurnOffTPP_Panel()
    {
        
        TPP_Panel.SetActive(false);
    }
    
}
