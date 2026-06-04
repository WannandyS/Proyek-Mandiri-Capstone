using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryBack : MonoBehaviour
{
    public GameObject gameOverPanel;

    private void Start()
    {
        gameOverPanel.transform.localPosition = new Vector3(0, -1500f, 0);
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Back()
    {
        SceneManager.LoadScene("Home");
    }
}
