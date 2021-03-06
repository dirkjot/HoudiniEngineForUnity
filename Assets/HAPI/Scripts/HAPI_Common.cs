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
 * 		Contains main HAPI API constants and structures.
 * 
 */

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

// Typedefs
using HAPI_StringHandle = System.Int32;
using HAPI_AssetLibraryId = System.Int32;
using HAPI_AssetId = System.Int32;
using HAPI_NodeId = System.Int32;
using HAPI_ParmId = System.Int32;
using HAPI_ObjectId = System.Int32;
using HAPI_GeoId = System.Int32;
using HAPI_PartId = System.Int32;
using HAPI_MaterialId = System.Int32;

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Defines
	
public struct HAPI_Constants
{
	// Unity-Only Constants ---------------------------------------------
	//
	// You may change these values if you wish. Nothing should break terribly.

	public const string HAPI_PRODUCT_NAME				= "Houdini Engine";
		
	// Used for things like window titles that have limited space.
	public const string HAPI_PRODUCT_SHORT_NAME			= "Houdini";

	public static string HAPI_TEXTURES_PATH				= Application.dataPath + "/Textures";
	public static string HAPI_BAKED_ASSETS_PATH			= Application.dataPath + "/Baked Assets";

	public const int HAPI_MAX_PAGE_SIZE					= 32768;
	public const int HAPI_SEC_BEFORE_PROGRESS_BAR_SHOW	= 3;
	public const int HAPI_MIN_VERTICES_PER_FACE			= 3;
	public const int HAPI_MAX_VERTICES_PER_FACE			= 3;

	public const bool HAPI_CURVE_REFINE_TO_LINEAR		= true;
	public const float HAPI_CURVE_LOD					= 8.0f;

	public const float HAPI_VOLUME_POSITION_MULT		= 2.0f;
	public const float HAPI_VOLUME_SURFACE_MAX_PT_PER_C = 64000; // Max points per container. 65000 is Unity max.
	public const float HAPI_VOLUME_SURFACE_DELTA_MULT	= 1.2f;
	public const float HAPI_VOLUME_SURFACE_PT_SIZE_MULT = 1800.0f;

	// Shared Constants -------------------------------------------------
	//
	// IMPORTANT: Changes to these constants will not change the behavior of the
	// underlying Houdini Engine. These are here to serve as C# duplicates of the
	// constants defined in the HAPI_Common.h C++ header. In fact, if you
	// change any of these you will most likely break the Unity plugin.

	public const int HAPI_POSITION_VECTOR_SIZE			= 3;
	public const int HAPI_SCALE_VECTOR_SIZE				= 3;
	public const int HAPI_NORMAL_VECTOR_SIZE			= 3;
	public const int HAPI_QUATERNION_VECTOR_SIZE		= 4;
	public const int HAPI_EULER_VECTOR_SIZE				= 3;
	public const int HAPI_UV_VECTOR_SIZE				= 2;
	public const int HAPI_COLOR_VECTOR_SIZE				= 4;
	public const int HAPI_CV_VECTOR_SIZE				= 4;

	public const int HAPI_PRIM_MIN_VERTEX_COUNT			= 1;
	public const int HAPI_PRIM_MAX_VERTEX_COUNT			= 16;

	public const int HAPI_INVALID_PARM_ID 				= -1;

	// Default Attributes' Names
	public const string HAPI_ATTRIB_POSITION			= "P";
	public const string HAPI_ATTRIB_UV					= "uv";
	public const string HAPI_ATTRIB_NORMAL				= "N";
	public const string HAPI_ATTRIB_TANGENT				= "tangentu";
	public const string HAPI_ATTRIB_COLOR				= "Cd";

	// Common image file format names (to use with the material extract APIs).
	// Note that you may still want to check if they are supported via
	// HAPI_GetSupportedImageFileFormats() since all formats are loaded 
	// dynamically by Houdini on-demand so just because these formats are defined
	// here doesn't mean they are supported in your instance.
	public const string HAPI_RAW_FORMAT_NAME			= "HAPI_RAW"; // HAPI-only Raw Format
	public const string HAPI_PNG_FORMAT_NAME			= "PNG";
	public const string HAPI_JPEG_FORMAT_NAME			= "JPEG";
	public const string HAPI_BMP_FORMAT_NAME			= "Bitmap";
	public const string HAPI_TIFF_FORMAT_NAME			= "TIFF";
	public const string HAPI_TGA_FORMAT_NAME			= "Targa";

	// Default image file format's name - used when the image generated and has
	// no "original" file format and the user does not specify a format to
	// convert to.
	public const string HAPI_DEFAULT_IMAGE_FORMAT_NAME	= HAPI_PNG_FORMAT_NAME;

	public const string HAPI_UNSUPPORTED_PLATFORM_MSG   =
		"Houdini Plugin for Unity currently only supports the Standalone Windows platform in Editor.\n" +
		"\n" +
		"To switch to the Standalone Windows platform go to File > Build Settings... and under 'Platform' " +
		"choose 'PC, Mac & Linux Standalone' and click 'Switch Platform'. Afterwards, on the right hand side, " +
		"under 'Target Platform' choose 'Windows'.\n" +
		"\n" +
		"When you switch back to the Standalone Windows platform you might need to Rebuild each asset to get back " +
		"the controls. Just click on the 'Rebuild' button in the Houdini Asset's Inspector.";
}
	
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Enums
	
