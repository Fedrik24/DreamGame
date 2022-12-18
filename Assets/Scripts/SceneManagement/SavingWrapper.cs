using System;
using System.Collections;
using System.Collections.Generic;
using Dreams.Saving;
using UnityEngine;

namespace Dreams.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        private const string saveFile = "save";

        private IEnumerator Start()
        {
            yield return GetComponent<SavingSystem>().LoadLastScene(saveFile);
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L)) // Load 
            {
                Load();
            }

            if (Input.GetKeyDown(KeyCode.S)) // save 
            {
                Save();
            } 
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(saveFile);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(saveFile);
        }
    }
}