using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HAPI_InstancerPersistentData : ScriptableObject
{
	public string instancerName;
	public List< string > uniqueNames;
	public List< GameObject > objsToInstantiate;
	public List< int > numObjsToInstantiate;
	public List< int > variationChoice;
	public List< bool > recalculateVariations;
	public bool showInstancerGUI = true;

	public List< HAPI_InstancerOverrideInfo > overriddenInstances; 

	public HAPI_InstancerPersistentData()
	{
		uniqueNames = new List< string >();
		objsToInstantiate = new List< GameObject > ();
		overriddenInstances = new List< HAPI_InstancerOverrideInfo >();
		numObjsToInstantiate = new List< int >();
		variationChoice = new List< int >();
		recalculateVariations = new List< bool >();
	}

	public int baseIndex( string name )
	{
		int index = 0;
		for ( int ii = 0; ii < uniqueNames.Count; ii++ )
		{
			if ( uniqueNames[ ii ] == name )
			{
				return index;
			}
			index += numObjsToInstantiate[ ii ];
		}
		return -1;
	}

	public int baseIndex( int logical_index )
	{
		if ( logical_index >= uniqueNames.Count )
			return -1;

		int index = 0;
		for ( int ii = 0; ii < logical_index; ii++ )
		{
			index += numObjsToInstantiate[ ii ];
		}
		return index;
	}

	public GameObject getUserObjToInstantiateFromName( string name, int point_index )
	{
		for ( int ii = 0; ii < uniqueNames.Count; ii++ )
		{
			if ( uniqueNames[ ii ] == name )
			{
				int base_index = baseIndex( name );
				if ( point_index >= variationChoice.Count )
				{
					Debug.LogError(
						"point_index out of range in " +
						"HAPI_InstancerPersistentData::getUserObjToInstantiateFromName" );
					return null;
				}

				if ( recalculateVariations[ ii ] || 
					variationChoice[ point_index ] < 0 ||
					variationChoice[ point_index ] >= numObjsToInstantiate[ ii ] )
				{
					int random_index = UnityEngine.Random.Range( 0, numObjsToInstantiate[ ii ] );
					variationChoice[ point_index ] = random_index;
				}

				int variation_choice = base_index + variationChoice[ point_index ];
				return objsToInstantiate[ variation_choice ];
			}
		}
		return null;
	}
}