public enum HAPI_StatusType
{
	HAPI_STATUS_RESULT,
	HAPI_STATUS_WARNING,
	HAPI_STATUS_STATE,
	HAPI_STATUS_MAX
};

public enum HAPI_Result 
{
	HAPI_RESULT_SUCCESS						= 0,
	HAPI_RESULT_FAILURE						= 1,
	HAPI_RESULT_ALREADY_INITIALIZED			= 2,
	HAPI_RESULT_NOT_INITIALIZED				= 3,
	HAPI_RESULT_CANT_LOADFILE				= 4,
	HAPI_RESULT_PARM_SET_FAILED				= 5,
	HAPI_RESULT_INVALID_ARGUMENT			= 6,
	HAPI_RESULT_CANT_LOAD_GEO				= 7,
	HAPI_RESULT_CANT_GENERATE_PRESET		= 8,
	HAPI_RESULT_CANT_LOAD_PRESET			= 9
};

public enum HAPI_State
{
	HAPI_STATE_READY,
	HAPI_STATE_READY_WITH_ERRORS,
	HAPI_STATE_STARTING_COOK,
	HAPI_STATE_COOKING,
	HAPI_STATE_STARTING_LOAD,
	HAPI_STATE_LOADING,
	HAPI_STATE_MAX
};

public enum HAPI_RampType
{
	HAPI_RAMPTYPE_INVALID = -1,
	HAPI_RAMPTYPE_FLOAT = 0,
	HAPI_RAMPTYPE_COLOR,
	HAPI_RAMPTYPE_MAX
};
	
public enum HAPI_ParmType
{
	HAPI_PARMTYPE_INT = 0,
	HAPI_PARMTYPE_MULTIPARMLIST,
	HAPI_PARMTYPE_TOGGLE,
	HAPI_PARMTYPE_BUTTON,
		
	HAPI_PARMTYPE_FLOAT,
	HAPI_PARMTYPE_COLOR,
		
	HAPI_PARMTYPE_STRING,
	HAPI_PARMTYPE_FILE,
		
	HAPI_PARMTYPE_FOLDERLIST,

	HAPI_PARMTYPE_FOLDER,
	HAPI_PARMTYPE_SEPARATOR,
		
	// Helpers
		
	HAPI_PARMTYPE_MAX, // Total number of supported parameter types.
		
	HAPI_PARMTYPE_INT_START			= HAPI_PARMTYPE_INT,
	HAPI_PARMTYPE_INT_END			= HAPI_PARMTYPE_BUTTON,
		
	HAPI_PARMTYPE_FLOAT_START		= HAPI_PARMTYPE_FLOAT,
	HAPI_PARMTYPE_FLOAT_END			= HAPI_PARMTYPE_COLOR,
		
	HAPI_PARMTYPE_STRING_START		= HAPI_PARMTYPE_STRING,
	HAPI_PARMTYPE_STRING_END		= HAPI_PARMTYPE_FILE,

	HAPI_PARMTYPE_CONTAINER_START 	= HAPI_PARMTYPE_FOLDERLIST,
	HAPI_PARMTYPE_CONTAINER_END 	= HAPI_PARMTYPE_FOLDERLIST,
		
	HAPI_PARMTYPE_NONVALUE_START	= HAPI_PARMTYPE_FOLDER,
	HAPI_PARMTYPE_NONVALUE_END		= HAPI_PARMTYPE_SEPARATOR
}
	
public enum HAPI_AssetType
{
	HAPI_ASSETTYPE_INVALID = -1,
	HAPI_ASSETTYPE_OBJ = 0,
	HAPI_ASSETTYPE_SOP,
	HAPI_ASSETTYPE_POPNET,
	HAPI_ASSETTYPE_POP,
	HAPI_ASSETTYPE_CHOPNET,
	HAPI_ASSETTYPE_CHOP,
	HAPI_ASSETTYPE_ROP,
	HAPI_ASSETTYPE_SHOP,
	HAPI_ASSETTYPE_COP2,
	HAPI_ASSETTYPE_COPNET,
	HAPI_ASSETTYPE_VOP,
	HAPI_ASSETTYPE_VOPNET,
	HAPI_ASSETTYPE_DOP,
	HAPI_ASSETTYPE_MGR,
	HAPI_ASSETTYPE_DIR,
	HAPI_ASSETTYPE_MAX
}
	
public enum HAPI_AssetSubType
{
	HAPI_ASSETSUBTYPE_INVALID = -1,
	HAPI_ASSETSUBTYPE_DEFAULT,
	HAPI_ASSETSUBTYPE_CURVE,
	HAPI_ASSETSUBTYPE_INPUT,
	HAPI_ASSETSUBTYPE_EXTERNALINPUT,
	HAPI_ASSETSUBTYPE_MAX
}

public enum HAPI_GroupType
{
	HAPI_GROUPTYPE_INVALID = -1,
	HAPI_GROUPTYPE_POINT,
	HAPI_GROUPTYPE_PRIM,
	HAPI_GROUPTYPE_MAX
}

