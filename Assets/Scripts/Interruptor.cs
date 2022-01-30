using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interruptor : MonoBehaviour
{

    [SerializeField] GameObject LuzActivable;
    [SerializeField] bool LuzActiva;



    void Awake() {
        //LuzActivable.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        LuzActivable.SetActive(LuzActiva);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            LuzActivable.SetActive(!LuzActivable.activeSelf);
        }
    }
}
