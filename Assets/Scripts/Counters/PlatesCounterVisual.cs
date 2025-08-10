using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
     [SerializeField] private PlatesCounter platesCounter;
     [SerializeField] private Transform counterTopPoint;
     [SerializeField] private GameObject plateVisualPrefab;
     [SerializeField] private List<GameObject> plateVisualGameObjectList;

     private void Awake()
     {
          plateVisualGameObjectList = new List<GameObject>();
     }

     private void Start()
     //When the event OnPlateSpwaned is called, we instantiate another plate and position it on the stack and adding 1 to the list
     //When the event OnPlateRemoved is called, we remove the top plate from the stack 
     {
          platesCounter.OnPlateSpwaned += (sender, args) =>
          {
               Transform plateVisualTransform = Instantiate(plateVisualPrefab.transform, counterTopPoint);
               float plateOffsetY = 0.1f;
               plateVisualTransform.localPosition = new Vector3(0f, plateOffsetY * plateVisualGameObjectList.Count, 0f);
               plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
          };
          platesCounter.OnPlateRemoved += (sender, args) =>
          {
               GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
               plateVisualGameObjectList.Remove(plateGameObject);
               Destroy(plateGameObject);
          };

     }
}
