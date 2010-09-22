// Skybox Shader
// Vertex Shader
// Richard S. Wright Jr.
// OpenGL SuperBible
#version 130

// Incoming per vertex... just the position
in vec3 in_position;


// Transformation matrix
uniform mat4   mvpMatrix;  


// Texture Coordinate to fragment program
out vec3 vVaryingTexCoord;


void main(void) 
{
	// Pass on the texture coordinates 
	vVaryingTexCoord = normalize(in_position);

	// Don't forget to transform the geometry!
	gl_Position = mvpMatrix * vec4(in_position, 1.0);
}
