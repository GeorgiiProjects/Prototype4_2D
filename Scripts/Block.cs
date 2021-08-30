using UnityEngine;

public class Block : MonoBehaviour
{
    // создаем класс AudioClip для того чтобы поместить в инспекторе в него аудио.
    [SerializeField] AudioClip breakSound;
    // создаем GameObject для того чтобы поместить в инспекторе в него эффект(Particle).
    [SerializeField] GameObject blockSparklesVFX;
    // создаем переменную для подсчета количества ударов по блоку (дебаг для разработчика).
    [SerializeField] int timesHit;
    // Создаем массив спрайтов, для отображения различной анимации при нанесении повреждения блоку, массив настраиваем вручную в инспекторе.
    [SerializeField] Sprite[] hitSprites; 
    // Создаем класс Level для последующего получения к нему доступа.
    Level level;
    // Создаем класс GameSession для последующего получения к нему доступа.
    GameSession gameSession;

    private void Start()
    {
        // вызываем метод для подсчета уничтожаемых блоков.
        CountBreakableBlocks();
        // получаем доступ к скрипту/классу GameSession.
        gameSession = FindObjectOfType<GameSession>();
    }

    // создаем метод для подсчета уничтожаемых блоков.
    private void CountBreakableBlocks()
    {
        // получаем доступ к скрипту/классу Level.
        level = FindObjectOfType<Level>();
        // если у блока тэг Breakable
        if (tag == "Breakable")
        {
            // вызываем метод CountBlocks() из класса/скрипта Level.
            level.CountBreakableBlocks();
        }
    }

    // создаем метод для взаимодействия при соприкосновении коллайдера префаба Ball с коллайдером Block.
    private void OnCollisionEnter2D(Collision2D other)
    {
        // если у блока таг Breakable
        if (tag == "Breakable")
        {
            // вызываем метод для подсчета количества ударов по блоку и его уничтожения.
            HandleHit();
        }
    }

    // создаем метод для подсчета количества ударов по блоку.
    private void HandleHit()
    {
        // увеличиваем количество ударов на 1.
        timesHit++;
        // максимальное количество ударов по блоку это длина массива смены спрайтов + 1 (так как массив начинается с 0);
        int maxHits = hitSprites.Length + 1;
        // если количество ударов >= максимальному количеству ударов.
        if (timesHit >= maxHits)
        {
            // вызываем метод для уничтожения префаба Block, проигрывания звука и прибавления очков.
            DestroyBlock();
        }
        // иначе
        else
        {
            // показываем следующую анимацию повреждения префаба Block.
            ShowNextHitSprite();
        }
    }

    // создаем метод для показа следующей анимации повреждения блока.
    private void ShowNextHitSprite()
    {
        // создаем переменную spriteIndex для того чтобы отсчет хитов начинался с нулевого массива, в зависимости от количества ударов анимация будет меняться. 
        int spriteIndex = timesHit - 1;
        // Если значение массива спрайтов повреждения (картинок) в блоке не ноль.
        if(hitSprites[spriteIndex] != null)
        {
            // получаем доступ к компоненту SpriteRenderer в префабе Block, получаем доступ к спрайту, 
            // присваиваем ему различную анимацию в зависимости от количества хитов.
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        // иначе
        else
        {
            // выводим ошибку о пропаже спрайта блока из массива и его имени.
            Debug.LogError("Block sprite is missing from array " + gameObject.name);
        }  
    }

    // создаем метод для уничтожения префаба Block
    private void DestroyBlock()
    {
        // когда разрушаемый префаб Block уничтожен проигрываем звуковой эффект и прибавляем очки.
        BlockDestroyPlaySFX();
        // уничтожаем префаб Block при соприкосновении с префабом Ball.
        Destroy(gameObject);
        // вызываем метод уничтожения префаба Block из класса Level.
        level.BlockDestroyed();
        // Вызываем Particle, когда уничтожается префаб Block в его координатах.
        TriggerSparklesVFX();
    }
    
    // создаем метод для прибавления очков и проигрывания звука.
    private void BlockDestroyPlaySFX()
    {
        // Создаем AudioSource, воспроизводим аудио в координатах нахождения камеры.
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        // вызываем метод прибавления очков после уничтожения блока.
        gameSession.AddToScore();
    }

    // создаем метод для того чтобы создать копии эффекта(Particle);
    private void TriggerSparklesVFX()
    {
        // создаем копии эффекта (Particles), используя координаты того куда он будет помещен, в нашем случае префаба Block, поворот оставляем по умолчанию.
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        // уничтожаем Particles в иерархии через 1 секунду, после создания клона.
        Destroy(sparkles, 1f);        
    }
}
