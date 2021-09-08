using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataSaver : MonoBehaviour
{
    public static DataSaver instance = null;
    public int userIdx;
    //public int islandIdx;
    public string currMemoryidx; // °Ô½Ã±Û
    public string currBogeumidx;
    public string BogeumTitle;
    public string BogeumDate;
    public string BogeumBookmarks;
    public string BogeumImageString;
    public bool Star;
    public int numofmemory;
    public Transform Parent_content;
    public RawImage BogeumImage;

    public List<int> contentsIdx;
    public List<string> contentsImg;
    public List<int> likes;
    public List<string> createDate;
    public List<int> commentCount;
    // Start is called before the first frame update
    private void Awake()
    {
       
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this){
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
