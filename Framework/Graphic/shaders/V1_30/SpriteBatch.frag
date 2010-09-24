#version 130

uniform sampler2D texture;		// Texture sampler

in vec2 out_texture;			// Texture coordinate
in vec4 out_color;				// Fragment color

out vec4 frag_color;			// Final fragment

void main()
{
	// Fragment color
	frag_color = texture2D(texture, out_texture) * out_color;
}