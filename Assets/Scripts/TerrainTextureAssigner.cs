using UnityEngine;
using System.Collections;

public class TerrainTextureAssigner : MonoBehaviour
{
	public Texture2D[] TerrainTextures;
	public Texture2D[] TerrainTextures1;
	public Texture2D[] TerrainTextures2;
	public Texture2D[] TerrainTextures3;
	public Material skybox, skybox1, skybox2, skybox3;
	public TerrainData terraindata;
	public Terrain terrain;
	public Transform terrainT;
	void Awake(){
		Time.timeScale = 0;
		CreateTerrain ();

	}

    [System.Obsolete]
    public void CreateTerrain(){
		SplatPrototype[] tex = new SplatPrototype [TerrainTextures.Length];
		for (int i=0; i<TerrainTextures.Length; i++) {
			tex [i] = new SplatPrototype ();

			if (GlobalVeriables.Instance.CurrentLevel == 0) {
				tex [i].texture = TerrainTextures [i];    //Sets the texture
			}else if (GlobalVeriables.Instance.CurrentLevel == 1) {
				tex [i].texture = TerrainTextures [i];
			}else if (GlobalVeriables.Instance.CurrentLevel == 2) {
				tex [i].texture = TerrainTextures1 [i];
			}else if (GlobalVeriables.Instance.CurrentLevel == 3) {
				tex [i].texture = TerrainTextures1 [i];
			}else if (GlobalVeriables.Instance.CurrentLevel == 4) {
				tex [i].texture = TerrainTextures2 [i];
			}else if (GlobalVeriables.Instance.CurrentLevel == 5) {
				tex [i].texture = TerrainTextures2 [i];
			}else if (GlobalVeriables.Instance.CurrentLevel == 6) {
				tex [i].texture = TerrainTextures3 [i];
			}else if (GlobalVeriables.Instance.CurrentLevel == 7) {
				tex [i].texture = TerrainTextures3 [i];
			}else if (GlobalVeriables.Instance.CurrentLevel == 8) {
				tex [i].texture = TerrainTextures1 [i];
			}else if (GlobalVeriables.Instance.CurrentLevel == 9) {
				tex [i].texture = TerrainTextures2 [i];
			}
		
		}
		terraindata.splatPrototypes = tex;
        terrain.terrainData = terraindata;//Terrain.CreateTerrainGameObject (terraindata).GetComponent<Terrain> ();

        terrain.gameObject.transform.tag = "Scene";
		terrain.gameObject.transform.position = terrainT.position;
		Time.timeScale = 1;

		if (GlobalVeriables.Instance.CurrentLevel == 0) {
			RenderSettings.skybox = skybox;    //Sets the texture
		}else if (GlobalVeriables.Instance.CurrentLevel == 1) {
			RenderSettings.skybox = skybox;
		}else if (GlobalVeriables.Instance.CurrentLevel == 2) {
			RenderSettings.skybox = skybox1;
		}else if (GlobalVeriables.Instance.CurrentLevel == 3) {
			RenderSettings.skybox = skybox1;
		}else if (GlobalVeriables.Instance.CurrentLevel == 4) {
			RenderSettings.skybox = skybox2;
		}else if (GlobalVeriables.Instance.CurrentLevel == 5) {
			RenderSettings.skybox = skybox2;
		}else if (GlobalVeriables.Instance.CurrentLevel == 6) {
			RenderSettings.skybox = skybox3;
		}else if (GlobalVeriables.Instance.CurrentLevel == 7) {
			RenderSettings.skybox = skybox3;
		}else if (GlobalVeriables.Instance.CurrentLevel == 8) {
			RenderSettings.skybox = skybox1;
		}else if (GlobalVeriables.Instance.CurrentLevel == 9) {
			RenderSettings.skybox = skybox2;
		}
	
	}
}