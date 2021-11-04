using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMenuHandler : MonoBehaviour
{
    public List<GameObject> LeadreBoxes = new List<GameObject>();
    public Texture2D CursorTexture;
    void Awake()
    {
        DataHolder.SetUIMenuHandler(this);
        
    }

    private void Start()
    {
        UpdateLeaderBoard();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLeaderBoard()
    {
        
        List<Player> Players = new List<Player>();
        Players = Saver.LoadPlayers();
        Saver.SortPlayersByScore(ref Players);
        for(int i = 0;i < LeadreBoxes.Count; i++)
        {
            Text rankText = LeadreBoxes[i].transform.GetChild(0).GetComponent<Text>();
            Text nameText = LeadreBoxes[i].transform.GetChild(1).GetComponent<Text>();
            Text scoreText = LeadreBoxes[i].transform.GetChild(2).GetComponent<Text>();
            if(i <= Players.Count - 1)
            {
                rankText.text = (i + 1).ToString();
                nameText.text = Players[i].NickName;
                scoreText.text =Players[i].Score.ToString();
            }
            else
            {
                rankText.text = " ";
                nameText.text = " ";
                scoreText.text = " ";
            }
        }
    }

 

    
}
