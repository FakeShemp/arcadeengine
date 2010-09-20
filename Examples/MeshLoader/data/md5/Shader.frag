#version 130

uniform sampler2D textureUnit0;

in vec2 vTex;
in vec4 vColor;

out vec4 frag_color;



void main(void) 
{
	//frag_color = texture2D(textureUnit0, vTex) * vColor; 
	frag_color =vColor; // vec4(1.0, 1.0, 1.0, 1.0); 
}
