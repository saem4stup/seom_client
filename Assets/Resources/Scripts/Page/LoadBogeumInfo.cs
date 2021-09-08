using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
public class LoadBogeumInfo : MonoBehaviour
{
    public TextMeshProUGUI bogeumName;
    public TextMeshProUGUI bogeumBirthDeathDate;
    public TextMeshProUGUI bogeumBookmarks;
    public GameObject fullStar;
    public GameObject blankStar;
    public Transform parent_content;
    public RawImage bogeumImage;

    private List<GameObject> memoryArray;
    private GameObject memoryPrefab;
    private GameObject tmp;
    private int numOfMemory;
    private void Awake()
    {
        memoryPrefab = Resources.Load<GameObject>("a_memory");
        bogeumName.text = DataSaver.instance.BogeumTitle;
        bogeumBirthDeathDate.text = DataSaver.instance.BogeumDate;
        bogeumBookmarks.text = DataSaver.instance.BogeumBookmarks;
        //bogeumImage.texture = DataSaver.instance.BogeumImage.texture;
        
        StartCoroutine("LoadDeceasedImage");
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("LoadContents");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (DataSaver.instance.Star)
        {
            fullStar.SetActive(true);
            blankStar.SetActive(false);
        }

        if (DataSaver.instance.Star==false)
        {
            fullStar.SetActive(false);
            blankStar.SetActive(true);
        }
    }

    IEnumerator LoadDeceasedImage()
    {
        UnityWebRequest ImgWWW = UnityWebRequestTexture.GetTexture(DataSaver.instance.BogeumImageString);
        yield return ImgWWW.SendWebRequest();
        while (!ImgWWW.isDone)
        {
            yield return null;
        }
        bogeumImage.texture = ((DownloadHandlerTexture)ImgWWW.downloadHandler).texture;
    }
    IEnumerator LoadContents()
    {
        Debug.Log(DataSaver.instance.numofmemory);
        if (DataSaver.instance.numofmemory > 0)
        {
            for (int i = 0; i < DataSaver.instance.numofmemory; i++)
            {
                tmp = Instantiate(memoryPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                tmp.transform.parent = parent_content;
                tmp.transform.localScale = new Vector3(1, 1, 1);
                tmp.transform.localPosition = Vector3.zero;
                tmp.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = DataSaver.instance.createDate[i];
                //tmp.transform.GetChild(0).transform.GetChild(0).GetComponent<Ttmp.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().textextMeshProUGUI>().text = bogeumData.contentsInfo[i].createDate;
                tmp.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = DataSaver.instance.commentCount[i].ToString();
                //tmp.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = bogeumData.contentsInfo[i].commentCount.ToString();
                tmp.transform.GetChild(3).GetComponent<ContentIdx>().thisIdx = DataSaver.instance.contentsIdx[i];
                //tmp.transform.GetChild(3).GetComponent<ContentIdx>().thisIdx = bogeumData.contentsInfo[i].contentsIdx;
                Debug.Log(DataSaver.instance.contentsImg[i]);

                UnityWebRequest ContentsImgwww = UnityWebRequestTexture.GetTexture(DataSaver.instance.contentsImg[i]);
                yield return ContentsImgwww.SendWebRequest();

                tmp.transform.GetChild(2).transform.GetChild(0).GetComponent<RawImage>().texture = ((DownloadHandlerTexture)ContentsImgwww.downloadHandler).texture;
            }
        }
    }
}
