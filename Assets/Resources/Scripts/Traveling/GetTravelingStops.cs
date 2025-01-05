using System.Collections.Generic;
using UnityEngine;

public class GetTravelingStops : MonoBehaviour
{
    public List<Vector3> DetectSiblingStops()
    {
        List<Vector3> positions = new List<Vector3>();
        
        Transform parentTransform = transform.parent;
        
        foreach (Transform sibling in parentTransform)
        {
            if (sibling != transform && sibling.CompareTag("Traveling_Stop"))
            {
                positions.Add(sibling.position);
            }
        }
        
        return positions;
    }
}
