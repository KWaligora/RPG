using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagment
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }

            if(Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }    

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }    
    }
}
