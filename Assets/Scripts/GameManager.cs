using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Score & Économie")]
    public int scoreDéchets = 0;
    public int nombreGraines = 0;
    public int arbresPlantes = 0;
    public int objectifArbres = 6;

    [Header("Timer & Game Over")]
    public float tempsRestant = 120f;
    private bool jeuTermine = false;

    [Header("UI")]
    public TMP_Text textScore;
    public TMP_Text textGraines;
    public TMP_Text textTimer;
    public TMP_Text textObjectif; // Ajout du texte objectif
    public TMP_Text textVictoire;
    public TMP_Text textFin; // Texte pour Victoire ou GameOver

    [Header("Spawn & Caméra")]
    public GameObject[] objetsDechets;
    public GameObject mainCam;

    void Start()
    {
        MettreAJourUI();
        InvokeRepeating("SpawnDechet", 2f, 4f);
        textVictoire.gameObject.SetActive(false);
    }

    void Update()
    {
        if (jeuTermine) return;

        // Gestion du Timer
        if (tempsRestant > 0)
        {
            tempsRestant -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(tempsRestant / 60);
            int secondes = Mathf.FloorToInt(tempsRestant % 60);
            textTimer.text = string.Format("Temps : {0:00}:{1:00}", minutes, secondes);
        }
        else
        {
            FinDePartie(false); // Temps écoulé = Défaite
        }
    }

    void FinDePartie(bool estVictoire)
    {
        jeuTermine = true;
        Time.timeScale = 0; // Pause le jeu
        
        if (textFin != null)
        {
            textFin.gameObject.SetActive(true);
            textFin.text = estVictoire ? "MISSION ACCOMPLIE !" : "GAME OVER : Ville polluée !";
        }
    }

    // --- LOGIQUE DE JEU ---

    public void AjouterPoint()
    {
        scoreDéchets++;
        MettreAJourUI();
    }

    public void AjouterGraines(int quantite)
    {
        nombreGraines += quantite;
        MettreAJourUI();
    }

    public void ArbrePlante()
    {
        arbresPlantes++;
        MettreAJourUI(); // Met à jour l'objectif à chaque arbre
        
        if (arbresPlantes >= objectifArbres)
        {
            textVictoire.text = "MISSION ACCOMPLIE !";
            textVictoire.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    // --- UI ---

    public void MettreAJourUI() // <-- J'ai ajouté 'void' ici
    {
        if (textScore != null) textScore.text = "Déchets : " + scoreDéchets;
        if (textGraines != null) textGraines.text = "Graines : " + nombreGraines;
        
        // Mise à jour de l'objectif
        if (textObjectif != null)
        {
            textObjectif.text = "Objectif : Plante 6 arbres ! (" + arbresPlantes + "/" + objectifArbres + ")";
        }
    }

    // --- SPAWN & MALUS ---

    void SpawnDechet()
    {
        if (objetsDechets.Length == 0) return;
        int randomIndex = Random.Range(0, objetsDechets.Length);
        Vector3 randomPos = new Vector3(Random.Range(-20f, 20f), 0.5f, Random.Range(-20f, 20f));
        Instantiate(objetsDechets[randomIndex], randomPos, Quaternion.identity);
    }

    public void DeclencherMalus()
    {
        StartCoroutine(MalusCameraRotation());
    }

    IEnumerator MalusCameraRotation()
    {
        if (mainCam != null)
        {
            mainCam.transform.Rotate(0, 0, 180);
            yield return new WaitForSeconds(2f);
            mainCam.transform.Rotate(0, 0, -180);
        }
    }
}