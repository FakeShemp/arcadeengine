// Reflection Shader
// Vertex Shader
// Richard S. Wright Jr.
// OpenGL SuperBible
#version 130

// Uniforms
uniform mat4   mvpMatrix;
uniform mat4   mvMatrix;
uniform mat4   NormalMatrix;
uniform mat4   mInverseCamera;

// Incoming per vertex... position and normal
in vec3 in_position;
in vec3 in_normal;
in vec2 in_texcoord;

// Texture coordinate to fragment program
smooth out vec3 vVaryingTexCoord;
smooth out vec2 vTarnishCoords;

void main(void) 
{
	mat3 normalMatrix = mat3(NormalMatrix);

	// Normal in Eye Space
	vec3 vEyeNormal = normalMatrix * in_normal;
    
	// Vertex position in Eye Space
	vec4 vVert4 = mvMatrix * vec4(in_position, 1.0);
	vec3 vEyeVertex = normalize(vVert4.xyz / vVert4.w);
    
	// Get reflected vector
	vec4 vCoords = vec4(reflect(vEyeVertex, vEyeNormal), 1.0);
   
	// Rotate by flipped camera
	vCoords = mInverseCamera * vCoords;
	vVaryingTexCoord.xyz = normalize(vCoords.xyz);
	
	vTarnishCoords = in_texcoord.st;

	// Don't forget to transform the geometry!
	gl_Position = mvpMatrix * vec4(in_position, 1.0);
}

