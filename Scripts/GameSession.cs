using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    // создаем переменную для управлением скоростью игры.
    // [Range(0.1f, 5f)] позволяет контролировать скорость игры прямо в инспекторе от 0.1 до 5.
    [Range(0.1f, 5f)] [SerializeField] float gameSpeed = 1f;
    // переменная для определения количества очков от уничтоженного блока.
    [SerializeField] int pointsForBlockDestroyed = 5;
    // создаем переменную для подсчета очков.
    [SerializeField] int currentScore = 0;
    // создаем класс TextMeshProUGUI, чтобы он был виден в инспекторе и помещаем в него Score Text.
    [SerializeField] TextMeshProUGUI scoreText;
    // создаем переменную для авто-игры.
    [SerializeField] bool isAutoPlayEnabled;

    // создаем метод для подсчета количества GameSession, перед началом метода Start
    private void Awake()
    {
        // Ищем количество объектов/префабов GameSession при помощи массива.
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        // если количество префабов GameSession > 1
        if (gameStatusCount > 1)
        {
            // лишний префаб GameSession перестает быть активным на начальных фреймах и FindObjectsOfType не ищет его 
            // иначе количество очков в следующей сцене не будет дополняться.
            gameObject.SetActive(false);
            // уничтожаем лишний префаб GameSession при загрузке следующей сцены.
            Destroy(gameObject);
        }
        else
            // иначе при загрузке следующей сцены нужный префаб GameSession не уничтожается.
            DontDestroyOnLoad(gameObject);      
    }

    private void Start()
    {
        // получаем доступ к текстовому полю score text в префабе Game Session - Canvas - Score Text
        // и присваиваем ему значение current score конвертированное в строку при помощи метода ToString(), со старта игры отображается 0 очков.
        scoreText.text = currentScore.ToString();
    }

    void Update()
    {
        // передаем значение скорости игры 1f в Time.timeScale для движения времени аналогичному реальному времени.
        Time.timeScale = gameSpeed;
    }

    // создаем публичный метод для прибавления очков, метод будет вызываться в скрипте Block.
    public void AddToScore()
    {
        // прибавляем к текущим очкам очки за уничтожения блока.
        currentScore += pointsForBlockDestroyed;
        // получаем доступ к текстовому полю score text в префабе Game Session - Canvas - Score Text
        // и присваиваем ему значение current score конвертированное в строку при помощи метода ToString(), счет обновляется, начиная от 0.
        scoreText.text = currentScore.ToString();
    }

    // создаем публичный метод для сброса очков, метод будет вызываться в скрипте SceneLoader.
    public void ResetGame()
    {
        // уничтожаем префаб Game Session и очки сбрасываются на 0.
        Destroy(gameObject);
    }

    // создаем публичный метод для активации авто игры, метод будет вызываться в скрипте Paddle.
    public bool IsAutoPlayEnabled()
    {
        // происходит авто игра если isAutoPlayEnabled == true в скрипте Paddle.
        return isAutoPlayEnabled;
    }
}
