using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Создаем класс GameSession для последующего получения к нему доступа.
    GameSession gameSession;

    private void Start()
    {
        // получаем доступ к классу/скрипту GameSession
        gameSession = FindObjectOfType<GameSession>();
    }

    // Создаем публичный метод для загрузки следующей сцены, метод будет вызываться в скрипте Level.
    public void LoadNextScene()
    {
        // Получаем доступ к сцене которая активна т.е. на которой находится Player.
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // Переходим с активной сцены на следующую.
        SceneManager.LoadScene(currentSceneIndex + 1);       
    }

    // создаем метод для загрузки начального экрана.
    private void LoadStartScene()
    {
        // Загружаем начальный экран, или пишем 0 за место "Start Menu"
        SceneManager.LoadScene("Start Menu");
        // запускаем метод ResetGame() из класса/скрипта GameSession для обнуления очков на начальной сцене.
        gameSession.ResetGame();
    }

    // Создаем метод для выхода из игры.
    private void QuitGame()
    {
        // Выходим из игры
        Application.Quit();
    }
}
