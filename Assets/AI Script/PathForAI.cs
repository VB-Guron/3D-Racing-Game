using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathForAI : MonoBehaviour
{
    public Color lineColor;

    List<Transform> nodes = new List<Transform>();

	private void OnDrawGizmos()
	{
		Gizmos.color = lineColor;

		Transform[] pathTransforms = GetComponentsInChildren<Transform>();
		nodes = new List<Transform>();

		for(int i = 0; i<pathTransforms.Length; i++)
		{
			if(pathTransforms[i] != transform)
			{
				nodes.Add(pathTransforms[i]);
			}
		}

		for(int i = 0; i<nodes.Count; i++)
		{
			Vector3 curentNode = nodes[i].position;
			Vector3 previousNode = Vector3.zero;

			if (i > 0)
			{
				previousNode = nodes[i - 1].position;
			}
			else if(i == 0 && nodes.Count > 1)
			{
				previousNode = nodes[nodes.Count - 1].position;
			}

			Gizmos.DrawLine(previousNode, curentNode);
			Gizmos.DrawWireSphere(curentNode, 0.3f);
		}
	}
}
