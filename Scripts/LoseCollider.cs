using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    // создаем метод OnTriggerEnter2D для, того, чтобы запустить проигрыш в игре.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // когда коллайдер префаба Ball соприкасается с коллайдером префаба Lose Collider, запускается сцена Game Over.
        SceneManager.LoadScene("Game Over");
    }
}
