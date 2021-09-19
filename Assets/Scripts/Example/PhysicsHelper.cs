using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace SFTool
{
    public class PhysicsHelper : MonoBehaviour
    {
        static PhysicsHelper s_Instance;
        static PhysicsHelper Instance
        {
            get
            {
                if (s_Instance != null)
                    return s_Instance;

                s_Instance = FindObjectOfType<PhysicsHelper>();

                if (s_Instance != null)
                    return s_Instance;

                Create();

                return s_Instance;
            }
            set { s_Instance = value; }
        }

        static void Create()
        {
            GameObject physicsHelperGameObject = new GameObject("PhysicsHelper");
            s_Instance = physicsHelperGameObject.AddComponent<PhysicsHelper>();
        }

        Dictionary<Collider2D, PlatformEffector2D> m_PlatformEffectorCache = new Dictionary<Collider2D, PlatformEffector2D>();
        Dictionary<Collider2D, Tilemap> m_TilemapCache = new Dictionary<Collider2D, Tilemap>();

        void Awake()
        {
            if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            PopulateColliderDictionary(m_PlatformEffectorCache);
            PopulateColliderDictionary<Tilemap>(m_TilemapCache);
        }

        protected void PopulateColliderDictionary<TComponent>(Dictionary<Collider2D, TComponent> dict) where TComponent : Component
        {
            TComponent[] components = FindObjectsOfType<TComponent>();

            for (int i = 0; i < components.Length; i++)
            {
                Collider2D[] componentColliders = components[i].GetComponents<Collider2D>();

                for (int j = 0; j < componentColliders.Length; j++)
                {
                    dict.Add(componentColliders[j], components[i]);
                }
            }
        }

        public static bool ColliderHasPlatformEffector(Collider2D collider)
        {
            return Instance.m_PlatformEffectorCache.ContainsKey(collider);
        }

        public static bool ColliderHasTilemap(Collider2D collider)
        {
            return Instance.m_TilemapCache.ContainsKey(collider);
        }

        public static bool TryGetPlatformEffector(Collider2D collider, out PlatformEffector2D platformEffector)
        {
            return Instance.m_PlatformEffectorCache.TryGetValue(collider, out platformEffector);
        }

        public static bool TryGetTilemap(Collider2D collider, out Tilemap tilemap)
        {
            return Instance.m_TilemapCache.TryGetValue(collider, out tilemap);
        }

        /// <summary>
        /// 查找TileBase
        /// </summary>
        public static TileBase FindTileForOverride(Collider2D collider, Vector2 position, Vector2 direction)
        {
            Tilemap tilemap;
            if (TryGetTilemap(collider, out tilemap))
            {
                //根据 XYZ 坐标，获取到对应瓦片。
                return tilemap.GetTile(tilemap.WorldToCell(position + direction * 0.4f));
            }

            return null;
        }
    }
}