public enum HAPI_AttributeOwner
{
	HAPI_ATTROWNER_INVALID = -1,
	HAPI_ATTROWNER_VERTEX,
	HAPI_ATTROWNER_POINT,
	HAPI_ATTROWNER_PRIM,
	HAPI_ATTROWNER_DETAIL,
	HAPI_ATTROWNER_MAX
}
	
public enum HAPI_CurveType
{
	HAPI_CURVETYPE_INVALID = -1,
	HAPI_CURVETYPE_LINEAR,
	HAPI_CURVETYPE_BEZIER,
	HAPI_CURVETYPE_NURBS,
	HAPI_CURVETYPE_MAX
}

public enum HAPI_StorageType
{
	HAPI_STORAGETYPE_INVALID = -1,
	HAPI_STORAGETYPE_INT,
	HAPI_STORAGETYPE_FLOAT,
	HAPI_STORAGETYPE_STRING,
	HAPI_STORAGETYPE_MAX
}

public enum HAPI_GeoType
{
	HAPI_GEOTYPE_INVALID = -1,
	HAPI_GEOTYPE_DEFAULT,
	HAPI_GEOTYPE_INTERMEDIATE,
	HAPI_GEOTYPE_INPUT,
	HAPI_GEOTYPE_CURVE,
	HAPI_GEOTYPE_MAX
};
	
public enum HAPI_InputType
{
	HAPI_INPUT_INVALID = -1,
	HAPI_INPUT_TRANSFORM,
	HAPI_INPUT_GEOMETRY,
	HAPI_INPUT_MAX
};

public enum HAPI_CurveOrders
{
	HAPI_CURVE_ORDER_VARYING = 0,
	HAPI_CURVE_ORDER_INVALID = 1,
	HAPI_CURVE_ORDER_LINEAR = 2,
	HAPI_CURVE_ORDER_QUADRATIC = 3,
	HAPI_CURVE_ORDER_CUBIC = 4,
}

public enum HAPI_TransformComponent
{
	HAPI_TRANSFORM_TX = 0,
	HAPI_TRANSFORM_TY,
	HAPI_TRANSFORM_TZ,
	HAPI_TRANSFORM_RX,
	HAPI_TRANSFORM_RY,
	HAPI_TRANSFORM_RZ,
	HAPI_TRANSFORM_QX,
	HAPI_TRANSFORM_QY,
	HAPI_TRANSFORM_QZ,
	HAPI_TRANSFORM_QW,
	HAPI_TRANSFORM_SX,
	HAPI_TRANSFORM_SY,
	HAPI_TRANSFORM_SZ
};

public enum HAPI_RSTOrder
{
	HAPI_TRS = 0,
	HAPI_TSR,
	HAPI_RTS,
	HAPI_RST,
	HAPI_STR,
	HAPI_SRT
}
	
public enum HAPI_XYZOrder
{
	HAPI_XYZ = 0,
	HAPI_XZY,
	HAPI_YXZ,
	HAPI_YZX,
	HAPI_ZXY,
	HAPI_ZYX
}

public enum HAPI_ShaderType
{
	HAPI_SHADER_INVALID = -1,
	HAPI_SHADER_OPENGL,
	HAPI_SHADER_MANTRA,
	HAPI_SHADER_MAX
};

public enum HAPI_ImageDataFormat
{
	HAPI_IMAGE_DATA_UNKNOWN = -1,
	HAPI_IMAGE_DATA_INT8,
	HAPI_IMAGE_DATA_INT16,
	HAPI_IMAGE_DATA_INT32,
	HAPI_IMAGE_DATA_FLOAT16,
	HAPI_IMAGE_DATA_FLOAT32,
	HAPI_IMAGE_DATA_MAX
};

public enum HAPI_ImagePacking
{
	HAPI_IMAGE_PACKING_UNKNOWN = -1,
	HAPI_IMAGE_PACKING_SINGLE,	// Single Channel
	HAPI_IMAGE_PACKING_DUAL,	// Dual Channel
	HAPI_IMAGE_PACKING_RGB,		// RGB
	HAPI_IMAGE_PACKING_BGR,		// RGB Reveresed
	HAPI_IMAGE_PACKING_RGBA,	// RGBA
	HAPI_IMAGE_PACKING_ABGR,	// RGBA Reversed
	HAPI_IMAGE_PACKING_MAX,

	HAPI_IMAGE_PACKING_DEFAULT3 = HAPI_IMAGE_PACKING_RGB,
	HAPI_IMAGE_PACKING_DEFAULT4 = HAPI_IMAGE_PACKING_RGBA
};

public enum HAPI_EnvIntType
{
	HAPI_ENVINT_INVALID = -1,
		
	// The three components of the Houdini version that HAPI is
	// expecting to compile against.
	HAPI_ENVINT_VERSION_HOUDINI_MAJOR,
	HAPI_ENVINT_VERSION_HOUDINI_MINOR,
	HAPI_ENVINT_VERSION_HOUDINI_BUILD,
		
