float _TargetEye;
float _MaxLookRange; // Max rotation
float _MaxLookDistance; // Max distance from camera until tracking stops. Calculated from object origin.
float _EyeTrackingScrollSpeed;
float _EyeTrackingBlur;
float _EyeTrackingRotationCorrection;
sampler2D _EyeTrackingPatternTex;

inline float3x3 xRotation3dRadiansEye(float rad) {
    float s = sin(rad);
    float c = cos(rad);
    return float3x3(
        1, 0, 0,
        0, c, s,
        0, -s, c);
}

inline float3x3 yRotation3dRadiansEye(float rad) {
    float s = sin(rad);
    float c = cos(rad);
    return float3x3(
        c, 0, -s,
        0, 1, 0,
        s, 0, c);
}
 
inline float3x3 zRotation3dRadiansEye(float rad) {
    float s = sin(rad);
    float c = cos(rad);
    return float3x3(
        c, s, 0,
        -s, c, 0,
        0, 0, 1);
}

// Get the position of the required eye. Can be left, right or center in VR, but is always "center" in non-VR.
float3 GetEyePos()
{
    #if defined(USING_STEREO_MATRICES)
        return lerp(unity_StereoWorldSpaceCameraPos[0], unity_StereoWorldSpaceCameraPos[1], (_TargetEye * 0.5));
    #else
        return _WorldSpaceCameraPos;
    #endif
}

float EyeTrackingCurve(float t)
{
    // Scroll the current pixel value based on time
    float4 currentCol = tex2Dlod(_EyeTrackingPatternTex, float4(t * 0.01 * _EyeTrackingScrollSpeed, 0.5, 0, _EyeTrackingBlur));
    return currentCol.r;
}

