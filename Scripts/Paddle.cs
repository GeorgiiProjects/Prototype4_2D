using UnityEngine;

public class Paddle : MonoBehaviour
{
    // количество юнитов, которые можно разместить на экране 16.
    [SerializeField] float screenWidthInUnits = 16f;
    // префаб Paddle может двигаться по оси х от значения 1.
    [SerializeField] float xMin = 1f;
    // префаб Paddle может двигаться по оси х до значения 16.8.
    [SerializeField] float xMax = 16.8f;
    // Создаем класс GameSession для последующего получения к нему доступа.
    GameSession gameSession;
    // Создаем класс Ball для последующего получения к нему доступа.
    Ball ball;

    void Start()
    {
        // получаем доступ к классу/скрипту GameSession
        gameSession = FindObjectOfType<GameSession>();
        // получаем доступ к классу/скрипту Ball
        ball = FindObjectOfType<Ball>();
    }

    void Update()
    {
        // Вызываем метод передвижения префаба Paddle каждый фрейм.
        PaddleMovement();
    }

    // создаем метод для передвижения префаба Paddle.
    private void PaddleMovement()
    {
        // по оси y префаб paddle будет находиться в координатах указанных в инспекторе.
        // по оси x префаб paddle будет находиться в координатах нахождения мыши.
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        // Paddle может передвигаться по оси x автоматически, либо при помощи мыши в координатах от 1 до 16.8
        paddlePos.x = Mathf.Clamp(GetXPos(), xMin, xMax);
        // Paddle появляется в заданных координатах, путем получения данных координат из объекта/префаба Paddle.
        // при помощи компонента transform.position префаб Paddle может передвигаться.
        transform.position = paddlePos;
    }

    // создаем метод для определения позиции префаба Paddle по оси x.
    private float GetXPos()
    {
        // если метод IsAutoPlayEnabled() авто-игры активен
        if (gameSession.IsAutoPlayEnabled())
        {
            // Заставляем префаб Paddle двигаться за префабом Ball по оси x. 
            return ball.transform.position.x;
        }
        // иначе
        else
        {
            // возвращаем доступ к позиции мыши по оси x, делим её на ширину экрана и умножаем на 16 юнитов.
            // префаб Paddle двигается куда указывает мышь по оси x.
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }
}
