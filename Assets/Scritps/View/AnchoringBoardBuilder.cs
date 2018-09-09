using UnityEngine;

public class AnchoringBoardBuilder : MonoBehaviour
{
    public GameObject RootObject;
    public GameObject HorizontalReference;
    public GameObject VerticalReference;

    public int SizeX;
    public int SizeY;    

    private void OnEnable()
    {
        var horizontalDistance = HorizontalReference.transform.position - RootObject.transform.position;
        var verticalDistance = VerticalReference.transform.position - RootObject.transform.position;

        for (int i = 0; i < SizeY; i++)
        {
            for(int j = 0; j < SizeX; j++)
            {
                var position = RootObject.transform.position + horizontalDistance * j + verticalDistance * i;
                var newBlock = Instantiate(RootObject, position, Quaternion.identity, transform);
                newBlock.SetActive(true);
            }
        }
    }
}
