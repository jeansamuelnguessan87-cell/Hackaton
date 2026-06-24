using UnityEngine;

public class DechetItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 1. On vérifie si c'est le Joueur OU si c'est la Sphère/Balle qui touche le déchet
        if (other.CompareTag("Player") || other.CompareTag("Ball") || other.gameObject.name.Contains("Sphere"))
        {
            // On cherche le GameManager pour ajouter un point
            GameManager manager = FindObjectOfType<GameManager>();

            if (manager != null)
            {
                manager.AjouterPoint();
            }
            else
            {
                Debug.LogError("Le GameManager n'a pas été trouvé dans la scène !");
            }

            // On détruit le déchet ramassé
            Destroy(gameObject);
        }
    }
}
