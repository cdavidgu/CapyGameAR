using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;


public class GameOverAlertController : MonoBehaviour
{
    [SerializeField] GameObject TilePositionPrefab;
    [SerializeField] Transform ContainerPositionTransform;
    [SerializeField] Button RestartButton;
    [SerializeField] Button ExitButton;
    
    TextMeshProUGUI[] scoreTexts = new TextMeshProUGUI[] {null,null,null,null,null};
    private void Start() {
        PopulateLeadBoard(); 
        RestartButton.onClick.AddListener(RestartGame);   
        ExitButton.onClick.AddListener(ExitGame);
    }
    
    void PopulateLeadBoard() {
        for (int i = 0; i < AppController.SharedInstance.leaderPoints.Length; i++) {
            GameObject tmp = Instantiate(TilePositionPrefab, ContainerPositionTransform);
            tmp.transform.Find("LeadPositionText").gameObject.GetComponent<TextMeshProUGUI>().text = (i+1).ToString();
            scoreTexts[i] = tmp.transform.Find("ScoreText").gameObject.GetComponent<TextMeshProUGUI>();
            // leaderBoard[i].text = leaderPoints[i].ToString();
        }
    }

    void ExitGame() {
        AppController.SharedInstance.ExitGame();
    }

    void RestartGame() {
        AppController.SharedInstance.RestartGame();
    }

    public void UpdateScoreDisplay()
    {
        for (int i = 0; i < scoreTexts.Length && i <  AppController.SharedInstance.leaderPoints.Length; i++)
        {
            scoreTexts[i].text =  AppController.SharedInstance.leaderPoints[i].ToString();
        }
    }

}