using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour {
    private Transform recordsContainer;
    private Transform recordTemplate;

    void Start() {
        var someObject = getDataForRanking();
        showDataOnRanking(someObject);
    }

    private Object getDataForRanking() {
        return new Object();
    }

    private void showDataOnRanking(Object someObject) {
        recordsContainer = GameObject.Find("RecordsContainer").transform;
        recordTemplate = recordsContainer.Find("RecordTemplate");
        recordTemplate.gameObject.SetActive(false);

        for(int position = 0; position < 10; position++) {
            var record = initializeRecord(position);
            //fillRecordWithData(someObject.get(position));
        }
    }

    public Transform initializeRecord(int position) {
        Transform recordTransform = Instantiate(recordTemplate, recordsContainer);
        RectTransform recordRectTransform = recordTransform.GetComponent<RectTransform>();

        float recordCoordinateY = 310 - 50 * position;
        float recordCoordinateX = 0;
        recordRectTransform.anchoredPosition = new Vector2(recordCoordinateX, recordCoordinateY);

        recordTransform.gameObject.SetActive(true);

        return recordTransform;
    }

    private void fillRecordWithData(Object record) {
        //recordTransform.Find("PlaceText").GetComponent<Text>().text;
    }
}
