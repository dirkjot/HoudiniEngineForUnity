/*
 * PROPRIETARY INFORMATION.  This software is proprietary to
 * Side Effects Software Inc., and is not to be reproduced,
 * transmitted, or disclosed in any way without written permission.
 *
 * Produced by:
 *      Side Effects Software Inc
 *		123 Front Street West, Suite 1401
 *		Toronto, Ontario
 *		Canada   M5J 2M2
 *		416-504-9876
 *
 * COMMENTS:
 * 
 */

using UnityEngine;
using UnityEditor;
using System.Collections;

using HAPI;

[ CustomEditor( typeof( HAPI_PartControl ) ) ]
[ CanEditMultipleObjects ]
public class HAPI_PartControlGUI : Editor 
{
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Public
	
	public void OnSceneGUI()
	{
		HAPI_PartControl control = target as HAPI_PartControl;
		control.selectParent();
	}
	
	public virtual void OnEnable() 
	{
		myPartControl = target as HAPI_PartControl;
	}
	
	private HAPI_Instancer instancerFromPartObject( GameObject part_object )
	{
		GameObject instancer_game_object = null;
		
		if( part_object.transform.parent != null &&
			part_object.transform.parent.parent != null &&
			part_object.transform.parent.parent.parent != null )
			instancer_game_object = part_object.transform.parent.parent.parent.gameObject;
		
		HAPI_Instancer instancer = null;
			
		if( instancer_game_object != null )
			instancer = instancer_game_object.GetComponent< HAPI_Instancer >();
		
		return instancer;
	}
	
	private void pinPartObject( GameObject part_object, bool pin )
	{
		
		HAPI_Instancer instancer = instancerFromPartObject( part_object );
		if( instancer == null )
			return;
		
		HAPI_PartControl part_control = part_object.GetComponent< HAPI_PartControl >();
		if ( part_control == null )
			return;
							
		if( !pin )
		{			
			instancer.unPinInstance( part_control.prInstancePointNumber );			
		}
		else
		{			
			Transform game_object_xform = part_control.gameObject.transform;
			
			HAPI_TransformEuler xform = new HAPI_TransformEuler( true );
			xform.position[ 0 ] = game_object_xform.position.x;
			xform.position[ 1 ] = game_object_xform.position.y;
			xform.position[ 2 ] = game_object_xform.position.z;
			
			Vector3 euler_rotation = game_object_xform.rotation.eulerAngles;
			xform.rotationEuler[ 0 ] = euler_rotation.x;
			xform.rotationEuler[ 1 ] = euler_rotation.y;
			xform.rotationEuler[ 2 ] = euler_rotation.z;
			
			Vector3 scale = game_object_xform.localScale;
			
			Transform parent = game_object_xform.parent;
			while ( parent != null )
			{
				scale.x *= parent.localScale.x;
				scale.y *= parent.localScale.y;
				scale.z *= parent.localScale.z;						
				parent = parent.parent;						
			}
			
			xform.scale[ 0 ] = scale.x;
			xform.scale[ 1 ] = scale.y;
			xform.scale[ 2 ] = scale.z;
								
			
			HAPI_InstancerOverrideInfo override_info = new HAPI_InstancerOverrideInfo();
			override_info.objectToInstantiate = part_control.prObjectToInstantiate;
			override_info.xform = xform;
			override_info.instancePointNumber = part_control.prInstancePointNumber;
			
			instancer.pinInstance( override_info );
		}
		
		
	}
	
	public override void OnInspectorGUI() 
	{
		if ( myPartControl.prGeoType == HAPI_GeoType.HAPI_GEOTYPE_EXPOSED_EDIT )
		{
			if ( GUILayout.Button( "Update Intermediate Result" ) ) 
			{
				MeshFilter mesh_filter			= myPartControl.gameObject.GetComponent< MeshFilter >();
				Mesh shared_mesh				= mesh_filter.sharedMesh;
				HAPI_PartControl part_control	= myPartControl.gameObject.GetComponent< HAPI_PartControl >();
				HAPI_AssetUtility.setMesh(		myPartControl.prAsset.prAssetId, 
												myPartControl.prObjectId,
												myPartControl.prGeoId,
												ref shared_mesh,
												part_control );
				
				myPartControl.prAsset.prFullBuild = false;
				myPartControl.prAsset.build();
			}
			
			if ( GUILayout.Button( "Clear Edits" ) ) 
			{
				HAPI_Host.revertGeo( myPartControl.prAsset.prAssetId,
									 myPartControl.prObjectId,
									 myPartControl.prGeoId );
				
				myPartControl.prAsset.prFullBuild = false;
				myPartControl.prAsset.build();
			}
		}
		
		Object[] selection = Selection.objects;
		if( selection.Length > 1 )
		{
			if ( GUILayout.Button( "Pin Instances" ) ) 
			{
				foreach( Object obj in selection )
				{
					if( obj.GetType() == typeof(GameObject) )
						pinPartObject( (GameObject) obj, true );
				}
			}
			
			if ( GUILayout.Button( "UnPin Instances" ) ) 
			{
				foreach( Object obj in selection )
				{
					if( obj.GetType() == typeof(GameObject) )
						pinPartObject( (GameObject) obj, false );
				}
			}
		}
		else
		{
			
			HAPI_Instancer instancer = instancerFromPartObject( myPartControl.gameObject );
			if( !instancer )
				return;
		
			bool is_overridden = instancer.isPointOverridden( myPartControl.prInstancePointNumber );
			if( is_overridden )
			{
				if ( GUILayout.Button( "UnPin Instance" ) ) 
				{
					pinPartObject( myPartControl.gameObject, false );
				}
			}
			else
			{
				if ( GUILayout.Button( "Pin Instance" ) ) 
				{
					pinPartObject( myPartControl.gameObject, true );
				}
			}			
			
		}
		
	}
	
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Private

	[SerializeField] private HAPI_PartControl myPartControl = null;
	
}