	// The three components of the Houdini version that HAPI belongs to.
	// The HAPI library itself can come from a different baseline than
	// the Houdini it is being compiled against when we do something like 
	// "backgrafting" where we take, say, a 13.0 (1.5) HAPI and ship it
	// with a Houdini 12.5. This version is always locked with the
	// actual Houdini Engine version (below) because Houdini Engine is in
	// the same baseline as Houdini so their releases always coincide.
	HAPI_ENVINT_VERSION_ORIG_HOUDINI_MAJOR,
	HAPI_ENVINT_VERSION_ORIG_HOUDINI_MINOR,
	HAPI_ENVINT_VERSION_ORIG_HOUDINI_BUILD,
		
	// The two components of the Houdini Engine (marketed) version.
	HAPI_ENVINT_VERSION_HOUDINI_ENGINE_MAJOR,
	HAPI_ENVINT_VERSION_HOUDINI_ENGINE_MINOR,
		
	// This is a monotonously increasing API version number that can be used
	// to lock against a certain API for compatibility purposes. Basically,
	// when this number changes code compiled against the h methods
	// might no longer compile. Semantic changes to the methods will also
	// cause this version to increase. This number will be reset to 0
	// every time the Houdini Engine version is bumped.
	HAPI_ENVINT_VERSION_HOUDINI_ENGINE_API,
		
	HAPI_ENVINT_MAX,
};

// Unity-Only:

public enum HAPI_GeoInputFormat
{
	HAPI_GEO_INPUT_FORMAT_INVALID = -1,
	HAPI_GEO_INPUT_FORMAT_OBJECT,
	HAPI_GEO_INPUT_FORMAT_FILE,
	HAPI_GEO_INPUT_FORMAT_MAX,

	HAPI_GEO_INPUT_FORMAT_DEFAULT = HAPI_GEO_INPUT_FORMAT_OBJECT
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Main API Structs
	
// GENERICS -----------------------------------------------------------------------------------------------------

[ StructLayout( LayoutKind.Sequential ) ]
public struct HAPI_Transform 
{
	public HAPI_Transform( bool initialize_fields )
	{
		position			= new float[ HAPI_Constants.HAPI_POSITION_VECTOR_SIZE ];
		rotationQuaternion	= new float[ HAPI_Constants.HAPI_QUATERNION_VECTOR_SIZE ];
		scale				= new float[ HAPI_Constants.HAPI_SCALE_VECTOR_SIZE ];
		rstOrder			= HAPI_RSTOrder.HAPI_SRT;
	}
		
	[ MarshalAs( UnmanagedType.ByValArray, 
					SizeConst = HAPI_Constants.HAPI_POSITION_VECTOR_SIZE, 
					ArraySubType = UnmanagedType.R4 ) ]
	public float[] position;
				
	[ MarshalAs( UnmanagedType.ByValArray, 
					SizeConst = HAPI_Constants.HAPI_QUATERNION_VECTOR_SIZE, 
					ArraySubType = UnmanagedType.R4 ) ]
	public float[] rotationQuaternion;
		
	[ MarshalAs( UnmanagedType.ByValArray, 
					SizeConst = HAPI_Constants.HAPI_SCALE_VECTOR_SIZE, 
					ArraySubType = UnmanagedType.R4 ) ]
	public float[] scale;

	public HAPI_RSTOrder rstOrder;
}
	
[ StructLayout( LayoutKind.Sequential ) ]
public struct HAPI_TransformEuler 
{
	public HAPI_TransformEuler( bool initialize_fields )
	{
		position 		= new float[ HAPI_Constants.HAPI_POSITION_VECTOR_SIZE ];
		rotationEuler 	= new float[ HAPI_Constants.HAPI_EULER_VECTOR_SIZE ];
		scale 			= new float[ HAPI_Constants.HAPI_SCALE_VECTOR_SIZE ];
		rotationOrder 	= 0;
		rstOrder 		= 0;
	}

	[ MarshalAs( UnmanagedType.ByValArray, 
					SizeConst = HAPI_Constants.HAPI_POSITION_VECTOR_SIZE, 
					ArraySubType = UnmanagedType.R4 ) ]
	public float[] position;

	[ MarshalAs( UnmanagedType.ByValArray, 
					SizeConst = HAPI_Constants.HAPI_EULER_VECTOR_SIZE, 
					ArraySubType = UnmanagedType.R4 ) ]
	public float[] rotationEuler;

	[ MarshalAs( UnmanagedType.ByValArray, 
					SizeConst = HAPI_Constants.HAPI_SCALE_VECTOR_SIZE, 
					ArraySubType = UnmanagedType.R4 ) ]
	public float[] scale;

	public HAPI_XYZOrder rotationOrder;
	public HAPI_RSTOrder rstOrder;
}
	
// ASSETS -------------------------------------------------------------------------------------------------------
		
[ StructLayout( LayoutKind.Sequential ) ]
public struct HAPI_AssetInfo 
{
	public HAPI_AssetId			id;
	public HAPI_AssetType		type;
	public HAPI_AssetSubType	subType;