v2f vertEyeTracking(appdata v)
{
    // If vertex offset is enabled, apply that first
    #if defined(_VERTEXOFFSET_ON)
        VertexOffset(v);
    #endif

    // Fix wrong mesh rotation
    // Blender exports are 90 degrees off on the X axis by default
    // But even with the correct orientation, this shader messes up the initial rotation somewhat.
    if(_EyeTrackingRotationCorrection == 1)
    {
        v.vertex.xyz = mul(yRotation3dRadiansEye(radians(180)), v.vertex.xyz);
        v.normal = mul(yRotation3dRadiansEye(radians(180)), v.normal);
    }
    else
    {
        v.vertex.xyz = mul(xRotation3dRadiansEye(radians(-90)), v.vertex.xyz);
        v.normal = mul(xRotation3dRadiansEye(radians(-90)), v.normal);

        v.vertex.xyz = mul(zRotation3dRadiansEye(radians(180)), v.vertex.xyz);
        v.normal = mul(zRotation3dRadiansEye(radians(180)), v.normal);
    }
    
    // Pre-apply scale
    // Calculate the XYZ scales
    float4 modelX = float4(1.0, 0.0, 0.0, 0.0);
    float4 modelY = float4(0.0, 1.0, 0.0, 0.0);
    float4 modelZ = float4(0.0, 0.0, 1.0, 0.0);
     
    float4 modelXInWorld = mul(unity_ObjectToWorld, modelX);
    float4 modelYInWorld = mul(unity_ObjectToWorld, modelY);
    float4 modelZInWorld = mul(unity_ObjectToWorld, modelZ);
     
    float scaleX = length(modelXInWorld);
    float scaleY = length(modelYInWorld);
    float scaleZ = length(modelZInWorld);
    
    // Apply scales
    v.vertex.xyz *= float3(scaleX, scaleY, scaleZ);
    v.normal = normalize(v.normal * float3(scaleX, scaleY, scaleZ));

    // The world position of the center of the object
    float3 worldPos = mul(unity_ObjectToWorld, float4(0, 0, 0, 1)).xyz;

    float3 camPos = GetEyePos();

    // Distance between the camera and the center
    float3 dist = camPos - worldPos;

    // Get camera vector
    float3 camVect = normalize(worldPos - _WorldSpaceCameraPos);
    
    // Get the default looking ahead vector
    // To obtain this vector, a forward vector is created and multiplied by unity_ObjectToWorld.
    // Casting it to float3x3 eliminates translation data and only leaves the rotation and scale of the mesh.
    // Normalizing it will then get rid of the scale.
    float3 forwardVect = normalize(mul((float3x3)unity_ObjectToWorld, float3(0,0,-1)));
    
    //Check if the camera is behind the eye
    float3 behindDir = -forwardVect;
    if(dot(camVect, behindDir) > _MaxLookRange)
    {
        camVect = forwardVect;
    }
    else
    {
        //Look forward or to the camera depending on texture
        float lerpValue = EyeTrackingCurve(_Time.y);
        
        // Stop looking from a distance
        float camDist = distance(camPos, worldPos);
        lerpValue = (1 - smoothstep(_MaxLookDistance, _MaxLookDistance + 1, camDist)) * lerpValue;
        
        camVect = normalize(lerp(forwardVect, camVect, saturate(lerpValue)));
    }

    // atan2(dist.x, dist.z) = atan (dist.x / dist.z)
    // With atan the tree inverts when the camera has the same z position
    float angle = atan2(dist.x, dist.z);

    float3x3 rotMatrix;
    float cosinus = cos(angle);
    float sinus = sin(angle);
    float tang = (dist.x / dist.z);

    // "Up" should actually match the object rotation so the eyes don't roll around in their sockets
    float3 up = normalize(mul((float3x3)unity_ObjectToWorld, float3(0,1,0)));

    // Create LookAt matrix
    float3 zaxis = camVect;
    float3 xaxis = normalize(cross(up, zaxis));
    float3 yaxis = cross(zaxis, xaxis);

    float3x3 lookatMatrix = {
        xaxis.x,            yaxis.x,            zaxis.x,
        xaxis.y,            yaxis.y,            zaxis.y,
        xaxis.z,            yaxis.z,            zaxis.z
    };

    // Rotation matrix in Y
    rotMatrix[0].xyz = float3(cosinus, 0, sinus);
    rotMatrix[1].xyz = float3(0, 1, 0);
    rotMatrix[2].xyz = float3(- sinus, 0, cosinus);

    // The position of the vertex after the rotation
    float4 newPos = float4(mul(lookatMatrix, v.vertex), 1);
    float3 newNormal = normalize(mul(lookatMatrix, v.normal));
    // The model matrix without the rotation and scale
    float4x4 matrix_M_noRot = unity_ObjectToWorld;
    matrix_M_noRot[0][0] = 1;
    matrix_M_noRot[0][1] = 0;
    matrix_M_noRot[0][2] = 0;

    matrix_M_noRot[1][0] = 0;
    matrix_M_noRot[1][1] = 1;
    matrix_M_noRot[1][2] = 0;

    matrix_M_noRot[2][0] = 0;
    matrix_M_noRot[2][1] = 0;
    matrix_M_noRot[2][2] = 1;
    
    // Apply our custom object to world matrix to the vertex position
    float4 vertWorldPos = mul(matrix_M_noRot, newPos);

    // Apply it again to the normals
    newNormal = normalize(mul(matrix_M_noRot, newNormal));
    

    v2f o;
    // The position of the vertex in clip space ignoring the rotation and scale of the object
    o.pos = mul(UNITY_MATRIX_VP, vertWorldPos);
    // If used, pack UV0 and UV1 into a single float4
    #if defined(_DETAILNORMAL_UV1)
        float2 uv0 = TRANSFORM_TEX(v.uv, _MainTex);
        float2 uv1 = TRANSFORM_TEX(v.uv1, _DetailNormalMap);
        o.uv = float4(uv0, uv1);
    #else
        o.uv = TRANSFORM_TEX(v.uv, _MainTex);
    #endif
    o.normalDir = newNormal;

	// If we're not using normal maps or detail normal maps, we don't need the tangent or bitangent at all, saving us 2 interpolators.
	#if defined(_NORMALMAP) || defined(DETAILNORMALMAP)
		o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
		o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
	#endif

    o.worldPos = mul(unity_ObjectToWorld, v.vertex);
    o.objWorldPos = mul(unity_ObjectToWorld, float4(0,0,0,1));
    TRANSFER_SHADOW(o);
    UNITY_TRANSFER_FOG(o, o.pos);
    
    return o;
}