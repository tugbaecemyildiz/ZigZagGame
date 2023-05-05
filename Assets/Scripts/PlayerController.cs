using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 yon = Vector3.left;
    [SerializeField] float speed;
    public GroundSpawner groundSpawner;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (yon.x==0) //z ekseninde hareket ediyor demektir
            {
                yon = Vector3.left;
            }
            else
            {
                yon = Vector3.back;
            }
        }
    }
    private void FixedUpdate()
    {
        Vector3 hareket = yon * speed * Time.deltaTime; //objemizin hareket değeri
        transform.position += hareket; //hareket değerini sürekli pozisyonuma ekle
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zemin"))
        {
            groundSpawner.ZeminOlustur();
        }
    }






}//class
