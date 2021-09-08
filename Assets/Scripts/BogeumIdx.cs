using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BogeumIdx : MonoBehaviour
{
    public int bogeumIdx = -1;
    
    public void setBogeumIdxAtDeleteView()
    {
        GameObject.Find("ConfirmDelete").GetComponent<BogeumIdx>().bogeumIdx = bogeumIdx;
    }
    public void deleteThis()
    {
        GameObject.Find("SEOMInfo").GetComponent<GetSEOMInfo>().DeleteBogeum(bogeumIdx);
    }
}
