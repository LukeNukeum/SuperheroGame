using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LupiLab.LevelManagement
{
    public class LevelPortal : MonoBehaviour
    {

        [SerializeField] private string AcceptedTag = "";
        [SerializeField] private string LevelName = "";
        [SerializeField] private LevelManager LevelManager;


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(AcceptedTag))
            {
                //LevelLoader.LoadLevel(LevelName);
            }
        }
    }
}