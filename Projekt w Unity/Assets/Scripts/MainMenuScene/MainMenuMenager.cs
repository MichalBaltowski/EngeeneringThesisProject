using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMenager : MonoBehaviour
{
    void Start() {
        setDefaultParameters();
    }

    public void startSimulation() {
        SceneManager.LoadScene("SimulationScene");
    }

    public void setParameters() {
        SceneManager.LoadScene("SetParametersScene");
    }

    public void openRanking() {
        SceneManager.LoadScene("RankingScene");
    }

    public void terminateProgram() {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    private void setDefaultParameters() {
        if (!ParametersDto.isParamsInitialized()) {
            ParametersDto.setDefault();
        }
    }
}