	// This id is primarily used to check whether the asset still exists
	// within the Houdini scene running inside the runtime. The asset id
	// alone is not enough as asset ids are re-used between sessions.
	// We use this id to determine whether we need to re-instatiate an asset
	// we have on the client side so that Houdini also knows about it -
	// which is different from the case where a new asset is being loaded
	// for the first time.
	public int validationId;

	// Use the node id to get the asset's parameters.
	public HAPI_NodeId nodeId;

	// The objectNodeId differs from the regular nodeId in that for
	// geometry based assets (SOPs) it will be the node id of the dummy
	// object (OBJ) node instead of the asset node. For object based assets
	// the objectNodeId will equal the nodeId. The reason the distinction
	// exists is because transforms are always stored on the object node
	// but the asset parameters may not be on the asset node if the asset
	// is a geometry asset so we need both.
	public HAPI_NodeId objectNodeId;

	[ MarshalAs( UnmanagedType.U1 ) ] public bool hasEverCooked;

	private HAPI_StringHandle nameSH; // Instance name (the label + a number).
	private HAPI_StringHandle labelSH;
	private HAPI_StringHandle filePathSH; // Path to the .otl file for this asset.

	private HAPI_StringHandle versionSH; // User-defined asset version.
	private HAPI_StringHandle fullOpNameSH; // Full asset name and namespace.
	private HAPI_StringHandle helpTextSH; // Asset help marked-up text.

	public int objectCount;
	public int handleCount;
		
	public int minTransInputCount;
	public int maxTransInputCount;
	public int minGeoInputCount;
	public int maxGeoInputCount;
		
	// Accessors
	public string name
	{ get { return HAPI_Host.getString( nameSH ); } private set {} }
	public string label
	{ get { return HAPI_Host.getString( labelSH ); } private set {} }
	public string filePath
	{ get { return HAPI_Host.getString( filePathSH ); } private set {} }
	public string version
	{ get { return HAPI_Host.getString( versionSH ); } private set {} }
	public string fullOpName
	{ get { return HAPI_Host.getString( fullOpNameSH ); } private set {} }
	public string helpText
	{ get { return HAPI_Host.getString( helpTextSH ); } private set {} }
}

[ StructLayout( LayoutKind.Sequential ) ]
public struct HAPI_CookOptions
{
	/// Normally, geos are split into parts in two different ways. First it
    /// is split by group and within each group it is split by primitive type.
    ///
    /// For example, if you have a geo with group1 covering half of the mesh
    /// and volume1 and group2 covering the other half of the mesh, all of
    /// curve1, and volume2 you will end up with 5 parts. First two parts
    /// will be for the half-mesh of group1 and volume1, and the last three
    /// will cover group2.
    ///
    /// This toggle lets you disable the splitting by group and just have
    /// the geo be split by primitive type alone. By default, this is true
    /// and therefore geos will be split by group and primitive type. If
    /// set to false, geos will only be split by primtive type.
    [ MarshalAs( UnmanagedType.U1 ) ] public bool splitGeosByGroup;

	/// For meshes only, this is enforced by convexing the mesh. Use -1
    /// to avoid convexing at all and get some performance boost.
	public int maxVerticesPerPrimitive;

	// Curves
	[ MarshalAs( UnmanagedType.U1 ) ] public bool refineCurveToLinear;
	public float curveRefineLOD;
}

// NODES --------------------------------------------------------------------------------------------------------

[ StructLayout( LayoutKind.Sequential ) ]
public struct HAPI_NodeInfo 
{
	public HAPI_NodeId id;
	public HAPI_AssetId assetId;
	public HAPI_StringHandle nameSH;

	public int uniqueHoudiniNodeId;
	private HAPI_StringHandle internalNodePathSH;

	public int parmCount;
	public int parmIntValueCount;
	public int parmFloatValueCount;
	public int parmStringValueCount;
	public int parmChoiceCount;

	public string name
	{ get { return HAPI_Host.getString( nameSH ); } private set {} }
	public string internalNodePath
	{ get { return HAPI_Host.getString( internalNodePathSH ); } private set {} }
}

[ StructLayout( LayoutKind.Sequential ) ]
public struct HAPI_GlobalNodes 
{
	public HAPI_NodeId defaultCamera;
	public HAPI_NodeId defaultLight;
	public HAPI_NodeId mantraRenderer;
}

// PARAMETERS ---------------------------------------------------------------------------------------------------
	
[ StructLayout( LayoutKind.Sequential ) ]
public struct HAPI_ParmInfo
{
	public bool isInt()
	{
		return ( type >= HAPI_ParmType.HAPI_PARMTYPE_INT_START &&
			type <= HAPI_ParmType.HAPI_PARMTYPE_INT_END )
			|| type == HAPI_ParmType.HAPI_PARMTYPE_MULTIPARMLIST;
	}
	public bool isFloat()
	{
		return ( type >= HAPI_ParmType.HAPI_PARMTYPE_FLOAT_START &&
			type <= HAPI_ParmType.HAPI_PARMTYPE_FLOAT_END );
	}
	public bool isString()
	{
		return ( type >= HAPI_ParmType.HAPI_PARMTYPE_STRING_START &&
			type <= HAPI_ParmType.HAPI_PARMTYPE_STRING_END );
	}
	public bool isNonValue()
	{
		return ( type >= HAPI_ParmType.HAPI_PARMTYPE_NONVALUE_START &&
			type <= HAPI_ParmType.HAPI_PARMTYPE_NONVALUE_END );
	}

