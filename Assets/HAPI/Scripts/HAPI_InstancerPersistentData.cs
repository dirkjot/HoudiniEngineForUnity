using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using HAPI;


public class HAPI_InstancerPersistentData : ScriptableObject
{
	public string instancerName;
	public List< string > uniqueNames;
	public List< GameObject > objsToInstantiate;
	public bool showInstancerGUI = true;

	public List< HAPI_InstancerOverrideInfo > overriddenInstances; 

	public HAPI_InstancerPersistentData()
	{
		uniqueNames = new List< string >();
		objsToInstantiate = new List< GameObject > ();
		overriddenInstances = new List< HAPI_InstancerOverrideInfo >();
	}
}
