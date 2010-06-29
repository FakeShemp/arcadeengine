#version 130

uniform sampler2D in_texture;
uniform mat4 texture_matrix;

in vec2 out_texture;
in vec4 out_color;

out vec4 frag_color;

void main()
{
	//frag_color = texture2D(in_texture, out_texture.st) * out_color;
	frag_color = out_color;
}