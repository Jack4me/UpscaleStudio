using System;
using System.Collections;
using System.Collections.Generic;
using _UpscaleStudio._Scripts.Player;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   private void OnTriggerEnter(Collider other) {
      if (other.TryGetComponent(out PlayerHandle playerHandle)){
         Debug.Log("DIE");
      }
   }
}
