using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] GameObject sonZemin;
    [SerializeField] GameObject altin;
    [SerializeField] Transform sParent ;

    private void Start()
    {
        for (int i = 1; i < 20; i++)
        {
            ZeminOlustur();
        }
    }

    public void ZeminOlustur()
    {
        Vector3 yon;
        if (Random.Range(0,2)==0 ) //0 gelirse x ekseninde zemin koy
        {
            yon = Vector3.left;
        }
        else
        {
            yon = Vector3.back; //1 gelirse z ekseninde zemin koy
        }

        GameObject yeniZemin = Instantiate(sonZemin, sonZemin.transform.position + yon, sonZemin.transform.rotation, sParent);

        if (yeniZemin.transform.childCount > 0)
        {
            foreach (Transform item in yeniZemin.transform)
            {
                Destroy(item.gameObject);
            }
        } 

        int randomNumber = Random.Range(0, 2);

        if (randomNumber == 1)
        {
            Instantiate(altin, yeniZemin.transform);
        }
        sonZemin = yeniZemin;
    }




}//class
