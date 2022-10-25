using UnityEngine;

public class CameraScript : MonoBehaviour {
    public Transform targetObject;
    private Vector3 initalOffset;
    private Vector3 cameraPosition;
    private Car bestCarToFallow;

    void Start() {
        initalOffset = transform.position - GameObject.FindGameObjectWithTag("Spawn").transform.position;
    }

    void FixedUpdate() {
       /* cameraPosition = bestCarToFallow.transform.position + initalOffset;
        transform.position = cameraPosition;*/
    }

    public void setBestCar(Car bestCar) {
        this.bestCarToFallow = bestCar;
    }

    public void updateBestCar(Car bestCar) {
        if (bestCarToFallow != bestCar) {
            this.bestCarToFallow = bestCar;
        }
    }
}