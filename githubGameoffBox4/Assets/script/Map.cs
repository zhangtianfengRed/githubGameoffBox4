using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
    public int high;
    public int width;
    public GameObject ground;
    private GameObject GROUND;
    public GameObject Boxgroup;
    public GameObject Targetgroup;
    private MapData[,] data;
	// Use this for initialization
	void Start () {
        data = new MapData[width, high];

        GROUND = new GameObject("GROUND");
        mapStart();
        mapDataload(Boxgroup);
        mapDataload(Targetgroup);
        TestmapData();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    #region 加载关卡
    public interface load
    {
        void loadlevel(int level);
    }

    class levelload : load
    {

        public void loadlevel(int level)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(level+1);
        }
    }
    public interface level
    {
        void Done();
    }
    public class Level:level
    {
        public GameObject mylevel { get; set; }
        private load Load;
        public int _level { get; set; }

        public Level(load _load)
        {
            this.Load = _load;
        }

        public void Done()
        {
            Load.loadlevel(_level);
        }
    }
    #endregion
   

    void mapStart()
    {
        for (int x = 0; x < high; x++) {
            for(int y = 0; y < width; y++)
            {
                GameObject T= Instantiate(ground, new Vector3Int(x, y, 0), Quaternion.identity);
                T.transform.parent = GROUND.transform;
            }
        }
    }

    void mapDataload(GameObject T)
    {
        foreach(Transform i in T.GetComponentInChildren<Transform>())
        {
            if (i != null)
            {
                Vector2Int iVector = new Vector2Int((int)i.position.x, (int)i.position.y);
                data[iVector.x, iVector.y] = new MapData();
                data[iVector.x, iVector.y].Boxtag(i.gameObject);
            }
        }
    }
    void TestmapData()
    {
        foreach(MapData u in data)
        {
            Debug.Log(u);
        }
    }

    class MapData
    {
        public GameObject Box { get; set; }
        public GameObject Target { get; set; }

        public void Boxtag(GameObject gameObject)
        {
            switch (gameObject.transform.tag)
            {
                case ("box"):
                    if (Box != null) { Box = gameObject; }                    
                    break;
                case ("target"):
                    if (Target != null) { Target = gameObject; }                  
                    break;
            }
        }

        protected void BoxClean()
        {
            Box = null;
        }
        protected void TargetClean()
        {
            Target = null;
        }
        protected void ALLClean()
        {
            Box = null;
            Target = null;
        }
        
        
    }


    
}
