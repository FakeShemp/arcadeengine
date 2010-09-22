// Skybox Shader
// Fragment Shader
// Richard S. Wright Jr.
// OpenGL SuperBible
#version 130

// Texture
uniform samplerCube  cubeMap;

//
in vec3 vVaryingTexCoord;

// Output fragment
out vec4 vFragColor;


void main(void)
{ 
	vFragColor = texture(cubeMap, vVaryingTexCoord);
}