	// The parent id points to the id of the parent parm
	// of this parm. The parent parm is something like a folder.
	public HAPI_ParmId id;
	public HAPI_ParmId parentId;

	public HAPI_ParmType type;
	public int size; // Tuple Size
	public int choiceCount;

	// Note that folders are not real parameters in Houdini so they do not
	// have names. The folder names given here are generated from the name
	// of the folderlist (or switcher) parameter which is a parameter. The
	// folderlist parameter simply defines how many of the "next" parameters
	// belong to the first folder, how many of the parameters after that
	// belong to the next folder, and so on. This means that folder names
	// can change just by reordering the folders around so don't rely on
	// them too much. The only guarantee here is that the folder names will
	// be unique among all other parameter names.
	private HAPI_StringHandle nameSH;
	private HAPI_StringHandle labelSH;

	// If this parameter is a multiparm instance than the templateNameSH
	// will be the hash-templated parm name, containing #'s for the 
	// parts of the name that use the instance number. Compared to the
	// nameSH, the nameSH will be the templateNameSH but with the #'s
	// replaced by the instance number. For regular parms, the templateNameSH
	// is identical to the nameSH.
	private HAPI_StringHandle templateNameSH;

	[ MarshalAs( UnmanagedType.U1 ) ]
	public bool hasMin;

	[ MarshalAs( UnmanagedType.U1 ) ]
	public bool hasMax;

	[ MarshalAs( UnmanagedType.U1 ) ]
	public bool hasUIMin;

	[ MarshalAs( UnmanagedType.U1 ) ]
	public bool hasUIMax;

	[ MarshalAs( UnmanagedType.R4) ]
	public float min;

	[ MarshalAs( UnmanagedType.R4) ]
	public float max;

	[ MarshalAs( UnmanagedType.R4) ]
	public float UIMin;

	[ MarshalAs( UnmanagedType.R4) ]
	public float UIMax;

	[ MarshalAs( UnmanagedType.U1 ) ]
	public bool invisible;

	// Whether this parm should be on the same line as the next parm.
	[ MarshalAs( UnmanagedType.U1 ) ]
	public bool joinNext;

	[ MarshalAs( UnmanagedType.U1 ) ]
	public bool labelNone;

	public int intValuesIndex;
	public int floatValuesIndex;
	public int stringValuesIndex;
	public int choiceIndex;

	[ MarshalAs( UnmanagedType.U1 ) ]
	public bool isChildOfMultiParm;

	public int instanceNum; // The index of the instance in the multiparm.
	public int instanceLength; // The number of parms in a multiparm instance.
	public int instanceCount; // The number of instances in a multiparm.
	public int instanceStartOffset; // First instanceNum either 0 or 1.

	public HAPI_RampType rampType;

	// Accessors
	public int getNameSH()
	{ return nameSH; }
	public int getLabelSH()
	{ return labelSH; }
	public string name
	{ get { return HAPI_Host.getString( nameSH ); } private set {} }
	public string label
	{ get { return HAPI_Host.getString( labelSH ); } private set {} }
	public string templateName
	{ get { return HAPI_Host.getString( templateNameSH ); } private set {} }
}
	
[ StructLayout( LayoutKind.Sequential ) ]
public struct HAPI_ParmChoiceInfo
{
	public HAPI_ParmId parentParmId;
	private HAPI_StringHandle labelSH;
	private HAPI_StringHandle valueSH;
		
	// Accessors
	public string label
	{ get { return HAPI_Host.getString( labelSH ); } private set {} }
	public string value
	{ get { return HAPI_Host.getString( valueSH ); } private set {} }
}
	
// HANDLES ------------------------------------------------------------------------------------------------------
	
[ StructLayout( LayoutKind.Sequential ) ]
public struct HAPI_HandleInfo
{
	private HAPI_StringHandle nameSH;
	private HAPI_StringHandle typeNameSH;

	public int bindingsCount;

	// Accessors
	public string name
	{ get { return HAPI_Host.getString( nameSH ); } private set {} }
	public string typeName
	{ get { return HAPI_Host.getString( typeNameSH ); } private set {} }
}

	
[ StructLayout( LayoutKind.Sequential ) ]
public struct HAPI_HandleBindingInfo
{
	private HAPI_StringHandle handleParmNameSH;
	private HAPI_StringHandle assetParmNameSH;

	public HAPI_ParmId assetParmId;

	// Accessors
	public string handleParmName
	{ get { return HAPI_Host.getString( handleParmNameSH ); } private set {} }
	public string assetParmName
	{ get { return HAPI_Host.getString( assetParmNameSH ); } private set {} }
};
	
// OBJECTS ------------------------------------------------------------------------------------------------------
	
[ StructLayout( LayoutKind.Sequential ) ]
public struct HAPI_ObjectInfo 
{
	public HAPI_ObjectId id;

	private HAPI_StringHandle nameSH;
	private HAPI_StringHandle objectInstancePathSH;

