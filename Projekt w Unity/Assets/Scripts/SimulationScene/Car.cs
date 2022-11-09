using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

    private Rigidbody2D rb;
    private List<GameObject> achievedCheckpoints;
    private GameObject[] sensors;
    private float[] input;

    private GameObject topSensor;
    private GameObject leftFirstSensor;
    private GameObject leftSecondSensor;
    private GameObject rightFirstSensor;
    private GameObject rightSecondSensor;
    private float sensorLineLenght = ParametersDto.getCarSensorsLength();

    public bool eliminated = false;
    public bool finishSimulation = false;

    private Vector2 lastPosition;
    private float totalDistanceTravelled;
    private float fitnessValue;

    public NeuralNetwork network;

    private float torqueForce = -150f;
    private float timeRemaining;

    private bool manualSteering = ParametersDto.isManualSteering();
    private LineRenderer lr;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        achievedCheckpoints = new List<GameObject>();
        fitnessValue = 0;
        totalDistanceTravelled = 0;
        lastPosition = transform.position;

        input = new float[5];
        timeRemaining = ParametersDto.getCarLifeSpan();
        initializeSensors();
    }
    void FixedUpdate() {
        calculateFitnessValue();
        if (!eliminated) {
            sensor();
            drive();
            countTotalDistanceTravelled();
        }
    }

    void Update() {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining < 0) {
            eliminated = true;
        }
    }

    public float getTotalDistanceTravelled() {
        return totalDistanceTravelled;
    }

    public float getFitnessValue() {
        return fitnessValue;
    }

    public void setFitnessValue(int value) {
        fitnessValue = value;
    }

    public void calculateFitnessValue() {
        fitnessValue += totalDistanceTravelled;
    }

    public void countTotalDistanceTravelled() {
        totalDistanceTravelled += Vector2.Distance(transform.position, lastPosition);
        lastPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        eliminated = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!achievedCheckpoints.Contains(collision.gameObject)) {
            achievedCheckpoints.Add(collision.gameObject);
            fitnessValue += timeRemaining * 10;
            fitnessValue += (float)fitnessValue * 0.1f;
            timeRemaining = ParametersDto.getCarLifeSpan();

            if (collision.name.Equals("META")) {
                finishSimulation = true;
            }
        }
    }

    private void initializeSensors() {
        topSensor = gameObject.transform.GetChild(0).gameObject;
        leftFirstSensor = gameObject.transform.GetChild(1).gameObject;
        leftSecondSensor = gameObject.transform.GetChild(2).gameObject;
        rightFirstSensor = gameObject.transform.GetChild(3).gameObject;
        rightSecondSensor = gameObject.transform.GetChild(4).gameObject;
        sensors = new GameObject[5];
        sensors[0] = leftFirstSensor;
        sensors[1] = leftSecondSensor;
        sensors[2] = topSensor;
        sensors[3] = rightSecondSensor;
        sensors[4] = rightFirstSensor;
    }

    private void sensor() {
        int detectionLayer = 1 << 9;
        for (int i = 0; i < sensors.Length ; i++) {
            Vector2 genericDirection = Quaternion.Euler(0, 0, i * -45 + 90) * sensors[i].transform.TransformDirection(Vector2.up) * sensorLineLenght;
            Vector2 genericSensorVector = sensors[i].transform.position;
            RaycastHit2D genericHit = Physics2D.Raycast(genericSensorVector, genericDirection, sensorLineLenght, detectionLayer);

            if (genericHit.collider == null) {
                input[i] = sensorLineLenght;

                Debug.DrawRay(genericSensorVector, genericDirection, Color.green);

            } else {
                input[i] = genericHit.distance;
                Debug.DrawRay(genericSensorVector, genericDirection, Color.red);
            }
        }
    }

    private void drive() {
        if (!manualSteering) {
            network.giveDataToNetwork(input);
            float[] output = network.feedForward();
            rb.angularVelocity = output[0] * torqueForce;
            rb.AddForce(transform.up * output[1] * 25f);
        } else {
            rb.angularVelocity = Input.GetAxis("Horizontal") * torqueForce;
            if (Input.GetButton("Accelerate")) {
                rb.AddForce(transform.up * 25f);
            }
        }
    }
}
