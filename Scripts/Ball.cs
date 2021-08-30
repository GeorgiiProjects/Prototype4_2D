using UnityEngine;

public class Ball : MonoBehaviour
{
    // создаем класс Paddle для того чтобы поместить в инспекторе в него префаб Paddle.
    [SerializeField] Paddle paddle;
    // сила полета префаба Ball по оси x.
    [SerializeField] float xPush = 1f;
    // сила полета префаба Ball по оси y.
    [SerializeField] float yPush = 15f;
    // создаем массив класса AudioClip для того чтобы поместить в инспекторе в него различные звуки.
    [SerializeField] AudioClip[] ballSounds;
    // создаем переменную для того чтобы префаб Ball рандомно двигался.
    [SerializeField] float randomFactor = 1f;
    // инициализируем Vector2 для определния расстояния от префаба Ball до префаба Paddle.
    Vector2 paddleToBallDistance;
    // создаем переменную для того чтобы понимать, запущен префаб Ball с префаба Paddle или нет.
    bool hasStarted = false;
    // создаем класс AudioSource для того чтобы проигрывать звуковой эффект.
    AudioSource myAudioSource;
    // создаем класс Rigidbody2D для получения доступа к Rigidbody2D префаба Ball.
    Rigidbody2D myRigidBody2D;

    void Start()
    {
        // вычисляем расстояние от префаба Ball до префаба Paddle, позиция Ball - позиция Paddle.
        paddleToBallDistance = transform.position - paddle.transform.position;
        // получаем доступ к AudioSource префаба Ball через компонент GetComponent.
        myAudioSource = GetComponent<AudioSource>();
        // получаем доступ к Rigidbody2D префаба Ball, через компонент Rigidbody2D GetComponent.
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // если префаб Ball не запущен с префаба Paddle.
        if (!hasStarted)
        {
            // префаб Ball привязан к префабу Paddle.
            LockBallToPaddle();
            // запускаем префаб Ball по клику на левую кнопку мыши.
            LaunchBallOnMouseClick();
        }    
    }

    // создаем метод для запуска мяча по нажатию левой кнопки мыши.
    private void LaunchBallOnMouseClick()
    {
        // если мы нажимаем левую кнопку мыши.
        if (Input.GetMouseButtonDown(0))
        {
            // префаб Ball запускается с префаба Paddle.
            hasStarted = true;
            // получаем доступ к компоненту Rigidbody2D префаба Ball и его скорости(velocity), задаем силу полета Ball.
            myRigidBody2D.velocity = new Vector2(xPush, yPush);      
        }
    }

    // создаем метод для привязки префаба Ball к префабу Paddle.
    private void LockBallToPaddle()
    {
        // получаем значения того где конкретно находится префаб Paddle по осям x и y.
        Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        // заставляем префаб Ball двигаться в тех же координатах что и префаб Paddle.
        transform.position = paddlePos + paddleToBallDistance;
    }

    // создаем метод для воспроизведения звука при соприкосновении мяча с поверхностью.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Vector2 velocity нужен для того, чтобы создать рандомизацию полета префаба Ball по осям x и y, задаем рандомизацию при помощи формулы от 0 до 1.
        Vector2 velocityTweak = new Vector2(Random.Range(0f, randomFactor), Random.Range(0f, randomFactor));
        // если префаб Ball запущен с префаба Paddle.
        if (hasStarted)
        {
            // проигрываем аудио в случайном порядке в нашем случае от 0 до 3, используя массив ballSounds и добавив в него рандомизацию.
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            // проигрываем звуковые эффекты, один раз, используем метод PlayOneShot(), чтобы не было конфликта между аудио.
            myAudioSource.PlayOneShot(clip);
            // получаем доступ к компоненту RigidBody2D, получаем доступ к скорости RigidBody2D, добавляем к скорости рандомное движение Ball.
            myRigidBody2D.velocity += velocityTweak;
        }    
    }
}