	[ MarshalAs( UnmanagedType.U1 ) ] public bool hasTransformChanged;
	[ MarshalAs( UnmanagedType.U1 ) ] public bool haveGeosChanged;
		
	[ MarshalAs( UnmanagedType.U1 ) ] public bool isVisible;
	[ MarshalAs( UnmanagedType.U1 ) ] public bool isInstancer;

	public int geoCount;

	// Use the node id to get the node's parameters.
	public HAPI_NodeId nodeId;

	public HAPI_ObjectId objectToInstanceId;

	// Accessors
	public string name
	{ get { return HAPI_Host.getString( nameSH ); } private set {} }
	public string objectInstancePath
	{ get { return HAPI_Host.getString( objectInstancePathSH ); } private set {} }
}
	
// GEOMETRY -----------------------------------------------------------------------------------------------------
	
[ StructLayout( LayoutKind.Sequential ) ]
public struct HAPI_GeoInfo
{
	public int getGroupCountByType( HAPI_GroupType type )
	{
		switch ( type )
		{
			case HAPI_GroupType.HAPI_GROUPTYPE_POINT: return pointGroupCount;
			case HAPI_GroupType.HAPI_GROUPTYPE_PRIM: return primitiveGroupCount;
			default: return 0;
		}
	}

	public HAPI_GeoId id;
	public HAPI_GeoType type;
	private HAPI_StringHandle nameSH;

	// Use the node id to get the node's parameters.
	public HAPI_NodeId nodeId;

	[ MarshalAs( UnmanagedType.U1 ) ] public bool isEditable;
	[ MarshalAs( UnmanagedType.U1 ) ] public bool isTemplated;
	[ MarshalAs( UnmanagedType.U1 ) ] public bool isDisplayGeo; // Final Result (Display SOP)
		
	[ MarshalAs( UnmanagedType.U1 ) ] public bool hasGeoChanged;
	[ MarshalAs( UnmanagedType.U1 ) ] public bool hasMaterialChanged;

	public int pointGroupCount;
	public int primitiveGroupCount;

	public int partCount;

	// Accessors
	public string name
	{ get { return HAPI_Host.getString( nameSH ); } private set {} }
}
	
[ StructLayout( LayoutKind.Sequential ) ]
public struct HAPI_GeoInputInfo
{
	public HAPI_ObjectId objectId;
	public HAPI_GeoId geoId;
	public HAPI_NodeId objectNodeId;
}

[ StructLayout( LayoutKind.Sequential ) ]
public struct HAPI_PartInfo
{
	public int getElementCountByAttributeOwner( HAPI_AttributeOwner owner )
	{
		switch ( owner )
		{
			case HAPI_AttributeOwner.HAPI_ATTROWNER_VERTEX: return vertexCount;
			case HAPI_AttributeOwner.HAPI_ATTROWNER_POINT: return pointCount;
			case HAPI_AttributeOwner.HAPI_ATTROWNER_PRIM: return faceCount;
			case HAPI_AttributeOwner.HAPI_ATTROWNER_DETAIL: return 1;
			default: return 0;
		}
	}

	public int getElementCountByGroupType( HAPI_GroupType type )
	{
		switch ( type )
		{
			case HAPI_GroupType.HAPI_GROUPTYPE_POINT: return pointCount;
			case HAPI_GroupType.HAPI_GROUPTYPE_PRIM: return faceCount;
			default: return 0;
		}
	}

	public int getAttributeCountByOwner( HAPI_AttributeOwner owner )
	{
		switch ( owner )
		{
			case HAPI_AttributeOwner.HAPI_ATTROWNER_VERTEX: return vertexAttributeCount;
			case HAPI_AttributeOwner.HAPI_ATTROWNER_POINT: return pointAttributeCount;
			case HAPI_AttributeOwner.HAPI_ATTROWNER_PRIM: return faceAttributeCount;
			case HAPI_AttributeOwner.HAPI_ATTROWNER_DETAIL: return detailAttributeCount;
			default: return 0;
		}
	}

	public HAPI_PartId id;
	private HAPI_StringHandle nameSH;

	public HAPI_MaterialId materialId;
		
	public int faceCount;
	public int vertexCount;
	public int pointCount;

	public int pointAttributeCount;
	public int faceAttributeCount;
	public int vertexAttributeCount;
	public int detailAttributeCount;

	[ MarshalAs( UnmanagedType.U1 ) ]
	public bool hasVolume;

	[ MarshalAs( UnmanagedType.U1 ) ]
	public bool isCurve;

	// Accessors
	public string name
	{ get { return HAPI_Host.getString( nameSH ); } private set {} }
}
	
[ StructLayout( LayoutKind.Sequential ) ]
public struct HAPI_AttributeInfo
{		
	public HAPI_AttributeInfo( string attr_name )
	{
		exists 		= false;
		owner 		= HAPI_AttributeOwner.HAPI_ATTROWNER_INVALID;
		storage 	= HAPI_StorageType.HAPI_STORAGETYPE_INVALID;
		count 		= 0;
		tupleSize 	= 0;
	}
		
	[ MarshalAs( UnmanagedType.U1 ) ]
	public bool exists;
		
