using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMenager : MonoBehaviour
{
    void Start()
    {
        
    }

    public void startSimulation() {
        SceneManager.LoadScene("SimulationScene");
    }

    public void setSimulationParameters() {

    }

    public void terminateProgram() {
        Application.Quit();
    }
}
