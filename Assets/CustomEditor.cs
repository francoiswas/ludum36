using UnityEngine;
using UnityEditor;

public class MenuItems
{

	[MenuItem("Utils/Create Project Hierarchy")]
	private static void CreateProjectHierarchy()
	{
		string F_3rdParty = AssetDatabase.CreateFolder( "Assets", "3rd-Party" );
		string F_Animations = AssetDatabase.CreateFolder( "Assets", "Animations" );
		string F_Audio = AssetDatabase.CreateFolder( "Assets", "Audio" );
		string F_Music = AssetDatabase.CreateFolder( "Assets/Audio", "Music" );
		string F_SFX = AssetDatabase.CreateFolder( "Assets/Audio", "SFX" );
		string F_Materials = AssetDatabase.CreateFolder( "Assets", "Materials" );
		string F_Models = AssetDatabase.CreateFolder( "Assets", "Models" );
		string F_Plugins = AssetDatabase.CreateFolder( "Assets", "Plugins" );
		string F_Prefabs = AssetDatabase.CreateFolder( "Assets", "Prefabs" );
		string F_Resources = AssetDatabase.CreateFolder( "Assets", "Resources" );
		string F_Textures = AssetDatabase.CreateFolder( "Assets", "Textures" );
		string F_Sandbox = AssetDatabase.CreateFolder( "Assets", "Sandbox" );
		string F_Scenes = AssetDatabase.CreateFolder( "Assets", "Scenes" );
		string F_Levels = AssetDatabase.CreateFolder( "Assets/Scenes", "Levels" );
		string F_Other = AssetDatabase.CreateFolder( "Assets/Scenes", "Other" );
		string F_Scripts = AssetDatabase.CreateFolder( "Assets", "Scripts" );
		string F_Editor = AssetDatabase.CreateFolder( "Assets/Scripts", "Editor" );
		string F_Shaders = AssetDatabase.CreateFolder( "Assets", "Shaders" );

	}
}