using UnityEngine;

public class Level : MonoBehaviour
{
    // создаем переменную для отслеживания количества ломающихся блоков в инспекторе.
    [SerializeField] int breakableBlocks;
    // создаем класс SceneLoader для последующего получения к нему доступа.
    SceneLoader sceneLoader;

    private void Start()
    {
        // получаем доступ к классу/скрипту SceneLoader
        sceneLoader = FindObjectOfType<SceneLoader>();      
    }

    // создаем публичный метод для подсчета ломающихся блоков, метод будет вызываться в скрипте Block.
    public void CountBreakableBlocks()
    {
        // Подсчитываем количество уничтожаемых блоков.
        breakableBlocks++;
    }

    // создаем публичный метод для отсчета блоков до нуля, метод будет вызываться в скрипте Block.
    public void BlockDestroyed()
    {
        // уменьшаем количество уничтожаемых блоков на один.
        breakableBlocks--;
        // если количество блоков <= 0
        if (breakableBlocks <= 0)
        {
            // запускаем следующую сцену.
            sceneLoader.LoadNextScene();
        }
    }
}
