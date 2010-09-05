#version 130

uniform sampler2D textureUnit0;
uniform vec4 in_color;

in vec2 vTex;

out vec4 frag_color;

void main(void) 
{
	frag_color = texture2D(textureUnit0, vTex) * in_color; 
}
