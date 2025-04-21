using System.Collections.Generic;
using UnityEngine;

namespace UIUtils
{
    public class RandomizeChildrenOrder : MonoBehaviour
    {
        private void Start()
        {
            if(GameManager.Instance.shuffleHomeScreen) RandomizeChildren();
        }
    
        private void RandomizeChildren()
        {
            // Collect all children into a list
            List<Transform> children = new List<Transform>();
            foreach (Transform child in transform)
            {
                children.Add(child);
            }

            // Shuffle using "Fisher–Yates algorithm"
            for (int i = children.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (children[i], children[j]) = (children[j], children[i]);
            }

            // Update sibling indices based on the new order
            for (int i = 0; i < children.Count; i++)
            {
                children[i].SetSiblingIndex(i);
            }
        }
    }
}