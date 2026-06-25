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
    public GameObject mainCam;

    void Start()
    {
        MettreAJourScore();
        InvokeRepeating("SpawnDechet", 2f, tempsEntreApparitions);
    }

    void SpawnDechet()
    {
        if (objetsDechets == null || objetsDechets.Length == 0 || objetsDechets[0] == null)
        {
            Debug.LogWarning("⚠️ Attention : Aucun Prefab n'est assigné dans la liste 'Objets Dechets' du GameManager !");
            return;
        }

        int randomIndex = Random.Range(0, objetsDechets.Length);

        Vector3 randomPos = new Vector3(
            Random.Range(-20f, 20f),
            0.5f,
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

    public void DeclencherMalus()
    {
        StartCoroutine(MalusCameraRotation());
    }

    IEnumerator MalusCameraRotation()
    {
        if (mainCam != null)
        {
            mainCam.transform.Rotate(0, 0, 180); // Retourne la caméra

            yield return new WaitForSeconds(2f); // MODIFIÉ : Attend maintenant 2 secondes au lieu de 4

            mainCam.transform.Rotate(0, 0, -180); // Remet à l'endroit
        }
    }
}