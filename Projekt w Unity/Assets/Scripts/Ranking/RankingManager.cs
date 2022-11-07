using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour {
    private Transform recordsContainer;
    private Transform recordTemplate;

    void Start() {
        var someObject = getDataForRanking();
        showDataOnRanking(someObject);
    }

    public void returnToMainMenu() {
        SceneManager.LoadScene("MainMenuScene");
    }

    private List<RankingData> getDataForRanking() {
        return new FileManager().deserializeRecordsFromFile();
    }

    private void showDataOnRanking(List<RankingData> rankingData) {
        recordsContainer = GameObject.Find("RecordsContainer").transform;
        recordTemplate = recordsContainer.Find("RecordTemplate");
        recordTemplate.gameObject.SetActive(false);

        for (int position = 0; position < rankingData.Count; position++) {
            var recordData = rankingData[position];
            var recordTransform = initializeRecord(position);
            fillRecordWithData(recordTransform, recordData, position);
        }
    }

    private Transform initializeRecord(int position) {
        Transform recordTransform = Instantiate(recordTemplate, recordsContainer);
        RectTransform recordRectTransform = recordTransform.GetComponent<RectTransform>();

        float recordCoordinateY = 310 - 50 * position;
        float recordCoordinateX = 0;
        recordRectTransform.anchoredPosition = new Vector2(recordCoordinateX, recordCoordinateY);

        recordTransform.gameObject.SetActive(true);

        return recordTransform;
    }

    private void fillRecordWithData(Transform recordTransform, RankingData data, int position) {
        recordTransform.Find("PlaceText").GetComponent<Text>().text = position.ToString();
        recordTransform.Find("GenerationText").GetComponent<Text>().text = data.getGenerationNumber().ToString();
        recordTransform.Find("DurationText").GetComponent<Text>().text = data.getDuration().ToString();
        recordTransform.Find("PopulationSizeText").GetComponent<Text>().text = data.getPopulationSize().ToString();
        recordTransform.Find("MutationChanceText").GetComponent<Text>().text = data.getMutationChance().ToString();
        recordTransform.Find("MutationStrengthText").GetComponent<Text>().text = data.getMutationStrength().ToString();
    }
}
