// Reflection Shader
// Fragment Shader
// Richard S. Wright Jr.
// OpenGL SuperBible
#version 130

uniform samplerCube cubeMap;
uniform sampler2D   tarnishMap;

smooth in vec3 vVaryingTexCoord;
smooth in vec2 vTarnishCoords;

out vec4 vFragColor;

void main(void)
{ 
	vFragColor = texture(cubeMap, vVaryingTexCoord);
	vFragColor *= texture(tarnishMap, vTarnishCoords);
}

