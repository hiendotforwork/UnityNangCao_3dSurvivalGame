using UnityEngine;

public class Blueprint : MonoBehaviour
{
    public string ItemName;
    public string Req1;
    public string Req2;
    public int Req1Amount;
    public int Req2Amount;
    public int numOfReq;

    public Blueprint(string name, string req1, string req2, int req1Amount, int req2Amount, int numOfReq)
    {
        ItemName = name;
        Req1 = req1;
        Req2= req2;
        Req1Amount = req1Amount;
        Req2Amount = req2Amount;
        this.numOfReq = numOfReq;
    }
}
