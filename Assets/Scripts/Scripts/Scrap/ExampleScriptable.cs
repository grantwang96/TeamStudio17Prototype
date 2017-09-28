using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "ExampleScriptable")]
public class ExampleScriptable : ScriptableObject {

	public int DankMemes;

	public bool CanShareMemes;

	[Range (0, 100)]
	public int gilgamesh;
}
