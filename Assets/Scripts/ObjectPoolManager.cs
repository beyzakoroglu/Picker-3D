using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor.SceneManagement;
using System;

public class ObjectPoolManager : MonoBehaviour {
    private static ObjectPoolManager objectPoolManager;
    public static ObjectPoolManager Instance { get {return objectPoolManager;} private set {objectPoolManager = value;} }

    public static List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>();

    public enum PoolType{  
        ParticleEffect
    }


    private static GameObject _poolHolder;
    private static GameObject _particleEffectsHolder;


    private void Awake(){
        if(Instance != null && Instance != this){
            Destroy(this.gameObject);
            return;
        } 
            
        Instance = this;
        Setup();
        
    }

    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.ParticleEffect){
        
        PooledObjectInfo pool = ObjectPools.Find(x => x.LookupString == objectToSpawn.name);

        if(pool == null){
            pool = new PooledObjectInfo(){ LookupString = objectToSpawn.name };
        
            ObjectPools.Add(pool);
        }

        // Check if there is any inactive object in the pool, if there is take it
        GameObject spawnableObject = pool.inactiveObjects.FirstOrDefault();

        if(spawnableObject == null){
            // If there is no inactive object in the pool, create a new one
            spawnableObject = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
            SetParent(spawnableObject, poolType);
        }
        else{
            // If there is an inactive object in the pool, take it and set it to active
            spawnableObject.transform.position = spawnPosition;
            spawnableObject.transform.rotation = spawnRotation;
            foreach(Transform child in spawnableObject.transform){
                child.gameObject.SetActive(true);
                child.transform.position = spawnPosition;
            }
            pool.inactiveObjects.Remove(spawnableObject);
            spawnableObject.SetActive(true);

        }
        return spawnableObject;
    }

    public static void ReturnObjectToPool(GameObject objectToReturn){
        string goName = objectToReturn.name.Substring(0, objectToReturn.name.Length - 7); // Remove '(Clone)'

        PooledObjectInfo pool = ObjectPools.Find(x => x.LookupString == goName);
        

        if(pool == null){
            Debug.LogWarning("ObjectPoolManager: Object pool not found for " + goName);
        }
        else{
            objectToReturn.SetActive(false);
            pool.inactiveObjects.Add(objectToReturn);
        }
    }

    private void Setup(){
        _poolHolder = new GameObject("PooledObjects");

        _particleEffectsHolder = new GameObject("ParticleEffects");
        _particleEffectsHolder.transform.SetParent(_poolHolder.transform);

    }

    private static void SetParent(GameObject go, PoolType poolType){
        if(_particleEffectsHolder == null)
            _particleEffectsHolder = new GameObject("ParticleEffects");

        switch(poolType){
            case PoolType.ParticleEffect:
                go.transform.SetParent(_particleEffectsHolder.transform);
                break;
            default:
                go.transform.SetParent(_poolHolder.transform);
                break;// can be added more
        }
        
    }


}



public class PooledObjectInfo
{
    public string LookupString;
    public List<GameObject> inactiveObjects = new List<GameObject>();

}

