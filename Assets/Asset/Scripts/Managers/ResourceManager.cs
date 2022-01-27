using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public T Load<T>(string path) where T : Object{//where는 generic에 대한 제한 조건
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null){
        GameObject prefab = Resources.Load<GameObject>($"Prefabs/{path}");
        if(prefab == null){
            Debug.Log($"Failed to Load : {path}");
            return null;
        }

        return Object.Instantiate(prefab, parent);
    }

    public GameObject InstantiatePosition(string path, Vector3 position){
        GameObject prefab = Resources.Load<GameObject>($"Prefabs/{path}");
        if(prefab == null){
            Debug.Log($"Failed to Load : {path}");
            return null;
        }

        return Object.Instantiate(prefab, position, Quaternion.identity);
    }


    public GameObject Instantiate(GameObject go, Transform parent = null){
        if(go == null){
            Debug.Log($"Failed to Load : {go}");
            return null;
        }

        return Object.Instantiate(go, parent);
    }

    public void Destroy(GameObject go, float time = 0) {
        if(go == null) return;

        Object.Destroy(go, time);
    }

    void Start() {
    }

}