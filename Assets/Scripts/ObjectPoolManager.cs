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
        Debug.Log("0");
        PooledObjectInfo pool = ObjectPools.Find(x => x.LookupString == objectToSpawn.name);

        Debug.Log("1");
        if(pool == null){
            pool = new PooledObjectInfo(){ LookupString = objectToSpawn.name };
        
            ObjectPools.Add(pool);
        }
        Debug.Log("2");

        // Check if there is any inactive object in the pool, if there is take it
        GameObject spawnableObject = pool.inactiveObjects.FirstOrDefault();

            Debug.Log("3");
        if(spawnableObject == null){
            // If there is no inactive object in the pool, create a new one
            Debug.Log("3.0");
            spawnableObject = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
            Debug.Log("3.0.1");
            SetParent(spawnableObject, poolType);
        }
        else{
            Debug.Log("3.1");
            // If there is an inactive object in the pool, take it and set it to active
            spawnableObject.transform.position = spawnPosition;
            spawnableObject.transform.rotation = spawnRotation;
            Debug.Log("3.2");
            foreach(Transform child in spawnableObject.transform){
                child.gameObject.SetActive(true);
                child.transform.position = spawnPosition;
            }
            Debug.Log("3.3");
            pool.inactiveObjects.Remove(spawnableObject);
            spawnableObject.SetActive(true);

        }
        Debug.Log("4");
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
        Debug.Log("3.0.2");
        if(_particleEffectsHolder == null)
            _particleEffectsHolder = new GameObject("ParticleEffects");

        switch(poolType){
            case PoolType.ParticleEffect:
            Debug.Log("3.0.3");
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

