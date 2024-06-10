using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiggerDamage : MonoBehaviour
{
    public heartController heart;
    public string _tagTargetDetection = "Jogador";
     public List<Collider2D> detectedObjs = new List<Collider2D>();

    private void OnCollisionEnter2D(Collider2D collision){
        if(collision.gameObject.tag == _tagTargetDetection){
             detectedObjs.Add(collision);
            heart.vida--;
        }
    }
}
