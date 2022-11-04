using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    private Transform recordsContainer;
    private Transform recordTemplate;

    void Start() {
        recordsContainer = transform.Find("RecordsContainer");
        recordTemplate = recordsContainer.Find("RecordTemplate");

        recordTemplate.gameObject.SetActive(false);

        for (int i = 0; i < 10; i++) {
            Transform recordTransform = Instantiate(recordTemplate, recordsContainer);
            RectTransform recordRectTransform = recordTransform.GetComponent<RectTransform>();
            recordRectTransform.anchoredPosition = new Vector2(0, -30f * i);
            recordTransform.gameObject.SetActive(true);
        }
        

    }

    public void click() {

    }
}
