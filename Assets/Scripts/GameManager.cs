using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Configuration du Score")]
    public int scoreDéchets = 0;
    public TMP_Text textScore;

    [Header("Objets à faire apparaître (Bouteilles, etc.)")]
    public GameObject[] objetsDechets;
    public float tempsEntreApparitions = 4f;

    [Header("Effets de Malus (Caméra)")]
    public Camera mainCam;

    void Start()
    {
        // Met à jour le texte du score au début
        MettreAJourScore();

        // Lance l'apparition aléatoire des déchets toutes les X secondes
        InvokeRepeating("SpawnDechet", 2f, tempsEntreApparitions);
    }

    void SpawnDechet()
    {
        // SÉCURITÉ CRITIQUE : Si la liste est vide ou qu'aucun objet n'est glissé dedans, 
        // on arrête proprement la fonction pour éviter l'erreur rouge "UnassignedReferenceException"
        if (objetsDechets == null || objetsDechets.Length == 0 || objetsDechets[0] == null)
        {
            Debug.LogWarning("⚠️ Attention : Aucun Prefab n'est assigné dans la liste 'Objets Dechets' du GameManager !");
            return;
        }

        // Choisit un déchet au hasard dans la liste (bouteille, canette...)
        int randomIndex = Random.Range(0, objetsDechets.Length);

        // Génère une position aléatoire sur le sol de TA ville (Axes X et Z)
        // Ajuste les valeurs (-20, 20) selon la taille de ta route
        Vector3 randomPos = new Vector3(
            Random.Range(-20f, 20f),
            0.5f,                    // Hauteur pour que l'objet flotte juste un peu au-dessus du sol
            Random.Range(-20f, 20f)
        );

        Instantiate(objetsDechets[randomIndex], randomPos, Quaternion.identity);
    }

    public void AjouterPoint()
    {
        scoreDéchets++;
        MettreAJourScore();
    }

    void MettreAJourScore()
    {
        if (textScore != null)
        {
            textScore.SetText("Déchets ramassés : " + scoreDéchets);
        }
    }

    // Le malus quand on touche une mauvaise boule !
    public void DeclencherMalus()
    {
        StartCoroutine(MalusCameraRotation());
    }

    IEnumerator MalusCameraRotation()
    {
        if (mainCam != null)
        {
            mainCam.transform.Rotate(0, 0, 180); // Retourne la caméra à l'envers !
            yield return new WaitForSeconds(4f); // Attend 4 secondes
            mainCam.transform.Rotate(0, 0, -180); // Remet la caméra à l'endroit
        }
    }
}
