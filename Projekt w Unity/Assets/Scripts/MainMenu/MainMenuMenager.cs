using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMenager : MonoBehaviour {
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
        Application.Quit();
    }

    private void setDefaultParameters() {
        if (!ParametersDto.isParamsInitialized()) {
            ParametersDto.setPopulationSize(90);
            ParametersDto.setMutationChance(0.5f);
            ParametersDto.setMutationStrength(0.2f);
        }
    }
}