	public HAPI_AttributeOwner owner;
	public HAPI_StorageType storage;

	public int count;
	public int tupleSize;
}
	
// MATERIALS ----------------------------------------------------------------------------------------------------

[ StructLayout( LayoutKind.Sequential ) ]
public struct HAPI_MaterialInfo
{
	public HAPI_MaterialId id;
	public HAPI_AssetId assetId;
	public HAPI_NodeId nodeId;

	[ MarshalAs( UnmanagedType.U1 ) ]
	public bool hasChanged;
}

[ StructLayout( LayoutKind.Sequential ) ]
public struct HAPI_ImageFileFormat
{
	public HAPI_StringHandle nameSH;
	public HAPI_StringHandle descriptionSH;
	public HAPI_StringHandle defaultExtensionSH;

	// Accessors
	public string name
	{ get { return HAPI_Host.getString( nameSH ); } private set {} }
	public string description
	{ get { return HAPI_Host.getString( descriptionSH ); } private set {} }
	public string defaultExtension
	{ get { return HAPI_Host.getString( defaultExtensionSH ); } private set {} }
};

[ StructLayout( LayoutKind.Sequential ) ]
public struct HAPI_ImageInfo
{
	// Unlike the other members of this struct changing imageFileFormatNameSH and 
	// giving this struct back to HAPI_Host.setImageInfo() nothing will happen.
	// Use this member variable to derive which image file format will be used
	// by the HAPI_Host.extractImageTo...() functions if called with image_file_format_name
	// set to (null). This way, you can decide whether to ask for a file format
	// conversion (slower) or not (faster).
	public HAPI_StringHandle imageFileFormatNameSH; // Readonly

	public int xRes;
	public int yRes;

	public HAPI_ImageDataFormat dataFormat;

	[ MarshalAs( UnmanagedType.U1 ) ]
	public bool interleaved; // ex: true = RGBRGBRGB, false = RRRGGGBBB

	public HAPI_ImagePacking packing;

	// Accessors
	public string imageFileFormatName
	{ get { return HAPI_Host.getString( imageFileFormatNameSH ); } private set {} }

	// Utility
	public bool isImageFileFormat( string image_file_format_name )
	{ return ( imageFileFormatName == image_file_format_name ); }
}

// ANIMATION ----------------------------------------------------------------------------------------------------
	
[ StructLayout( LayoutKind.Sequential ) ]
public struct HAPI_Keyframe
{
	HAPI_Keyframe( float t, float v, float in_tangent, float out_tangent )
	{
		time = t;
		value = v;
		inTangent = in_tangent;
		outTangent = out_tangent;
	}
		
	[ MarshalAs( UnmanagedType.R4) ]
	public float time;
		
	[ MarshalAs( UnmanagedType.R4) ]
	public float value;
		
	[ MarshalAs( UnmanagedType.R4) ]
	public float inTangent;
		
	[ MarshalAs( UnmanagedType.R4) ]
	public float outTangent;
	
}

// VOLUMES ------------------------------------------------------------------------------------------------------

/// This represents a volume primitive--sans the actual voxel values,
/// which can be retrieved on a per-tile basis
[ StructLayout( LayoutKind.Sequential ) ]
public struct HAPI_VolumeInfo
{
	public HAPI_StringHandle nameSH;

	// Each voxel is identified with an index. The indices will range between:
	// [ ( minX, minY, minZ ), ( minX+xLength, minY+yLength, minZ+zLength ) )
	public int xLength; 
	public int yLength;
	public int zLength;
	public int minX;
	public int minY;
	public int minZ;

	// Number of values per voxel.
	public int tupleSize;
	public HAPI_StorageType storage;

	// The dimensions of each tile.
	public int tileSize;

	// The transform of the volume with respect to the above lengths.

	[ MarshalAs( UnmanagedType.Struct ) ]
	public HAPI_Transform transform;

	[ MarshalAs( UnmanagedType.U1 ) ]
	public bool hasTaper;

	[ MarshalAs( UnmanagedType.R4 ) ]
	public float xTaper;

	[ MarshalAs( UnmanagedType.R4 ) ]
	public float yTaper;
};

/// A HAPI_VolumeTileInfo represents an 8x8x8 section of a volume with
/// bbox [(minX, minY, minZ), (minX+8, minY+8, minZ+8))
[ StructLayout( LayoutKind.Sequential ) ]
public struct HAPI_VolumeTileInfo
{
	public int minX;
	public int minY;
	public int minZ;

	[ MarshalAs( UnmanagedType.U1 ) ]
	public bool isValid;
};

// CURVES -------------------------------------------------------------------------------------------------------

[ StructLayout( LayoutKind.Sequential ) ]
public struct HAPI_CurveInfo
{
	public HAPI_CurveType curveType;
	public int curveCount;
	public int vertexCount;
	public int knotCount;

	[ MarshalAs( UnmanagedType.U1 ) ]
	public bool isPeriodic;

	[ MarshalAs( UnmanagedType.U1 ) ]
	public bool isRational;

	public int order; // Order of 1 is invalid. 0 means there is a varying order.

	[ MarshalAs( UnmanagedType.U1 ) ]
	public bool hasKnots;
};
