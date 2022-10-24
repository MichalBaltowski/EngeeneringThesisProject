using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMenager : MonoBehaviour
{
    void Start() {
      
    }

    private void Update() {
       
    }

    public void startSimulation() {
        SceneManager.LoadScene("SimulationScene");
    }

    public void setParameters() {
        SceneManager.LoadScene("SetParametersScene");
    }

    public void terminateProgram() {
        Application.Quit();
    }
}
