using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Out Component")]
    [SerializeField] float speed;
    [SerializeField] Text scoreText, bestScoreText;
    [SerializeField] GameObject restartPanel, playGamePanel;
    [SerializeField] GameObject tebrikMesaj;

    [Header("Public Variable")]
    public GroundSpawner groundSpawner;
    public static bool isDead = true;
    public float hizlanmaZorlugu;


    Vector3 yon = Vector3.left;
    int bestScore = 0;
    float score = 0f;
    int sonScoreKontrol = 0;
    float artisMiktari = 1f;
    bool bestGectiMi = false;


    private void Start()
    {
        if (RestartGame.isRestart)
        {
            isDead = false;
            playGamePanel.SetActive(false);
        }
        bestScore = PlayerPrefs.GetInt("BestScore");
        bestScoreText.text = "Best: " + bestScore.ToString();
    }
    private void Update()
    {
        if (isDead)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (yon.x == 0) //z ekseninde hareket ediyor demektir
            {
                yon = Vector3.left;
            }
            else
            {
                yon = Vector3.back;
            }
        }
        if (transform.position.y < 0.1f)
        {
            isDead = true;
            if (bestScore < score)
            {
                bestGectiMi = true;
                bestScore = (int)score;
                PlayerPrefs.SetInt("BestScore", bestScore);
            }
            restartPanel.SetActive(true);
            Destroy(gameObject, 3f);
        }

        if ((int)score / 30 > sonScoreKontrol && (int)score != 0 && bestScore != 0) // 30'a bölümünden kalan 0'sa
        {
            speed += 0.3f;
            artisMiktari += 0.2f;
            sonScoreKontrol++;
        }
    }
    private void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }

        Vector3 hareket = yon * speed * Time.deltaTime; //objemizin hareket değeri
        transform.position += hareket; //hareket değerini sürekli pozisyonuma ekle

        //speed += Time.deltaTime * hizlanmaZorlugu;
        //score += artisMiktari * speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Altin"))
        {
            Destroy(other.gameObject);
            score += 1;
            scoreText.text = "Score: " + ((int)score).ToString();
            if (score > bestScore && !bestGectiMi)
            {
                bestGectiMi = true;
                tebrikMesaj.SetActive(true);
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zemin"))
        {
            StartCoroutine(YokEt(collision.gameObject));
            groundSpawner.ZeminOlustur();
        }
    }
    IEnumerator YokEt(GameObject zemin)
    {
        yield return new WaitForSeconds(0.2f);
        zemin.AddComponent<Rigidbody>();

        yield return new WaitForSeconds(0.4f);
        Destroy(zemin);
    }

    public void PlayGame()
    {
        isDead = false;
        playGamePanel.SetActive(false);
    }






}//class
