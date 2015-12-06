using UnityEngine;
using System.Collections;

public class ShapeData : MonoBehaviour {

    [System.Serializable]
    public class coreShape
    {
        public string shapeType;

        [System.Serializable]
        public class coreShapeLevelData
        {
            public string coreObjectName;
            public GameObject coreObject;
        }

        public coreShapeLevelData[] levelData;
      
    }

    public coreShape[] coreShapeData;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
