﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.FantasyInventoryIcons.Scripts
{
    public class IconManager : MonoBehaviour
    {
        public Object IconFolder;
        public List<Sprite> Icons;
        public Transform Grid;
        public GameObject ItemPrefab;

        #if UNITY_EDITOR
        public void Refresh()
        {
            if (IconFolder == null) return;
        
            var path = AssetDatabase.GetAssetPath(IconFolder);
            var files = Directory.GetFiles(path, "*.png", SearchOption.AllDirectories);

            Icons = files.Select(AssetDatabase.LoadAssetAtPath<Sprite>).ToList();

            var icons = new List<GameObject>();

            for (var i = 0; i < Grid.childCount; i++)
            {
                icons.Add(Grid.GetChild(i).gameObject);
            }

            icons.ForEach(DestroyImmediate);

            foreach (var icon in Icons)
            {
                Instantiate(ItemPrefab, Grid).transform.Find("Icon").GetComponent<Image>().sprite = icon;
            }
        }
        #endif
    }
}
