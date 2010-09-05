#version 130

uniform mat4 mvpMatrix;

in vec3 in_position;
in vec2 in_texcoord;

out vec2 vTex;

void main(void) 
{ 
	vTex = in_texcoord;
	gl_Position = mvpMatrix * vec4(in_position, 1.0); 
}


