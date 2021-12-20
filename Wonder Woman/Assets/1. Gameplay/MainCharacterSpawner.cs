using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LupiLab.Camera;
using LupiLab.UI;
using LupiLab.Character;

namespace LupiLab.Core
{
    public class MainCharacterSpawner : Spawner
    {

        [SerializeField] private GameObject _prefab;
        [SerializeField] private HUDSettings _hudSettings;
        private MainCharacterHUDManager _mainCharacterHUDManager;

        public void SpawnVoid()
        {
            Spawn();
        }

        public override bool Spawn()
        {
            GameObject spawnedObject = Instantiate(_prefab, transform.position, transform.rotation);
            Character.Character character = spawnedObject.GetComponent<Character.Character>();

            if (ThirdPersonCamera.MainCameraRig != null)
            {
                ThirdPersonCamera.MainCameraRig.Target = spawnedObject.transform;
            }

            _mainCharacterHUDManager = MainCharacterHUDManager.Instance;
            _mainCharacterHUDManager?.SetTarget(character, _hudSettings);

            return true;
        }

        public override bool Spawn(out GameObject[] spawnedObjects)
        {
            GameObject spawnedObject = Instantiate(_prefab, transform.position, transform.rotation);
            Character.Character character = spawnedObject.GetComponent<Character.Character>();

            if (ThirdPersonCamera.MainCameraRig != null)
            {
                ThirdPersonCamera.MainCameraRig.Target = spawnedObject.transform;
            }

            _mainCharacterHUDManager = MainCharacterHUDManager.Instance;
            _mainCharacterHUDManager?.SetTarget(character, _hudSettings);

            spawnedObjects = new GameObject[] { spawnedObject };
            return true;
        }

        

    }
}