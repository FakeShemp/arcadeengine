#version 130

uniform sampler2D texture;

in vec4 out_texture;
in vec4 out_color;

out vec4 frag_color;

void main()
{
	frag_color = texture2D(texture, out_texture.st) * out_color;
}