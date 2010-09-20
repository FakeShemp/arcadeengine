#version 130

uniform mat4	mvpMatrix;		// ModelViewProjection matrix
uniform mat4	mvMatrix;		// ModelView matrix
//uniform mat3	normalMatrix;	// Normal matrix

in vec3 in_position;
in vec3 in_normal;
in vec3 in_tangent;
in vec2 in_texcoord;

out vec2 vTex;					// Texture coordinate
out vec4 vColor;				// Color to fragment program


void main(void) 
{ 
	gl_Position = mvpMatrix * vec4(in_position, 1.0); 
	vTex = in_texcoord;
	vColor = vec4(in_normal, 1.0); //vec4(1.0, 1.0, 1.0, 1.0);
}


